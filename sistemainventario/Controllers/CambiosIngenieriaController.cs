using Newtonsoft.Json;
using Proyecto.Models;
using Rotativa.Core.Options;
using sistemainventario.Helper;
using sistemainventario.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace sistemainventario.Controllers
{
    [Persmiso(31)]
    [Autenticado]
    public class CambiosIngenieriaController : Controller
    {
        TareasIng tareasing = new TareasIng();
        // GET: CambiosIngenieria
        public ActionResult Index()
        {
            return View();
        }
        public string retornarTareasIng(string fechaIni, string fechaFin)
        {
            DateTime ini = Convert.ToDateTime(fechaIni);
            DateTime fin = Convert.ToDateTime(fechaFin + " 23:59:59");
            int a = responsable.ObtenerArea(SessionHelper.GetIdUser());
            var rm = new ResponseModel();
            if(AutorizadorTarea.esAutorizador())
                rm = tareasing.ListarBandejaAutorizador(ini, fin);
            else
                rm = tareasing.ListarBandejaUsuario(ini, fin, a);
            rm.function = "mostrarTablaTareasIng";
            string resultado;
            resultado = JsonConvert.SerializeObject(rm, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,

                        }
                    );
            //return Json(rm);           
            return resultado;
        } 
        public ActionResult reportes()
        {
            var areas = new areas();
            ViewBag.areas = areas.Listar();
            return View();
        }
        public string retornarTareasIngReporte(string fechaIni, string fechaFin, string _idestado="0", string _idarea="0")
        {
            DateTime ini = Convert.ToDateTime(fechaIni);
            DateTime fin = Convert.ToDateTime(fechaFin + " 23:59:59");
            int a = Convert.ToInt32(_idarea);            
            int estado = Convert.ToInt32(_idestado);
            var rm = new ResponseModel();          
            rm = tareasing.Listar(ini, fin, estado, a);
            rm.function = "mostrarTablaTareasIngReportes";
            string resultado;
            resultado = JsonConvert.SerializeObject(rm, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,

                        }
                    );
            //return Json(rm);           
            return resultado;
        }
        public ActionResult crud(int id = 0)
        {
            int idusuario = SessionHelper.GetIdUser();
            var usuarios = new usuariosSistema();
            var responsable = new responsable();
            var autorizador = new AutorizadorTarea();
            var comentarios = new ComentariosTareasIng();
            if (id == 0)
            {
                //tareasing.Usuario_id = SessionHelper.GetUser();
                
                DateTime hoy = DateTime.Now;
                
                ViewBag.usuario = usuarios.ObtenerporId(idusuario);
                ViewBag.responsable = responsable.obtenerResponsableporIdusuario(idusuario);
                ViewBag.autorizador = autorizador.Obtener();
                ViewBag.hoy = hoy;
                ViewBag.comentarios = null;
            }
            else
            {
                tareasing = tareasing.Obtener(id);
                ViewBag.usuario = tareasing.responsable.usuariosSistema;
                ViewBag.responsable = tareasing.responsable;
                ViewBag.autorizador = tareasing.AutorizadorTarea;
                ViewBag.hoy = tareasing.fecha;
                ViewBag.comentarios = comentarios.Listar(tareasing.IdTareaIng);
            }
            ViewBag.esautorizador = AutorizadorTarea.esAutorizador();
            var tipotareas = new TipoTareaIng();
            ViewBag.tipoTareas = tipotareas.Listar();

            return View(tareasing);
        }
        public JsonResult Guardar(TareasIng model)
        {
            TareasIng tarea=new TareasIng();
            var rm = new ResponseModel();
            
            if (ModelState.IsValid)
            {
                   
                rm = this.Enviar(model);
                
                if (rm.response)
                {
                    rm.href = Url.Content("~/CambiosIngenieria");
                }
            }
            return Json(rm);
        }        
        public ResponseModel Enviar(TareasIng model)//crea si no existe y envia
        {
            var rm = new ResponseModel();
            DateTime hoy = DateTime.Now;
            model.fecha = hoy;
            model.IdResponsable = responsable.ObtenerIdResponsableUsuario(SessionHelper.GetIdUser());
            var autorizador = new AutorizadorTarea();
            autorizador = autorizador.Obtener();            
            model.IdAutorizadorTarea = autorizador.Obtener().IdAutorizador;
            if(model.IdEstadoTareaIng==null || model.IdEstadoTareaIng==3 || model.IdEstadoTareaIng == 1)
            {
                model.IdEstadoTareaIng = 1;
                model.IdArea = responsable.ObtenerArea(SessionHelper.GetIdUser());
                model.Resultado = null;
                model.FechaFin = null;
                model.FechaAprobacion = null;
                model.secuencia = null;
                rm = model.Guardar();
            }            
            return rm;
        }
        public JsonResult Observar(int idTareaIng)
        {
            var rm = new ResponseModel();
            var _tarea = new TareasIng();
            _tarea = _tarea.Obtener(idTareaIng);
            if(_tarea.IdEstadoTareaIng==1)
            rm=tareasing.Observar(idTareaIng);
            if (rm.response)
            {
                rm.href = Url.Content("~/CambiosIngenieria");
            }
            return Json(rm);
        }
        public JsonResult Aprobar(int idTareaIng)
        {
            var rm = new ResponseModel();
            var _tarea = new TareasIng();
            _tarea = _tarea.Obtener(idTareaIng);
            if (_tarea.IdEstadoTareaIng == 1)
                rm=tareasing.Aprobar(idTareaIng);
            if (rm.response)
            {
                rm.href = Url.Content("~/CambiosIngenieria");
            }
            return Json(rm);
        }
        public JsonResult Finalizar(int idTareaIng, string resultado)
        {
            var rm = new ResponseModel();
            var _tarea = new TareasIng();
            _tarea = _tarea.Obtener(idTareaIng);
            if (_tarea.IdEstadoTareaIng == 2)
                rm = tareasing.Finalizar(idTareaIng,resultado);
            if (rm.response)
            {
                rm.href = Url.Content("~/CambiosIngenieria");
            }
            return Json(rm);
        }
        public JsonResult agregarComentario(ComentariosTareasIng com)
        {
            DateTime hoy = DateTime.Now;
            com.Fecha = hoy;
            com.IdUsuario = SessionHelper.GetIdUser();            
            com.ComSist = 1;
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                rm = com.Guardar();
                if (rm.response)
                {
                    rm.function = "agregarcomIng";
                    rm.result = com;
                }
            }
            var usr = SessionHelper.GetNameUser(); ;
            var coment = new { comentario = com.Comentario, horafecha = hoy.ToString("yyyy-MM-dd HH:mm:ss"), usuario = usr };
            rm.result = coment;
            return Json(rm);
        }
        [AllowAnonymous]
        public ActionResult ExportarAPDF(int id=0)
        {
            var tareasingtmp = tareasing.Obtener(id);
            string json;
            try
            {
                
                json = JsonConvert.SerializeObject(tareasingtmp, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,

                        }
                    );
            }
            catch (Exception e)
            {

                throw;
            }
            


            string cusomtSwitches = string.Format("--print-media-type --header-html {0} --header-spacing 0",
                Url.Action("Footer", "CambiosIngenieria", new { fecha = tareasingtmp.fecha }, "http"));
            return new Rotativa.MVC.ActionAsPdf("PDF",new { json= json } )
            {

                RotativaOptions = {
                    PageSize = Size.Letter,
                    PageOrientation = Orientation.Portrait,
                    CustomSwitches=cusomtSwitches,
                    PageMargins = { Left = 20, Right = 20, Top=40, Bottom=20 }, // it's in millimeters
                    
                }

            };
        }
        [AllowAnonymous]
        public ActionResult Footer(DateTime fecha)
        {
            ViewBag.fecha = fecha;
            return View();
        }
        public ActionResult PDF(string json)
        {
            //var _tareasing = new JavaScriptSerializer().Deserialize<TareasIng>(json);
            var _tareasing = JsonConvert.DeserializeObject<TareasIng>(json);
            return View(_tareasing);
        }
    }
}