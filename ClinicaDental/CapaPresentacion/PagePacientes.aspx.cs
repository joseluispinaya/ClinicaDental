using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;
using System.Globalization;
using System.Web.Services;

namespace CapaPresentacion
{
	public partial class PagePacientes : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        [WebMethod]
        public static Respuesta<List<EPaciente>> ListaPacientes()
        {
            try
            {
                Respuesta<List<EPaciente>> Lista = NPaciente.GetInstance().ListaPacientes();
                return Lista;
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EPaciente>>()
                {
                    Estado = false,
                    Mensaje = "Error al obtener los Pacientes: " + ex.Message,
                    Data = null
                };
            }
        }

        [WebMethod]
        public static Respuesta<bool> Guardar(EPaciente oPaciente)
        {
            try
            {
                // Intentar parsear la fecha exacta
                DateTime VFechaNacimiento = DateTime.ParseExact(
                    oPaciente.FechaNacimiento,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                );

                // Crear objeto EPaciente con los datos
                EPaciente obj = new EPaciente
                {
                    NroCi = oPaciente.NroCi,
                    Nombres = oPaciente.Nombres,
                    Apellidos = oPaciente.Apellidos,
                    VFechaNacimiento = VFechaNacimiento,
                    Genero = oPaciente.Genero,  // "M" o "F"
                    Telefono = oPaciente.Telefono,
                    Alergias = oPaciente.Alergias
                };

                // Registrar el paciente
                Respuesta<bool> respuesta = NPaciente.GetInstance().RegistrarPacientes(obj);
                return respuesta;
            }
            catch (FormatException)
            {
                return new Respuesta<bool>
                {
                    Estado = false,
                    Mensaje = "La fecha de nacimiento no tiene el formato válido (dd/MM/yyyy)."
                };
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                return new Respuesta<bool>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message
                };
            }
        }

        [WebMethod]
        public static Respuesta<bool> Editar(EPaciente oPaciente)
        {
            try
            {
                if (oPaciente == null || oPaciente.IdPaciente <= 0)
                {
                    return new Respuesta<bool>()
                    {
                        Estado = false,
                        Mensaje = "Ocurrió un problema, intente más tarde"
                    };
                }

                // Intentar parsear la fecha en formato dd/MM/yyyy
                DateTime VFechaNacimiento = DateTime.ParseExact(
                    oPaciente.FechaNacimiento,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                );

                // Crear objeto EPaciente para editar
                EPaciente obj = new EPaciente
                {
                    IdPaciente = oPaciente.IdPaciente,
                    NroCi = oPaciente.NroCi,
                    Nombres = oPaciente.Nombres,
                    Apellidos = oPaciente.Apellidos,
                    VFechaNacimiento = VFechaNacimiento,
                    Genero = oPaciente.Genero,
                    Telefono = oPaciente.Telefono,
                    Alergias = oPaciente.Alergias
                };

                // Pasar el objeto transformado a la capa de negocio
                Respuesta<bool> respuesta = NPaciente.GetInstance().EditarPacientes(obj);

                return respuesta;
            }
            catch (FormatException)
            {
                return new Respuesta<bool>
                {
                    Estado = false,
                    Mensaje = "La fecha de nacimiento no tiene el formato válido (dd/MM/yyyy)."
                };
            }
            catch (Exception ex)
            {
                return new Respuesta<bool>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message
                };
            }
        }


    }
}