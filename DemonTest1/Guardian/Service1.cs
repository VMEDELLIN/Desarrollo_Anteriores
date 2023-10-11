using GuardianRun;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Guardian
{
    public partial class Service1 : ServiceBase
    {

        Run r = null;
        public Service1()
        {
            InitializeComponent();
            r = new Run();
        }

        protected override void OnStart(string[] args)
        {
            r.Start();
        }

        protected override void OnStop()
        {
            r.Stop();

        }
    }
}
