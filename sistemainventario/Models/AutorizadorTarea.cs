namespace sistemainventario.Models
{
    using Helper;
    using Proyecto.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("AutorizadorTarea")]
    public partial class AutorizadorTarea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AutorizadorTarea()
        {
            TareasIng = new HashSet<TareasIng>();
        }

        [Key]
        public int IdAutorizador { get; set; }

        public int IdResponsable { get; set; }

        public int estado { get; set; } // en la base admite null

        public virtual responsable responsable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TareasIng> TareasIng { get; set; }
        public AutorizadorTarea Obtener()
        {
            AutorizadorTarea autorizador = new AutorizadorTarea();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    autorizador = ctx.AutorizadorTarea.Where(x => x.estado == 1)
                                                    .Include(x=>x.responsable.usuariosSistema)
                                                    .FirstOrDefault();

                }
            }
            catch (Exception e)
            {

                throw;
            }
            return autorizador;
        }
        public static bool esAutorizador()
        {
            var responsable = new responsable();
            int idResponsable=responsable.obtenerResponsableporIdusuario(SessionHelper.GetIdUser()).IdResponsable;
            AutorizadorTarea autorizador = new AutorizadorTarea();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    autorizador = ctx.AutorizadorTarea.Where(x => x.estado == 1)
                                                    .Where(x=>x.IdResponsable==idResponsable)
                                                    .Include(x => x.responsable.usuariosSistema)
                                                    .FirstOrDefault();

                }
                if (autorizador != null)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {

                throw;
            }

        }

    }
}
