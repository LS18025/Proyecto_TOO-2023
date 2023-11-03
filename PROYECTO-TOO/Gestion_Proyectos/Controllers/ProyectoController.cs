using CapaDatos;
using CapaEntidad;
using Gestion_Proyectos.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;


namespace Gestion_Proyectos.Controllers
{
    [ValidarSesion]
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

        /*
        public ActionResult VistaActividad()
        {
            return View();
        }
        */
        public ActionResult VistaTarea()
        {
            return View();
        }


        [HttpGet]
        public JsonResult ListarProyectos()
        {
            if (Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                List<Proyecto> listaProyectos = ObtenerProyectosPorUsuario(usuario.id);

                return Json(new { data = listaProyectos }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Si el usuario no ha iniciado sesión, retorna un JSON vacío o un mensaje de error.
                return Json(new { data = new List<Proyecto>() }, JsonRequestBehavior.AllowGet);
            }
        }

        private List<Proyecto> ObtenerProyectosPorUsuario(int usuarioId)
        {
            return new GestionProyectos().GetProyectos(usuarioId);
        }


        [HttpPost]
        public JsonResult AgregarProyecto(Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                GestionProyectos gestionProyectos = new GestionProyectos();

                // Obtén el usuario actual de la sesión
                Usuario usuarioActual = (Usuario)Session["usuario"];

                if (usuarioActual != null)
                {
                    // Asigna el ID del usuario al proyecto
                    if (proyecto.fechaFin >= proyecto.fechaIni)
                    {
                        bool insercionExitosa = gestionProyectos.newProyecto(proyecto, usuarioActual.id);

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
                        return Json(new { success = false, message = "La fecha de finalización no puede ser anterior a la fecha de inicio" });
                    }
                    
                }
                else
                {
                    return Json(new { success = false, message = "Usuario no encontrado en la sesión" });
                }
            }
            else
            {
                return Json(new { success = false, message = "Por favor, complete todos los campos obligatorios correctamente" });
            }
        }

        [HttpPost]
        public JsonResult EditarProyecto(Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                if (proyecto.fechaFin >= proyecto.fechaIni)
                {
                    GestionProyectos gestionProyectos = new GestionProyectos();

                    bool actualizacionExitosa = gestionProyectos.EditProyecto(proyecto);

                    if (actualizacionExitosa)
                    {
                        return Json(new { success = true, message = "Proyecto actualizado exitosamente" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Error al actualizar el proyecto" });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "La fecha de finalización no puede ser anterior a la fecha de inicio" });
                }
                
            }
            else
            {
                return Json(new { success = false, message = "Por favor, complete todos los campos obligatorios correctamente" });
            }
        }
        [HttpPost]
        public JsonResult BorrarProyecto(int idProyecto)
        {
            GestionProyectos gestionProyectos = new GestionProyectos();

            bool eliminacionExitosa = gestionProyectos.EliminarProyecto(idProyecto);

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