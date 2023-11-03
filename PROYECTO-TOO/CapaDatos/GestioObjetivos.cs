using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class GestioObjetivos
    {
        public List<Objetivo> GetObjetivos()
        {
            List<Objetivo> listaObjetivos = new List<Objetivo>();

            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "SELECT IDOBJETIVO, DESCRIPCIONOBJ FROM OBJETIVO";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;
                    miConexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listaObjetivos.Add(
                                new Objetivo()
                                {
                                    idObjetivo = Convert.ToInt32(lector["IDOBJETIVO"]),
                                    descripcionObj = lector["DESCRIPCIONOBJ"].ToString(),
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones, puedes registrar errores o devolver una lista vacía según tu lógica.
                listaObjetivos = new List<Objetivo>();
            }

            return listaObjetivos;
        }
        public bool newObjetivo(Objetivo objetivo)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "INSERT INTO OBJETIVO (IDPROYECTO, DESCRIPCIONOBJ) " +
                                       "VALUES (@idProyecto, @descripcionObj)";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    // Asume que proyecto.idUsuario es una instancia de Usuario
                    comando.Parameters.AddWithValue("@idProyecto", objetivo.idPro.idProyecto);
                    comando.Parameters.AddWithValue("@descripcionObj", objetivo.descripcionObj);

                    miConexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false; // Manejo de excepciones, puedes registrar errores si es necesario.
            }
        }
        public bool EditObjetivo(Objetivo objetivo)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "UPDATE OBJETIVO SET DESCRIPCIONOBJ = @descripcionObj WHERE IDOBJETIVO = @idObjetivo";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@idObjetivo", objetivo.idObjetivo); // Asegúrate de incluir el ID del proyecto a editar

                    comando.Parameters.AddWithValue("@descripcionObj", objetivo.descripcionObj);


                    miConexion.Open();

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false; // Manejo de excepciones, puedes registrar errores si es necesario.
            }
        }
        public bool EliminarObjetivo(int idObjetivo)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "DELETE FROM OBJETIVO WHERE IDOBJETIVO = @idObjetivo";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@idObjetivo", idObjetivo);

                    miConexion.Open();

                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false; // Manejo de excepciones, puedes registrar errores si es necesario.
            }
        }
        public List<Objetivo> GetObjetivosPorProyecto(int idProyecto)
        {
            List<Objetivo> listaObjetivos = new List<Objetivo>();

            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "SELECT IDOBJETIVO, DESCRIPCIONOBJ FROM OBJETIVO WHERE IDPROYECTO = @idProyecto";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;
                    comando.Parameters.AddWithValue("@idProyecto", idProyecto);

                    miConexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listaObjetivos.Add(
                                new Objetivo()
                                {
                                    idObjetivo = Convert.ToInt32(lector["IDOBJETIVO"]),
                                    descripcionObj = lector["DESCRIPCIONOBJ"].ToString(),
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones, puedes registrar errores o devolver una lista vacía según tu lógica.
                listaObjetivos = new List<Objetivo>();
            }

            return listaObjetivos;
        }

    }
}
