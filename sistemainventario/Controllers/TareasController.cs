using Newtonsoft.Json;
using Proyecto.Models;
using sistemainventario.App_Start;
using sistemainventario.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistemainventario.Controllers
{
    public class TareasController : Controller
    {
        private tareas tareas = new tareas();
        // GET: Tareas
        public ActionResult Index()
        {
           // tareas.retornarFechaComprometida();
            var areas = new areas();
            var prioridades = new prioridades();
            var tipoTareas = new tipoTareas();
            ViewBag.areas = areas.Listar();
            ViewBag.prioridades = prioridades.Listar();
            ViewBag.tipoTareas = tipoTareas.Listar();

            return View();
        }
        public ActionResult Ver(int id)
        {
            var responsables = new responsable();
            var comentarios = new comentarios();
            ViewBag.comentarios = comentarios.Listar(id);
            ViewBag.tarea = tareas.Obtener(id);
            TempData["idarea"] = ViewBag.tarea.IdArea;
            TempData["idtarea"] = id;
            ViewBag.usuariosArea = responsables.listarResponsable(ViewBag.tarea.IdArea);
            return View();
        }
        public string retornarTareas(string fechaIni, string fechaFin, string area)
        {
            DateTime ini = Convert.ToDateTime(fechaIni);
            DateTime fin = Convert.ToDateTime(fechaFin+" 23:59:59");
        
            int a = Convert.ToInt32(area);

            var rm = new ResponseModel();
            rm = tareas.Listar(ini,fin,a);
            rm.function = "mostrarTablaTareas";
            string  resultado;
            resultado = JsonConvert.SerializeObject(rm, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,

                        }
                    );
            // return Json(rm);
            return resultado;
        }
        public JsonResult Guardar(tareas model)
        {            
            DateTime hoy = DateTime.Now;
            model.IdEstadoTarea = 1;//el estado siempre inicia en  ejecutando = 1
            model.FechaAsignacion = hoy;             
            var responsable = new responsable();
            var tarearesponsable = new tareaResponsable();
            var editar = false;
            editar = tarearesponsable.SeEditoArea(model.IdArea, model.IdTarea);
            var rm = new ResponseModel();
            var correlativo = 0 ;
            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                //correlativo = model.ObtenerCorrelativo();
                if (rm.response)
                {                    
                    rm.function = "retornarAjax(base_url('tareas/retornarTareas'))";
                    //agregar resposable de tarea
                    tarearesponsable.IdTarea = model.IdTarea;
                    tarearesponsable.FechaAsignacionResponsable = hoy;
                    tarearesponsable.IdResponsable = responsable.ObtenerIdResponsable(model.IdArea);
                    tarearesponsable.Guardar(editar);                    
                }
            }
            return Json(rm);
        }
        [HttpPost]
        public JsonResult agregarComentario(comentarios com)
        {
            DateTime hoy = DateTime.Now;          
            com.FechaHora = hoy;
            com.IdUsuario = 1;
            com.Visible = 1;
            com.ComentarioSistema = 1;
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                rm = com.Guardar();
                if (rm.response)
                {
                    rm.function = "agregarcom";
                    rm.result = com;
                }
            }
            var usr = "xxx";
            var coment = new { comentario=com.Comentario, horafecha=hoy.ToString("yyyy-MM-dd HH:mm:ss"),usuario=usr};
            rm.result = coment;
            return Json(rm);
        }
        [HttpPost]
        public JsonResult GuardarResHoras(responsableHora rh)
        {
            
            var rm = new ResponseModel();
            
            rh.idarea=Convert.ToInt32(TempData["idarea"]);
            rh.idtarea= Convert.ToInt32(TempData["idtarea"]);
            rm=rh.guardar();
            if(rm.response)
            {
                var comSis = new comentarios();
                comSis.comentarioSistema(rh.idtarea,"Se modificaron algunos datos de la descripcion de la tarea ");
                rm.href = Url.Content("self");

            }            
            return Json(rm);
        }
        public JsonResult FinalizarTarea()
        {
            var rm = new ResponseModel(); 
            tareas.IdTarea= Funciones.TareaVisualizada();
            rm =tareas.finalizarTarea();
            if(rm.response)
            {
                var comSis = new comentarios();
                comSis.comentarioSistema(tareas.IdTarea, "Tarea finalizada por el usuario");
                rm.href = Url.Content("self");
            }       
                
            return Json(rm);
        }
    }
}