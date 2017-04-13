﻿using System;
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
            var oficinas = new oficinas();
            var proveedores = new proveedores();
            var enlacesTipo = new enlacesTipo();
            var enlacesTecnologia = new enlacesTecnologia();

            ViewBag.oficinas = oficinas.Listar();
            ViewBag.proveedores = proveedores.Listar();
            ViewBag.enlacesTipo = enlacesTipo.Listar();
            ViewBag.enlacesTecnologia = enlacesTecnologia.Listar();

            ViewBag.nacional = enlaces.Listar(1);
            ViewBag.local = enlaces.Listar(2);
            ViewBag.servicios = enlaces.Listar(3);
            ViewBag.internet = enlaces.Listar(4);

            return View(enlaces);
        }
        public ActionResult VerEnlace(int id)
        {
           

            return View(enlaces.Obtener(id));
        }
        public List<oficinas> retornarOficinas()
        {
            var oficinas = new List <oficinas>();
            
            return oficinas;
        }
    }
}