using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EAtencionPacie
    {
        public int IdAtencion { get; set; }
        public string Codigo { get; set; }
        public int IdPaciente { get; set; }
        public int IdDoctor { get; set; }
        public decimal PrecioTotal { get; set; }
        public decimal Descuento { get; set; }
        public bool Estado { get; set; }
        public string FechaAtencion { get; set; }
        public DateTime VFechaAtencion { get; set; }
        public EPaciente RefPaciente { get; set; }
        public EDoctor RefDoctor { get; set; }
        public List<EHistorialCli> ListaHistorialCli { get; set; }
        public string PrecioTotStr => PrecioTotal.ToString("0.00") + " /BS";
        public string PrecioDescStr => Descuento.ToString("0.00") + " /BS";
    }
}
