using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Mutex mut = new Mutex();
            Saldo saldo = new Saldo(10000);
            List<Agencia> Agencias = new List<Agencia>()
            {
                new Agencia(saldo,1,mut),
                new Agencia(saldo,2,mut),
                new Agencia(saldo,3,mut),
                new Agencia(saldo,4,mut),
                new Agencia(saldo,5,mut),
                new Agencia(saldo,6,mut),
                new Agencia(saldo,7,mut),
                new Agencia(saldo,8,mut),
                new Agencia(saldo,9,mut),
                new Agencia(saldo,10,mut),
                new Agencia(saldo,11,mut),
                new Agencia(saldo,12,mut),
                new Agencia(saldo,13,mut),
            };
            foreach (var agencia in Agencias)
            {
                var thread = new Thread(new ThreadStart(agencia.Transferir));
                thread.Start();
            }

            Console.ReadLine();
        }
    }
}
