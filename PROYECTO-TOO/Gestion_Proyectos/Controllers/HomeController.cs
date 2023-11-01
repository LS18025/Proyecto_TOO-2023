using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaDatos;



namespace Gestion_Proyectos.Controllers
{
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

        [HttpGet]
        public JsonResult ListarTareas()
        {
            List<Tarea> oLista = new List<Tarea>();
            oLista = new CN_Tarea().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTarea(Tarea objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if(objeto.idTarea == 0)
            {
                resultado = new CN_Tarea().RegistrarTarea(objeto, out mensaje);
            }
            else
            {
                resultado = new CN_Tarea().EditarTarea(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarTarea(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Tarea().EliminarTarea(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}