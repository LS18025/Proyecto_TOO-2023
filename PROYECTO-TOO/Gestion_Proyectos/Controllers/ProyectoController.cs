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
        [HttpPost]
        public ActionResult InsertarUsuario(string nombre, string apellido, string correo, string contrasena)
        {
            Usuario nuevoUsuario = new Usuario
            {
                nombre = nombre,
                apellido = apellido,
                correo = correo,
                contrasena = contrasena
            };

            GestionUsuarios gestionUsuarios = new GestionUsuarios();
            bool insercionExitosa = gestionUsuarios.newUsuario(nuevoUsuario);

            if (insercionExitosa)
            {
                ViewBag.Mensaje = "Usuario insertado exitosamente.";
            }
            else
            {
                ViewBag.Mensaje = "Error al insertar el usuario.";
            }

            return View("VistaProyecto");
        }
    }
}