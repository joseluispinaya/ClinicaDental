using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EHistorialCli
    {
        public int IdHistorial { get; set; }
        public int IdAtencion { get; set; }
        public int IdTratamiento { get; set; }
        public decimal PrecioAplicado { get; set; }
        public int Cantidad { get; set; }
        public decimal ImporteTotal { get; set; }
        public ETratamiento RefTratamiento { get; set; }
        public string PrecioStr => PrecioAplicado.ToString("0.00") + " /BS";
        public string TotalInStr => ImporteTotal.ToString("0.00") + " /BS";
    }
}
