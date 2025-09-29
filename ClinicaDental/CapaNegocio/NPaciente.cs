using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NPaciente
    {
        #region "PATRON SINGLETON"
        private static NPaciente daoEmpleado = null;
        private NPaciente() { }
        public static NPaciente GetInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new NPaciente();
            }
            return daoEmpleado;
        }
        #endregion

        public Respuesta<List<EPaciente>> ObtenerPacientesFiltro(string Busqueda)
        {
            return DPaciente.GetInstance().ObtenerPacientesFiltro(Busqueda);
        }

        public Respuesta<List<EPaciente>> ListaPacientes()
        {
            return DPaciente.GetInstance().ListaPacientes();
        }
    }
}
