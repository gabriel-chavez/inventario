namespace sistemainventario.Models
{
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdResponsable { get; set; }

        public int? IdUsuario { get; set; }

        public int? IdArea { get; set; }

        public short? Encargado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tareaResponsable> tareaResponsable { get; set; }
        public virtual usuariosSistema usuariosSistema { get; set; }
        public int ObtenerIdResponsable(int IdArea)
        {
            int IdResponsable;

            //var modelResponsable = new List<responsable>();
            var modelResponsable = new responsable();
            //List<enlaces> enlaces = new List<enlaces>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    //enlaces = ctx.enlaces.ToList();
                    modelResponsable = ctx.responsable
                                        .Where(x => x.IdArea == IdArea)
                                        .Where(x => x.Encargado == 1)
                                        .SingleOrDefault();
                }
                IdResponsable = (int)modelResponsable.IdResponsable;
            }
            catch (Exception)
            {

                throw;
            }
            
            return IdResponsable;
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
    }
}
