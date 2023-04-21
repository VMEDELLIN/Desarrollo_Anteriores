using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using WALLET.Controllers;

namespace WALLET
{
    public class Singleton
    {
        private static Singleton instance = null;
        
        int count;
        public int contadorWallet=0;
        public int contadorTransaccion = 0;
        public string cadenaConexion { get; set; }
        public Queue<Cargo> PilaCargo = null;
        public Queue<TransferenciaEntity> ColaTrannsferencia = null;
        private Singleton()
        {
            LogClass.LogInfo("Singleton", $"Se creo singleton {DateTime.Now.ToString("HH:mm:ss")}");            
            PilaCargo = new Queue<Cargo>();
            ColaTrannsferencia = new Queue<TransferenciaEntity>();
            cadenaConexion = ConfigurationManager.AppSettings["DBTRANSFER"];            
        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                    instance = new Singleton();
                return instance;
            }
        }

        public void EncolarAbonoWallet(Cargo oCargo) {
            PilaCargo.Enqueue(oCargo);
        }
        public void EncolarTransferencia(TransferenciaEntity oTransferencia)
        {
            ColaTrannsferencia.Enqueue(oTransferencia);
        }
    }
}