using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

//clase creada para poder acceder a la base de datos
namespace CapaDatos
{
    public class GestionActividades
    {
        //
        public List<Actividad> GetActividades()
        {
            List<Actividad> lista = new List<Actividad>();

            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "SELECT IDACTIVIDAD, IDPROYECTO, NOMBREACT, DESCRIPCIONACT FROM ACTIVIDAD";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    miConexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            lista.Add(
                                    new Actividad()
                                    {
                                        idActividad = Convert.ToInt32(lector["ID"]),
                                        //pongo este asi porque de momento no se como relacionar el proyecto con esta cosa
                                        idProyect = new Proyecto(),
                                        nombreAct = lector["NOMBRE"].ToString(),
                                        descripcionAct = lector["NOMBRE"].ToString(),
                                        
                                    }
                                );
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<Actividad>();

            }

            return lista;
        }
        public bool newActividad(Actividad act)
        {
            try
            {
                using (SqlConnection miConexion = new SqlConnection(Conexion.cn))
                {
                    string sentencia = "INSERT INTO ACTIVIDAD (IDPROYECTO, NOMBREACT, DESCRIPCIONACT) " +
                                       "VALUES (@nombreAct, @DescripcionAct, @Proyecto)";
                    SqlCommand comando = new SqlCommand(sentencia, miConexion);
                    comando.CommandType = CommandType.Text;

                    comando.Parameters.AddWithValue("@nombreAct", act.nombreAct);
                    comando.Parameters.AddWithValue("@descripcionAct", act.descripcionAct);
                    comando.Parameters.AddWithValue("@proyectoAct", act.idProyect);

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

    }

}
}
