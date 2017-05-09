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