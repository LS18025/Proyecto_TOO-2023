using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaDatos;
using Gestion_Proyectos.Permisos;

namespace Gestion_Proyectos.Controllers
{
    [ValidarSesion]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult listarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            lista = new GestionUsuarios().GetUsuarios();
            return Json(lista, JsonRequestBehavior.AllowGet);


        }
    }
}