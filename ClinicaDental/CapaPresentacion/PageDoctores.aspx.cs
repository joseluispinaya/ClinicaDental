using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;
using System.Web.Services;

namespace CapaPresentacion
{
	public partial class PageDoctores : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        [WebMethod]
        public static Respuesta<List<EDoctor>> ListaDoctores()
        {
            try
            {
                Respuesta<List<EDoctor>> Lista = NDoctor.GetInstance().ListaDoctores();
                return Lista;
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EDoctor>>()
                {
                    Estado = false,
                    Mensaje = "Error al obtener los Doctores: " + ex.Message,
                    Data = null
                };
            }
        }

        [WebMethod]
        public static Respuesta<bool> Guardar(EDoctor oDoctor)
        {
            try
            {

                // Crear objeto EDoctor con los datos
                EDoctor obj = new EDoctor
                {
                    NroCi = oDoctor.NroCi,
                    Nombres = oDoctor.Nombres,
                    Apellidos = oDoctor.Apellidos,
                    Telefono = oDoctor.Telefono,
                    Correo = oDoctor.Correo,
                    ClaveHash = oDoctor.ClaveHash,
                    Token = Guid.NewGuid().ToString()
                };

                // Registrar el doctor
                Respuesta<bool> respuesta = NDoctor.GetInstance().RegistrarDoctores(obj);
                return respuesta;
            }
            catch (Exception)
            {
                // Manejar otras excepciones
                return new Respuesta<bool>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error intente mas tarde"
                };
            }
        }

        [WebMethod]
        public static Respuesta<bool> Editar(EDoctor oDoctor)
        {
            try
            {
                if (oDoctor == null || oDoctor.IdDoctor <= 0)
                {
                    return new Respuesta<bool>()
                    {
                        Estado = false,
                        Mensaje = "Intente acnualizar mas tarde"
                    };
                }

                // Crear objeto EDoctor para editar
                EDoctor obj = new EDoctor
                {
                    IdDoctor = oDoctor.IdDoctor,
                    NroCi = oDoctor.NroCi,
                    Nombres = oDoctor.Nombres,
                    Apellidos = oDoctor.Apellidos,
                    Telefono = oDoctor.Telefono,
                    Correo = oDoctor.Correo,
                    ClaveHash = oDoctor.ClaveHash,
                    Estado = oDoctor.Estado
                };

                // Pasar el objeto transformado a la capa de negocio
                Respuesta<bool> respuesta = NDoctor.GetInstance().EditarDoctores(obj);

                return respuesta;
            }
            catch (Exception)
            {
                return new Respuesta<bool>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error intente mas tarde"
                };
            }
        }
    }
}