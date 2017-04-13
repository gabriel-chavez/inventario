namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("enlacesTipo")]
    public partial class enlacesTipo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public enlacesTipo()
        {
            enlaces = new HashSet<enlaces>();
        }

        [Key]
        public int enlaceTipoID { get; set; }

        [StringLength(150)]
        public string tipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<enlaces> enlaces { get; set; }

        public List<enlacesTipo> Listar()
        {
            List<enlacesTipo> enlacesTipo = new List<enlacesTipo>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    enlacesTipo = ctx.enlacesTipo.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return enlacesTipo;
        }
    }
}
