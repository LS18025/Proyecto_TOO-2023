using CapaDatos;
using CapaEntidad;
using Gestion_Proyectos.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestion_Proyectos.Controllers
{
    public class TareaController : Controller
    {
        [ValidarSesion]
        // GET: Tarea
        public ActionResult VistaTarea(int? idActividad)
        {
            if (idActividad.HasValue)
            {
                // Obtener objetivos relacionados con el proyecto según el idProyecto
                List<Tarea> listaTarea = new GestionTareas().GetTareasPorActividad(idActividad.Value);
                return View(listaTarea);
            }
            else
            {
                // Si no se proporciona un valor para idProyecto, se generará una excepción y se redirigirá a la vista de error personalizada.
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public JsonResult ListarTareasPorActividad(int IDACTIVIDAD)
        {
            List<Tarea> listaTarea = new List<Tarea>();
            listaTarea = new GestionTareas().GetTareasPorActividad(IDACTIVIDAD);
            return Json(new { data = listaTarea }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AgregarTarea(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                GestionTareas gestionTareas = new GestionTareas();

                if (tarea.fechaFinT >= tarea.fechaIniT)
                {
                    bool insercionExitosa = gestionTareas.RegistrarTarea(tarea);

                    if (insercionExitosa)
                    {
                        return Json(new { success = true, message = "Tarea insertada exitosamente" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error al insertar la tarea" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "La fecha de finalización no puede ocurrir antes que la fecha de inicio" });
                }

            }
            else
            {
                return Json(new { success = false, message = "Por favor, complete todos los campos obligatorios correctamente" });
            }
        }

        [HttpPost]
        public JsonResult EditarTarea(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                if (tarea.fechaFinT >= tarea.fechaIniT)
                {
                    GestionTareas gestionTareas = new GestionTareas();

                    bool actualizacionExitosa = gestionTareas.EditarTarea(tarea);

                    if (actualizacionExitosa)
                    {
                        return Json(new { success = true, message = "Tarea actualizada exitosamente" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error al actualizar la tarea" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "La fecha de finalización no puede ocurrir antes que la fecha de inicio" });
                }

            }
            else
            {
                return Json(new { success = false, message = "Por favor, complete todos los campos obligatorios correctamente" });
            }
        }

        [HttpPost]
        public JsonResult EliminarTarea(int idTarea)
        {
            GestionTareas gestionTareas = new GestionTareas();

            bool eliminacionExitosa = gestionTareas.EliminarTarea(idTarea);

            if (eliminacionExitosa)
            {
                return Json(new { success = true, message = "Proyecto eliminado exitosamente" });
            }
            else
            {
                return Json(new { success = false, message = "Error al eliminar el proyecto" });
            }
        }
    }
}