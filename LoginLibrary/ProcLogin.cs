using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginLibrary
{
    public class ProcLogin
    {
        public ProcLogin() {
            
        }

        public List<LoginUser> Inicio() {
            List<LoginUser> ListaLogin = new List<LoginUser>();

            ListaLogin.Add(new LoginUser() { User = "user1", Pass = "pass1" });
            ListaLogin.Add(new LoginUser() { User = "user2", Pass = "pass2" });
            ListaLogin.Add(new LoginUser() { User = "user3", Pass = "pass3" });

            return ListaLogin;
        }
    }
}
