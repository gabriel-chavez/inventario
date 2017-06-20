using Codeplex.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto.Models;
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
        public JsonResult Autocompletar(string b)
        {
            using (StreamReader sr = new StreamReader(Server.MapPath("~/assets/usuarios.json")))
            {
                string json1 = sr.ReadToEnd();
                
                var myObj = DynamicJson.Parse(json1);
                foreach (var status in myObj)
                {
                    Console.WriteLine(status.user.screen_name);
                    Console.WriteLine(status.text);
                }
                JObject o = JObject.Parse(json1);


                var json = DynamicJson.Parse(@"{""foo"":""json"", ""bar"":100, ""nest"":{ ""foobar"":true } }");

                var r1 = json.foo; // "json" - dynamic(string)
                var r2 = json.bar; // 100 - dynamic(double)
                var r3 = json.nest.foobar; // true - dynamic(bool)
                var r4 = json["nest"]["foobar"]; // can access indexer
                return Json(o);
            }
           
          
        }
        public T Deserialize<T>(string json)
        {
            T concreteObject = Activator.CreateInstance<T>();
            var memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(json));
            var serializer = new DataContractJsonSerializer(concreteObject.GetType());
            concreteObject = (T)serializer.ReadObject(memoryStream);
            memoryStream.Close();
            memoryStream.Dispose();
            return concreteObject;
        }
      
    }
    
}