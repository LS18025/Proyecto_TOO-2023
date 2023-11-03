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
    public class GestionActividades
    {
        public List<Actividad> GetActividadesPorProyecto(int idProyecto)
        {
            List<Actividad> listaActividad = new List<Actividad>();

            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "SELECT IDACTIVIDAD,NOMBREACT, DESCRIPCIONACT FROM ACTIVIDAD WHERE IDPROYECTO = @idProyecto";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;
                    comando.Parameters.AddWithValue("@idProyecto", idProyecto);

                    miConexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            listaActividad.Add(
                                new Actividad()
                                {
                                    idActividad = Convert.ToInt32(lector["IDACTIVIDAD"]),
                                    nombreAct = lector["NOMBREACT"].ToString(),
                                    descripcionAct = lector["DESCRIPCIONACT"].ToString(),
                                }
                            );
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Manejo de excepciones, puedes registrar errores o devolver una lista vacía según tu lógica.
                listaActividad = new List<Actividad>();
            }

            return listaActividad;
        }

        public bool newActividad(Actividad act)
        {

            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "INSERT INTO ACTIVIDAD (IDPROYECTO, NOMBREACT, DESCRIPCIONACT) " +
                                       "VALUES (@idProyecto, @nombreAct, @descripcionAct)";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;
                    comando.Parameters.AddWithValue("@idProyecto", act.idProyect.idProyecto);
                    comando.Parameters.AddWithValue("@nombreAct", act.nombreAct);
                    comando.Parameters.AddWithValue("@descripcionAct", act.descripcionAct);

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

        public bool EditActividad(Actividad act)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "UPDATE ACTIVIDAD SET NOMBREACT = @nombreAct, DESCRIPCIONACT = @descripcionAct,  WHERE IDACTIVIDAD = @idActividad";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@idActividad", act.idActividad); // Asegúrate de incluir el ID del proyecto a editar
                    comando.Parameters.AddWithValue("@nombreAct", act.nombreAct);
                    comando.Parameters.AddWithValue("@descripcionAct", act.descripcionAct);


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
        public bool EliminarActividad(int idActividad)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "DELETE FROM ACTIVIDAD WHERE IDACTIVIDAD = @idActividad";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@idActividad", idActividad);

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
