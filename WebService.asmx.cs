using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Servicios
{
    /// <summary>
    /// Summary description for WebService
    /// Máster en Web Services con C# de Udemy
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        /// <summary>
        /// Método web de prueba que devuelve la cadena Hola Mundo en inglés.
        /// </summary>
        /// <returns>string Hello World</returns>
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(Description = "Saluda al usuario")]
        public string Saludar( string nombre )
        {
            return "\nHola " + nombre + "\n";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns>string OK</returns>
        [WebMethod]
        public string GuardarLog(string mensaje)
        {
            Funciones.Logs("LogServicio", mensaje);

            return "OK";
        }

        /// <summary>
        /// Suma dos números
        /// </summary>
        /// <param name="numero1">Primer número</param>
        /// <param name="numero2">Segundo número</param>
        /// <returns>Suma del primer más el segundo número</returns>
        [WebMethod]
        public int Sumar(int numero1, int numero2)
        {
            return numero1 + numero2;
        }
    }
}
