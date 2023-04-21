using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;

namespace Validaciones
{
    class Program
    {
        static void Main(string[] args)
        {

            Create<HttpWebRequest>();
            Console.ReadKey();
        }
        public static  Type Create<T>()
        {
            Type oType = typeof(T);

            

            return typeof(T);
        }
    }
}
