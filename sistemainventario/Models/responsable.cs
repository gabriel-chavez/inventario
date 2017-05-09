namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
    }
}
