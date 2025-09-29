﻿using System.Data.SqlClient;

namespace CapaDatos
{
    public class ConexionBD
    {
        #region "PATRON SINGLETON"
        public static ConexionBD conexion = null;

        public ConexionBD() { }

        public static ConexionBD GetInstance()
        {
            if (conexion == null)
            {
                conexion = new ConexionBD();
            }
            return conexion;
        }
        #endregion

        public SqlConnection ConexionDB()
        {
            SqlConnection conexion = new SqlConnection
            {
                //ConnectionString = @"Data Source=SQL1001.site4now.net;Initial Catalog=db_abec4f_exportacion;User Id=db_abec4f_exportacion_admin;Password=Yamil2025"
                ConnectionString = "Data Source=.;Initial Catalog=ClinicaDental;Integrated Security=True"
            };

            return conexion;
        }
    }
}
