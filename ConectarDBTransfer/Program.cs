using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConectarDBTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            DoMagic();

            WalletBLL w = new WalletBLL();
            w.ObtenerRemitenteSTP("646180204303000557");
            Console.ReadLine();
        }
        public static void DoMagic()
        {
            DateTime a = DateTime.Now;
            System.DateTime date1 = new DateTime(a.Year, a.Month, a.Day, a.Hour, a.Minute, a.Second);
            while (true)
            {
                Thread.Sleep(1000);
                DateTime b = DateTime.Now;
                System.DateTime date2 = new DateTime(b.Year, b.Month, b.Day, b.Hour, b.Minute, b.Second);
                System.TimeSpan diff2 = date2 - date1;
                if (diff2.Minutes > 2)
                {
                    Console.WriteLine("TimeOut");
                    break;
                }

            }
        }
    }
}
