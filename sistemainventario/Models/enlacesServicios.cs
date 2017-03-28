namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class enlacesServicios
    {
        [Key]
        public int enlaceServicioID { get; set; }

        public int enlaceID { get; set; }

        [Required]
        [StringLength(100)]
        public string servicio { get; set; }

        [Required]
        public string direccion { get; set; }

        public virtual enlaces enlaces { get; set; }
    }
}
