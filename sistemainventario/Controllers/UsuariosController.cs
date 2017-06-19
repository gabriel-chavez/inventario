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
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        
        private usuariosSistema usuarios = new usuariosSistema();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult retornarUsuarios()
        {           
            var rm = new ResponseModel();
            rm = usuarios.Listar();
           // rm.function = "mostrarTablaTareas";
            string resultado;
            //resultado = JsonConvert.SerializeObject(rm, Formatting.Indented,
            //            new JsonSerializerSettings()
            //            {
            //                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,

            //            }
            //        );
            return Json(rm);
            //return resultado;            
        }
    }
}