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
    }
}