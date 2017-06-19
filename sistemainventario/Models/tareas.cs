namespace sistemainventario.Models
{
    using Newtonsoft.Json;
    using Proyecto.Models;
    using App_Start;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Data.Entity.SqlServer;

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
        public int? Nro { get; set; }

        public virtual areas areas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<comentarios> comentarios { get; set; }

        public virtual estadoTarea estadoTarea { get; set; }

        public virtual prioridades prioridades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tareaResponsable> tareaResponsable { get; set; }

        public virtual tipoTareas tipoTareas { get; set; }
        public ResponseModel Listar(DateTime ini,DateTime fin, int area)
        {
            List<tareas> tareas = new List<tareas>();

            var rm = new ResponseModel();
            try
            {

                using (var ctx = new inventarioContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = true;
                    ctx.Configuration.ProxyCreationEnabled = false;
                    if(area==0)
                    {
                        tareas = ctx.tareas
                                        .Include("areas")
                                        .Include("prioridades")
                                        .Include("tipoTareas")
                                        .Include("estadoTarea")
                                        .Include("tareaResponsable.responsable.usuariosSistema")
                                        .Include(x => x.comentarios)
                                        .Where(x => x.FechaAsignacion >= ini)
                                        .Where(x => x.FechaAsignacion <= fin)
                                        .OrderByDescending(x => x.Nro)
                                        .OrderByDescending(x => x.FechaAsignacion)
                                        .ToList();
                    }
                    else
                    {
                        tareas = ctx.tareas
                                        .Include("areas")
                                        .Include("prioridades")
                                        .Include("tipoTareas")
                                        .Include("estadoTarea")
                                        .Include("tareaResponsable.responsable.usuariosSistema")
                                        .Include(x => x.comentarios)
                                        .Where(x=>x.IdArea==area)
                                        .Where(x => x.FechaAsignacion >= ini)
                                        .Where(x => x.FechaAsignacion <= fin)
                                        .OrderByDescending(x => x.Nro)
                                        .OrderByDescending(x => x.FechaAsignacion)
                                        .ToList();
                    }
                    
                  

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
                    if (this.IdTarea > 0)
                    {
                        
                        ctx.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        this.Nro = this.ObtenerCorrelativo();
                        ctx.Entry(this).State = EntityState.Added;
                    }

                    if(this.editardatos(this.IdTarea))//se puede editar?
                    {
                        ctx.SaveChanges();
                        rm.SetResponse(true);
                    }
                    else
                    {
                        rm.message = "No se puede editar esta tarea finalizada";
                    }                    
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return rm;
        }
        public tareas Obtener(int id)
        {
            tareas tarea = new tareas();
            try
            {
                using (var ctx = new inventarioContext())
                {

                    tarea = ctx.tareas.Include(x => x.areas)
                                       .Include(x => x.prioridades)
                                       .Include(x => x.tipoTareas)
                                       .Include(x => x.estadoTarea)
                                       .Include("tareaResponsable.responsable.usuariosSistema")
                                       .Where(x => x.IdTarea == id)
                                       .SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return tarea;
        }
        public ResponseModel finalizarTarea()
        {
            DateTime hoy = DateTime.Now;            
            var rm = new ResponseModel();
            DateTime comprometida = retornarFechaComprometida(this.IdTarea);
            int eficiencia = 0;            
            TimeSpan ts;
            if (hoy >= comprometida)
            { 
                ts = hoy - comprometida;
                // Diferencia dias.
                eficiencia = ts.Days;
            }
            string sql;
            try
            {
                using (var ctx = new inventarioContext())
                {
                    sql = "UPDATE dbo.tareas SET IdEstadoTarea=2,Eficiencia="+eficiencia+ ", FechaCierre='"+hoy.ToString("s") + "' Where idTarea=" + this.IdTarea;
                    ctx.Database.ExecuteSqlCommand(sql);
                    rm.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }
        public DateTime retornarFechaComprometida(int idTarea)
        {
            DateTime comprometida=new DateTime();
            string sql;
            
            try
            {
               /* using (var ctx = new inventarioContext())
                {
                    
                    sql = "Select FechaComprometida From dbo.tareas Where idTarea=" + this.IdTarea;
                    var xxx=ctx.Database.ExecuteSqlCommand(sql);                  
                }*/
                using (var ctx = new inventarioContext())
                {
                    sql = "Select FechaComprometida From dbo.tareas Where idTarea=" + idTarea;
                    comprometida = ctx.Database.SqlQuery<DateTime>(sql).FirstOrDefault<DateTime>();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return comprometida;
        }
        public bool editardatos(int idTarea)//se puede editar los datos???
        {
            bool editar = false;
            string sql;
            int estadoTarea;
            try
            {
                /* using (var ctx = new inventarioContext())
                 {

                     sql = "Select FechaComprometida From dbo.tareas Where idTarea=" + this.IdTarea;
                     var xxx=ctx.Database.ExecuteSqlCommand(sql);                  
                 }*/
                using (var ctx = new inventarioContext())
                {
                    sql = "Select IdEstadoTarea From dbo.tareas Where idTarea=" + idTarea;
                    estadoTarea = ctx.Database.SqlQuery<int>(sql).FirstOrDefault<int>();
                    if (estadoTarea == 2) //2 finalizado
                        editar = false;
                    else
                        editar = true;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return editar;
        }
        public int ObtenerCorrelativo() //obtiene numero correlativo de acuerdo al año
        {
           // List<tareas> tareas = new List<tareas>();
            tareas tareas1 = new tareas();
            DateTime hoy = DateTime.Now;
            int correlativo=0;
            try
            {
                using (var ctx = new inventarioContext())
                {
                    //para retornar solo de la tabla alumno
                    // alumno = ctx.Alumno.Where(x => x.id == id)
                    //                   .SingleOrDefault();
                    //para retornar la relacion
                    //tareas = ctx.tareas.Where(x => x.FechaAsignacion.Value.Year == hoy.Year)
                    //                   .OrderByDescending(x => x.Nro)
                    //                   .ToList();
                    tareas1 = ctx.tareas.Where(x => x.FechaAsignacion.Value.Year == hoy.Year)
                                       .OrderByDescending(x => x.Nro)
                                       .FirstOrDefault();

                    correlativo = 0;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            if (tareas1.Nro == null)
                correlativo = 0;
            else               
                correlativo = (int)tareas1.Nro;           
            return correlativo+1;
        }
    }
}
