using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Servicios//.system
{
    public class Funciones
    {
        public static void Logs(string nombre_archivo, string descripcion)
        {
            try
            {
                string directorio = AppDomain.CurrentDomain.BaseDirectory + "logs/" +
                    DateTime.Now.Year.ToString() + "/" +
                    DateTime.Now.Month.ToString() + "/" +
                    DateTime.Now.Day.ToString();

                // Si no existe el directorio, se creará
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                StreamWriter miArchivo = new StreamWriter(directorio + "/" + nombre_archivo + ".txt", true);

                string cadena = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " >>> " + descripcion;

                miArchivo.WriteLine(cadena);
                miArchivo.Close();
            }
            catch (Exception)
            {

                throw;
            }            
        }
    }
}