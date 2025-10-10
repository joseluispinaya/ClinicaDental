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
	public partial class PageHistorial : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        [WebMethod]
        public static Respuesta<List<EReporteAtencion>> DetalleAtencionPaciente(int IdPaciente)
        {

            try
            {
                Respuesta<List<EReporteAtencion>> Lista = NPaciente.GetInstance().DetalleAtencionPaciente(IdPaciente);
                return Lista;

            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EReporteAtencion>>()
                {
                    Estado = false,
                    Mensaje = "Error al obtener los detalles: " + ex.Message,
                    Data = null
                };
            }

        }

    }
}