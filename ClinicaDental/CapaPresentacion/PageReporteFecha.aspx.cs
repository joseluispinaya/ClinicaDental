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
	public partial class PageReporteFecha : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        [WebMethod]
        public static Respuesta<List<EReporteAtencion>> ReporteAtencFechas(string fechainicio, string fechafin)
        {

            try
            {
                if (string.IsNullOrEmpty(fechainicio) || string.IsNullOrEmpty(fechafin))
                {
                    return new Respuesta<List<EReporteAtencion>>()
                    {
                        Estado = false,
                        Mensaje = "Ingrese las fechas para la consulta",
                        Data = null
                    };

                }

                Respuesta<List<EReporteAtencion>> Lista = NAtencionPacie.GetInstance().ReporteAtencFechas(fechainicio, fechafin);
                return Lista;


            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EReporteAtencion>>()
                {
                    Estado = false,
                    Mensaje = "Error al obtener las Atenciones: " + ex.Message,
                    Data = null
                };
            }

        }

        [WebMethod]
        public static Respuesta<List<EHistorialCli>> DetalleHistorialCli(int IdAtencion)
        {

            try
            {

                Respuesta<List<EHistorialCli>> Lista = NAtencionPacie.GetInstance().DetalleHistorialCli(IdAtencion);
                return Lista;


            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EHistorialCli>>()
                {
                    Estado = false,
                    Mensaje = "Error al obtener los detalles: " + ex.Message,
                    Data = null
                };
            }

        }
    }
}