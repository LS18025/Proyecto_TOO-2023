using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Gestion_Proyectos.Controllers
{
    public class ProyectoController : Controller
    {
        // GET: Proyecto
        public ActionResult VistaProyecto()
        {
            return View();
        }
        public ActionResult VistaObjetivo()
        {
            return View();
        }
        public ActionResult VistaActividad()
        {
            return View();
        }
        public ActionResult VistaTarea()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ListarProyectos()
        {
            List<Proyecto> listaProyectos = new List<Proyecto>();
            listaProyectos = new GestionProyectos().GetProyectos();
            return Json(new { data = listaProyectos }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarProyecto(Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                GestionProyectos gestionProyectos = new GestionProyectos();

                bool insercionExitosa = gestionProyectos.newProyecto(proyecto);

                if (insercionExitosa)
                {
                    return Json(new { success = true, message = "Proyecto insertado exitosamente" });
                }
                else
                {
                    return Json(new { success = false, message = "Error al insertar el proyecto" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Por favor, complete todos los campos obligatorios correctamente" });
            }
        }


    }
}