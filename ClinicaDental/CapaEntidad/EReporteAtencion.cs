using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EReporteAtencion
    {
        public int IdAtencion { get; set; }
        public string Codigo { get; set; }
        public string NombrePaciente { get; set; }
        public string NombreDoctor { get; set; }
        public decimal PrecioTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal Totalpagado { get; set; }
        public string FechaAtencion { get; set; }
        public string PrecioTotStr => PrecioTotal.ToString("0.00") + " /BS";
        public string PrecioDescStr => Descuento.ToString("0.00") + " /BS";
        public string TotalPagaStr => Totalpagado.ToString("0.00") + " /BS";
    }
}
