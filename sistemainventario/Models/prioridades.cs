namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class prioridades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public prioridades()
        {
            tareas = new HashSet<tareas>();
        }

        [Key]
        public int idPrioridad { get; set; }

        [Required]
        [StringLength(50)]
        public string Prioridad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tareas> tareas { get; set; }
        public List<prioridades> Listar()
        {
            var prioridades = new List<prioridades>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    prioridades = ctx.prioridades.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return prioridades;
        }
    }
}
