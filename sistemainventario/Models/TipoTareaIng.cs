namespace sistemainventario.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("TipoTareaIng")]
    public partial class TipoTareaIng
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoTareaIng()
        {
            TareasIng = new HashSet<TareasIng>();
        }

        [Key]
        public int idTipoTareaIng { get; set; }

        [StringLength(70)]
        public string TipoTarea { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]
        public virtual ICollection<TareasIng> TareasIng { get; set; }

        public List<TipoTareaIng> Listar()
        {
            List<TipoTareaIng> tareas = new List<TipoTareaIng>();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    //enlaces = ctx.enlaces.ToList();
                    tareas = ctx.TipoTareaIng
                                        .ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return tareas;
        }
        public TipoTareaIng Obtener(int id)
        {
            TipoTareaIng tarea = new TipoTareaIng();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    tarea = ctx.TipoTareaIng
                                        .SingleOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return tarea;
        }
    }
   

}
