using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.CodeDom;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class GestionTareas
    {
        public List<Tarea> ListarTareasPorActividad(int IDACTIVIDAD)
        {
            List<Tarea> lista = new List<Tarea>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select IDTAREA,NOMBRETAREA,DESCRIPCIONT,FECHAINIT,FECHAFINT from TAREA where IDACTIVIDAD = @IDACTIVIDAD";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IDACTIVIDAD", IDACTIVIDAD);
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add
                                (
                                  new Tarea()
                                  {
                                      idTarea = Convert.ToInt32(dr["IDTAREA"]),
                                      nombreTarea = dr["NOMBRETAREA"].ToString(),
                                      descripcionT = dr["DESCRIPCIONT"].ToString(),
                                      fechaIniT = Convert.ToDateTime(dr["FECHAINIT"]),
                                      fechaFinT = Convert.ToDateTime(dr["FECHAFINT"])

                                  }
                                );
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Tarea>();
            }
            return lista;
        }

        public bool RegistrarTarea(Tarea tarea)
        {
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "INSERT INTO PROYECTO (IDACTIVIDAD, NOMBRETAREA, DESCRIPCIONT, FECHAINIT, FECHAFINT) " +
                                       "VALUES (@idAct, @nombreTarea, @descripcionT, @fechaIniT, @fechaFinT)";
                    SqlCommand cmd = new SqlCommand(sentencia, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idAct", tarea.idAct);
                    cmd.Parameters.AddWithValue("@nombreTarea", tarea.nombreTarea);
                    cmd.Parameters.AddWithValue("@descripcionT", tarea.descripcionT);
                    cmd.Parameters.AddWithValue("@fechaIniT", tarea.fechaIniT);
                    cmd.Parameters.AddWithValue("@fechaFinT", tarea.fechaFinT);

                    oconexion.Open();

                    int filautilizada = cmd.ExecuteNonQuery();

                    return filautilizada > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditarTarea(Tarea tarea)
        {
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "UPDATE PROYECTO SET NOMBRETAREA = @nombreTarea, DESCRIPCIONT = @descripcionT, FECHAINIT = @fechaIniT, FECHAFINT = @fechaFinT WHERE IDACTIVIDAD = @idAct";
                    SqlCommand cmd = new SqlCommand(sentencia, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idTarea", tarea.idTarea);
                    cmd.Parameters.AddWithValue("@nombreTarea", tarea.nombreTarea);
                    cmd.Parameters.AddWithValue("@descripcionT", tarea.descripcionT);
                    cmd.Parameters.AddWithValue("@fechaIniT", tarea.fechaIniT);
                    cmd.Parameters.AddWithValue("@fechaFinT", tarea.fechaFinT);

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    int filautilizada = cmd.ExecuteNonQuery();

                    return filautilizada > 0;
                }
            }
            catch (Exception )
            {
                return false;
            }
        }

        public bool EliminarTarea(int idTarea)
        {
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "DELETE FROM PROYECTO WHERE IDTAREA = @idTarea";
                    SqlCommand cmd = new SqlCommand(sentencia, oconexion);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@idTarea", idTarea);

                    oconexion.Open();
                    int filautilizada = cmd.ExecuteNonQuery();

                    return filautilizada > 0;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
