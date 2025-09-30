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
	public partial class PageAtencion : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        [WebMethod]
        public static Respuesta<List<ETratamiento>> ObtenerTratamientosFiltro(string busqueda)
        {
            try
            {
                var Lista = NPaciente.GetInstance().ObtenerTratamientosFiltro(busqueda);
                return Lista;
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<ETratamiento>>()
                {
                    Estado = false,
                    Mensaje = "Error al obtener los Tratamientos: " + ex.Message,
                    Data = null
                };
            }

        }
    }
}