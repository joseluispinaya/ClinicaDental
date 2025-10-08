using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ETratamiento
    {
        public int IdTratamiento { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public string PrecioStr => Precio.ToString("0.00") + " /BS";
    }
}
