namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class contratos
    {
        [Key]
        public int contratoID { get; set; }

        public int enlaceID { get; set; }

        [StringLength(150)]
        public string contrato { get; set; }

        [StringLength(50)]
        public string otros { get; set; }

        public virtual enlaces enlaces { get; set; }
    }
}
