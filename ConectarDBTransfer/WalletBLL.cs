using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConectarDBTransfer
{
    public class WalletBLL
    {
        public void  ObtenerRemitenteSTP(string CksReferencia)
        {
            

            MySqlConnection dbCnn = null;
            MySqlCommand dbCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string cnn = ConfigurationManager.AppSettings["DBTRANSFER"].ToString();
                dbCnn = new MySqlConnection(cnn);
                dbCmd = new MySqlCommand("dbtransfer.`sp_select_cuentastp`", dbCnn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;
                dbCmd.Parameters.AddWithValue("@CksReferencia", CksReferencia);
                Console.WriteLine($"ObtenerRemitenteSTP()=>Iniciar conección {DateTime.Now.ToString("HH:mm:ss:FFFF")}");
                dbCnn.Open();
                Console.WriteLine($"ObtenerRemitenteSTP()=>Conección establecida {DateTime.Now.ToString("HH:mm:ss:FFFF")}");


                IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                WaitHandle.WaitAny(HandleResult);
                Console.WriteLine($"ObtenerRemitenteSTP()=>Prepara comando {DateTime.Now.ToString("HH:mm:ss:FFFF")}");
                reader = dbCmd.EndExecuteReader(DBResult);
                Console.WriteLine($"ObtenerRemitenteSTP()=>Termina comando {DateTime.Now.ToString("HH:mm:ss:FFFF")}");
                if (reader.HasRows == true)
                {
                    reader.Read();
                    Console.WriteLine($"ObtenerRemitenteSTP()=>Leer resultado {DateTime.Now.ToString("HH:mm:ss:FFFF")}");
                    //reader.GetInt32("CodigoRespuesta");

                    reader.Close();
                }
                
            }
            catch (Exception ex)
            {
               Console.WriteLine("CrossBorderWeb", "WalletBLL Transferencia", "ObtenerRemitenteSTP()=>Error: " + ex.ToString());

            }
            finally
            {
                if ((reader != null) && !reader.IsClosed)
                    reader.Close();
                if ((dbCnn != null) && dbCnn.State == ConnectionState.Open)
                {
                    dbCnn.Close();
                    dbCnn.Dispose();
                }
            }
        }       
    }
}
