namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ciudades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ciudades()
        {
            oficinas = new HashSet<oficinas>();
        }

        [Key]
        public int ciudadID { get; set; }

        [Required]
        [StringLength(150)]
        public string ciudad { get; set; }

        public int departamentoID { get; set; }

        public virtual departamentos departamentos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<oficinas> oficinas { get; set; }
    }
}
