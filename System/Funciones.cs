using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Servicios//.system
{
    public class Funciones
    {
        /// <summary>
        /// Guarda información de log en archivo .txt
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo</param>
        /// <param name="descripcion">Descripción/contenido del archivo</param>
        public static void Logs(string nombreArchivo, string descripcion)
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

                StreamWriter miArchivo = new StreamWriter(directorio + "/" + nombreArchivo + ".txt", true);

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