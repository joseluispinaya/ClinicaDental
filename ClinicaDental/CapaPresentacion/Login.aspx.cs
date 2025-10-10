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
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            Response.AppendHeader("Cache-Control", "no-store");
        }
        [WebMethod]
        public static Respuesta<EDoctor> Logeo(string Usuario, string Clave)
        {
            try
            {
                var obj = NDoctor.GetInstance().LoginUsuario(Usuario, Clave);

                return obj;
            }
            catch (Exception ex)
            {
                return new Respuesta<EDoctor>
                {
                    Estado = false,
                    Valor = "",
                    Mensaje = "Ocurrió un error: " + ex.Message
                };
            }
        }
    }
}