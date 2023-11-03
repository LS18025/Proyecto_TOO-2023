using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
    public class GestionUsuarios
    {
        
        
        public bool EditarUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "UPDATE USUARIO SET NOMBRE = @nombre, APELLIDO = @apellido, CORREO = @correo WHERE ID = @id";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@id", usuario.id);
                    comando.Parameters.AddWithValue("@nombre", usuario.nombre);
                    comando.Parameters.AddWithValue("@apellido", usuario.apellido);
                    comando.Parameters.AddWithValue("@correo", usuario.correo);

                    miConexion.Open();

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteUsuario(int id)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "DELETE FROM USUARIO WHERE ID = @id";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@id", id);

                    miConexion.Open();

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Usuario ObtenerUsuarioPorID(int id)
        {
            Usuario usuario = null;

            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "SELECT ID, NOMBRE, APELLIDO, CORREO FROM USUARIO WHERE ID = @id";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@id", id);

                    miConexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (lector.Read())
                        {
                            usuario = new Usuario
                            {
                                id = Convert.ToInt32(lector["ID"]),
                                nombre = lector["NOMBRE"].ToString(),
                                apellido = lector["APELLIDO"].ToString(),
                                correo = lector["CORREO"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Manejar cualquier excepción o error aquí, por ejemplo, registrar un mensaje de error.
                // No necesitas devolver un valor en caso de error, ya que el usuario será nulo por defecto.
            }

            return usuario;
        }
        public bool RegistrarUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "INSERT INTO USUARIO (NOMBRE, APELLIDO, CORREO, CONTRASENA) " +
                                       "VALUES (@nombre, @apellido, @correo, @contrasena)";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@nombre", usuario.nombre);
                    comando.Parameters.AddWithValue("@apellido", usuario.apellido);
                    comando.Parameters.AddWithValue("@correo", usuario.correo);
                    comando.Parameters.AddWithValue("@contrasena", usuario.contrasena);

                    miConexion.Open();

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ExisteUsuarioPorCorreo(string correo)
        {
            using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
            {
                string sentencia = "SELECT 1 FROM USUARIO WHERE CORREO = @correo";
                SqlCommand comando = new SqlCommand(sentencia, miConexion);
                comando.CommandType = CommandType.Text;
                comando.Parameters.AddWithValue("@correo", correo);

                miConexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    // Si el lector tiene filas, significa que ya existe un usuario con ese correo
                    return lector.HasRows;
                }
            }
        }
        public int ValidarUsuario(string correo, string contrasena)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "SELECT ID FROM USUARIO WHERE CORREO = @correo AND CONTRASENA = @contrasena";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@correo", correo);
                    comando.Parameters.AddWithValue("@contrasena", contrasena);

                    miConexion.Open();

                    var result = comando.ExecuteScalar(); // Ejecuta la consulta y obtiene el resultado (ID del usuario)

                    if (result != null)
                    {
                        return (int)result; // Devuelve el ID del usuario si la validación fue exitosa
                    }
                }
            }
            catch (Exception)
            {
                // En caso de error, puedes manejar la excepción o simplemente devolver un valor predeterminado
            }

            return 0; // Devuelve 0 si la validación falla o si ocurre una excepción
        }







    }
}
