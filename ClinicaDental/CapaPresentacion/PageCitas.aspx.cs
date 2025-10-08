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
	public partial class PageCitas : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        [WebMethod]
        public static Respuesta<List<EPaciente>> ObtenerPacientesFiltro(string busqueda)
        {
            try
            {
                var Lista = NPaciente.GetInstance().ObtenerPacientesFiltro(busqueda);
                return Lista;
            }
            catch (Exception ex)
            {
                // Maneja cualquier error inesperado
                return new Respuesta<List<EPaciente>>()
                {
                    Estado = false,
                    Mensaje = "Error al obtener los pacientes: " + ex.Message,
                    Data = null
                };
            }

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
        public static Respuesta<List<ECita>> ListadeCitasFull()
        {
            try
            {
                Respuesta<List<ECita>> Lista = NCita.GetInstance().ListadeCitasFull();
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

    }
}