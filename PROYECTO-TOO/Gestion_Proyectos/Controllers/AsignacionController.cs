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
    public class AsignacionController : Controller
    {
        [ValidarSesion]
        // GET: Asignacion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MisTareas()
        {
            // Obtén el usuario actual desde la sesión
            Usuario user = Session["usuario"] as Usuario;
            GestionAsignaciones gestionAsignaciones = new GestionAsignaciones();
            if (user != null)
            {
                List<Tarea> tareasAsignadas = gestionAsignaciones.ObtenerTareasAsignadas(user.id);

                return View(tareasAsignadas);
            }
            else
            {
                // El usuario no ha iniciado sesión, redirige o muestra un mensaje de error
                return RedirectToAction("Index","Home"); // O una vista de error
            }
        }
        [HttpGet]
        public JsonResult ListarTareasAsignadas()
        {
            if (Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                List<Tarea> listaTareas = ObtenerAsignadas(usuario.id);

                return Json(new { data = listaTareas }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Si el usuario no ha iniciado sesión, retorna un JSON vacío o un mensaje de error.
                return Json(new { data = new List<Tarea>() }, JsonRequestBehavior.AllowGet);
            }
        }
        private List<Tarea> ObtenerAsignadas(int usuarioId)
        {
            return new GestionAsignaciones().ObtenerTareasAsignadas(usuarioId);
        }


        public JsonResult BuscarUsuarioPorCorreo(string correoUsuario)
        {
            GestionUsuarios gestionUsuario = new GestionUsuarios();
            Usuario usuario = gestionUsuario.ObtenerUsuarioPorCorreo(correoUsuario);

            if (usuario != null)
            {
                // Usuario encontrado, puedes devolver los datos del usuario como JSON
                return Json(new { success = true, nombre = usuario.nombre, apellido = usuario.apellido, correo = usuario.correo }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                // Usuario no encontrado, devolver un mensaje de error
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AsignarTarea(int idTarea, string usuarioCorreo)
        {
            try
            {
                // Primero, verifica si el usuario con el correo proporcionado existe en la base de datos
                GestionUsuarios gestionUsuario = new GestionUsuarios();
                Usuario usuario = gestionUsuario.ObtenerUsuarioPorCorreo(usuarioCorreo);


                // Si el usuario existe, puedes realizar la asignación de tarea
                GestionAsignaciones gestionAsignaciones = new GestionAsignaciones();
                bool asignacionExistente = gestionAsignaciones.ExisteAsignacion(idTarea);

                if (asignacionExistente)
                {
                    // La tarea ya está asignada al usuario, devuelve un mensaje de error
                    return Json(new { success = false, message = "La tarea ya está asignada." }, JsonRequestBehavior.AllowGet);
                }

                bool asignacionExitosa = gestionAsignaciones.newAsignacion(usuario.id, idTarea);

                if (asignacionExitosa)
                {
                    // Tarea asignada con éxito
                    return Json(new { success = true, message = "Tarea asignada con éxito." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    // Ocurrió un error durante la asignación, muestra un mensaje de error
                    return Json(new { success = false, message = "Error al asignar la tarea." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return Json(new { success = false, message = "Error: " + ex.Message, stackTrace = ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}