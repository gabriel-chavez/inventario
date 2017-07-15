namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EstadoTareasIng")]
    public partial class EstadoTareasIng
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EstadoTareasIng()
        {
            TareasIng = new HashSet<TareasIng>();
        }

        [Key]
        public int IdEstadoTareaIng { get; set; }

        [StringLength(80)]
        public string EstadoTareaIng { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TareasIng> TareasIng { get; set; }
    }
}
