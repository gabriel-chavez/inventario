namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("enlacesInternet")]
    public partial class enlacesInternet
    {
        public int enlacesInternetID { get; set; }

        public int enlaceID { get; set; }

        [StringLength(150)]
        public string planinternet { get; set; }

        public virtual enlaces enlaces { get; set; }
    }
}
