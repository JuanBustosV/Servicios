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
            return "Hola " + nombre;
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
    }
}
