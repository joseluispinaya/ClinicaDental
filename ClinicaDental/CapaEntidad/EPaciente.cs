using System;

namespace CapaEntidad
{
    public class EPaciente
    {
        public int IdPaciente { get; set; }
        public string NroCi { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string FechaNacimiento { get; set; }
        public DateTime VFechaNacimiento { get; set; }
        public char Genero { get; set; }
        public string Telefono { get; set; }
        public string Alergias { get; set; }
        public string FechaRegistro { get; set; }
        public DateTime VFechaRegistro { get; set; }
        public string FullNombre => $"{Nombres} {Apellidos}";
        public string Edad
        {
            get
            {
                var hoy = DateTime.Today;
                int edad = hoy.Year - VFechaNacimiento.Year;

                // Si aún no cumplió años este año, restamos 1
                if (VFechaNacimiento.Date > hoy.AddYears(-edad))
                    edad--;

                return $"{edad} años";
            }
        }
    }
}
