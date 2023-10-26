﻿using System;
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
                                        apellido = lector["NOMBRE"].ToString(),
                                        correo = lector["NOMBRE"].ToString(),
                                        contrasena = lector["NOMBRE"].ToString()
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
    }
}
