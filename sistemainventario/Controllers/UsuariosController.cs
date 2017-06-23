﻿
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto.Models;
using sistemainventario.Helper;
using sistemainventario.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace sistemainventario.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        
        private usuariosSistema usuarios = new usuariosSistema();
       

        public ActionResult Index()
        {
            var Areas = new areas();
            ViewBag.areas = Areas.Listar();
            return View();
        }
        public string retornarUsuarios()
        {           
            var rm = new ResponseModel();
            rm = usuarios.Listar();
           // rm.function = "mostrarTablaTareas";
            string resultado;
            resultado = JsonConvert.SerializeObject(rm, Formatting.Indented,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,

                        }
                    );
           // return Json(rm);
            return resultado;            
        }
        private bool buscarArray(string st, string[] array)
        {
            bool existe = false;
            foreach (var nombre in array)
            {
                if (contains2(st,nombre, StringComparison.OrdinalIgnoreCase))
                    existe= true;
                else
                    return false;
            }
            return existe;
        }
        //sobrecarga a funcion contains
        private bool contains2(string fuente, string achecar, StringComparison comp)
        {
            return fuente.IndexOf(achecar, comp) >= 0;
        }
        [HttpGet]
        public JsonResult Autocompletar(string b)
        {
            using (StreamReader sr = new StreamReader(Server.MapPath("~/assets/usuarios.json")))
            {
             
                List<string[]> matriz = new List<string[]>();
                

                bool buscardearray=false;
                string json1 = sr.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
                dynamic obj = serializer.Deserialize(json1, typeof(object));
                if (b.Contains(" "))                
			        buscardearray = true;                    			        
                else
                    buscardearray = false;

                string[] abuscar = b.Split(' ');
                
                for (int i = 1; i < obj.count-1; i++)
                {
                                       
                    if((obj[i].samaccountname!=null)&&(obj[i].description!=null)&&(obj[i].mailnickname != null))
                    {
                        if (buscardearray)
                        {
                            if (buscarArray(obj[i].cn[0], abuscar))
                            {
                                matriz.Add(new string[4] { obj[i].cn[0], obj[i].description[0], obj[i].samaccountname[0], obj[i].mailnickname[0] });
                            }
                        }
                        else
                        {
                            if (contains2(obj[i].cn[0],b, StringComparison.OrdinalIgnoreCase) || contains2(obj[i].samaccountname[0],b, StringComparison.OrdinalIgnoreCase))
                            {
                                matriz.Add(new string[4] { obj[i].cn[0], obj[i].description[0], obj[i].samaccountname[0], obj[i].mailnickname[0] });
                            }
                        }
                    }
                    
                }
                return Json(matriz, JsonRequestBehavior.AllowGet);
            }                     
        }
        public JsonResult Guardar(usuariosSistema model, string[] menus)
        {
            model.Estado = 1;            
            model.Roles=JsonConvert.SerializeObject(menus);
            var rm = new ResponseModel();            
            if (ModelState.IsValid)
            {
                rm = model.Guardar();            
                if (rm.response)
                {
                    rm.function = "retornarTablaUsuarios()";                                    
                }
            }
            return Json(rm);
        }

    }
    
}