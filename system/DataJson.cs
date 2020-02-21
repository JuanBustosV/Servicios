using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios
{
    public class DataJson
    {
        // Herramienta que ayuda para transformar JSONs a CSharp
        // http://json2csharp.com/
        // y así poder deserializar el string (que viene como JSON) y llevarlo a clase

        public string deporte { get; set; }
        public List<Equipos> equipos { get; set; }
    }

    //{"deporte":"Fútbol","equipos":[{"Nombre":"Manchester United","Pais":"Inglaterra"},{"Nombre":"Betis","Pais":"España"}]}
}