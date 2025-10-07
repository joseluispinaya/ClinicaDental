using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;
using System.Web.Services;
using System.Globalization;

namespace CapaPresentacion
{
	public partial class PageListaCitas : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        [WebMethod]
        public static Respuesta<List<ECita>> ObtenerCitasPorDoctor(int IdDoctor)
        {
            try
            {
                Respuesta<List<ECita>> Lista = NCita.GetInstance().ObtenerCitasPorDoctor(IdDoctor);
                return Lista;
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ECita>>()
                {
                    Estado = false,
                    Mensaje = "Error al obtener las citas: " + ex.Message,
                    Data = null
                };
            }
        }

        [WebMethod]
        public static Respuesta<bool> RegistrarCitas(ECita oCita)
        {
            try
            {
                if (oCita == null)
                {
                    return new Respuesta<bool> { Estado = false, Mensaje = "Datos de solicitud no válidos." };
                }

                if (!DateTime.TryParseExact(oCita.FechaCita, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechavisi))
                {
                    return new Respuesta<bool> { Estado = false, Mensaje = "El formato de la fecha no es válido. Debe ser dd/MM/yyyy." };
                }

                oCita.VFechaCita = fechavisi;

                return NCita.GetInstance().RegistrarCitas(oCita);
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

        [WebMethod]
        public static Respuesta<ECita> DetalleCitasci(int IdCita)
        {
            try
            {
                var respuesta = NCita.GetInstance().DetalleCitaId(IdCita);
                return respuesta;

            }
            catch (Exception ex)
            {
                return new Respuesta<ECita>
                {
                    Estado = false,
                    Data = null,
                    Mensaje = "Error inesperado: " + ex.Message
                };
            }
        }
    }
}