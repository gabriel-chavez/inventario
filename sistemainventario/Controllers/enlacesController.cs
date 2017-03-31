using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistemainventario.Models;

namespace sistemainventario.Controllers
{
    public class enlacesController : Controller
    {
        // GET: enlaces
        private enlaces enlaces = new enlaces();
        public ActionResult Index()
        {
            /*
            enlaceTipoID	tipo
                    1	Nacional
                    2	Local
                    3	Servicio
                    4	Internet
             */
            ViewBag.nacional = enlaces.Listar(1);
            ViewBag.local = enlaces.Listar(2);
            ViewBag.servicios = enlaces.Listar(3);
            ViewBag.internet = enlaces.Listar(4);
            return View(enlaces);
        }
        public ActionResult nacional(int id)
        {            
            return View(enlaces.Obtener(id));
        }

    }
}