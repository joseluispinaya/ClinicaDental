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
    public class DCita
    {
        #region "PATRON SINGLETON"
        public static DCita _instancia = null;

        private DCita()
        {

        }

        public static DCita GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DCita();
            }
            return _instancia;
        }
        #endregion

        public Respuesta<bool> RegistrarCitas(ECita cita)
        {
            try
            {
                bool respuesta = false;
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarCitas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdPaciente", cita.IdPaciente);
                        cmd.Parameters.AddWithValue("@IdDoctor", cita.IdDoctor);
                        cmd.Parameters.AddWithValue("@FechaCita", cita.VFechaCita);
                        cmd.Parameters.AddWithValue("@HoraCita", cita.HoraCita);
                        cmd.Parameters.AddWithValue("@Detalles", cita.Detalles);

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
                    Mensaje = respuesta ? "La cita fue registrada con exito." : "Ocurrio un error al registrar Intente más tarde"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Mensaje = "Ocurrió un error: " + ex.Message };
            }
        }

        public Respuesta<List<ECita>> ObtenerCitasPorDoctor(int IdDoctor)
        {
            try
            {
                List<ECita> rptLista = new List<ECita>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_CitasPorDoctor", con))
                    {
                        comando.Parameters.AddWithValue("@IdDoctor", IdDoctor);
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ECita()
                                {
                                    IdCita = Convert.ToInt32(dr["IdCita"]),
                                    IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                    IdDoctor = Convert.ToInt32(dr["IdDoctor"]),
                                    FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString()).ToString("yyyy-MM-dd"), // Formato ISO 8601
                                    VFechaCita = Convert.ToDateTime(dr["FechaCita"].ToString()),
                                    HoraCita = dr["HoraCita"].ToString(),
                                    Detalles = dr["Detalles"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    Estado = dr["Estado"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString()).ToString("dd/MM/yyyy"),
                                    VFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString())
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ECita>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Citas obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ECita>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<List<ECita>> ListadeCitasFull()
        {
            try
            {
                List<ECita> rptLista = new List<ECita>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ListaCitasFull", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ECita()
                                {
                                    IdCita = Convert.ToInt32(dr["IdCita"]),
                                    IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                    IdDoctor = Convert.ToInt32(dr["IdDoctor"]),
                                    FechaCita = Convert.ToDateTime(dr["FechaCita"].ToString()).ToString("yyyy-MM-dd"), // Formato ISO 8601
                                    VFechaCita = Convert.ToDateTime(dr["FechaCita"].ToString()),
                                    HoraCita = dr["HoraCita"].ToString(),
                                    Detalles = dr["Detalles"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"]),
                                    Estado = dr["Estado"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]).ToString("dd/MM/yyyy"),
                                    VFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString()),
                                    RefPaciente = new EPaciente
                                    {
                                        Nombres = dr["NombrePaciente"].ToString()
                                    },
                                    RefDoctor = new EDoctor
                                    {
                                        Nombres = dr["NombreDoctor"].ToString()
                                    }
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ECita>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Citas obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ECita>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<ECita> DetalleCitaId(int IdCita)
        {
            try
            {
                ECita obj = null;

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_DetalleCita", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@IdCita", IdCita);

                        con.Open();
                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            if (dr.HasRows && dr.Read())
                            {
                                obj = new ECita
                                {
                                    IdCita = Convert.ToInt32(dr["IdCita"]),
                                    FechaCita = Convert.ToDateTime(dr["FechaCita"]).ToString("dd/MM/yyyy"),
                                    HoraCita = dr["HoraCita"].ToString(),
                                    Detalles = dr["Detalles"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]).ToString("dd/MM/yyyy"),
                                    RefPaciente = new EPaciente
                                    {
                                        Nombres = dr["NombrePaciente"].ToString()
                                    },
                                    RefDoctor = new EDoctor
                                    {
                                        Nombres = dr["NombreDoctor"].ToString()
                                    }
                                };
                            }
                        }
                    }
                }

                return new Respuesta<ECita>
                {
                    Estado = obj != null,
                    Data = obj,
                    Mensaje = obj != null ? "Detalle Citas obtenido correctamente" : "Ocurrio un error intente mas tarde."
                };
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales
                return new Respuesta<ECita>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error inesperado: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
