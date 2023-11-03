using CapaDatos;
using CapaEntidad;
using Gestion_Proyectos.Permisos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gestion_Proyectos.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult VistaRegistro()
        {
            return View();
        }
        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Acceso"); // Redirige al usuario a la página de inicio de sesión
        }
        [HttpPost]
        public ActionResult RegistrarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.contrasena == usuario.confirmarContrasena)
                {
                    // Verificar si ya existe un usuario con el mismo correo
                    bool existeUsuario = new GestionUsuarios().ExisteUsuarioPorCorreo(usuario.correo);

                    if (existeUsuario)
                    {
                        TempData["ErrorMessage"] = "Ya existe un usuario con ese correo.";
                        return View("VistaRegistro", usuario);
                    }
                    else
                    {
                        GestionUsuarios gestionUsuarios = new GestionUsuarios();

                        bool registroExitoso = gestionUsuarios.RegistrarUsuario(usuario);

                        if (registroExitoso)
                        {
                            TempData["SuccessMessage"] = "Usuario registrado exitosamente.";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Error al registrar el usuario.";
                        }

                        return RedirectToAction("Login", "Acceso");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Las contraseñas no coinciden";
                    return View("VistaRegistro", usuario); // Vuelve a la vista de registro con los datos y errores.
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Por favor, complete todos los campos obligatorios correctamente.";
                return View("VistaRegistro", usuario); // Vuelve a la vista de registro con los datos y errores.
            }
        }
        [HttpPost]
        public ActionResult Iniciar(Usuario user)
        {
            using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
            {
                SqlCommand comando = new SqlCommand("SP_VALIDARUSUARIO", miConexion);

                comando.Parameters.AddWithValue("Correo", user.correo);
                comando.Parameters.AddWithValue("Contra", user.contrasena);

                comando.CommandType = CommandType.StoredProcedure;
                miConexion.Open();

                user.id = Convert.ToInt32(comando.ExecuteScalar().ToString());

                if (user.id != 0)
                {
                    string sentencia = "SELECT NOMBRE, APELLIDO FROM USUARIO WHERE ID= @id";
                    using (SqlCommand obtenerInfoUsuario = new SqlCommand(sentencia, miConexion))
                    {
                        obtenerInfoUsuario.Parameters.AddWithValue("@id", user.id);
                        obtenerInfoUsuario.CommandType = CommandType.Text;
                        
                        SqlDataReader reader = obtenerInfoUsuario.ExecuteReader();
                        if (reader.Read())
                        {
                            Session["usuario"] = user;
                            Session["nombre"] = reader["nombre"].ToString();
                            Session["apellido"] = reader["apellido"].ToString();
                            Session["correo"] = user.correo;
                        }
                    }


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Mensaje"] = "Usuario no encontrado";
                    return View(user);
                }

            }

        }
    }
}