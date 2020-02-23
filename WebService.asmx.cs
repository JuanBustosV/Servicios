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
            SqlDataReader record = null;
            SqlCommand com = null;

            if (!EnlaceSqlServer.ConectarSqlServer())
            {
                return "{\"ERROR\": \"No conecta a BD!\"}";
            }
            // CURSO: 45. Método que retorna una tabla de la base en formato JSON - P2
            try
            {
                com = new SqlCommand("SELECT * FROM productos", EnlaceSqlServer.Conexion)
                {
                    CommandType = CommandType.Text,
                    CommandTimeout = DatosEnlace.timeOutSqlServer
                };

                record = com.ExecuteReader();
                if (record.HasRows)
                {
                    // CURSO: 46. Método que retorna una tabla de la base en formato JSON - P3
                    Dictionary<string, string> row;

                    while (record.Read())
                    {
                        row = new Dictionary<string, string>();

                        for (int f = 0; f < record.FieldCount; f++)
                        {
                            row.Add(record.GetName(f), record.GetValue(f).ToString());
                        }

                        json.Add(row);
                    }
                }                
            }
            catch (Exception ex)
            {
                Funciones.Logs("ObtenerProductos", ex.Message);
                Funciones.Logs("ObtenerProductos_DEBUG", ex.StackTrace);
            }
            finally
            {
                if (record != null)
                {
                    record.Close();
                    record.Dispose();
                    //record = null;
                }
                if (com != null)
                    com.Dispose();
            }

            return JsonConvert.SerializeObject(json);
        }

        // CURSO: 51. Método para retornar un registro de la base de datos - P1
        [WebMethod]
        public Producto ObtenerProducto(int idproducto)
        {
            Producto producto = new Producto
            {
                // Devolverá todo a 0 o "" si no encuentra producto en la consulta SQL
                Idproducto = 0,
                Nombre = "",
                Precio = 0,
                Stock = 0
            };
            SqlCommand com = null;
            SqlDataReader record = null;

            if (!EnlaceSqlServer.ConectarSqlServer())
            {
                return producto;
            }

            try
            {
                // CURSO: 52. Método para retornar un registro de la base de datos - P2
                com = new SqlCommand("SELECT TOP 1 * FROM productos WHERE idproducto = " + idproducto, EnlaceSqlServer.Conexion)
                {
                    CommandType = CommandType.Text,
                    CommandTimeout = DatosEnlace.timeOutSqlServer
                };

                record = com.ExecuteReader();
                if (record.HasRows && record.Read())
                {
                    producto.Idproducto = int.Parse(record.GetValue(0).ToString());
                    producto.Nombre = record.GetValue(1).ToString();
                    producto.Precio = double.Parse(record.GetValue(2).ToString());
                    producto.Stock = int.Parse(record.GetValue(3).ToString());
                }                
            }
            catch (Exception ex)
            {
                Funciones.Logs("ObtenerProducto", ex.Message);
                Funciones.Logs("ObtenerProducto_DEBUG", ex.StackTrace);
            }
            finally // CURSO: 53. Método para retornar un registro de la base de datos - P3
            {
                if (record != null)
                {
                    record.Close();
                    record.Dispose();
                    //record = null;
                }

                if (com != null)
                {
                    com.Dispose();
                }                
            }

            return producto;
        }

        // CURSO: 55. Método para actualizar un registro de la base de datos - P1
        /// <summary>
        /// Actualiza un registro completo de un producto en BD
        /// </summary>
        /// <param name="producto"></param>
        /// <returns>mensaje sobre la actualización</returns>
        [WebMethod]
        public string ActualizarProducto(Producto producto)
        {
            string result = "";
            SqlCommand com = null;

            if (!EnlaceSqlServer.ConectarSqlServer())
            {
                return "ActualizarProducto: Error al conectar con SQL Server 2019";
            }

            try
            {
                // CURSO: 56. Método para actualizar un registro de la base de datos - P2
                com = new SqlCommand("UPDATE productos SET"+
                    " nombre = @Nombre, "+
                    " precio = @Precio, "+
                    " stock = @Stock "+
                    " WHERE "+
                    " idproducto = @IdProducto ", EnlaceSqlServer.Conexion);

                // Establecer los parámetros de la consulta UPDATE 
                com.Parameters.AddWithValue("@Nombre", producto.Nombre);
                com.Parameters.AddWithValue("@Precio", producto.Precio);
                com.Parameters.AddWithValue("@Stock", producto.Stock);
                com.Parameters.AddWithValue("@IdProducto", producto.Idproducto);

                // ejecutar consulta SQL
                int updates = com.ExecuteNonQuery(); // porque hacemos UPDATE
                // solo debe actualizar un registro, idproducto es clave primaria
                if (updates == 1)
                {
                    result = "ActualizarProducto: Producto actualizado con éxito";
                }
                else
                {
                    result = "ActualizarProducto: Error al actualizar el producto";
                }
            }
            catch (Exception ex)
            {
                Funciones.Logs("ActualizarProducto", ex.Message);
                Funciones.Logs("ActualizarProducto_DEBUG", ex.StackTrace);
            }
            finally
            {
                if (com != null)
                {
                    com.Dispose();
                }
            }

            return result;
        }


        // CURSO: 59. Método para insertar un registro de la base de datos - P1
        /// <summary>
        /// 
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        [WebMethod]
        public int GuardarProducto(Producto producto)
        {
            int idproducto = 0;
            SqlCommand com = null;

            if (!EnlaceSqlServer.ConectarSqlServer())
            {
                return 0;
            }

            try
            {
                // CURSO: 60. Método para insertar un registro de la base de datos - P2
                com = new SqlCommand("INSERT INTO productos ( nombre, precio, stock ) " +
                    " VALUES ( @Nombre, @Precio, @Stock ) " +
                    " ; SELECT CAST(scope_identity() AS int) ", EnlaceSqlServer.Conexion);

                // CURSO: 61. Método para insertar un registro de la base de datos - P3
                com.Parameters.AddWithValue("@Nombre", producto.Nombre);
                com.Parameters.AddWithValue("@Precio", producto.Precio);
                com.Parameters.AddWithValue("@Stock", producto.Stock);

                idproducto = (int)com.ExecuteScalar(); // coger solo el id
            }
            catch (Exception ex)
            {
                Funciones.Logs("GuardarProducto", ex.Message);
                Funciones.Logs("GuardarProducto_DEBUG", ex.StackTrace);
            }
            finally
            {
                if (com != null)
                {
                    com.Dispose();
                }
            }

            return idproducto;
        }

        // CURSO: 63. Método para eliminar un registro de la base de datos - P1

        /// <summary>
        /// Elimina el producto seleccionado por id de la BD
        /// </summary>
        /// <param name="idproducto"></param>
        /// <returns></returns>
        [WebMethod]
        public string EliminarProducto(int idproducto)
        {
            string result = string.Empty;
            SqlCommand com = null;

            if (!EnlaceSqlServer.ConectarSqlServer())
            {
                return "";
            }

            try
            {
                // CURSO: 64. Método para eliminar un registro de la base de datos - P2
                com = new SqlCommand("DELETE FROM productos WHERE idproducto = " + idproducto, EnlaceSqlServer.Conexion);

                int eliminados = com.ExecuteNonQuery();

                if (eliminados == 1)
                {
                    result = "EliminarProducto: Producto eliminado con éxito";
                }
                else
                {
                    result = "EliminarProducto: El producto no existe";
                }

            }
            catch (Exception ex)
            {
                Funciones.Logs("EliminarProducto", ex.Message);
                Funciones.Logs("EliminarProducto_DEBUG", ex.StackTrace);                
            }
            finally
            {
                if (com != null)
                {
                    com.Dispose();
                }
            }

            return result;
        }

        // CURSO:

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        // CURSO:

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        // CURSO:

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        // CURSO:

        //[WebMethod]
        //public string HelloWorld()
        //{
        //    return "Hello World";
        //}

        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
        // CURSO:
    }
}
