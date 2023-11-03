using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestion_Proyectos.Controllers
{
    public class ActividadController : Controller
    {
        // GET: Actividad
        public ActionResult VistaActividad(int? idProyecto)

        {
            if (idProyecto.HasValue)
            {
                // Obtener objetivos relacionados con el proyecto según el idProyecto
                List<Actividad> listaActividad = new GestionActividades().GetActividadesPorProyecto(idProyecto.Value);
                return View(listaActividad);
            }
            else
            {
                // Si no se proporciona un valor para idProyecto, se generará una excepción y se redirigirá a la vista de error personalizada.
                return RedirectToAction("Index", "Home");
            }

        }
        //
        [HttpGet]
        public JsonResult ListarActividadesPorProyecto(int idProyecto)
        {
            List<Actividad> listaActividad = new List<Actividad>();
            listaActividad = new GestionActividades().GetActividadesPorProyecto(idProyecto);
            return Json(new { data = listaActividad }, JsonRequestBehavior.AllowGet);
        }
        //
        [HttpPost]
        public JsonResult AgregarActividad(Actividad Act)
        {
            if (ModelState.IsValid)
            {
                GestionActividades gestionActividades = new GestionActividades();

                bool insercionExitosa = gestionActividades.newActividad(Act);

                if (insercionExitosa)
                {
                    return Json(new { success = true, message = "Actividad insertado exitosamente" });
                }
                else
                {
                    return Json(new { success = false, message = "Error al insertar la actividad" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Por favor, complete todos los campos obligatorios correctamente" });
            }
        }
        //
        [HttpPost]
        public JsonResult EditarActividad(Actividad act)
        {
            if (ModelState.IsValid)
            {
                GestionActividades gestionActividades = new GestionActividades();

                bool actualizacionExitosa = gestionActividades.EditActividad(act);

                if (actualizacionExitosa)
                {
                    return Json(new { success = true, message = "Actividad actualizada exitosamente" });
                }
                else
                {
                    return Json(new { success = false, message = "Error al actualizar la actividad" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Por favor, complete todos los campos obligatorios correctamente" });
            }
        }
        //
        [HttpPost]
        public JsonResult BorrarActividad(int idActividad)
        {
            GestionActividades gestionActividades = new GestionActividades();

            bool eliminacionExitosa = gestionActividades.EliminarActividad(idActividad);

            if (eliminacionExitosa)
            {
                return Json(new { success = true, message = "Actividad eliminada exitosamente" });
            }
            else
            {
                return Json(new { success = false, message = "Error al eliminar la actividad" });
            }
        }

    }
}
