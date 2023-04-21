using LBWorkerService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioWS
{
    public partial class Service1 : ServiceBase
    {
        MainWorkerServer mws = null;
        public Service1()
        {
            if (!System.Diagnostics.EventLog.SourceExists("TransferDirecto WorkerService"))
            {
                System.Diagnostics.EventLog.CreateEventSource("TransferDirecto WorkerService", "TransferDirecto");
            }
            InitializeComponent();
            eventLog1.Source = "TransferDirecto WorkerService";
            eventLog1.Log = "TransferDirecto";

            mws = new MainWorkerServer(null);

            
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Iniciando", EventLogEntryType.Information, 0);
            mws.Iniciar();
            eventLog1.WriteEntry("IniciandoFin", EventLogEntryType.Information, 0);
        }

        protected override void OnStop()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            mws.Detener(token);         
        }
    }
}
