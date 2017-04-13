namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class oficinas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public oficinas()
        {
            enlaces = new HashSet<enlaces>();
        }

        [Key]
        public int oficinaID { get; set; }

        public byte estado { get; set; }

        public int ciudadID { get; set; }

        public int tipoOficinaID { get; set; }

        [Required]
        public string nombre_oficina { get; set; }

        public int nroOficina { get; set; }

        [Required]
        public string direccion { get; set; }

        public virtual ciudades ciudades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<enlaces> enlaces { get; set; }

        public virtual tipoOficina tipoOficina { get; set; }



        public List<oficinas> Listar()
        {
            List<oficinas> oficinas = new List<oficinas>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    oficinas = ctx.oficinas.ToList();       
                }
            }
            catch (Exception)
            {

                throw;
            }
            return oficinas;
        }
    }
}
