using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            return "Hola " + nombre + "\n";
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

            return "GuardarLog: OK";
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

        // CURSO: 30. Método que retorna un texto con formato JSON

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string RetornarJson()
        {
            dynamic json = new Dictionary<string, dynamic>();

            json.Add("deporte", "Fútbol");

            // Emula formato JSON clave, valor
            List<Dictionary<string, string>> equipos = new List<Dictionary<string, string>>();

            Dictionary<string, string> equipo1 = new Dictionary<string, string>();
            equipo1.Add("Nombre", "Manchester United");
            equipo1.Add("Pais", "Inglaterra");

            equipos.Add(equipo1);

            Dictionary<string, string> equipo2 = new Dictionary<string, string>();
            equipo2.Add("Nombre", "Betis");
            equipo2.Add("Pais", "España");

            equipos.Add(equipo2);

            json.Add("equipos", equipos);

            return JsonConvert.SerializeObject(json);
        }

        // CURSO: 33. Método que recibe un texto con formato JSON

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [WebMethod]
        public string GuardarJson(string json)
        {
            //{"deporte":"Fútbol","equipos":[{"Nombre":"Manchester United","Pais":"Inglaterra"},{"Nombre":"Betis","Pais":"España"}]}
            var data_json = JsonConvert.DeserializeObject<DataJson>(json);

            Funciones.Logs("JSON", "Deporte: " + data_json.deporte + "; Equipos: ");

            foreach (var equipo in data_json.equipos)
            {
                Funciones.Logs("JSON", "Deporte: " + equipo.Nombre + " - " + equipo.Pais);
            }

            return "Proceso GuardarJson realizado con éxito";
        }

        // CURSO: 44. Método que retorna una tabla de la base en formato JSON - P1
        [WebMethod]
        public string ObtenerProductos()
        {
            List<Dictionary<string, string>> json = new List<Dictionary<string, string>>();

            if (!EnlaceSqlServer.ConectarSqlServer())
            {
                return "No conecta a BD!";
            }
            // CURSO: 45. Método que retorna una tabla de la base en formato JSON - P2
            try
            {
                SqlCommand com = new SqlCommand("SELECT * FROM productos", EnlaceSqlServer.Conexion);
                com.CommandType = CommandType.Text;
                com.CommandTimeout = DatosEnlace.timeOutSqlServer;

                SqlDataReader record = com.ExecuteReader();
                if (record.HasRows)
                {

                }
            }
            catch (Exception ex)
            {
                Funciones.Logs("ObtenerProductos", ex.Message);
                Funciones.Logs("ObtenerProductos_DEBUG", ex.StackTrace);
            }

            return JsonConvert.SerializeObject(json);
        }

        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
    }
}
