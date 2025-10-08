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

        public Respuesta<bool> RegistrarPacientes(EPaciente oPaciente)
        {
            return DPaciente.GetInstance().RegistrarPacientes(oPaciente);
        }

        public Respuesta<bool> EditarPacientes(EPaciente oPaciente)
        {
            return DPaciente.GetInstance().EditarPacientes(oPaciente);
        }

        public Respuesta<List<EPaciente>> ObtenerPacientesFiltro(string Busqueda)
        {
            return DPaciente.GetInstance().ObtenerPacientesFiltro(Busqueda);
        }

        public Respuesta<List<EPaciente>> ListaPacientes()
        {
            return DPaciente.GetInstance().ListaPacientes();
        }

        public Respuesta<List<ETratamiento>> ObtenerTratamientosFiltro(string Busqueda)
        {
            return DPaciente.GetInstance().ObtenerTratamientosFiltro(Busqueda);
        }

        public Respuesta<bool> RegistrarTratamiento(ETratamiento oTratamiento)
        {
            return DPaciente.GetInstance().RegistrarTratamiento(oTratamiento);
        }

        public Respuesta<bool> EditarTratamiento(ETratamiento oTratamiento)
        {
            return DPaciente.GetInstance().EditarTratamiento(oTratamiento);
        }

        public Respuesta<List<ETratamiento>> ListaServicios()
        {
            return DPaciente.GetInstance().ListaServicios();
        }
    }
}
