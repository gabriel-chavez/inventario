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

    public partial class areas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public areas()
        {
            tareas = new HashSet<tareas>();
        }

        [Key]
        public int IdArea { get; set; }

        [StringLength(100)]
        public string Area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tareas> tareas { get; set; }
        public ResponseModel ListarAjax()
        {
            var areas = new List<areas>();
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new inventarioContext())
                {

                    ctx.Configuration.ProxyCreationEnabled = false;
                    areas = ctx.areas.ToList();
                }
                rm.response = true;
                rm.message = "";
                /****SERIALIZAR A JSON CON JSON.NET*****/
                rm.result = JsonConvert.SerializeObject(tareas, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        }
                    );
                /****FIN SERIALIZAR A JSON CON JSON.NET*****/
            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }
        public List<areas> Listar()
        {
            var areas = new List<areas>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    areas = ctx.areas.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return areas;
        }
    }
    
}
