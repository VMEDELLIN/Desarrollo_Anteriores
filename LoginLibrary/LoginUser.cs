using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginLibrary
{
    public class LoginUser
    {

        public string User { get; set; }
        public string Pass { get; set; }

        /* public decimal Valox {
             set { _Valox = value; }
             get { return _Valox + 1; }
         }
         private decimal _Valox;

         public decimal Resultado
         {
             get {
                 return this._Valox * 0.10M;
             }
         }
        */
        public LoginUser() {
            
        }
    }
}
