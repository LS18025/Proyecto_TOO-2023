using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class GestionAsignaciones
    {
        public bool newAsignacion(int idUsuario, int idTarea)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "INSERT INTO ASIGNACION_TAREA (ID, IDTAREA) " +
                                       "VALUES (@idUsuario, @idTarea)";
                    SqlCommand cmd = new SqlCommand(sentencia, conexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@idTarea", idTarea);

                    conexion.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ExisteAsignacion(int idTarea)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion.cn))
            {
                string consulta = "SELECT COUNT(*) FROM ASIGNACION_TAREA WHERE IDTAREA = @idTarea";
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                cmd.Parameters.AddWithValue("@idTarea", idTarea);

                conexion.Open();

                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
        }
        public List<Tarea> ObtenerTareasAsignadas(int userId)
        {
            List<Tarea> tareas = new List<Tarea>();

            using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
            {
                

                // Realiza una consulta para obtener las tareas asignadas al usuario actual
                string query = "SELECT T.* FROM TAREA AS T " +
                    "INNER JOIN ASIGNACION_TAREA AS AT ON T.IDTAREA = AT.IDTAREA " +
                    "WHERE AT.ID = @userId";

                miConexion.Open();
                using (SqlCommand comando = new SqlCommand(query, miConexion))
                {
                    comando.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tarea tarea = new Tarea
                            {
                                idTarea = Convert.ToInt32(reader["idTarea"]),
                                nombreTarea = reader["NOMBRETAREA"].ToString(),
                                descripcionT = reader["DESCRIPCIONT"].ToString(),
                                fechaIniT = Convert.ToDateTime(reader["FECHAINIT"]),
                                fechaFinT = Convert.ToDateTime(reader["FECHAFINT"])
                                // Completa aquí las demás propiedades de la tarea
                            };
                            tareas.Add(tarea);
                        }
                    }
                }
            }

            return tareas;
        }


    }
}
