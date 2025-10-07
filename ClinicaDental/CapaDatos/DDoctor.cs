using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class DDoctor
    {
        #region "PATRON SINGLETON"
        public static DDoctor _instancia = null;

        private DDoctor()
        {

        }

        public static DDoctor GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DDoctor();
            }
            return _instancia;
        }
        #endregion

        public Respuesta<bool> RegistrarDoctores(EDoctor oDoctor)
        {
            try
            {
                bool respuesta = false;
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarDoctores", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NroCi", oDoctor.NroCi);
                        cmd.Parameters.AddWithValue("@Nombres", oDoctor.Nombres);
                        cmd.Parameters.AddWithValue("@Apellidos", oDoctor.Apellidos);
                        cmd.Parameters.AddWithValue("@Telefono", oDoctor.Telefono);
                        cmd.Parameters.AddWithValue("@Correo", oDoctor.Correo);
                        cmd.Parameters.AddWithValue("@ClaveHash", oDoctor.ClaveHash);
                        cmd.Parameters.AddWithValue("@Token", oDoctor.Token);

                        SqlParameter outputParam = new SqlParameter("@Resultado", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        respuesta = Convert.ToBoolean(outputParam.Value);
                    }
                }
                return new Respuesta<bool>
                {
                    Estado = respuesta,
                    Mensaje = respuesta ? "Se registro correctamente" : "Error al registrar el nro de ci o correo ya existen"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Mensaje = "Ocurrió un error: " + ex.Message };
            }
        }

        public Respuesta<bool> EditarDoctores(EDoctor oDoctor)
        {
            try
            {
                bool respuesta = false;
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ModificarDoctores", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdDoctor", oDoctor.IdDoctor);
                        cmd.Parameters.AddWithValue("@NroCi", oDoctor.NroCi);
                        cmd.Parameters.AddWithValue("@Nombres", oDoctor.Nombres);
                        cmd.Parameters.AddWithValue("@Apellidos", oDoctor.Apellidos);
                        cmd.Parameters.AddWithValue("@Telefono", oDoctor.Telefono);
                        cmd.Parameters.AddWithValue("@Correo", oDoctor.Correo);
                        cmd.Parameters.AddWithValue("@ClaveHash", oDoctor.ClaveHash);
                        cmd.Parameters.AddWithValue("@Estado", oDoctor.Estado);

                        SqlParameter outputParam = new SqlParameter("@Resultado", SqlDbType.Bit)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        respuesta = Convert.ToBoolean(outputParam.Value);
                    }
                }
                return new Respuesta<bool>
                {
                    Estado = respuesta,
                    Mensaje = respuesta ? "Se actualizo correctamente" : "Error al actualizar nro de ci o correo ya existen"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Mensaje = "Ocurrió un error: " + ex.Message };
            }
        }

        public Respuesta<List<EDoctor>> ListaDoctores()
        {
            try
            {
                List<EDoctor> rptLista = new List<EDoctor>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ObtenerDoctores", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EDoctor()
                                {
                                    IdDoctor = Convert.ToInt32(dr["IdDoctor"]),
                                    NroCi = dr["NroCi"].ToString(),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Correo = dr["Correo"].ToString(),
                                    ClaveHash = dr["ClaveHash"].ToString(),
                                    Token = dr["Token"].ToString(),
                                    Estado = Convert.ToBoolean(dr["Estado"]),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]).ToString("dd/MM/yyyy"),
                                    VFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString())
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EDoctor>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Doctores obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EDoctor>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
