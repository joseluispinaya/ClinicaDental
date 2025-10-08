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
	public partial class PageServicios : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        [WebMethod]
        public static Respuesta<bool> Guardar(ETratamiento oServicio)
        {
            try
            {
                // Registrar el doctor
                Respuesta<bool> respuesta = NPaciente.GetInstance().RegistrarTratamiento(oServicio);
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
        public static Respuesta<bool> Editar(ETratamiento oServicio)
        {
            try
            {
                if (oServicio == null || oServicio.IdTratamiento <= 0)
                {
                    return new Respuesta<bool>()
                    {
                        Estado = false,
                        Mensaje = "Intente acnualizar mas tarde"
                    };
                }

                // Pasar el objeto transformado a la capa de negocio
                Respuesta<bool> respuesta = NPaciente.GetInstance().EditarTratamiento(oServicio);

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