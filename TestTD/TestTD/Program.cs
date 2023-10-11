using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTD
{
    class Program
    {
        private static string logDirectory = "Log/TransferDirecto/Gateway2"; // Nombre de la carpeta para los archivos de registro
        private static string logFileName = "TCPGatewayServer";
        private static LogEventLevel logLevel = LogEventLevel.Information; // Nivel de log deseado

        //"TCPGatewayServer", "Log/TransferDirecto/Gateway"
        private static int Tick = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Capture ticket");
            Tick = Convert.ToInt32(Console.ReadLine()); 

            Menu(0);
        }
        public static void Menu( int op) {
            //Ejecución paralela al WB ts
            //ConfigureLogging();
            //Log.Information("Este es un mensaje de ejemplo.");

            //SeriLogTD.setLogClass(logFileName, logDirectory, LogEventLevel.Information);
            //Parallel.For(0, 1, i =>
            //   {
            //       //SBTDService.TDResponse res =  srv.TD_BuscaEnvio(R);
            //       //Console.WriteLine($"Inicia número {i}");
            //       Probar(i.ToString("D5"));
            //       Console.WriteLine($"Finaliza número {i}");
            //   });
            //SeriLogTD.LogDispose();
            switch (op)
            {
                case 0:
                    Console.WriteLine("Seleccione el TEST");
                    Console.WriteLine("1    -TEST CONSULTA");
                    Console.WriteLine("2    -TEST BUSCA USUARIO");
                    Console.WriteLine("3    -TEST COBRO");
                    var opcion = Convert.ToInt32(Console.ReadLine());
                    Menu(opcion);
                    break;
                case 1:
                    Console.WriteLine("***************************************************");
                    Console.WriteLine("Introduce el núumero de peticiones paralelas");
                    int pet =Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(".......");
                    Parallel.For(0, pet, i =>
                    {
                        EjecutarConsulta(i);
                        EjecutarConsultalocal(i);
                    });
                    Console.WriteLine("---FIN---");
                    Console.WriteLine("***************************************************");
                    Menu(0);
                    break;
                case 3:
                    Console.WriteLine("***************************************************");
                    Console.WriteLine("Introduce el núumero de peticiones paralelas");
                    int pet1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(".......");
                    Parallel.For(0, pet1, i =>
                    {
                        EjecutarCobro(i);
                    });
                    Console.WriteLine("---FIN---");
                    Console.WriteLine("***************************************************");
                    Menu(0);
                    break;
                default:
                    break;
            }

            


            //int numeroCuenta = 0;
            //try
            //{




            //    int clienteId = 5;
            //    Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            //    string Token = Encrypt.CreateMD5("02cbad694da85e57af95f1d9df613911" + unixTimestamp + clienteId);
            //    string descripcion = "Cuenta para Transpay";

            //    ServicioBilletera.Service wsBilletera = new ServicioBilletera.Service();
            //    //string respuesta = wsBilletera.RegistraCuenta(clienteId, unixTimestamp, Token, telefono, correo, "", "", descripcion, empresa);
            //    //{"Correo":"vmedellin@transferdirecto.com","Empresa":{"RazonSocial":"Direct2Global Test","NombreComercial":"Direct2Global Test","FechaConstitutiva":"2010-01-01",
            //    //"RFC":"RDL0904102F4","Telefono":"8116020430","CorreoElectronico":"vmedellin@transferdirecto.com","GiroNegocio":20,"TipoSociedad":1,"NombreCalle":"Blvd. Antonio L. Rodiguez",
            //    //"NumeroExterior":"3058","NumerioInterior":"1","CodigoPostal":"64650","Colonia":"86525","Estado":0,"Municipio":0,"EstadoRegistro":null,"IdEstado":14,"IdCiudad":986,"ValRazonSocail":null,
            //    //"ValNombreComercial":null,"ValFechaConstitutiva":null,"ValFechaExpiracion":null,"ValRFC":null,"ValTelefono":null,"ValCorreo":null,"ValGiroNegocio":null,"ValTipoSociedad":null,
            //    //"ValCalle":null,"ValNumExterior":null,"ValNumInterior":null,"ValCodigoPostal":null,"ValEstado":null,"ValMunicipio":null,"ValColonia":null,"code":0,"message":null,"validado":null,
            //    //"idEntidad":null}}
            //    string respuesta = wsBilletera.ConsultaSaldo(clienteId, unixTimestamp, Token, 1240100004);
            //    Console.WriteLine($"Esto respondio {respuesta}");
            //    //ResponseWallet response;
            //    //response = JsonConvert.DeserializeObject<ResponseWallet>(respuesta);
            //    //if (response.ErrorCode == 0)
            //    //{
            //    //    numeroCuenta = response.NumCuenta;
            //    //    correcto = true;
            //    //    LogClass.LogInfo("APITransfer", "EmpresaBLL RegistrarCuentaWallet", response.ErrorMsg);
            //    //}
            //    //else
            //    //{
            //    //    correcto = false;
            //    //    LogClass.LogError("APITransfer", "EmpresaBLL RegistrarCuentaWallet", response.ErrorMsg);
            //    //}
            //}
            ////catch (JsonException ex)
            ////{
            ////    correcto = false;
            ////    LogClass.LogError("APITransfer", "EmpresaBLL RegistrarCuentaWallet", "JsonException ex :" + ex.Message);
            ////}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error {ex.Message}");
            //    //correcto = false;
            //    //LogClass.LogError("APITransfer", "EmpresaBLL RegistrarCuentaWallet", "Exception ex :" + ex.Message);
            //}
            ////LogClass.LogInfo("APITransfer", "EmpresaBLL registarEnPayCash", "Begin registarEnPayCash Function");
            Console.WriteLine("Fin");
            Console.ReadLine();
            Log.CloseAndFlush();
        }
        public static async Task EjecutarCobro(int i)
        {
            //Console.WriteLine($"Ejecutar Ini {i}");
            //TDService.TDCustomerServices srv = new TDService.TDCustomerServices();
            //TDService.TDRequest R = new TDService.TDRequest();
            //R.Encabezado = new TDService.Header();

            MTDService.TDCustomerServices srv = new MTDService.TDCustomerServices();
            MTDService.TDRequest R = new MTDService.TDRequest();
            R.Encabezado = new MTDService.Header();

            R.Encabezado.idAgente = 1;
            R.Encabezado.idAgencia = 1;
            R.Encabezado.Token = "REDEFCOR";
            R.Encabezado.Caja = "";
            R.Encabezado.Ticket = $"{Tick++}";
            R.Encabezado.CveOperador = "2121";
            //R.RequestData = "PFJlbWVzYT4KICAgICAgICAgICAgICAgIDxDbGF2ZT4zNjIyOTc3MDEzMTwvQ2xhdmU+CiAgICAgICAgICAgICAgICA8TW9udG8+MDwvTW9udG8+CiAgICAgICAgICAgICAgICA8SWRFbWlzb3I+MDwvSWRFbWlzb3I+CiAgICAgICAgICAgICAgICA8SWRPcmlnZW4+MTwvSWRPcmlnZW4+CiAgICAgICAgICAgICAgICA8SWRBZ2VuY2lhPjE8L0lkQWdlbmNpYT4KICAgICAgICAgICAgICAgIDxPcGVyYWRvcj4KICAgICAgICAgICAgICAgICAgICA8Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPgogICAgICAgICAgICAgICAgICAgIDxQYXRlcm5vPlJlZDwvUGF0ZXJubz4KICAgICAgICAgICAgICAgICAgICA8TWF0ZXJubz5TYW5jaGV6PC9NYXRlcm5vPgogICAgICAgICAgICAgICAgPC9PcGVyYWRvcj4KICAgICAgICAgICAgPC9SZW1lc2E+";
            //R.RequestData = "PFJlbWVzYT48Q2xhdmU+U0c3MTE3NzQ3MjA5NDwvQ2xhdmU+PFNlc2lvbj48L1Nlc2lvbj48TW9udG8+MzQwMjAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+MjEwNzA5PC9Vc2VySWQ+PFRlbGVmb25vPjQxMzExNzU4MjQ8L1RlbGVmb25vPjxOb21icmU+QUxFWEFORFJJQTwvTm9tYnJlPjxQYXRlcm5vPlBBVElOTzwvUGF0ZXJubz48TWF0ZXJubz5QQVRJTk88L01hdGVybm8+PEZlY05hYz4xOTk5LTAxLTE5PC9GZWNOYWM+PE5vbWJyZUNvbXBsZXRvPmFsZXhhbmRyaWEgcGF0aW5vIHBhdGlubzwvTm9tYnJlQ29tcGxldG8+PEdlbmVybz4xPC9HZW5lcm8+PE9jdXBhY2lvbj4xPC9PY3VwYWNpb24+PENvcnJlbz5ub3RpZW5lQGdtYWlsLmNvbTwvQ29ycmVvPjxOYWNpb25hbGlkYWQ+MTY0PC9OYWNpb25hbGlkYWQ+PFBhaXNOYWM+MTY0PC9QYWlzTmFjPjxFbnRpZGFkTmFjPjExPC9FbnRpZGFkTmFjPjxMdWdhck5hYz5BUEFTRU8gRUwgQUxUTzwvTHVnYXJOYWM+PERvY3VtZW50bz48SWRlbnRpZmljYWNpb24+PFRpcG8+MTwvVGlwbz48TnVtZXJvPjAyNzUxMTc0MTU1Mjc8L051bWVybz48VmVuY2ltaWVudG8+MjAzMi0xMi0zMTwvVmVuY2ltaWVudG8+PFBhaXM+MTY0PC9QYWlzPjwvSWRlbnRpZmljYWNpb24+PC9Eb2N1bWVudG8+PERpcmVjY2lvbj48Q2FsbGU+Q0FSUiBBUEFTRU8gSkVSRUNVQVJPPC9DYWxsZT48TnVtRXh0PjIwNjwvTnVtRXh0PjxOdW1JbnQ+PC9OdW1JbnQ+PENvbG9uaWE+MjkxNjwvQ29sb25pYT48Q2l1ZGFkPjQ8L0NpdWRhZD48c0NpdWRhZD5BcGFzZW8gZWwgQWx0bzwvc0NpdWRhZD48RXN0YWRvPjExPC9Fc3RhZG8+PENQPjM4NTI3PC9DUD48UGFpcz4xNjQ8L1BhaXM+PC9EaXJlY2Npb24+PC9Vc3VhcmlvPjwvQmVuZWZpY2lhcmlvPjxPcGVyYWRvcj48Tm9tYnJlPk1BUklDRUxBPC9Ob21icmU+PFBhdGVybm8+Q0VSVkFOVEVTPC9QYXRlcm5vPjxNYXRlcm5vPlZFR0E8L01hdGVybm8+PC9PcGVyYWRvcj48L1JlbWVzYT4=";

            //COBRO
            R.RequestData = "PFJlbWVzYT48Q2xhdmU+U0c3MTE3NzQ3MjA5NDwvQ2xhdmU+PFNlc2lvbj48L1Nlc2lvbj48TW9udG8+MzQwMjAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+Mjc5MDQ1PC9Vc2VySWQ+PFRlbGVmb25vPjgxMTYwMjA0MzU8L1RlbGVmb25vPjxOb21icmU+QUxFWEFORFJJQTwvTm9tYnJlPjxQYXRlcm5vPlBBVElOTzwvUGF0ZXJubz48TWF0ZXJubz5QQVRJTk88L01hdGVybm8+PEZlY05hYz4yMDAwLTA0LTAyPC9GZWNOYWM+PE5vbWJyZUNvbXBsZXRvPkFMRVhBTkRSSUEgUEFUSU5PIFBBVElOTzwvTm9tYnJlQ29tcGxldG8+PEdlbmVybz4xPC9HZW5lcm8+PE9jdXBhY2lvbj4xPC9PY3VwYWNpb24+PENvcnJlbz48L0NvcnJlbz48TmFjaW9uYWxpZGFkPjE2NDwvTmFjaW9uYWxpZGFkPjxQYWlzTmFjPjE2NDwvUGFpc05hYz48RW50aWRhZE5hYz4xPC9FbnRpZGFkTmFjPjxMdWdhck5hYz5NZXhpY288L0x1Z2FyTmFjPjxEb2N1bWVudG8+PElkZW50aWZpY2FjaW9uPjxUaXBvPjE8L1RpcG8+PE51bWVybz44MTE2MDIwNDM1MTIzPC9OdW1lcm8+PFZlbmNpbWllbnRvPjIwMjMtMTItMzE8L1ZlbmNpbWllbnRvPjxQYWlzPjE2NDwvUGFpcz48L0lkZW50aWZpY2FjaW9uPjwvRG9jdW1lbnRvPjxEaXJlY2Npb24+PENhbGxlPkNhbGxlIFZpbGxhIG5vdmE8L0NhbGxlPjxOdW1FeHQ+OTM8L051bUV4dD48TnVtSW50PjU8L051bUludD48Q29sb25pYT4yNDkzPC9Db2xvbmlhPjxDaXVkYWQ+MTg8L0NpdWRhZD48c0NpdWRhZD5HYXJjw61hPC9zQ2l1ZGFkPjxFc3RhZG8+MTk8L0VzdGFkbz48Q1A+NjYwMDA8L0NQPjxQYWlzPjE2NDwvUGFpcz48L0RpcmVjY2lvbj48L1VzdWFyaW8+PC9CZW5lZmljaWFyaW8+PE9wZXJhZG9yPjxOb21icmU+TWNLYWluPC9Ob21icmU+PFBhdGVybm8+UmVkPC9QYXRlcm5vPjxNYXRlcm5vPlNhbmNoZXo8L01hdGVybm8+PC9PcGVyYWRvcj48L1JlbWVzYT4=";

            //CONSULTA
            //R.RequestData = "PFJlbWVzYT4NCiAgICAgICAgICAgICAgICA8Q2xhdmU+U0c3MTE3NzQ3MzAwMDwvQ2xhdmU+DQogICAgICAgICAgICAgICAgPE1vbnRvPjA8L01vbnRvPg0KICAgICAgICAgICAgICAgIDxJZEVtaXNvcj4wPC9JZEVtaXNvcj4NCiAgICAgICAgICAgICAgICA8SWRPcmlnZW4+MTwvSWRPcmlnZW4+DQogICAgICAgICAgICAgICAgPElkQWdlbmNpYT4xPC9JZEFnZW5jaWE+DQogICAgICAgICAgICAgICAgPE9wZXJhZG9yPg0KICAgICAgICAgICAgICAgICAgICA8Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPg0KICAgICAgICAgICAgICAgICAgICA8UGF0ZXJubz5SZWQ8L1BhdGVybm8+DQogICAgICAgICAgICAgICAgICAgIDxNYXRlcm5vPlNhbmNoZXo8L01hdGVybm8+DQogICAgICAgICAgICAgICAgPC9PcGVyYWRvcj4NCiAgICAgICAgICAgIDwvUmVtZXNhPg==";

            //SBTDService.TDResponse res =
            //srv.TD_BuscaEnvioAsync(R);
            srv.TD_CobraEnvioAsync(R);
            //Console.WriteLine($"Ejecutar Fin {i}");
        }

        public static async Task EjecutarConsulta(int i) {
            //Console.WriteLine($"Ejecutar Ini {i}");
            //TDService.TDCustomerServices srv = new TDService.TDCustomerServices();
            //TDService.TDRequest R = new TDService.TDRequest();
            //R.Encabezado = new TDService.Header();

            MTDService.TDCustomerServices srv = new MTDService.TDCustomerServices();
            MTDService.TDRequest R = new MTDService.TDRequest();
            R.Encabezado = new MTDService.Header();

            R.Encabezado.idAgente = 1;
            R.Encabezado.idAgencia = 1;
            R.Encabezado.Token = "REDEFCOR";
            R.Encabezado.Caja = "";
            R.Encabezado.Ticket = $"{Tick++}";
            R.Encabezado.CveOperador = "2121";
            //R.RequestData = "PFJlbWVzYT4KICAgICAgICAgICAgICAgIDxDbGF2ZT4zNjIyOTc3MDEzMTwvQ2xhdmU+CiAgICAgICAgICAgICAgICA8TW9udG8+MDwvTW9udG8+CiAgICAgICAgICAgICAgICA8SWRFbWlzb3I+MDwvSWRFbWlzb3I+CiAgICAgICAgICAgICAgICA8SWRPcmlnZW4+MTwvSWRPcmlnZW4+CiAgICAgICAgICAgICAgICA8SWRBZ2VuY2lhPjE8L0lkQWdlbmNpYT4KICAgICAgICAgICAgICAgIDxPcGVyYWRvcj4KICAgICAgICAgICAgICAgICAgICA8Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPgogICAgICAgICAgICAgICAgICAgIDxQYXRlcm5vPlJlZDwvUGF0ZXJubz4KICAgICAgICAgICAgICAgICAgICA8TWF0ZXJubz5TYW5jaGV6PC9NYXRlcm5vPgogICAgICAgICAgICAgICAgPC9PcGVyYWRvcj4KICAgICAgICAgICAgPC9SZW1lc2E+";
            //R.RequestData = "PFJlbWVzYT48Q2xhdmU+U0c3MTE3NzQ3MjA5NDwvQ2xhdmU+PFNlc2lvbj48L1Nlc2lvbj48TW9udG8+MzQwMjAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+MjEwNzA5PC9Vc2VySWQ+PFRlbGVmb25vPjQxMzExNzU4MjQ8L1RlbGVmb25vPjxOb21icmU+QUxFWEFORFJJQTwvTm9tYnJlPjxQYXRlcm5vPlBBVElOTzwvUGF0ZXJubz48TWF0ZXJubz5QQVRJTk88L01hdGVybm8+PEZlY05hYz4xOTk5LTAxLTE5PC9GZWNOYWM+PE5vbWJyZUNvbXBsZXRvPmFsZXhhbmRyaWEgcGF0aW5vIHBhdGlubzwvTm9tYnJlQ29tcGxldG8+PEdlbmVybz4xPC9HZW5lcm8+PE9jdXBhY2lvbj4xPC9PY3VwYWNpb24+PENvcnJlbz5ub3RpZW5lQGdtYWlsLmNvbTwvQ29ycmVvPjxOYWNpb25hbGlkYWQ+MTY0PC9OYWNpb25hbGlkYWQ+PFBhaXNOYWM+MTY0PC9QYWlzTmFjPjxFbnRpZGFkTmFjPjExPC9FbnRpZGFkTmFjPjxMdWdhck5hYz5BUEFTRU8gRUwgQUxUTzwvTHVnYXJOYWM+PERvY3VtZW50bz48SWRlbnRpZmljYWNpb24+PFRpcG8+MTwvVGlwbz48TnVtZXJvPjAyNzUxMTc0MTU1Mjc8L051bWVybz48VmVuY2ltaWVudG8+MjAzMi0xMi0zMTwvVmVuY2ltaWVudG8+PFBhaXM+MTY0PC9QYWlzPjwvSWRlbnRpZmljYWNpb24+PC9Eb2N1bWVudG8+PERpcmVjY2lvbj48Q2FsbGU+Q0FSUiBBUEFTRU8gSkVSRUNVQVJPPC9DYWxsZT48TnVtRXh0PjIwNjwvTnVtRXh0PjxOdW1JbnQ+PC9OdW1JbnQ+PENvbG9uaWE+MjkxNjwvQ29sb25pYT48Q2l1ZGFkPjQ8L0NpdWRhZD48c0NpdWRhZD5BcGFzZW8gZWwgQWx0bzwvc0NpdWRhZD48RXN0YWRvPjExPC9Fc3RhZG8+PENQPjM4NTI3PC9DUD48UGFpcz4xNjQ8L1BhaXM+PC9EaXJlY2Npb24+PC9Vc3VhcmlvPjwvQmVuZWZpY2lhcmlvPjxPcGVyYWRvcj48Tm9tYnJlPk1BUklDRUxBPC9Ob21icmU+PFBhdGVybm8+Q0VSVkFOVEVTPC9QYXRlcm5vPjxNYXRlcm5vPlZFR0E8L01hdGVybm8+PC9PcGVyYWRvcj48L1JlbWVzYT4=";

            //COBRO
            //R.RequestData = "PFJlbWVzYT48Q2xhdmU+U0c3MTE3NzQ3MjA5NDwvQ2xhdmU+PFNlc2lvbj48L1Nlc2lvbj48TW9udG8+MzQwMjAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+Mjc5MDQ1PC9Vc2VySWQ+PFRlbGVmb25vPjgxMTYwMjA0MzU8L1RlbGVmb25vPjxOb21icmU+QUxFWEFORFJJQTwvTm9tYnJlPjxQYXRlcm5vPlBBVElOTzwvUGF0ZXJubz48TWF0ZXJubz5QQVRJTk88L01hdGVybm8+PEZlY05hYz4yMDAwLTA0LTAyPC9GZWNOYWM+PE5vbWJyZUNvbXBsZXRvPkFMRVhBTkRSSUEgUEFUSU5PIFBBVElOTzwvTm9tYnJlQ29tcGxldG8+PEdlbmVybz4xPC9HZW5lcm8+PE9jdXBhY2lvbj4xPC9PY3VwYWNpb24+PENvcnJlbz48L0NvcnJlbz48TmFjaW9uYWxpZGFkPjE2NDwvTmFjaW9uYWxpZGFkPjxQYWlzTmFjPjE2NDwvUGFpc05hYz48RW50aWRhZE5hYz4xPC9FbnRpZGFkTmFjPjxMdWdhck5hYz5NZXhpY288L0x1Z2FyTmFjPjxEb2N1bWVudG8+PElkZW50aWZpY2FjaW9uPjxUaXBvPjE8L1RpcG8+PE51bWVybz44MTE2MDIwNDM1MTIzPC9OdW1lcm8+PFZlbmNpbWllbnRvPjIwMjMtMTItMzE8L1ZlbmNpbWllbnRvPjxQYWlzPjE2NDwvUGFpcz48L0lkZW50aWZpY2FjaW9uPjwvRG9jdW1lbnRvPjxEaXJlY2Npb24+PENhbGxlPkNhbGxlIFZpbGxhIG5vdmE8L0NhbGxlPjxOdW1FeHQ+OTM8L051bUV4dD48TnVtSW50PjU8L051bUludD48Q29sb25pYT4yNDkzPC9Db2xvbmlhPjxDaXVkYWQ+MTg8L0NpdWRhZD48c0NpdWRhZD5HYXJjw61hPC9zQ2l1ZGFkPjxFc3RhZG8+MTk8L0VzdGFkbz48Q1A+NjYwMDA8L0NQPjxQYWlzPjE2NDwvUGFpcz48L0RpcmVjY2lvbj48L1VzdWFyaW8+PC9CZW5lZmljaWFyaW8+PE9wZXJhZG9yPjxOb21icmU+TWNLYWluPC9Ob21icmU+PFBhdGVybm8+UmVkPC9QYXRlcm5vPjxNYXRlcm5vPlNhbmNoZXo8L01hdGVybm8+PC9PcGVyYWRvcj48L1JlbWVzYT4=";

            //CONSULTA
            R.RequestData = "PFJlbWVzYT4NCiAgICAgICAgICAgICAgICA8Q2xhdmU+U0c3MTE3NzQ3MzAwMDwvQ2xhdmU+DQogICAgICAgICAgICAgICAgPE1vbnRvPjA8L01vbnRvPg0KICAgICAgICAgICAgICAgIDxJZEVtaXNvcj4wPC9JZEVtaXNvcj4NCiAgICAgICAgICAgICAgICA8SWRPcmlnZW4+MTwvSWRPcmlnZW4+DQogICAgICAgICAgICAgICAgPElkQWdlbmNpYT4xPC9JZEFnZW5jaWE+DQogICAgICAgICAgICAgICAgPE9wZXJhZG9yPg0KICAgICAgICAgICAgICAgICAgICA8Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPg0KICAgICAgICAgICAgICAgICAgICA8UGF0ZXJubz5SZWQ8L1BhdGVybm8+DQogICAgICAgICAgICAgICAgICAgIDxNYXRlcm5vPlNhbmNoZXo8L01hdGVybm8+DQogICAgICAgICAgICAgICAgPC9PcGVyYWRvcj4NCiAgICAgICAgICAgIDwvUmVtZXNhPg==";

            //SBTDService.TDResponse res =
            srv.TD_BuscaEnvioAsync(R);
            //srv.TD_CobraEnvioAsync(R);
            //Console.WriteLine($"Ejecutar Fin {i}");
        }

        public static async Task EjecutarConsultalocal(int i)
        {
            //Console.WriteLine($"Ejecutar Ini {i}");
           TDService.TDCustomerServices srv = new TDService.TDCustomerServices();
           TDService.TDRequest R = new TDService.TDRequest();
           R.Encabezado = new TDService.Header();

           

            R.Encabezado.idAgente = 1;
            R.Encabezado.idAgencia = 1;
            R.Encabezado.Token = "REDEFCOR";
            R.Encabezado.Caja = "";
            R.Encabezado.Ticket = $"{Tick++}";
            R.Encabezado.CveOperador = "2121";
            //R.RequestData = "PFJlbWVzYT4KICAgICAgICAgICAgICAgIDxDbGF2ZT4zNjIyOTc3MDEzMTwvQ2xhdmU+CiAgICAgICAgICAgICAgICA8TW9udG8+MDwvTW9udG8+CiAgICAgICAgICAgICAgICA8SWRFbWlzb3I+MDwvSWRFbWlzb3I+CiAgICAgICAgICAgICAgICA8SWRPcmlnZW4+MTwvSWRPcmlnZW4+CiAgICAgICAgICAgICAgICA8SWRBZ2VuY2lhPjE8L0lkQWdlbmNpYT4KICAgICAgICAgICAgICAgIDxPcGVyYWRvcj4KICAgICAgICAgICAgICAgICAgICA8Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPgogICAgICAgICAgICAgICAgICAgIDxQYXRlcm5vPlJlZDwvUGF0ZXJubz4KICAgICAgICAgICAgICAgICAgICA8TWF0ZXJubz5TYW5jaGV6PC9NYXRlcm5vPgogICAgICAgICAgICAgICAgPC9PcGVyYWRvcj4KICAgICAgICAgICAgPC9SZW1lc2E+";
            //R.RequestData = "PFJlbWVzYT48Q2xhdmU+U0c3MTE3NzQ3MjA5NDwvQ2xhdmU+PFNlc2lvbj48L1Nlc2lvbj48TW9udG8+MzQwMjAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+MjEwNzA5PC9Vc2VySWQ+PFRlbGVmb25vPjQxMzExNzU4MjQ8L1RlbGVmb25vPjxOb21icmU+QUxFWEFORFJJQTwvTm9tYnJlPjxQYXRlcm5vPlBBVElOTzwvUGF0ZXJubz48TWF0ZXJubz5QQVRJTk88L01hdGVybm8+PEZlY05hYz4xOTk5LTAxLTE5PC9GZWNOYWM+PE5vbWJyZUNvbXBsZXRvPmFsZXhhbmRyaWEgcGF0aW5vIHBhdGlubzwvTm9tYnJlQ29tcGxldG8+PEdlbmVybz4xPC9HZW5lcm8+PE9jdXBhY2lvbj4xPC9PY3VwYWNpb24+PENvcnJlbz5ub3RpZW5lQGdtYWlsLmNvbTwvQ29ycmVvPjxOYWNpb25hbGlkYWQ+MTY0PC9OYWNpb25hbGlkYWQ+PFBhaXNOYWM+MTY0PC9QYWlzTmFjPjxFbnRpZGFkTmFjPjExPC9FbnRpZGFkTmFjPjxMdWdhck5hYz5BUEFTRU8gRUwgQUxUTzwvTHVnYXJOYWM+PERvY3VtZW50bz48SWRlbnRpZmljYWNpb24+PFRpcG8+MTwvVGlwbz48TnVtZXJvPjAyNzUxMTc0MTU1Mjc8L051bWVybz48VmVuY2ltaWVudG8+MjAzMi0xMi0zMTwvVmVuY2ltaWVudG8+PFBhaXM+MTY0PC9QYWlzPjwvSWRlbnRpZmljYWNpb24+PC9Eb2N1bWVudG8+PERpcmVjY2lvbj48Q2FsbGU+Q0FSUiBBUEFTRU8gSkVSRUNVQVJPPC9DYWxsZT48TnVtRXh0PjIwNjwvTnVtRXh0PjxOdW1JbnQ+PC9OdW1JbnQ+PENvbG9uaWE+MjkxNjwvQ29sb25pYT48Q2l1ZGFkPjQ8L0NpdWRhZD48c0NpdWRhZD5BcGFzZW8gZWwgQWx0bzwvc0NpdWRhZD48RXN0YWRvPjExPC9Fc3RhZG8+PENQPjM4NTI3PC9DUD48UGFpcz4xNjQ8L1BhaXM+PC9EaXJlY2Npb24+PC9Vc3VhcmlvPjwvQmVuZWZpY2lhcmlvPjxPcGVyYWRvcj48Tm9tYnJlPk1BUklDRUxBPC9Ob21icmU+PFBhdGVybm8+Q0VSVkFOVEVTPC9QYXRlcm5vPjxNYXRlcm5vPlZFR0E8L01hdGVybm8+PC9PcGVyYWRvcj48L1JlbWVzYT4=";

            //COBRO
            //R.RequestData = "PFJlbWVzYT48Q2xhdmU+U0c3MTE3NzQ3MjA5NDwvQ2xhdmU+PFNlc2lvbj48L1Nlc2lvbj48TW9udG8+MzQwMjAwMDwvTW9udG8+PElkT3JpZ2VuPjE8L0lkT3JpZ2VuPjxJZEFnZW5jaWE+MTwvSWRBZ2VuY2lhPjxJZEVtaXNvcj45PC9JZEVtaXNvcj48QmVuZWZpY2lhcmlvPjxVc3VhcmlvPjxVc2VySWQ+Mjc5MDQ1PC9Vc2VySWQ+PFRlbGVmb25vPjgxMTYwMjA0MzU8L1RlbGVmb25vPjxOb21icmU+QUxFWEFORFJJQTwvTm9tYnJlPjxQYXRlcm5vPlBBVElOTzwvUGF0ZXJubz48TWF0ZXJubz5QQVRJTk88L01hdGVybm8+PEZlY05hYz4yMDAwLTA0LTAyPC9GZWNOYWM+PE5vbWJyZUNvbXBsZXRvPkFMRVhBTkRSSUEgUEFUSU5PIFBBVElOTzwvTm9tYnJlQ29tcGxldG8+PEdlbmVybz4xPC9HZW5lcm8+PE9jdXBhY2lvbj4xPC9PY3VwYWNpb24+PENvcnJlbz48L0NvcnJlbz48TmFjaW9uYWxpZGFkPjE2NDwvTmFjaW9uYWxpZGFkPjxQYWlzTmFjPjE2NDwvUGFpc05hYz48RW50aWRhZE5hYz4xPC9FbnRpZGFkTmFjPjxMdWdhck5hYz5NZXhpY288L0x1Z2FyTmFjPjxEb2N1bWVudG8+PElkZW50aWZpY2FjaW9uPjxUaXBvPjE8L1RpcG8+PE51bWVybz44MTE2MDIwNDM1MTIzPC9OdW1lcm8+PFZlbmNpbWllbnRvPjIwMjMtMTItMzE8L1ZlbmNpbWllbnRvPjxQYWlzPjE2NDwvUGFpcz48L0lkZW50aWZpY2FjaW9uPjwvRG9jdW1lbnRvPjxEaXJlY2Npb24+PENhbGxlPkNhbGxlIFZpbGxhIG5vdmE8L0NhbGxlPjxOdW1FeHQ+OTM8L051bUV4dD48TnVtSW50PjU8L051bUludD48Q29sb25pYT4yNDkzPC9Db2xvbmlhPjxDaXVkYWQ+MTg8L0NpdWRhZD48c0NpdWRhZD5HYXJjw61hPC9zQ2l1ZGFkPjxFc3RhZG8+MTk8L0VzdGFkbz48Q1A+NjYwMDA8L0NQPjxQYWlzPjE2NDwvUGFpcz48L0RpcmVjY2lvbj48L1VzdWFyaW8+PC9CZW5lZmljaWFyaW8+PE9wZXJhZG9yPjxOb21icmU+TWNLYWluPC9Ob21icmU+PFBhdGVybm8+UmVkPC9QYXRlcm5vPjxNYXRlcm5vPlNhbmNoZXo8L01hdGVybm8+PC9PcGVyYWRvcj48L1JlbWVzYT4=";

            //CONSULTA
            R.RequestData = "PFJlbWVzYT4NCiAgICAgICAgICAgICAgICA8Q2xhdmU+U0c3MTE3NzQ3MzAwMDwvQ2xhdmU+DQogICAgICAgICAgICAgICAgPE1vbnRvPjA8L01vbnRvPg0KICAgICAgICAgICAgICAgIDxJZEVtaXNvcj4wPC9JZEVtaXNvcj4NCiAgICAgICAgICAgICAgICA8SWRPcmlnZW4+MTwvSWRPcmlnZW4+DQogICAgICAgICAgICAgICAgPElkQWdlbmNpYT4xPC9JZEFnZW5jaWE+DQogICAgICAgICAgICAgICAgPE9wZXJhZG9yPg0KICAgICAgICAgICAgICAgICAgICA8Tm9tYnJlPk1jS2FpbjwvTm9tYnJlPg0KICAgICAgICAgICAgICAgICAgICA8UGF0ZXJubz5SZWQ8L1BhdGVybm8+DQogICAgICAgICAgICAgICAgICAgIDxNYXRlcm5vPlNhbmNoZXo8L01hdGVybm8+DQogICAgICAgICAgICAgICAgPC9PcGVyYWRvcj4NCiAgICAgICAgICAgIDwvUmVtZXNhPg==";

            //SBTDService.TDResponse res =
            srv.TD_BuscaEnvioAsync(R);
            //srv.TD_CobraEnvioAsync(R);
            //Console.WriteLine($"Ejecutar Fin {i}");
        }

        static void Probar(String llamada)
        {

            //if (llamada == 500)
            //    SeriLogTD.currentTime= SeriLogTD.currentTime.AddDays(1);
            //if(llamada==700)
            //    SeriLogTD.currentTime = SeriLogTD.currentTime.AddDays(1);

            SeriLogTD.Information($"{llamada}", "CargaDatos", "Nivel establecido Information\\ Este es un mensaje de ejemplo1 de información.");
            SeriLogTD.Warning($"{llamada}", "CargaDatos", "Nivel establecido Information\\ Este es un mensaje de ejemplo1 de Warning");
            SeriLogTD.Error($"{llamada}", "CargaDatos", "Nivel establecido Information\\ Este es un mensaje de ejemplo1 de Error.");

            //SeriLogTD.setLogEventLevel(LogEventLevel.Error);
            //SeriLogTD.Information($"{llamada}", "CargaDatos", "Nivel establecido Error\\ Este es un mensaje de ejemplo1 de información.");
            //SeriLogTD.Warning($"{llamada}", "CargaDatos", "Nivel establecido Error\\ Este es un mensaje de ejemplo1 de Warning");
            //SeriLogTD.Error($"{llamada}", "CargaDatos", "Nivel establecido Error\\ Este es un mensaje de ejemplo1 de Error.");

            //SeriLogTD.setLogEventLevel(LogEventLevel.Warning);
            //SeriLogTD.Information($"{llamada}", "CargaDatos", "Nivel establecido Warning\\ Este es un mensaje de ejemplo1 de información.");
            //SeriLogTD.Warning($"{llamada}", "CargaDatos", "Nivel establecido Warning\\ Este es un mensaje de ejemplo1 de Warning");
            //SeriLogTD.Error($"{llamada}", "CargaDatos", "Nivel establecido Warning\\ Este es un mensaje de ejemplo1 de Error.");

            //SeriLogTD.setLogEventLevel(LogEventLevel.Verbose);
            //SeriLogTD.Information($"{llamada}", "CargaDatos", "Nivel establecido Verbose\\ Este es un mensaje de ejemplo1 de información.");
            //SeriLogTD.Warning($"{llamada}", "CargaDatos", "Nivel establecido Verbose\\ Este es un mensaje de ejemplo1 de Warning");
            //SeriLogTD.Error($"{llamada}", "CargaDatos", "Nivel establecido Verbose\\ Este es un mensaje de ejemplo1 de Error.");
        }
        static void ConfigureLogging()
        {
            string logFilePath = GetLogFilePath();

            if (logFilePath != null)
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Is(logLevel)
                    .WriteTo.File(
                        new RenderedCompactJsonFormatter(
                            new Serilog.Formatting.Json.JsonValueFormatter(
                                "{Timestamp:HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                            ), // Utiliza el formato compacto para los registros
                        logFilePath,
                        rollingInterval: RollingInterval.Day, // Genera un nuevo archivo cada día
                        rollOnFileSizeLimit: true, // Cambiar de archivo cuando se alcance un tamaño límite
                        retainedFileCountLimit: 30) // Límite de archivos antiguos a retener
                    .CreateLogger();

            }
            else
            {
                Console.WriteLine("La unidad no está disponible para escribir el log.");
            }
        }
        static string GetLogFilePath()
        {
            string monthDirectoryName = DateTime.Now.ToString("yyMM");
            string rootPathD = "D:\\"; // Cambiar esto por la unidad D que deseas verificar
            string rootPathC = "C:\\";

            string logFilePath = Path.Combine(logDirectory, monthDirectoryName, logFileName);

            if (DriveExists(rootPathD))
            {
                if (!Directory.Exists(rootPathD + logDirectory))
                {
                    Directory.CreateDirectory(rootPathD + logDirectory);
                }

                return Path.Combine(rootPathD, logFilePath);
            }
            else if (DriveExists(rootPathC))
            {
                if (!Directory.Exists(rootPathC + logDirectory))
                {
                    Directory.CreateDirectory(rootPathC + logDirectory);
                }

                return Path.Combine(rootPathC, logFilePath);
            }
            else
            {
                return null;
            }
        }

        static bool DriveExists(string rootPath)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                if (drive.Name.Equals(rootPath, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}



