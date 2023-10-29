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
        public ActionResult VistaUsuario()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult EditarUsuario(int id)
        {
            Usuario usuario = new GestionUsuarios().ObtenerUsuarioPorID(id);

            if (usuario != null)
            {
                return View(usuario);
            }
            else
            {
                // Manejar el caso en el que no se encuentre el usuario
                // Por ejemplo, puedes redirigir a una vista de error.
                return RedirectToAction("UsuarioNoEncontrado");
            }
        }
        public ActionResult UsuarioNoEncontrado()
        {
            // Puedes mostrar una vista de error o redirigir a otra página aquí.
            return View("Error");
        }

        [HttpPost]
        public ActionResult InsertarUsuario(string nombre, string apellido, string correo, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                ViewBag.Mensaje = "Por favor, complete todos los campos obligatorios.";
                return View("VistaUsuario");
            }

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

            return View("VistaUsuario");
        }
        [HttpGet]
        public JsonResult ListarUsuarios()
        {

            List<Usuario> listaUsuarios = new List<Usuario>();
            listaUsuarios = new GestionUsuarios().GetUsuarios();
            return Json(new { data = listaUsuarios }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult BorrarUsuario(int id)
        {
            GestionUsuarios gestionUsuarios = new GestionUsuarios(); // Crear una instancia de GestionUsuarios
            bool eliminacionExitosa = gestionUsuarios.DeleteUsuario(id);

            if (eliminacionExitosa)
            {
                TempData["SuccessMessage"] = "Usuario eliminado exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error al eliminar el usuario.";
            }

            return RedirectToAction("VistaUsuario");
        }

        [HttpPost]
        public ActionResult GuardarEdicionUsuario(Usuario usuario)
        {
            GestionUsuarios gestionUsuarios = new GestionUsuarios(); // Crear una instancia de GestionUsuarios

            if (ModelState.IsValid)
            {
                bool edicionExitosa = gestionUsuarios.EditarUsuario(usuario);

                if (edicionExitosa)
                {
                    TempData["SuccessMessage"] = "Usuario actualizado exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al actualizar el usuario.";
                }

                return RedirectToAction("VistaUsuario");

            }
            else
            {
                TempData["ErrorMessage"] = "Por favor, complete todos los campos obligatorios correctamente.";
                return View("EditarUsuario", usuario); // Vuelve a la vista de edición con los datos y errores.
            }
        }
        


    }
}