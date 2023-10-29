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
        public ActionResult AgregarProyecto(Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                GestionProyectos gestionProyectos = new GestionProyectos(); // Crear una instancia de GestionProyectos

                bool insercionExitosa = gestionProyectos.newProyecto(proyecto);

                if (insercionExitosa)
                {
                    TempData["SuccessMessage"] = "Proyecto insertado exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al insertar el proyecto.";
                }

                return RedirectToAction("VistaProyecto");
            }
            else
            {
                TempData["ErrorMessage"] = "Por favor, complete todos los campos obligatorios correctamente.";
                return View("VistaProyecto", proyecto); // Vuelve a la vista de proyectos con los datos y errores.
            }
        }

    }
}