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
        public List<Tarea> Listar()
        {
            List<Tarea> lista = new List<Tarea>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select IDTAREA,IDACTIVIDAD,NOMBRETAREA,DESCRIPCIONT,FECHAINIT,FECHAFINT from TAREA";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

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
                                      idAct = Convert.ToInt32(dr["IDACTIVIDAD"]),
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

        public int RegistrarTarea(Tarea obj, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarTarea", oconexion);
                    cmd.Parameters.AddWithValue("IDACTIVIDAD", obj.idAct);
                    cmd.Parameters.AddWithValue("NOMBRETAREA", obj.nombreTarea);
                    cmd.Parameters.AddWithValue("DESCRIPCIONT", obj.descripcionT);
                    cmd.Parameters.AddWithValue("FECHAINIT", obj.fechaIniT);
                    cmd.Parameters.AddWithValue("FECHAFINT", obj.fechaFinT);
                    cmd.Parameters.Add("Resultado",SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        public bool EditarTarea(Tarea obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarTarea", oconexion);
                    cmd.Parameters.AddWithValue("IDTAREA", obj.idTarea);
                    cmd.Parameters.AddWithValue("IDACTIVIDAD", obj.idAct);
                    cmd.Parameters.AddWithValue("NOMBRETAREA", obj.nombreTarea);
                    cmd.Parameters.AddWithValue("DESCRIPCIONT", obj.descripcionT);
                    cmd.Parameters.AddWithValue("FECHAINIT", obj.fechaIniT);
                    cmd.Parameters.AddWithValue("FECHAFINT", obj.fechaFinT);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }

        public bool EliminarTarea(int id,out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("delete top (1) from TAREA where IDTAREA = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch(Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
