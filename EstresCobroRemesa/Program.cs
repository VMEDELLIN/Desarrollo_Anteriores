using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstresCobroRemesa
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime DA = DateTime.Now;
            Console.WriteLine($"La fecha ToShortDateString es: {DA.ToShortDateString()}");
            Console.WriteLine($"La fecha ToShortTimeString es: {DA.ToShortTimeString()}");
            Console.WriteLine($"La fecha ToLongDateString es: {DA.ToLongDateString()}");
            Console.WriteLine($"La fecha ToLongTimeString es: {DA.ToLongTimeString()}");
            Console.ReadLine();
            //EstresCobro();
        }

        public static void EstresCobro() {
            Stopwatch watchIniciaEstres = null;
            try
            {
                List<ServiceRemesaLocalHost.TDRequest> lstRequest = new List<ServiceRemesaLocalHost.TDRequest>();
                Random ra = new Random();
                int Ticket = 6640;
                for (int i = 1; i < 50; i++)
                {
                    int val = ra.Next(1, 5);
                    lstRequest.Add(ObtenerRequest(val, (Ticket + i), (i + 1)));
                }
                IEnumerable<ServiceRemesaLocalHost.TDRequest> iRequest = lstRequest.AsEnumerable();

                watchIniciaEstres = Stopwatch.StartNew();
                int Exitosas = 0;
                var opcionesParalelismo = new ParallelOptions { MaxDegreeOfParallelism = 5 };
                ParallelLoopResult rp = Parallel.ForEach(iRequest, (Request) => {

                    Console.WriteLine($"Invocando {Request.Encabezado.Ticket}");
                    //ServiceRemesa.TDCustomerServicesSoapClient ws = new ServiceRemesa.TDCustomerServicesSoapClient();                    
                    //ServiceRemesa.TDResponse resp = ws.TD_CobraEnvio(Request);

                    ServiceRemesaLocalHost.TDCustomerServicesSoapClient ws = new ServiceRemesaLocalHost.TDCustomerServicesSoapClient();
                    ServiceRemesaLocalHost.TDResponse resp = ws.TD_CobraEnvio(Request);
                    if (resp.ErrorCode == 0)
                        Exitosas++;
                    Console.WriteLine($"Codigo {resp.ErrorCode} Mensaje {resp.ErrorMsg}");
                });
                if (rp.IsCompleted)
                {
                    watchIniciaEstres.Stop();
                    Console.WriteLine($"Parallel.ForEach loop | Time Taken : {watchIniciaEstres.ElapsedMilliseconds} ms.");
                    Console.WriteLine($"Exitosas : {Exitosas} .");
                }

            }
            catch (Exception ex)
            {
                watchIniciaEstres.Stop();
                Console.WriteLine($"Parallel.ForEach loop | Time Taken Error: {watchIniciaEstres.ElapsedMilliseconds} ms. Error=> {ex.Message} {ex.InnerException.ToString()}");
            }


            Console.ReadLine();


        }

        public static ServiceRemesaLocalHost.TDRequest ObtenerRequest(int i, int ticket, int c)
        {

            //|idAgente|1|idAgencia|1|Token|REDEFCOR|Caja||Ticket|3897|CveOperador|2121
            ServiceRemesaLocalHost.TDRequest req = new ServiceRemesaLocalHost.TDRequest();
            req.Encabezado = new ServiceRemesaLocalHost.Header();
            req.Encabezado.idAgente = 1;
            req.Encabezado.idAgencia = 1;
            req.Encabezado.Token = "REDEFCOR";
            req.Encabezado.Caja = "";
            req.Encabezado.Ticket = ticket.ToString();
            req.Encabezado.CveOperador = "2121";
            Console.WriteLine($"{c} Seleccionado: {i} Ticket {ticket}");



            switch (i)
            {
                case 1:
                    req.RequestData = "PFJlbWVzYT48Q2xhdmU+NDE5MTk2MTk2OTY8L0NsYXZlPjxTZXNpb24+PC9TZXNpb24+PE1vbnRvPjI1MDAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+PC9Vc2VySWQ+PFRlbGVmb25vPjgxMTcwMDAwMDY8L1RlbGVmb25vPjxOb21icmU+R1VBTEJFUlRPMTwvTm9tYnJlPjxQYXRlcm5vPkNhc3RybzwvUGF0ZXJubz48TWF0ZXJubz5NQVJUSU5FWjwvTWF0ZXJubz48RmVjTmFjPjE5ODktMTItMjY8L0ZlY05hYz48Tm9tYnJlQ29tcGxldG8+R1VBTEJFUlRPMSBDYXN0cm8gTUFSVElORVo8L05vbWJyZUNvbXBsZXRvPjxHZW5lcm8+MjwvR2VuZXJvPjxPY3VwYWNpb24+MTwvT2N1cGFjaW9uPjxDb3JyZW8+PC9Db3JyZW8+PE5hY2lvbmFsaWRhZD4xNjQ8L05hY2lvbmFsaWRhZD48UGFpc05hYz4xNjQ8L1BhaXNOYWM+PEVudGlkYWROYWM+MTwvRW50aWRhZE5hYz48THVnYXJOYWM+TWV4aWNvPC9MdWdhck5hYz48RG9jdW1lbnRvPjxJZGVudGlmaWNhY2lvbj48VGlwbz4xPC9UaXBvPjxOdW1lcm8+ODExNzAwMDAwMDAwNjwvTnVtZXJvPjxWZW5jaW1pZW50bz4yMDIzLTEyLTMxPC9WZW5jaW1pZW50bz48UGFpcz4xNjQ8L1BhaXM+PC9JZGVudGlmaWNhY2lvbj48L0RvY3VtZW50bz48RGlyZWNjaW9uPjxDYWxsZT5Nb250ZXMgUGlyaW5lb3M8L0NhbGxlPjxOdW1FeHQ+MTAyODwvTnVtRXh0PjxOdW1JbnQ+PC9OdW1JbnQ+PENvbG9uaWE+MjQ5MzwvQ29sb25pYT48Q2l1ZGFkPjE4PC9DaXVkYWQ+PHNDaXVkYWQ+R2FyY8OtYTwvc0NpdWRhZD48RXN0YWRvPjE5PC9Fc3RhZG8+PENQPjY2MDAwPC9DUD48UGFpcz4xNjQ8L1BhaXM+PC9EaXJlY2Npb24+PC9Vc3VhcmlvPjwvQmVuZWZpY2lhcmlvPjxPcGVyYWRvcj48Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPjxQYXRlcm5vPlJlZDwvUGF0ZXJubz48TWF0ZXJubz5TYW5jaGV6PC9NYXRlcm5vPjwvT3BlcmFkb3I+PC9SZW1lc2E+";
                    break;
                case 2:
                    req.RequestData = "PFJlbWVzYT48Q2xhdmU+NDE5MTk2MTk2OTc8L0NsYXZlPjxTZXNpb24+PC9TZXNpb24+PE1vbnRvPjI1MDAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+PC9Vc2VySWQ+PFRlbGVmb25vPjgxMTcwMDAwMDc8L1RlbGVmb25vPjxOb21icmU+R1VBTEJFUlRPMjwvTm9tYnJlPjxQYXRlcm5vPkNhc3RybzwvUGF0ZXJubz48TWF0ZXJubz5NQVJUSU5FWjwvTWF0ZXJubz48RmVjTmFjPjE5ODktMTItMjY8L0ZlY05hYz48Tm9tYnJlQ29tcGxldG8+R1VBTEJFUlRPMiBDYXN0cm8gTUFSVElORVo8L05vbWJyZUNvbXBsZXRvPjxHZW5lcm8+MjwvR2VuZXJvPjxPY3VwYWNpb24+MTwvT2N1cGFjaW9uPjxDb3JyZW8+PC9Db3JyZW8+PE5hY2lvbmFsaWRhZD4xNjQ8L05hY2lvbmFsaWRhZD48UGFpc05hYz4xNjQ8L1BhaXNOYWM+PEVudGlkYWROYWM+MTwvRW50aWRhZE5hYz48THVnYXJOYWM+TWV4aWNvPC9MdWdhck5hYz48RG9jdW1lbnRvPjxJZGVudGlmaWNhY2lvbj48VGlwbz4xPC9UaXBvPjxOdW1lcm8+ODExNzAwMDAwMDAwNzwvTnVtZXJvPjxWZW5jaW1pZW50bz4yMDIzLTEyLTMxPC9WZW5jaW1pZW50bz48UGFpcz4xNjQ8L1BhaXM+PC9JZGVudGlmaWNhY2lvbj48L0RvY3VtZW50bz48RGlyZWNjaW9uPjxDYWxsZT5Nb250ZXMgUGlyaW5lb3M8L0NhbGxlPjxOdW1FeHQ+MTAyODwvTnVtRXh0PjxOdW1JbnQ+PC9OdW1JbnQ+PENvbG9uaWE+MjQ5MzwvQ29sb25pYT48Q2l1ZGFkPjE4PC9DaXVkYWQ+PHNDaXVkYWQ+R2FyY8OtYTwvc0NpdWRhZD48RXN0YWRvPjE5PC9Fc3RhZG8+PENQPjY2MDAwPC9DUD48UGFpcz4xNjQ8L1BhaXM+PC9EaXJlY2Npb24+PC9Vc3VhcmlvPjwvQmVuZWZpY2lhcmlvPjxPcGVyYWRvcj48Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPjxQYXRlcm5vPlJlZDwvUGF0ZXJubz48TWF0ZXJubz5TYW5jaGV6PC9NYXRlcm5vPjwvT3BlcmFkb3I+PC9SZW1lc2E+";
                    break;
                case 3:
                    req.RequestData = "PFJlbWVzYT48Q2xhdmU+NDE5MTk2MTk2OTg8L0NsYXZlPjxTZXNpb24+PC9TZXNpb24+PE1vbnRvPjI1MDAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+PC9Vc2VySWQ+PFRlbGVmb25vPjgxMTcwMDAwMDg8L1RlbGVmb25vPjxOb21icmU+R1VBTEJFUlRPMzwvTm9tYnJlPjxQYXRlcm5vPkNhc3RybzwvUGF0ZXJubz48TWF0ZXJubz5NQVJUSU5FWjwvTWF0ZXJubz48RmVjTmFjPjE5ODktMTItMjY8L0ZlY05hYz48Tm9tYnJlQ29tcGxldG8+R1VBTEJFUlRPMyBDYXN0cm8gTUFSVElORVo8L05vbWJyZUNvbXBsZXRvPjxHZW5lcm8+MjwvR2VuZXJvPjxPY3VwYWNpb24+MTwvT2N1cGFjaW9uPjxDb3JyZW8+PC9Db3JyZW8+PE5hY2lvbmFsaWRhZD4xNjQ8L05hY2lvbmFsaWRhZD48UGFpc05hYz4xNjQ8L1BhaXNOYWM+PEVudGlkYWROYWM+MTwvRW50aWRhZE5hYz48THVnYXJOYWM+TWV4aWNvPC9MdWdhck5hYz48RG9jdW1lbnRvPjxJZGVudGlmaWNhY2lvbj48VGlwbz4xPC9UaXBvPjxOdW1lcm8+ODExNzAwMDAwMDAwODwvTnVtZXJvPjxWZW5jaW1pZW50bz4yMDIzLTEyLTMxPC9WZW5jaW1pZW50bz48UGFpcz4xNjQ8L1BhaXM+PC9JZGVudGlmaWNhY2lvbj48L0RvY3VtZW50bz48RGlyZWNjaW9uPjxDYWxsZT5Nb250ZXMgUGlyaW5lb3M8L0NhbGxlPjxOdW1FeHQ+MTAyODwvTnVtRXh0PjxOdW1JbnQ+PC9OdW1JbnQ+PENvbG9uaWE+MjQ5MzwvQ29sb25pYT48Q2l1ZGFkPjE4PC9DaXVkYWQ+PHNDaXVkYWQ+R2FyY8OtYTwvc0NpdWRhZD48RXN0YWRvPjE5PC9Fc3RhZG8+PENQPjY2MDAwPC9DUD48UGFpcz4xNjQ8L1BhaXM+PC9EaXJlY2Npb24+PC9Vc3VhcmlvPjwvQmVuZWZpY2lhcmlvPjxPcGVyYWRvcj48Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPjxQYXRlcm5vPlJlZDwvUGF0ZXJubz48TWF0ZXJubz5TYW5jaGV6PC9NYXRlcm5vPjwvT3BlcmFkb3I+PC9SZW1lc2E+";
                    break;
                case 4:
                    req.RequestData = "PFJlbWVzYT48Q2xhdmU+NDE5MTk2MTk2OTk8L0NsYXZlPjxTZXNpb24+PC9TZXNpb24+PE1vbnRvPjI1MDAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+PC9Vc2VySWQ+PFRlbGVmb25vPjgxMTcwMDAwMDk8L1RlbGVmb25vPjxOb21icmU+R1VBTEJFUlRPNDwvTm9tYnJlPjxQYXRlcm5vPkNhc3RybzwvUGF0ZXJubz48TWF0ZXJubz5NQVJUSU5FWjwvTWF0ZXJubz48RmVjTmFjPjE5ODktMTItMjY8L0ZlY05hYz48Tm9tYnJlQ29tcGxldG8+R1VBTEJFUlRPNCBDYXN0cm8gTUFSVElORVo8L05vbWJyZUNvbXBsZXRvPjxHZW5lcm8+MjwvR2VuZXJvPjxPY3VwYWNpb24+MTwvT2N1cGFjaW9uPjxDb3JyZW8+PC9Db3JyZW8+PE5hY2lvbmFsaWRhZD4xNjQ8L05hY2lvbmFsaWRhZD48UGFpc05hYz4xNjQ8L1BhaXNOYWM+PEVudGlkYWROYWM+MTwvRW50aWRhZE5hYz48THVnYXJOYWM+TWV4aWNvPC9MdWdhck5hYz48RG9jdW1lbnRvPjxJZGVudGlmaWNhY2lvbj48VGlwbz4xPC9UaXBvPjxOdW1lcm8+ODExNzAwMDAwMDAwOTwvTnVtZXJvPjxWZW5jaW1pZW50bz4yMDIzLTEyLTMxPC9WZW5jaW1pZW50bz48UGFpcz4xNjQ8L1BhaXM+PC9JZGVudGlmaWNhY2lvbj48L0RvY3VtZW50bz48RGlyZWNjaW9uPjxDYWxsZT5Nb250ZXMgUGlyaW5lb3M8L0NhbGxlPjxOdW1FeHQ+MTAyODwvTnVtRXh0PjxOdW1JbnQ+PC9OdW1JbnQ+PENvbG9uaWE+MjQ5MzwvQ29sb25pYT48Q2l1ZGFkPjE4PC9DaXVkYWQ+PHNDaXVkYWQ+R2FyY8OtYTwvc0NpdWRhZD48RXN0YWRvPjE5PC9Fc3RhZG8+PENQPjY2MDAwPC9DUD48UGFpcz4xNjQ8L1BhaXM+PC9EaXJlY2Npb24+PC9Vc3VhcmlvPjwvQmVuZWZpY2lhcmlvPjxPcGVyYWRvcj48Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPjxQYXRlcm5vPlJlZDwvUGF0ZXJubz48TWF0ZXJubz5TYW5jaGV6PC9NYXRlcm5vPjwvT3BlcmFkb3I+PC9SZW1lc2E+";
                    break;
                case 5:
                    req.RequestData = "PFJlbWVzYT48Q2xhdmU+NDE5MTk2MTk3MDA8L0NsYXZlPjxTZXNpb24+PC9TZXNpb24+PE1vbnRvPjI1MDAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+PC9Vc2VySWQ+PFRlbGVmb25vPjgxMTcwMDAwMTA8L1RlbGVmb25vPjxOb21icmU+R1VBTEJFUlRPNTwvTm9tYnJlPjxQYXRlcm5vPkNhc3RybzwvUGF0ZXJubz48TWF0ZXJubz5NQVJUSU5FWjwvTWF0ZXJubz48RmVjTmFjPjE5ODktMTItMjY8L0ZlY05hYz48Tm9tYnJlQ29tcGxldG8+R1VBTEJFUlRPNSBDYXN0cm8gTUFSVElORVo8L05vbWJyZUNvbXBsZXRvPjxHZW5lcm8+MjwvR2VuZXJvPjxPY3VwYWNpb24+MTwvT2N1cGFjaW9uPjxDb3JyZW8+PC9Db3JyZW8+PE5hY2lvbmFsaWRhZD4xNjQ8L05hY2lvbmFsaWRhZD48UGFpc05hYz4xNjQ8L1BhaXNOYWM+PEVudGlkYWROYWM+MTwvRW50aWRhZE5hYz48THVnYXJOYWM+TWV4aWNvPC9MdWdhck5hYz48RG9jdW1lbnRvPjxJZGVudGlmaWNhY2lvbj48VGlwbz4xPC9UaXBvPjxOdW1lcm8+ODExNzAwMDAwMDAxMDwvTnVtZXJvPjxWZW5jaW1pZW50bz4yMDIzLTEyLTMxPC9WZW5jaW1pZW50bz48UGFpcz4xNjQ8L1BhaXM+PC9JZGVudGlmaWNhY2lvbj48L0RvY3VtZW50bz48RGlyZWNjaW9uPjxDYWxsZT5Nb250ZXMgUGlyaW5lb3M8L0NhbGxlPjxOdW1FeHQ+MTAyODwvTnVtRXh0PjxOdW1JbnQ+PC9OdW1JbnQ+PENvbG9uaWE+MjQ5MzwvQ29sb25pYT48Q2l1ZGFkPjE4PC9DaXVkYWQ+PHNDaXVkYWQ+R2FyY8OtYTwvc0NpdWRhZD48RXN0YWRvPjE5PC9Fc3RhZG8+PENQPjY2MDAwPC9DUD48UGFpcz4xNjQ8L1BhaXM+PC9EaXJlY2Npb24+PC9Vc3VhcmlvPjwvQmVuZWZpY2lhcmlvPjxPcGVyYWRvcj48Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPjxQYXRlcm5vPlJlZDwvUGF0ZXJubz48TWF0ZXJubz5TYW5jaGV6PC9NYXRlcm5vPjwvT3BlcmFkb3I+PC9SZW1lc2E+";
                    break;
                default:
                    break;
            }
            return req;
        }
    }
}
