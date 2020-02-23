using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios//.system para que se acceda a la clase desde WebService.asmx
{
    public class AuthUser : System.Web.Services.Protocols.SoapHeader
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            if (this.UserName == "admin" && this.Password == "1234")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}