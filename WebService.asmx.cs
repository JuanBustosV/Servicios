using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string[] ObtenerFrutas()
        {
            string[] frutas = {"Fresa", "Limón", "Melón"};

            return frutas;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frutas"></param>
        /// <returns></returns>
        [WebMethod]
        public string GuardarFrutas(string[] frutas)
        {
            foreach(string fruta in frutas)
            {
                Funciones.Logs("Frutas", fruta);
            }

            return "Proceso GuardarFrutas realizado con éxito";
        }

        // CURSO: 21. Método que retorna un vector tipo Clase

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public List<Equipos> ObtenerEquipos()
        {
            List<Equipos> equipos = new List<Equipos>()
            {
                new Equipos { Nombre = "Milan", Pais = "Italia" },
                new Equipos { Nombre = "Real Madrid", Pais = "España" },
                new Equipos { Nombre = "Granada", Pais = "España" }
            };

            return equipos;
        }

        // CURSO: 23. Método que recibe un vector tipo Clase

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GuardarEquipos(Equipos[] equipos)
        {
            foreach (Equipos equipo in equipos)
            {
                Funciones.Logs("Equipos", equipo.Nombre + " - " + equipo.Pais);
            }

            return "Proceso GuardarEquipos realizado con éxito";
        }

        // CURSO: 26. Método que recibe un texto con formato XML - P1
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GuardarXML(string xml)
        {
            XmlDocument dataXML = new XmlDocument();

            dataXML.LoadXml(xml);

            XmlNode documento = dataXML.SelectSingleNode("documento");

            string deporte = documento["deporte"].InnerText;

            Funciones.Logs("XML", "Deporte: " + deporte + "; Equipos: ");

            // CURSO: 27. Método que recibe un texto con formato XML - P2

            XmlNodeList nodeEquipos = dataXML.GetElementsByTagName("equipos");

            // Obtenemos todos los elementos "equipo" del nodo equipos
            XmlNodeList equipos = ((XmlElement)nodeEquipos[0]).GetElementsByTagName("equipo");

            foreach (XmlElement equipo in equipos)
            {
                string nombre = equipo.GetElementsByTagName("nombre")[0].InnerText;
                string pais = equipo.GetElementsByTagName("pais")[0].InnerText;

                Funciones.Logs("XML", nombre + " - " + pais);
            }

            return "Proceso GuardarXML realizado con éxito";
        }
    }
}
