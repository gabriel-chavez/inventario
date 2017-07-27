namespace sistemainventario.Models
{
    using Helper;
    using Newtonsoft.Json;
    using Proyecto.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("TareasIng")]
    public partial class TareasIng
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TareasIng()
        {
            ComentariosTareasIng = new HashSet<ComentariosTareasIng>();
        }
        [Key]
        public int IdTareaIng { get; set; }

        public DateTime fecha { get; set; }        

        public int IdResponsable { get; set; }

        public int? IdAutorizadorTarea { get; set; }

        [Required]
        [StringLength(100)]
        public string TareaAccion { get; set; }

        [StringLength(100)]
        public string Motivo { get; set; }

        public DateTime? FechaEjecucion { get; set; }

        public int IdTipoTareaIng { get; set; }

        public int? IdEstadoTareaIng { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [StringLength(500)]
        public string Detalle { get; set; }

        [StringLength(500)]
        public string Resultado { get; set; }

        public DateTime? FechaFin { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        
        public int? secuencia { get; set; }
        public int IdArea { get; set; }
        public virtual AutorizadorTarea AutorizadorTarea { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ComentariosTareasIng> ComentariosTareasIng { get; set; }

        public virtual EstadoTareasIng EstadoTareasIng { get; set; }

        public virtual responsable responsable { get; set; }

        public virtual TipoTareaIng TipoTareaIng { get; set; }
        
        public virtual areas areas { get; set; }
        public ResponseModel ListarBandejaUsuario(DateTime ini, DateTime fin, int area)
        {
            List<TareasIng> tareasing = new List<TareasIng>();
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = true;
                    ctx.Configuration.ProxyCreationEnabled = false;                   
                    tareasing = ctx.TareasIng
                                    .Include("responsable")
                                    .Include("responsable.areas")
                                    .Include("responsable.usuariosSistema")
                                    .Include("TipoTareaIng")
                                    .Include("EstadoTareasIng")
                                    .Include(x=>x.AutorizadorTarea.responsable.usuariosSistema)
                                    //.Include("TipoTareaIng")
                                    //.Include("EstadoTareasIng")                                                                                
                                    .Where(x => x.fecha >= ini)
                                    .Where(x => x.fecha <= fin)
                                    .Where(x => x.IdArea == area)
                                    //.OrderByDescending(x => x.id)                                        
                                    .ToList();                   
                }
                rm.response = true;
                rm.message = "";               
                rm.result = tareasing;
            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }
        public ResponseModel ListarBandejaAutorizador(DateTime ini, DateTime fin)
        {
            List<TareasIng> tareasing = new List<TareasIng>();
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = true;
                    ctx.Configuration.ProxyCreationEnabled = false;
                    tareasing = ctx.TareasIng
                                    .Include("responsable")
                                    .Include("responsable.areas")
                                    .Include("responsable.usuariosSistema")
                                    .Include("TipoTareaIng")
                                    .Include("EstadoTareasIng")
                                    .Include(x => x.AutorizadorTarea.responsable.usuariosSistema)
                                    .Where(x => x.IdEstadoTareaIng==1)
                                    .Where(x => x.fecha >= ini)
                                    .Where(x => x.fecha <= fin)                                    
                                    .ToList();
                }
                rm.response = true;
                rm.message = "";
                rm.result = tareasing;
            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }
        public ResponseModel Listar(DateTime ini, DateTime fin, int _idestado=0,int _idarea=0)
        {
            List<TareasIng> tareasing = new List<TareasIng>();
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = true;
                    ctx.Configuration.ProxyCreationEnabled = false;
                    var _tareasing = ctx.TareasIng
                                    .Include("responsable")
                                    .Include("responsable.areas")
                                    .Include("responsable.usuariosSistema")
                                    .Include("TipoTareaIng")
                                    .Include("EstadoTareasIng")
                                    .Include(x => x.AutorizadorTarea.responsable.usuariosSistema)
                                    .Where(x => x.fecha >= ini)
                                    .Where(x => x.fecha <= fin);
                    if (_idestado > 0)
                        _tareasing = _tareasing.Where(x=>x.IdEstadoTareaIng==_idestado);
                    if (_idarea > 0)
                        _tareasing = _tareasing.Where(x => x.IdArea == _idarea);
                    tareasing = _tareasing.ToList();
                }
                rm.response = true;
                rm.message = "";
                rm.result = tareasing;
            }
            catch (Exception e)
            {
                throw;
            }
            return rm;
        }
        public TareasIng Obtener(int id)
        {
            var tareaing = new TareasIng();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = true;
                    ctx.Configuration.ProxyCreationEnabled = false;
                    tareaing = ctx.TareasIng.Where(x => x.IdTareaIng == id)
                                .Include(x => x.responsable.areas)            
                                .Include(x=>x.responsable.usuariosSistema)
                                .Include(x => x.AutorizadorTarea.responsable.usuariosSistema)
                                .SingleOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tareaing;
        }
        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new inventarioContext())
                {
                    if (this.IdTareaIng > 0) ctx.Entry(this).State = EntityState.Modified;
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
        public ResponseModel Observar(int idTareaing)
        {
            var rm = new ResponseModel();
            string sql;      
            try
            {
                using (var ctx = new inventarioContext())
                {
                    sql = "UPDATE dbo.TareasIng SET IdEstadoTareaIng=3 WHERE IdTareaIng="+idTareaing;
                    ctx.Database.ExecuteSqlCommand(sql);
                    rm.SetResponse(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }
        public ResponseModel Aprobar(int idTareaing)
        {
            DateTime hoy = DateTime.Now;
            var rm = new ResponseModel();
            string sql;
            try
            {
                using (var ctx = new inventarioContext())
                {
                    sql = "UPDATE dbo.TareasIng SET IdEstadoTareaIng=2, FechaAprobacion='"+ hoy.ToString("s") + "' WHERE IdTareaIng=" + idTareaing;
                    ctx.Database.ExecuteSqlCommand(sql);
                    rm.SetResponse(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }
        public ResponseModel Finalizar(int idTareaing,string resultado)
        {
            DateTime hoy = DateTime.Now;
            var rm = new ResponseModel();
            string sql;
            int area = responsable.ObtenerArea(SessionHelper.GetIdUser());            
            int secuencia=TareasIng.ObtenerSecuencia(area);
            try
            {
                using (var ctx = new inventarioContext())
                {
                    sql = "UPDATE dbo.TareasIng SET IdEstadoTareaIng=4, secuencia="+secuencia+", Resultado='"+resultado+"', FechaFin='"+ hoy.ToString("s") + "' WHERE IdTareaIng=" + idTareaing;
                    ctx.Database.ExecuteSqlCommand(sql);
                    rm.SetResponse(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rm;
        }
        public static int ObtenerSecuencia(int area)
        {
            var tareas1 = new TareasIng();
            DateTime hoy = DateTime.Now;
            int correlativo = 0;
            try
            {
                using (var ctx = new inventarioContext())
                {               
                    tareas1 = ctx.TareasIng
                                        .Where(x => x.IdArea==area)
                                        .Where(x => x.FechaEjecucion.Value.Year == hoy.Year)
                                        .Where(x=>x.secuencia!=null)
                                        .OrderByDescending(x => x.secuencia)
                                        .FirstOrDefault();
                    correlativo = 0;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            if (tareas1 != null)
            {
                if (tareas1.secuencia == null)
                    correlativo = 0;
                else
                    correlativo = (int)tareas1.secuencia;
            }
            else
            {
                correlativo = 0;
            }
            return correlativo + 1;
        }
    }
}
