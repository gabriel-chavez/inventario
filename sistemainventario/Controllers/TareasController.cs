using Newtonsoft.Json;
using Proyecto.Models;
using sistemainventario.Models;
using System;
using System.Collections.Generic;
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
            var comentarios = new comentarios();
            ViewBag.comentarios = comentarios.Listar(id);
            ViewBag.tarea = tareas.Obtener(id);
            return View();
        }
        public string retornarTareas()
        {
            var rm = new ResponseModel();
            rm = tareas.Listar();
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
            DateTime hoy = DateTime.Today;
            model.IdEstadoTarea = 1;
            model.FechaAsignacion = hoy; //revisar que solo se guarde en
            /******************/
            var responsable = new responsable();
            //responsable.
            var tarearesponsable = new tareaResponsable();
            var editar = false;
            editar = tarearesponsable.SeEditoArea(model.IdArea, model.IdTarea);
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                rm = model.Guardar();
                if (rm.response)
                {                    
                    rm.function = "retornarAjax('tareas/retornarTareas')";
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
            DateTime hoy = DateTime.Today;
            /******************/
            // var comentario = new comentarios();
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
                    rm.function = "refrescarTareas()";                                 
                }
            }
            return Json(rm);
        }
    }
}