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
        public List<Usuario> GetUsuarios()
        {
            List<Usuario> lista= new List<Usuario>();

            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn)) {
                    string sentencia = "SELECT ID, NOMBRE, APELLIDO, CORREO FROM USUARIO";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    miConexion.Open();

                    using (SqlDataReader lector= comando.ExecuteReader()) {
                        while (lector.Read())
                        {
                            lista.Add(
                                    new Usuario()
                                    {
                                        id = Convert.ToInt32(lector["ID"]),
                                        nombre = lector["NOMBRE"].ToString(),
                                        apellido = lector["APELLIDO"].ToString(),
                                        correo = lector["CORREO"].ToString(),
                                        
                                    }
                                );
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Usuario>();
                
            }

            return lista;
        }
        public bool newUsuario(Usuario usuario)
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
        public Usuario ValidarCredenciales(string correo, string contrasena)
        {
            Usuario usuario = null;

            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "SELECT ID, NOMBRE, APELLIDO, CORREO FROM USUARIO WHERE CORREO = @correo AND CONTRASENA = @contrasena";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@correo", correo);
                    comando.Parameters.AddWithValue("@contrasena", contrasena);

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
                // Manejar cualquier excepción o error aquí.
            }

            return usuario;
        }




    }
}
