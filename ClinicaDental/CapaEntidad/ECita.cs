using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ECita
    {
        public int IdCita { get; set; }
        public int IdPaciente { get; set; }
        public int IdDoctor { get; set; }
        public string FechaCita { get; set; }
        public DateTime VFechaCita { get; set; }
        public string HoraCita { get; set; }
        public string Detalles { get; set; }
        public bool Activo { get; set; }
        public string Estado { get; set; }
        public string FechaRegistro { get; set; }
        public DateTime VFechaRegistro { get; set; }

        public EPaciente RefPaciente { get; set; }
        public EDoctor RefDoctor { get; set; }

        public string Color => Estado == "Confirmada"
            ? "#0061a9"
            : "#ff2301";
    }
}
