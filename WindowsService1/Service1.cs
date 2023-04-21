using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;



namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private System.Diagnostics.EventLog eventLog1;

        public Service1()
        {
            InitializeComponent();
            eventLog1 = new EventLog();
            eventLog1.Source = "WorkerService";
            eventLog1.Log = "TransferDirecto";
        }

        protected override void OnStart(string[] args)
        {
            
        }

        protected override void OnStop()
        {
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //   Host.CreateDefaultBuilder(args)
        //   .UseWindowsService()
        //   .ConfigureServices((hostContext, services) =>
        //   {
        //       services.AddHostedService<Worker>()
        //       .AddSingleton<IFileData, FileData>();
        //   });
    }
}
