using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Servicios//.system
{
    public class EnlaceSqlServer
    {
        private static SqlConnection conexion = null;

        public static SqlConnection Conexion
        {
            get { return EnlaceSqlServer.conexion; }
        }

        public static bool ConectarSqlServer()
        {
            bool estado = false;

            try
            {

            }
            catch (Exception ex)
            {
                estado = false;
                Funciones.Logs("ENLACESQLSERVER", "Problemas al abrir la conexión; Captura error: " + ex.Message);
                Funciones.Logs("ENLACESQLSERVER_debug", ex.StackTrace);
            }

            return estado;
        }
    }
}