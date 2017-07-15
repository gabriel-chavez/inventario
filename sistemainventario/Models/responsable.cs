namespace sistemainventario.Models
{
    using Helper;
    using Newtonsoft.Json;
    using Proyecto.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("responsable")]
    public partial class responsable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public responsable()
        {
            tareaResponsable = new HashSet<tareaResponsable>();
        }

        [Key]        
        public int IdResponsable { get; set; }

        public int? IdUsuario { get; set; }

        public int? IdArea { get; set; }

        public short? Encargado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
       // [JsonIgnore]
        public virtual ICollection<tareaResponsable> tareaResponsable { get; set; }
       // [JsonIgnore]
        public virtual usuariosSistema usuariosSistema { get; set; }
        public virtual areas areas { get; set; }

        public int ObtenerIdResponsable(int IdArea)
        {
            int IdResponsable;
            var modelResponsable = new responsable();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    modelResponsable = ctx.responsable
                                        .Where(x => x.IdArea == IdArea)
                                        .Where(x => x.Encargado == 1)
                                        .OrderByDescending(x => x.IdUsuario)
                                        .FirstOrDefault();
                }
                IdResponsable = (int)modelResponsable.IdResponsable;
            }
            catch (Exception)
            {
                throw;
            }            
            return IdResponsable;
        }
        public responsable obtenerResponsableporIdusuario(int IdUsuario)
        {            
            var modelResponsable = new responsable();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    modelResponsable = ctx.responsable
                                        .Include(x=>x.areas)
                                        .Where(x => x.IdUsuario==IdUsuario)
                                       
                                        .FirstOrDefault();
                }                
            }
            catch (Exception)
            {
                throw;
            }
            return modelResponsable;
        }
        public List<responsable> obtenerResposables(int IdArea)
        {
            List <responsable> _responsables;
            var modelResponsable = new responsable();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    _responsables = ctx.responsable
                                        .Where(x => x.IdArea == IdArea)
                                        .Where(x => x.Encargado == 1)
                                        .OrderByDescending(x => x.IdUsuario)
                                        .ToList();
                }
                
            }
            catch (Exception)
            {
                throw;
            }
            return _responsables;
        }
        public static int ObtenerIdResponsableUsuario(int IdUsuario)
        {
            int _IdUsuario;
            var modelResponsable = new responsable();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    modelResponsable = ctx.responsable
                                        .Where(x => x.IdUsuario == IdUsuario)
                                        //.Where(x => x.Encargado == 1)                                        
                                        .FirstOrDefault();
                }
                if (modelResponsable != null)
                    _IdUsuario = (int)modelResponsable.IdResponsable;
                else
                    _IdUsuario = -1;
            }
            catch (Exception)
            {
                throw;
            }
            return _IdUsuario;
        }
        public static int ObtenerArea(int IdUsuario)
        {
            int area;
            var modelResponsable = new responsable();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    modelResponsable = ctx.responsable
                                        .Where(x => x.IdUsuario== IdUsuario)                                        
                                        .SingleOrDefault();
                }
                if (modelResponsable != null)
                    area = (int)modelResponsable.IdArea;
                else
                    area = -100;
            }
            catch (Exception)
            {
                throw;
            }
            return area;
        }
        public List<responsable> listarResponsable(int IdArea)
        {


            //var modelResponsable = new List<responsable>();
            List<responsable> modelResponsable = new List<responsable>();
            //List<enlaces> enlaces = new List<enlaces>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    //enlaces = ctx.enlaces.ToList();
                    modelResponsable = ctx.responsable
                                        .Include("usuariosSistema")
                                        .Where(x => x.IdArea == IdArea)
                                        .ToList();
                }                
            }
            catch (Exception)
            {

                throw;
            }

            return modelResponsable;
        }
        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    if (this.IdResponsable > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
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
        public static bool esEncargado()
        {
            int IdUsuario = SessionHelper.GetIdUser();
            var _responsable = new responsable();
            try
            {
                using (var ctx = new inventarioContext())
                { 
                    _responsable = ctx.responsable
                                        .Where(x => x.IdUsuario == IdUsuario)
                                        .Where(x => x.Encargado == 1)
                                        .SingleOrDefault();
                }
                if (_responsable != null)
                    return true;    
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }            
        }
        public static void eliminarResponsable(int IdResponsable)
        {
            if (IdResponsable > 0)
            {
                var rm = new ResponseModel();
                try
                {
                    using (var ctx = new inventarioContext())
                    {
                        //this.IdTarea = id;
                        ctx.Database.ExecuteSqlCommand("DELETE FROM dbo.tareaResponsable WHERE IdResponsable = " + IdResponsable);
                        ctx.Database.ExecuteSqlCommand("DELETE FROM dbo.responsable WHERE IdResponsable = " + IdResponsable);
                        //    ctx.SaveChanges();

                        rm.SetResponse(true);
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
                //return rm;
            }
        }
    }
}
