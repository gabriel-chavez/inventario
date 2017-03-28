namespace sistemainventario.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class enlaces
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public enlaces()
        {
            contratos = new HashSet<contratos>();
            enlacesInternet = new HashSet<enlacesInternet>();
            enlacesServicios = new HashSet<enlacesServicios>();
        }

        [Key]
        public int enlaceID { get; set; }

        public int proveedorID { get; set; }

        public int oficinaID { get; set; }

        public int enlaceTipoID { get; set; }

        public int enlace { get; set; }

        public int enlaceTecnologiaID { get; set; }

        [StringLength(50)]
        public string velocidad { get; set; }

        public int? mensualidad { get; set; }

        [Required]
        public string observaciones { get; set; }

        public byte estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contratos> contratos { get; set; }

        public virtual enlacesTecnologia enlacesTecnologia { get; set; }

        public virtual enlacesTipo enlacesTipo { get; set; }

        public virtual oficinas oficinas { get; set; }

        public virtual proveedores proveedores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<enlacesInternet> enlacesInternet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<enlacesServicios> enlacesServicios { get; set; }
        /*********************************/
        public List<enlaces> Listar()
        {
            List<enlaces> enlaces = new List<enlaces>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    //enlaces = ctx.enlaces.ToList();
                    enlaces = ctx.enlaces.Include("proveedores")
                                        .Include("oficinas")
                                        .Include("oficinas.ciudades")
                                        .Include("oficinas.ciudades.departamentos")
                                        .Include("enlacesTipo")
                                        .Include("enlacesTecnologia")
                                        .ToList();
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return enlaces;
        }
    }
}
