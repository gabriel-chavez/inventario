namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("enlacesTecnologia")]
    public partial class enlacesTecnologia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public enlacesTecnologia()
        {
            enlaces = new HashSet<enlaces>();
        }

        [Key]
        public int enlaceTecnologiaID { get; set; }

        [Required]
        [StringLength(150)]
        public string tecnologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<enlaces> enlaces { get; set; }
        public List<enlacesTecnologia> Listar()
        {
            List<enlacesTecnologia> enlacesTecnologia = new List<enlacesTecnologia>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    enlacesTecnologia = ctx.enlacesTecnologia.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return enlacesTecnologia;
        }
    }
}
