using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios//.system
{
    public class Producto
    {
        // CURSO: 50. Clase Producto
        public int Idproducto { get; set; }

        public string Nombre { get; set; }

        public double Precio { get; set; }

        public int Stock { get; set; }
    }
}