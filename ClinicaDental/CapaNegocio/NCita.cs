using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NCita
    {
        #region "PATRON SINGLETON"
        private static NCita daoEmpleado = null;
        private NCita() { }
        public static NCita GetInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new NCita();
            }
            return daoEmpleado;
        }
        #endregion

        public Respuesta<bool> RegistrarCitas(ECita cita)
        {
            return DCita.GetInstance().RegistrarCitas(cita);
        }

        public Respuesta<List<ECita>> ObtenerCitasPorDoctor(int IdDoctor)
        {
            return DCita.GetInstance().ObtenerCitasPorDoctor(IdDoctor);
        }

        public Respuesta<List<ECita>> ListadeCitasFull()
        {
            return DCita.GetInstance().ListadeCitasFull();
        }

        public Respuesta<ECita> DetalleCitaId(int IdCita)
        {
            return DCita.GetInstance().DetalleCitaId(IdCita);
        }

        public Respuesta<bool> CambiarEstadoCita(int IdCita)
        {
            return DCita.GetInstance().CambiarEstadoCita(IdCita);
        }

    }
}
