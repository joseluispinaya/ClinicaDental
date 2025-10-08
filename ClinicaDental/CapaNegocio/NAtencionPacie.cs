using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NAtencionPacie
    {
        #region "PATRON SINGLETON"
        private static NAtencionPacie daoEmpleado = null;
        private NAtencionPacie() { }
        public static NAtencionPacie GetInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new NAtencionPacie();
            }
            return daoEmpleado;
        }
        #endregion

        public Respuesta<int> RegistrarAtencion(string ActivoXml)
        {
            return DAtencionPacie.GetInstance().RegistrarAtencion(ActivoXml);
        }

        public Respuesta<EAtencionPacie> ObtenerAtencionPacie(int IdAtencion)
        {
            return DAtencionPacie.GetInstance().ObtenerAtencionPacie(IdAtencion);
        }

        public Respuesta<List<EHistorialCli>> DetalleHistorialCli(int IdAtencion)
        {
            return DAtencionPacie.GetInstance().DetalleHistorialCli(IdAtencion);
        }

        public Respuesta<List<EReporteAtencion>> ReporteAtencFechas(string FechaInicio, string FechaFin)
        {
            return DAtencionPacie.GetInstance().ReporteAtencFechas(FechaInicio, FechaFin);
        }
    }
}
