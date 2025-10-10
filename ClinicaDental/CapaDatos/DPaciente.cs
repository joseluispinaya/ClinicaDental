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
    public class DPaciente
    {
        #region "PATRON SINGLETON"
        public static DPaciente _instancia = null;

        private DPaciente()
        {

        }

        public static DPaciente GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new DPaciente();
            }
            return _instancia;
        }
        #endregion

        public Respuesta<bool> RegistrarPacientes(EPaciente oPaciente)
        {
            try
            {
                bool respuesta = false;
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarPacientes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@NroCi", oPaciente.NroCi);
                        cmd.Parameters.AddWithValue("@Nombres", oPaciente.Nombres);
                        cmd.Parameters.AddWithValue("@Apellidos", oPaciente.Apellidos);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", oPaciente.VFechaNacimiento);
                        cmd.Parameters.AddWithValue("@Genero", oPaciente.Genero);
                        cmd.Parameters.AddWithValue("@Telefono", oPaciente.Telefono);
                        cmd.Parameters.AddWithValue("@Alergias", oPaciente.Alergias);

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
                    Mensaje = respuesta ? "Se registro correctamente" : "Error al registrar ingrese otro nro de ci"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Mensaje = "Ocurrió un error: " + ex.Message };
            }
        }

        public Respuesta<bool> EditarPacientes(EPaciente oPaciente)
        {
            try
            {
                bool respuesta = false;
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ModificarPacientes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdPaciente", oPaciente.IdPaciente);
                        cmd.Parameters.AddWithValue("@NroCi", oPaciente.NroCi);
                        cmd.Parameters.AddWithValue("@Nombres", oPaciente.Nombres);
                        cmd.Parameters.AddWithValue("@Apellidos", oPaciente.Apellidos);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", oPaciente.VFechaNacimiento);
                        cmd.Parameters.AddWithValue("@Genero", oPaciente.Genero);
                        cmd.Parameters.AddWithValue("@Telefono", oPaciente.Telefono);
                        cmd.Parameters.AddWithValue("@Alergias", oPaciente.Alergias);

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
                    Mensaje = respuesta ? "Se actualizo correctamente" : "Error al actualizar ingrese otro nro de ci"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Mensaje = "Ocurrió un error: " + ex.Message };
            }
        }

        public Respuesta<List<EPaciente>> ObtenerPacientesFiltro(string Busqueda)
        {
            try
            {
                List<EPaciente> rptLista = new List<EPaciente>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerPacientesFiltro", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Busqueda", Busqueda);
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EPaciente()
                                {
                                    IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                    NroCi = dr["NroCi"].ToString(),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]).ToString("dd/MM/yyyy"),
                                    VFechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString()),
                                    Telefono = dr["Telefono"].ToString(),
                                    Alergias = dr["Alergias"].ToString()
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EPaciente>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Pacientes obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EPaciente>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<List<EPaciente>> ListaPacientes()
        {
            try
            {
                List<EPaciente> rptLista = new List<EPaciente>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ObtenerPacientes", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new EPaciente()
                                {
                                    IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                    NroCi = dr["NroCi"].ToString(),
                                    Nombres = dr["Nombres"].ToString(),
                                    Apellidos = dr["Apellidos"].ToString(),
                                    FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]).ToString("dd/MM/yyyy"),
                                    VFechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString()),
                                    Genero = Convert.ToChar(dr["Genero"].ToString()),
                                    Telefono = dr["Telefono"].ToString(),
                                    Alergias = dr["Alergias"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"]).ToString("dd/MM/yyyy"),
                                    VFechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString())
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<EPaciente>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Pacientes obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EPaciente>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<List<ETratamiento>> ObtenerTratamientosFiltro(string Busqueda)
        {
            try
            {
                List<ETratamiento> rptLista = new List<ETratamiento>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerTratamientosFiltro", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@Busqueda", Busqueda);
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ETratamiento()
                                {
                                    IdTratamiento = Convert.ToInt32(dr["IdTratamiento"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"])
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ETratamiento>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Tratamientos obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ETratamiento>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }

        public Respuesta<List<EReporteAtencion>> DetalleAtencionPaciente(int IdPaciente)
        {
            try
            {
                List<EReporteAtencion> rptLista = new List<EReporteAtencion>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("sp_ObtenerListaAtencionPaci", con))
                    {
                        comando.Parameters.AddWithValue("@IdPaciente", IdPaciente);
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
                                    NombreDoctor = dr["NombreDoctor"].ToString(),
                                    PrecioTotal = Convert.ToDecimal(dr["PrecioTotal"]),
                                    Descuento = Convert.ToDecimal(dr["Descuento"]),
                                    Totalpagado = Convert.ToDecimal(dr["totalpagado"]),
                                    FechaAtencion = dr["FechaAtencion"].ToString(),
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

        // crud tratamientos

        public Respuesta<bool> RegistrarTratamiento(ETratamiento oTratamiento)
        {
            try
            {
                bool respuesta = false;
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_RegistrarTratamientos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nombre", oTratamiento.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", oTratamiento.Descripcion);
                        cmd.Parameters.AddWithValue("@Precio", oTratamiento.Precio);

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
                    Mensaje = respuesta ? "Se registro correctamente" : "Error al registrar intente mas tarde"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Mensaje = "Ocurrió un error: " + ex.Message };
            }
        }

        public Respuesta<bool> EditarTratamiento(ETratamiento oTratamiento)
        {
            try
            {
                bool respuesta = false;
                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand cmd = new SqlCommand("usp_ModificarTratamientos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdTratamiento", oTratamiento.IdTratamiento);
                        cmd.Parameters.AddWithValue("@Nombre", oTratamiento.Nombre);
                        cmd.Parameters.AddWithValue("@Descripcion", oTratamiento.Descripcion);
                        cmd.Parameters.AddWithValue("@Precio", oTratamiento.Precio);
                        cmd.Parameters.AddWithValue("@Estado", oTratamiento.Estado);

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
                    Mensaje = respuesta ? "Se actualizo correctamente" : "Error al actualizar intente mas tarde"
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool> { Estado = false, Mensaje = "Ocurrió un error: " + ex.Message };
            }
        }

        public Respuesta<List<ETratamiento>> ListaServicios()
        {
            try
            {
                List<ETratamiento> rptLista = new List<ETratamiento>();

                using (SqlConnection con = ConexionBD.GetInstance().ConexionDB())
                {
                    using (SqlCommand comando = new SqlCommand("usp_ObtenerTratamientos", con))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        con.Open();

                        using (SqlDataReader dr = comando.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                rptLista.Add(new ETratamiento()
                                {
                                    IdTratamiento = Convert.ToInt32(dr["IdTratamiento"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"]),
                                    Estado = Convert.ToBoolean(dr["Estado"])
                                });
                            }
                        }
                    }
                }
                return new Respuesta<List<ETratamiento>>()
                {
                    Estado = true,
                    Data = rptLista,
                    Mensaje = "Tratamientos obtenidos correctamente"
                };
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ETratamiento>>()
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
