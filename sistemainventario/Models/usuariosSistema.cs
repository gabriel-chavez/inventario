namespace sistemainventario.Models
{
    using Newtonsoft.Json;
    using Proyecto.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("usuariosSistema")]
    public partial class usuariosSistema
    {
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public usuariosSistema()
        {
            comentarios = new HashSet<comentarios>();
        }
        [Key]
        public int idUsuario { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }
        [StringLength(50)]
        public string Usuario { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public int? Tipo { get; set; }
        [StringLength(100)]
        public string Roles { get; set; }
        [StringLength(100)]
        public string Cargo { get; set; }
        [StringLength(100)]
        public string Imagen { get; set; }
        public int? Estado { get; set; }
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]       
        public virtual ICollection<comentarios> comentarios { get; set; }
        public ResponseModel Listar()
        {
            List<usuariosSistema> usuarios = new List<usuariosSistema>();

            var rm = new ResponseModel();
            try
            {

                using (var ctx = new inventarioContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = true;
                    ctx.Configuration.ProxyCreationEnabled = false;
                    usuarios = ctx.usuariosSistema.ToList();                    
                }
                rm.response = true;
                rm.message = "";
                rm.function = "mostrarTablaUsuarios";
                rm.result = usuarios;
                
            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }
        public usuariosSistema Obtener(string usuario)
        {
            var usuariosSistema = new usuariosSistema();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    //para retornar solo de la tabla alumno
                    // alumno = ctx.Alumno.Where(x => x.id == id)
                    //                   .SingleOrDefault();
                    //para retornar la relacion

                    usuariosSistema =  ctx.usuariosSistema
                                       .Where(x => x.Usuario == usuario)
                                       .SingleOrDefault();
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            return usuariosSistema;
        }
    }
}
