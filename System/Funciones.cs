using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios//.system
{
    public class Funciones
    {
        public static void Logs(string nombre_archivo, string descripcion)
        {
            string directorio = AppDomain.CurrentDomain.BaseDirectory + "logs/" +
                    DateTime.Now.Year.ToString() + "/" +
                    DateTime.Now.Month.ToString() + "/" +
                    DateTime.Now.Day.ToString();
        }
    }
}