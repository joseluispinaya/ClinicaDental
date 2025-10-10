using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NDoctor
    {
        #region "PATRON SINGLETON"
        private static NDoctor daoEmpleado = null;
        private NDoctor() { }
        public static NDoctor GetInstance()
        {
            if (daoEmpleado == null)
            {
                daoEmpleado = new NDoctor();
            }
            return daoEmpleado;
        }
        #endregion

        public Respuesta<bool> RegistrarDoctores(EDoctor oDoctor)
        {
            return DDoctor.GetInstance().RegistrarDoctores(oDoctor);
        }

        public Respuesta<bool> EditarDoctores(EDoctor oDoctor)
        {
            return DDoctor.GetInstance().EditarDoctores(oDoctor);
        }

        public Respuesta<List<EDoctor>> ListaDoctores()
        {
            return DDoctor.GetInstance().ListaDoctores();
        }

        public Respuesta<EDoctor> LoginUsuario(string Correo, string Clave)
        {
            return DDoctor.GetInstance().LoginUsuario(Correo, Clave);
        }
    }
}
