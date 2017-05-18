namespace sistemainventario.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("estadoTarea")]
    public partial class estadoTarea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public estadoTarea()
        {
            tareas = new HashSet<tareas>();
        }

        [Key]
        public int IdEstadoTarea { get; set; }

        [Column("EstadoTarea")]
        [StringLength(50)]
        public string EstadoTarea1 { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tareas> tareas { get; set; }
    }
}
