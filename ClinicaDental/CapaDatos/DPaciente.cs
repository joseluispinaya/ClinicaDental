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
    }
}
