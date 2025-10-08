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
    public class DAtencionPacie
    {
        #region "PATRON SINGLETON"
        public static DAtencionPacie _instancia = null;

        private DAtencionPacie()
        {

        }

        public static DAtencionPacie GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DAtencionPacie();
            }
            return _instancia;
        }
        #endregion

        public Respuesta<int> RegistrarAtencion(string ActivoXml)
        {
            var respuesta = new Respuesta<int>();

            try
            {
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarAtencion", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Agregar parámetro de entrada
                        cmd.Parameters.AddWithValue("@Detalle", ActivoXml);

                        // Agregar parámetro de salida
                        var outputParam = new SqlParameter("@Resultado", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        // Abrir la conexión y ejecutar el comando
                        con.Open();
                        cmd.ExecuteNonQuery();

                        // Obtener el valor del parámetro de salida
                        int resultado = Convert.ToInt32(outputParam.Value);

                        // Configurar respuesta de éxito
                        respuesta.Estado = resultado > 0;
                        respuesta.Valor = resultado.ToString();
                        respuesta.Mensaje = resultado > 0 ? "Registro realizado correctamente." : "Error al registrar, intente más tarde.";
                        respuesta.Data = resultado;
                    }
                }
            }
            catch (SqlException ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = $"Ocurrió un error en la base de datos: {ex.Message}";
            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = $"Ocurrió un error: {ex.Message}";
            }

            return respuesta;
        }

        public Respuesta<EAtencionPacie> ObtenerAtencionPacie(int IdAtencion)
        {
            try
            {
                EAtencionPacie obj = null;

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    con.Open();

                    // Obtener propietario
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerAtencionPac", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@IdAtencion", IdAtencion);

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            if (dr.HasRows && dr.Read())
                            {
                                obj = new EAtencionPacie
                                {
                                    IdAtencion = Convert.ToInt32(dr["IdAtencion"]),
                                    Codigo = dr["Codigo"].ToString(),
                                    RefPaciente = new EPaciente
                                    {
                                        Nombres = dr["NombrePaciente"].ToString()
                                    },
                                    RefDoctor = new EDoctor
                                    {
                                        Nombres = dr["NombreDoctor"].ToString()
                                    },
                                    PrecioTotal = Convert.ToDecimal(dr["PrecioTotal"]),
                                    Descuento = Convert.ToDecimal(dr["Descuento"]),
                                    FechaAtencion = dr["FechaAtencion"].ToString(),
                                    ListaHistorialCli = new List<EHistorialCli>() // Inicializamos la lista vacía
                                };
                            }
                        }
                    }

                    // Si se encontró
                    if (obj != null)
                    {
                        using (SqlCommand mascotaCmd = new SqlCommand("sp_ObtenerDetalleHistoria", con))
                        {
                            mascotaCmd.CommandType = CommandType.StoredProcedure;
                            mascotaCmd.Parameters.AddWithValue("@IdAtencion", IdAtencion);

                            using (SqlDataReader mascotaDr = mascotaCmd.ExecuteReader())
                            {
                                while (mascotaDr.Read())
                                {
                                    EHistorialCli detallesac = new EHistorialCli()
                                    {
                                        IdHistorial = Convert.ToInt32(mascotaDr["IdHistorial"]),
                                        RefTratamiento = new ETratamiento
                                        {
                                            Nombre = mascotaDr["Nombre"].ToString()
                                        },
                                        PrecioAplicado = Convert.ToDecimal(mascotaDr["PrecioAplicado"]),
                                        Cantidad = Convert.ToInt32(mascotaDr["Cantidad"]),
                                        ImporteTotal = Convert.ToDecimal(mascotaDr["ImporteTotal"])
                                    };

                                    obj.ListaHistorialCli.Add(detallesac);
                                }
                            }
                        }
                    }
                }

                return new Respuesta<EAtencionPacie>
                {
                    Estado = obj != null,
                    Data = obj,
                    Mensaje = obj != null ? "Detalle obtenidos correctamente" : "El Detalle no se encuentra en el sistema"
                };
            }
            catch (SqlException ex)
            {
                return new Respuesta<EAtencionPacie>
                {
                    Estado = false,
                    Mensaje = "Error en la base de datos: " + ex.Message,
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<EAtencionPacie>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error inesperado: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<List<EHistorialCli>> DetalleHistorialCli(int IdAtencion)
        {
            try
            {
                List<EHistorialCli> rptLista = new List<EHistorialCli>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerDetalleHistoria", con))
                    {
                        comando.Parameters.AddWithValue("@IdAtencion", IdAtencion);
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EHistorialCli()
                                {
                                    IdHistorial = Convert.ToInt32(dr["IdHistorial"]),
                                    RefTratamiento = new ETratamiento
                                    {
                                        Nombre = dr["Nombre"].ToString()
                                    },
                                    PrecioAplicado = Convert.ToDecimal(dr["PrecioAplicado"]),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                    ImporteTotal = Convert.ToDecimal(dr["ImporteTotal"])
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EHistorialCli>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EHistorialCli>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<List<EReporteAtencion>> ReporteAtencFechas(string FechaInicio, string FechaFin)
        {
            try
            {
                List<EReporteAtencion> rptLista = new List<EReporteAtencion>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("sp_ReporteAtencionPac", con))
                    {
                        comando.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        comando.Parameters.AddWithValue("@FechaFin", FechaFin);
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EReporteAtencion()
                                {
                                    IdAtencion = Convert.ToInt32(dr["IdAtencion"]),
                                    Codigo = dr["Codigo"].ToString(),
                                    NombrePaciente = dr["NombrePaciente"].ToString(),
                                    NombreDoctor = dr["NombreDoctor"].ToString(),
                                    PrecioTotal = Convert.ToDecimal(dr["PrecioTotal"]),
                                    Descuento = Convert.ToDecimal(dr["Descuento"]),
                                    Totalpagado = Convert.ToDecimal(dr["totalpagado"]),
                                    FechaAtencion = dr["FechaAtencion"].ToString()

                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EReporteAtencion>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Lista obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EReporteAtencion>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
