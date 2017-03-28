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
            return View(enlaces.Listar());
        }
        
    }
}