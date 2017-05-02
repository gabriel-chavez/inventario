namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using System.Linq;
    using Proyecto.Models;
    using Newtonsoft.Json;

    public partial class enlaces
    {
       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public enlaces()
        //{
        //    contratos = new HashSet<contratos>();
        //    enlacesInternet = new HashSet<enlacesInternet>();
        //    enlacesServicios = new HashSet<enlacesServicios>();
        //}

        [Key]
        public int enlaceID { get; set; }

        public int proveedorID { get; set; }

        public int oficinaID { get; set; }

        public int enlaceTipoID { get; set; }

        public int enlace { get; set; }

        public int enlaceTecnologiaID { get; set; }

        [StringLength(50)]
        public string velocidad { get; set; }

        public int? mensualidad { get; set; }

        [Required]
        public string observaciones { get; set; }

        public byte estado { get; set; }

        
        public virtual ICollection<contratos> contratos { get; set; }

        public virtual enlacesTecnologia enlacesTecnologia { get; set; }

        public virtual enlacesTipo enlacesTipo { get; set; }

        public virtual oficinas oficinas { get; set; }

        public virtual proveedores proveedores { get; set; }

        /***************PARA GUARDAR**************/
        [NotMapped] //para evitar traer de la base de datos
        public string _servicio { get; set; }
        [NotMapped]
        public string _direccion { get; set; }
        [NotMapped]
        public string _planinternet { get; set; }
        [NotMapped]
        public int enlaceServicioID { get; set; }
        [NotMapped]
        public int enlacesInternetID { get; set; }
        /*****************FIN*******************/
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<enlacesInternet> enlacesInternet { get; set; }

       // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<enlacesServicios> enlacesServicios { get; set; }
        /*********************************/
        public List<enlaces> Listar(int tipo)
        {
            List<enlaces> enlaces = new List<enlaces>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    //enlaces = ctx.enlaces.ToList();
                    enlaces = ctx.enlaces.Include("proveedores")
                                        .Include("oficinas")
                                        .Include("oficinas.ciudades")
                                        .Include("oficinas.tipoOficina")
                                        .Include("oficinas.ciudades.departamentos")
                                        .Include("enlacesTipo")
                                        .Include("enlacesTecnologia")
                                        .Where(x=> x.enlaceTipoID==tipo)
                                        .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return enlaces;
        }
        public enlaces Obtener(int id)
        {
            enlaces enlace = new enlaces();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    //para retornar solo de la tabla alumno
                    // alumno = ctx.Alumno.Where(x => x.id == id)
                    //                   .SingleOrDefault();
                    //para retornar la relacion
           
                    enlace = ctx.enlaces.Include("proveedores")
                                       .Include("oficinas")
                                       .Include("oficinas.ciudades")
                                       .Include("oficinas.tipoOficina")
                                       .Include("oficinas.ciudades.departamentos")
                                       .Include("enlacesTipo")
                                       .Include("enlacesTecnologia")
                                       .Include("enlacesServicios")
                                       .Include("enlacesInternet")
                                       .Include(x=>x.contratos)
                                       .Where(x => x.enlaceID == id)
                                       .SingleOrDefault();
                    enlace.contratos = enlace.contratos.OrderByDescending(x => x.contratoID).ToList();                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            return enlace;
        }
        public ResponseModel ObtenerAjax(int id)
        {
            enlaces enlaceAjax = new enlaces();
            var rm = new ResponseModel();
            try
            {
                
                using (var ctx = new inventarioContext())
                {
                    // ctx.Configuration.LazyLoadingEnabled = false;
                    //ctx.Configuration.ProxyCreationEnabled = false;// para retornar Json
                    ctx.Configuration.ProxyCreationEnabled = false;


                    enlaceAjax = ctx.enlaces.Include("proveedores")
                                       .Include("oficinas")
                                       .Include("oficinas.ciudades")
                                       .Include("oficinas.tipoOficina")
                                       .Include("oficinas.ciudades.departamentos")
                                       .Include("enlacesTipo")//aqui esta el problema
                                       .Include("enlacesTecnologia")
                                       .Include("enlacesServicios")
                                       .Include("enlacesInternet")
                                       .Include(x => x.contratos)
                                       .Where(x => x.enlaceID == id)
                                       
                                       .SingleOrDefault();


                    enlaceAjax.contratos = enlaceAjax.contratos.OrderByDescending(x => x.contratoID).ToList();
                    
                }
                rm.response = true;
                rm.message = "";
                /****SERIALIZAR A JSON CON JSON.NET*****/
                rm.result = JsonConvert.SerializeObject(enlaceAjax, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        }
                    );
                /****FIN SERIALIZAR A JSON CON JSON.NET*****/
               
                
            }
            catch (Exception)
            {
                throw;
            }
            
            return (rm);
           // return this.Json
        }
        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
                      
            try
            {
                using (var ctx = new inventarioContext())
                {
                    ctx.Entry(this).State = EstadoAgregarEditar(this.enlaceID);

                    if (this.enlaceTipoID==3)//servicios
                    {
                        var servicios = new enlacesServicios();
                        servicios.enlaceID = this.enlaceID;
                        servicios.servicio = this._servicio;
                        servicios.direccion = this._direccion;
                        servicios.enlaceServicioID = this.enlaceServicioID;
                        ctx.Entry(servicios).State = EstadoAgregarEditar(this.enlaceServicioID);
                    }
                    if (this.enlaceTipoID == 4)//servicios
                    {
                        var internet = new enlacesInternet();
                        internet.enlaceID = this.enlaceID;
                        internet.planinternet = this._planinternet;
                        internet.enlacesInternetID = this.enlacesInternetID;
                        ctx.Entry(internet).State = EstadoAgregarEditar(this.enlacesInternetID);
                    }
                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return rm;
        }
        private EntityState EstadoAgregarEditar(int id)
        {
            if (id == 0) return EntityState.Added;
            else return EntityState.Modified;
        }
    }
}
