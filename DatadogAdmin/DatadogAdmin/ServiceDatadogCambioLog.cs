using DDAdmin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatadogAdmin
{
    public class ServiceDatadogCambioLog
    {
        
        string ServicesInicial = "datadogagent";
        string[] targetServices = { "datadogagent", "datadog-process-agent", "datadog-trace-agent" }; // Nombres de los servicios objetivo
        Timer timer;
        private bool Default { get; set; }
        private TimeSpan StartTime { get; set; }
        private TimeSpan EndTime { get; set; }
        public  Int32 Period { get; set; }
        public Int32 Mes { get; set; }
        public ServiceDatadogCambioLog()
        {
            LogClass.setLogClass("DataDogCambios", "Log/TransferDirecto/DataDogCambios", 4);
            LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Inicio carga configuraciones", 4);
            
            var dafult =Convert.ToInt32(ConfigurationManager.AppSettings["dafault"]);
            var startTime = ConfigurationManager.AppSettings["startTime"];
            var endTime = ConfigurationManager.AppSettings["endTime"];
            var period =Convert.ToInt32(ConfigurationManager.AppSettings["period"]);
            Mes = Convert.ToInt32(ConfigurationManager.AppSettings["mes"]);

            Default = (dafult == 1 ? true : false);            
            var startTimeArray = startTime.Split(',');           
            var endTimeArray = endTime.Split(',');

            StartTime =(Default? TimeSpan.Zero: new TimeSpan(Convert.ToInt32(startTimeArray[0]), Convert.ToInt32(startTimeArray[1]), Convert.ToInt32(startTimeArray[2])));
            EndTime = (Default ? new TimeSpan(01,00, 0) : new TimeSpan(Convert.ToInt32(endTimeArray[0]), Convert.ToInt32(endTimeArray[1]), Convert.ToInt32(endTimeArray[2])));            
            Period = (Default ? 25 : period); 

            LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Configuraciones Default:{Default} StartTime:{StartTime} EndTime:{EndTime} Period:{Period} Mes:{Mes}", 4);
        }
        private void CheckNameLogs(object state)
        {
            DateTime now = DateTime.Now;
            TimeSpan time = now.TimeOfDay;

         
            LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"CheckNameLogs..{time.ToString()} {StartTime.ToString()} {EndTime.ToString()}",4);

            if (time > StartTime && time < EndTime )
            {
                LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Tiempo de cambio {time.ToString()}",4);

                CambioDatos cambios = new CambioDatos(Default,Mes);
                bool rga = cambios.Gateway();
                bool rco = cambios.Conectores();

                if (rga || rco)
                {
                    LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Cambios aplicados reiniciar servicios {time.ToString()}",4);

                    ServiceController servicedatadogagent = new ServiceController(ServicesInicial);
                    if (servicedatadogagent.Status == ServiceControllerStatus.Running)
                    {
                        try
                        {
                            servicedatadogagent.Stop();
                            LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Ejecuto cambios detiene {ServicesInicial}",4);
                            servicedatadogagent.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(5));                            
                        }
                        catch (Exception ex){}
                    }

                    Thread.Sleep(5000);

                    try
                    {
                        servicedatadogagent.Start();
                        LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Ejecuto cambios levanta {ServicesInicial}", 4);
                        servicedatadogagent.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(5));
                    }
                    catch (Exception ex) { }
                    Thread.Sleep(1000);
                    foreach (string serviceName in targetServices)
                    {
                        ServiceController service = new ServiceController(serviceName);
                        if (service.Status != ServiceControllerStatus.Running)
                        {
                            try
                            {
                                service.Start();
                                LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Ejecuto cambios levanta {serviceName}", 4);
                                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(5));
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
        }
        public void Start(int Period)
        {
            timer = new Timer(CheckNameLogs, null, TimeSpan.Zero, TimeSpan.FromMinutes(Period));
            LogClass.LogInfo("System", "ServiceDatadogCambioLog:Ejecutando", $"Start periodo de {Period} minutos", 4);
        }
        public void Stop()
        {
            timer?.Dispose();
        }
    }
}
