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
        public ActionResult Ver()
        {
            return View();
        }
        public JsonResult retornarTareas()
        {
            var rm = new ResponseModel();
            rm = tareas.Listar();
            rm.function = "mostrarTablaTareas";
            return Json(rm);
        }
    }
}