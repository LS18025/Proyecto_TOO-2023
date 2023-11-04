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
    [ValidarSesion]
    public class ObjetivoController : Controller
    {
        // GET: Objetivo
        public ActionResult VistaObjetivo(int? idProyecto)
        {
            if (idProyecto.HasValue)
            {
                // Obtener objetivos relacionados con el proyecto según el idProyecto
                List<Objetivo> listaObjetivos = new GestioObjetivos().GetObjetivosPorProyecto(idProyecto.Value);
                return View(listaObjetivos);
            }
            else
            {
                // Si no se proporciona un valor para idProyecto, se generará una excepción y se redirigirá a la vista de error personalizada.
                return RedirectToAction("Index","Home");
            }
        }

        [HttpGet]
        public JsonResult ListarObjetivos()
        {
            List<Objetivo> listaObjetivos = new List<Objetivo>();
            listaObjetivos = new GestioObjetivos().GetObjetivos();
            return Json(new { data = listaObjetivos }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AgregarObjetivo(Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                GestioObjetivos gestionObjetivos = new GestioObjetivos();

                bool insercionExitosa = gestionObjetivos.newObjetivo(objetivo);

                if (insercionExitosa)
                {
                    return Json(new { success = true, message = "Objetivo insertado exitosamente" });
                }
                else
                {
                    return Json(new { success = false, message = "Error al insertar el Objetivo" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Por favor, complete todos los campos obligatorios correctamente" });
            }
        }
        [HttpPost]
        public JsonResult EditarObjetivo(Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                GestioObjetivos gestionObjetivos = new GestioObjetivos();

                bool actualizacionExitosa = gestionObjetivos.EditObjetivo(objetivo);

                if (actualizacionExitosa)
                {
                    return Json(new { success = true, message = "Objetivo actualizado exitosamente" });
                }
                else
                {
                    return Json(new { success = false, message = "Error al actualizar el Objetivo" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Por favor, complete todos los campos obligatorios correctamente" });
            }
        }
        [HttpPost]
        public JsonResult BorrarObjetivo(int idObjetivo)
        {
            GestioObjetivos gestionObjetivos = new GestioObjetivos();

            bool eliminacionExitosa = gestionObjetivos.EliminarObjetivo(idObjetivo);

            if (eliminacionExitosa)
            {
                return Json(new { success = true, message = "Objetivo eliminado exitosamente" });
            }
            else
            {
                return Json(new { success = false, message = "Error al eliminar el Objetivo" });
            }
        }
        [HttpGet]
        public JsonResult ListarObjetivosPorProyecto(int idProyecto)
        {
            List<Objetivo> listaObjetivos = new List<Objetivo>();
            listaObjetivos = new GestioObjetivos().GetObjetivosPorProyecto(idProyecto);
            return Json(new { data = listaObjetivos }, JsonRequestBehavior.AllowGet);
        }

    }
}