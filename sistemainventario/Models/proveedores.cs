namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class proveedores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public proveedores()
        {
            enlaces = new HashSet<enlaces>();
        }

        [Key]
        public int proveedorID { get; set; }

        [Required]
        [StringLength(100)]
        public string proveedor { get; set; }

        [Required]
        [StringLength(20)]
        public string telefono { get; set; }

        [Required]
        [StringLength(200)]
        public string direccion { get; set; }

        [Required]
        [StringLength(100)]
        public string correo { get; set; }

        [Required]
        [StringLength(100)]
        public string web { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<enlaces> enlaces { get; set; }

        public List<proveedores> Listar()
        {
            List<proveedores> proveedores = new List<proveedores>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    proveedores = ctx.proveedores.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return proveedores;
        }
    }
}
