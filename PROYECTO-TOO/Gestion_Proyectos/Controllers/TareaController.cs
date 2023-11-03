using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestion_Proyectos.Controllers
{
    public class TareaController : Controller
    {
        // GET: Tarea
        public ActionResult VistaTarea()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTareasPorActividad(int IDACTIVIDAD)
        {
            List<Tarea> listaTarea = new List<Tarea>();
            listaTarea = new GestionTareas().ListarTareasPorActividad(IDACTIVIDAD);
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