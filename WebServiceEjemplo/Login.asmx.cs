using LoginLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace WebServiceEjemplo
{
    /// <summary>
    /// Descripción breve de Login
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Login : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorldLoin()
        {
            return "Hola a todos";
        }

        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [XmlInclude(typeof(LoginUser))]
        [XmlInclude(typeof(List<LoginUser>))]
        public List<LoginUser> WLogin(LoginUser oLogin)
        {
            ProcLogin oProcLogin = new ProcLogin();
            


            return oProcLogin.Inicio();
        }

        [WebMethod]
        public string WLogin2(LoginUser oLogin)
        {
            return oLogin.Pass;
        }
    }
}
