namespace sistemainventario.Models
{
    using Newtonsoft.Json;
    using Proyecto.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    public partial class tareas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tareas()
        {
            comentarios = new HashSet<comentarios>();
            tareaResponsable = new HashSet<tareaResponsable>();
        }

        [Key]
        public int IdTarea { get; set; }

        public int IdArea { get; set; }

        public int IdPrioridad { get; set; }

        [StringLength(500)]
        public string TareaAsignada { get; set; }

        public int IdTipoTarea { get; set; }

        [StringLength(500)]
        public string Acciones { get; set; }

        public DateTime? FechaAsignacion { get; set; }

        public DateTime? FechaComprometida { get; set; }

        public int? HorasDiariasAsignadas { get; set; }

        public int IdEstadoTarea { get; set; }

        public DateTime? FechaCierre { get; set; }

        public int? Eficiencia { get; set; }

        public virtual areas areas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comentarios> comentarios { get; set; }

        public virtual estadoTarea estadoTarea { get; set; }

        public virtual prioridades prioridades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tareaResponsable> tareaResponsable { get; set; }

        public virtual tipoTareas tipoTareas { get; set; }
        public ResponseModel Listar()
        {
            List<tareas> tareas = new List<tareas>();
          
            var rm = new ResponseModel();
            try
            {
              
                using (var ctx = new inventarioContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = true;
                    ctx.Configuration.ProxyCreationEnabled = false;
                    tareas = ctx.tareas
                                        .Include("areas")
                                        .Include("prioridades")
                                        .Include("tipoTareas")
                                        .Include("estadoTarea")
                                        .Include("tareaResponsable.responsable.usuariosSistema")
                                        .ToList();
                 //   var records = from entity in ctx.E

                }
                rm.response = true;
                rm.message = "";
                /****SERIALIZAR A JSON CON JSON.NET*****/

                //rm.result = JsonConvert.SerializeObject(tareas, Formatting.Indented,
                //        new JsonSerializerSettings()
                //        {
                //            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,

                //        }
                //    );
                rm.result = tareas;
                /****FIN SERIALIZAR A JSON CON JSON.NET*****/
                //rm.result = JsonConvert.SerializeObject(tareas);
            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }
        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    if (this.IdTarea > 0) ctx.Entry(this).State = EntityState.Modified;
                    else ctx.Entry(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    rm.SetResponse(true);
                }                
            }
            catch (Exception e)
            {

                throw;
            }
            return rm;
        }
    }
}
