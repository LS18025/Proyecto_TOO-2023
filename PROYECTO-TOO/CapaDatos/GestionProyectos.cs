using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class GestionProyectos
    {
        public List<Proyecto> GetProyectos()
        {
            List<Proyecto> listaProyectos = new List<Proyecto>();

            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "SELECT IDPROYECTO, NOMBREPROYECTO, DESCRIPCION, FECHAINI, FECHAFIN FROM PROYECTO";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    miConexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listaProyectos.Add(
                                new Proyecto()
                                {
                                    idProyecto = Convert.ToInt32(lector["IDPROYECTO"]),
                                    nombreProyecto = lector["NOMBREPROYECTO"].ToString(),
                                    descripcion = lector["DESCRIPCION"].ToString(),
                                    fechaIni = Convert.ToDateTime(lector["FECHAINI"]),
                                    fechaFin = Convert.ToDateTime(lector["FECHAFIN"])
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones, puedes registrar errores o devolver una lista vacía según tu lógica.
                listaProyectos = new List<Proyecto>();
            }

            return listaProyectos;
        }
        public bool newProyecto(Proyecto proyecto)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "INSERT INTO PROYECTO (NOMBREPROYECTO, DESCRIPCION, FECHAINI, FECHAFIN) " +
                                       "VALUES (@nombreProyecto, @descripcion, @fechaIni, @fechaFin)";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    // Asume que proyecto.idUsuario es una instancia de Usuario
                    comando.Parameters.AddWithValue("@nombreProyecto", proyecto.nombreProyecto);
                    comando.Parameters.AddWithValue("@descripcion", proyecto.descripcion);
                    comando.Parameters.AddWithValue("@fechaIni", proyecto.fechaIni);
                    comando.Parameters.AddWithValue("@fechaFin", proyecto.fechaFin);

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
        public bool EditProyecto(Proyecto proyecto)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "UPDATE PROYECTO SET NOMBREPROYECTO = @nombreProyecto, DESCRIPCION = @descripcion, FECHAINI = @fechaIni, FECHAFIN = @fechaFin WHERE IDPROYECTO = @idProyecto";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@idProyecto", proyecto.idProyecto); // Asegúrate de incluir el ID del proyecto a editar
                    comando.Parameters.AddWithValue("@nombreProyecto", proyecto.nombreProyecto);
                    comando.Parameters.AddWithValue("@descripcion", proyecto.descripcion);
                    comando.Parameters.AddWithValue("@fechaIni", proyecto.fechaIni);
                    comando.Parameters.AddWithValue("@fechaFin", proyecto.fechaFin);

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
        public bool EliminarProyecto(int idProyecto)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "DELETE FROM PROYECTO WHERE IDPROYECTO = @idProyecto";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@idProyecto", idProyecto);

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



    }
}
