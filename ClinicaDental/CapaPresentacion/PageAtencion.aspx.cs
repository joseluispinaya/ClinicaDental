using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidad;
using CapaNegocio;
using System.Web.Services;
using System.Xml.Linq;

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

        [WebMethod]
        public static Respuesta<List<ETratamiento>> ListaServicios()
        {
            try
            {
                Respuesta<List<ETratamiento>> Lista = NPaciente.GetInstance().ListaServicios();
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

        [WebMethod]
        public static Respuesta<int> GuardarAtencion(EAtencionPacie eAtencionPa, List<EHistorialCli> RequestList)
        {
            try
            {
                if (RequestList == null || !RequestList.Any())
                {
                    return new Respuesta<int> { Estado = false, Mensaje = "La lista está vacía" };
                }

                XElement activoa = new XElement("Atencion",
                    new XElement("IdPaciente", eAtencionPa.IdPaciente),
                    new XElement("IdDoctor", eAtencionPa.IdDoctor),
                    new XElement("PrecioTotal", eAtencionPa.PrecioTotal),
                    new XElement("Descuento", eAtencionPa.Descuento)
                );

                XElement detalleHistorial = new XElement("DetalleHistorial");

                foreach (EHistorialCli item in RequestList)
                {
                    detalleHistorial.Add(new XElement("Item",

                        new XElement("IdTratamiento", item.IdTratamiento),
                        new XElement("PrecioAplicado", item.PrecioAplicado),
                        new XElement("Cantidad", item.Cantidad),
                        new XElement("ImporteTotal", item.ImporteTotal)
                        )

                    );
                }

                activoa.Add(detalleHistorial);

                // Llamar a RegistrarActivo en la capa de negocio y recibir la respuesta
                Respuesta<int> respuesta = NAtencionPacie.GetInstance().RegistrarAtencion(activoa.ToString());
                return respuesta;
            }
            catch (Exception ex)
            {
                // Capturar cualquier error y retornar una respuesta de fallo
                return new Respuesta<int>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message
                };
            }
        }

        [WebMethod]
        public static Respuesta<EAtencionPacie> ObtenerAtencionPacie(int IdAtencion)
        {
            try
            {
                // Obtener el activo principal
                var respuesta = NAtencionPacie.GetInstance().ObtenerAtencionPacie(IdAtencion);
                return respuesta;
            }
            catch (Exception ex)
            {
                return new Respuesta<EAtencionPacie>
                {
                    Estado = false,
                    Data = null,
                    Mensaje = "Error inesperado: " + ex.Message
                };
            }
        }

        //solo para pruebas
        [WebMethod]
        public static Respuesta<bool> GuardarAtencionPruebas(EAtencionPacie eAtencionPa, List<EHistorialCli> RequestList)
        {
            try
            {
                if (RequestList == null || !RequestList.Any())
                {
                    return new Respuesta<bool> { Estado = false, Mensaje = "La lista está vacía" };
                }

                XElement activoa = new XElement("Atencion",
                    new XElement("IdPaciente", eAtencionPa.IdPaciente),
                    new XElement("IdDoctor", eAtencionPa.IdDoctor),
                    new XElement("PrecioTotal", eAtencionPa.PrecioTotal),
                    new XElement("Descuento", eAtencionPa.Descuento)
                );

                XElement detalleHistorial = new XElement("DetalleHistorial");

                foreach (EHistorialCli item in RequestList)
                {
                    detalleHistorial.Add(new XElement("Item",

                        new XElement("IdTratamiento", item.IdTratamiento),
                        new XElement("PrecioAplicado", item.PrecioAplicado),
                        new XElement("Cantidad", item.Cantidad),
                        new XElement("ImporteTotal", item.ImporteTotal)
                        )

                    );
                }

                activoa.Add(detalleHistorial);
                var estructura = activoa.ToString();
                bool encontrado = !string.IsNullOrEmpty(estructura);

                return new Respuesta<bool>
                {
                    Estado = encontrado,
                    Mensaje = encontrado ? "Estructura xml bien" : "No tiene estructura"
                };


            }
            catch (Exception ex)
            {
                // Capturar cualquier error y retornar una respuesta de fallo
                return new Respuesta<bool>
                {
                    Estado = false,
                    Mensaje = "Ocurrió un error: " + ex.Message
                };
            }
        }
    }
}