using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp2
{
    public class Saldo
    {
        private decimal _catidad { get; set; }
        public Saldo(decimal cantidad) {
            _catidad = cantidad;
        }
        public void Transferir(decimal val)
        {
            if (_catidad > val)
                _catidad = _catidad - val;
            //else
            //{
            //    Console.WriteLine($"No se puede realizar la tranferencia de {val} el saldo actual es de {_catidad}");
            //    throw new AbandonedMutexException($"Saldo insufuciente {_catidad}");
            //}

        }
        public decimal ObtenerSaldo() => _catidad;
    }
    public class Agencia {
        private Saldo _saldo;
        private int _idAgencia;
        private Mutex _mutex;
        public Agencia(Saldo saldo, int idAgencia, Mutex mutex) {
            _saldo = saldo;
            _idAgencia = idAgencia;
            _mutex = mutex;
        }
        public void Transferir() {
            decimal val = 0;
            Random r = new Random();
            val = Convert.ToDecimal(r.Next(50, 100));

            while (_saldo.ObtenerSaldo() > val)
            {                
                try
                {                    
                    _mutex.WaitOne();

                    if (_saldo.ObtenerSaldo() > val)
                    {
                        Console.WriteLine($"Agencia => {_idAgencia} Saldo existente {_saldo.ObtenerSaldo()} y transfiere {val}");
                        _saldo.Transferir(val);
                        Console.WriteLine($"Agencia => {_idAgencia} dejo el  saldo en {_saldo.ObtenerSaldo()}");
                        _mutex.ReleaseMutex();
                    }
                }
                catch (AbandonedMutexException ex)
                {
                    Console.WriteLine($"La agencia {_idAgencia} intentaba transferir {val} pero el saldo no es suficiente => {ex.Message}");
                }
            }
        }
    }
}
