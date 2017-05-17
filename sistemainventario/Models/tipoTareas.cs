namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tipoTareas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipoTareas()
        {
            tareas = new HashSet<tareas>();
        }

        [Key]
        public int idTipoTarea { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoTarea { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tareas> tareas { get; set; }
        public List<tipoTareas> Listar()
        {
            var tipoTareas = new List<tipoTareas>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    tipoTareas = ctx.tipoTareas.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return tipoTareas;
        }
    }
}
