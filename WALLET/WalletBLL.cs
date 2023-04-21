using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using WALLET.Controllers;

namespace WALLET
{
    public class WalletBLL
    {
        #region Atributos

        

        #endregion
        public bool Transferencia(string key, TransferenciaRequest Request, ref TransferenciaResponse Response)
        {
            try
            {
               
                Transport oTransport = new Transport();
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "ObtenerRemitenteSTP()");
                oTransport = ObtenerRemitenteSTP(key,Request.cuentaBeneficiario);
                RemitenteEntity oRem = new RemitenteEntity();
                if (oTransport.Status)
                {
                    oRem = (RemitenteEntity)oTransport.Model[0];

                    
                    Cargo cargo = new Cargo();
                    cargo.Id = Singleton.Instance.contadorWallet++;
                    cargo.ClienteId = Convert.ToInt32(ConfigurationManager.AppSettings["ClienteIdWallet"]);
                    cargo.TipoRecurso = Convert.ToInt32(ConfigurationManager.AppSettings["TipoRecursoWallet"]);
                    cargo.Descripcion = ConfigurationManager.AppSettings["DescripcionEnvio"];
                    cargo.Monto = Request.monto;
                    cargo.NumCuenta = Convert.ToInt32(oRem.nNumCuenta);
                    LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"Encolar petición {cargo.Id}");
                    Singleton.Instance.EncolarAbonoWallet(cargo);
                    LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"Total de colas {Singleton.Instance.PilaCargo.Count}");

                    ////Realiza abono en la wallet        
                    //string request = JsonConvert.SerializeObject(cargo);

                    //ResponseCargo responseDataCargo = realizarAbono(cargo);

                    //string response = JsonConvert.SerializeObject(responseDataCargo);

                    //if (responseDataCargo.ErrorCode != 0)
                    //{
                    //    Response.mensaje = responseDataCargo.ErrorMsg;
                    //    return false;
                    //}

                }
                else {
                    Response.mensaje = "Remitente no encontrado";
                    return true;
                }



                //Inserta los datos de Transferencia
                TransferenciaEntity oTransfer = new TransferenciaEntity();
                oTransfer.nidRemitente = oRem.nIdRemitente;
                oTransfer.nId = Convert.ToInt32(Request.id);
                oTransfer.nFechaOperacion = Convert.ToInt32(Request.fechaOperacion);
                oTransfer.nInstitucionOrdenante = Convert.ToInt32(Request.institucionOrdenante);
                oTransfer.nInstitucionBeneficiaria = Convert.ToInt32(Request.institucionBeneficiaria);
                oTransfer.sClaveRastreo = Request.claveRastreo;
                oTransfer.dMonto = Convert.ToDecimal(Request.monto);
                oTransfer.sNombreOrdenante = Request.nombreOrdenante;
                oTransfer.nTipoCuentaOrdenante = Convert.ToInt32(Request.tipoCuentaOrdenante);
                oTransfer.sCuentaOrdenante = Request.cuentaOrdenante;
                oTransfer.sRfcCurpOrdenante = Request.rfcCurpOrdenante;
                oTransfer.sNombreBeneficiario = Request.nombreBeneficiario;
                oTransfer.nTipoCuentaBeneficiario = Convert.ToInt32(Request.tipoCuentaBeneficiario);
                oTransfer.sCuentaBeneficiario = Request.cuentaBeneficiario;
                oTransfer.sNombreBeneficiario2 = Request.nombreBeneficiario2;
                oTransfer.sTipoCuentaBeneficiario2 = Request.tipoCuentaBeneficiario2;
                oTransfer.sCuentaBeneficiario2 = Request.cuentaBeneficiario2;
                oTransfer.sRfcCurpBeneficiario = Request.rfcCurpBeneficiario;
                oTransfer.sConceptoPago = Request.conceptoPago;
                oTransfer.nReferenciaNumerica = Convert.ToInt32(Request.referenciaNumerica);
                oTransfer.sEmpresa = Request.empresa;
                oTransfer.nTipoPago = Convert.ToInt32(Request.tipoPago);
                oTransfer.sTsLiquidacion = Request.tsLiquidacion;
                oTransfer.sFolioCodi = Request.folioCodi;
                oTransport = null;
                oTransport=InsertarTransferencia(key, oTransfer);
                if (!oTransport.Status)
                {
                   
                    Response.mensaje = oTransport.MessagesToString();
                    Response.mensaje = "Datos no obtenidos";
                    return true;
                }
                Response.mensaje = "confirmar";
                return true;
            }
            catch (JsonException ex)
            {                
                Response.mensaje = ex.Message;
                return false;
            }
            catch (Exception ex)
            {               
                Response.mensaje = ex.Message;
                return false;
            }
        }
       
        public Transport ObtenerRemitenteSTP(string key, string CksReferencia)
        {
            Transport oTransport = new Transport();
            
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
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Iniciar conección ");
                dbCnn.Open();
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Conección establecida ");


                IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                WaitHandle.WaitAny(HandleResult);
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Prepara comando ");
                reader = dbCmd.EndExecuteReader(DBResult);
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Ejecuta comando ");
                if (reader.HasRows == true)
                {
                    //reader.Read();
                    oTransport.Model = Maper.DataReaderToList<RemitenteEntity>(new Mapeo(typeof(RemitenteEntity)), ((IDataReader)reader));
                    oTransport.Status = (oTransport.Model.Count > 0 ? true : false);
                    oTransport.ErrorCode = (oTransport.Status ? 0 : 1005);
                    oTransport.ErrorMessage = (oTransport.Status ? "Consulta exitosa." : "No se encontraron registros.");
                    LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "ObtenerRemitenteSTP()=> " + (oTransport.Status ? "con resultados" : " sin resultados"));
                    reader.Close();
                }
                else
                {
                    oTransport.Status = false;
                    oTransport.ErrorCode = 1005;
                    oTransport.ErrorMessage = "No se encontraron registros.";
                    LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "ObtenerRemitenteSTP()=>Sin resultados");

                }
                oTransport.SetMessage(new Message(TypeMessage.INFORMATION, oTransport.ErrorCode, oTransport.ErrorMessage));                
            }
            catch (Exception ex)
            {
                oTransport.ErrorCode = 1005;
                oTransport.ErrorMessage = ex.Message;
                oTransport.Status = false;
                oTransport.SetMessage(new Message(ex));
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "ObtenerRemitenteSTP()=>Error: "+ ex.ToString());

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
            return oTransport;
        }
        public ResponseCargo realizarAbono(Cargo cargo)
        {
            ResponseCargo response = new ResponseCargo();
            if (cargo.ClienteId != 0 && cargo.NumCuenta != 0 && cargo.TipoRecurso != 0 && !string.IsNullOrEmpty(cargo.Descripcion) && cargo.Monto != 0)
            {
                try
                {
                    ServicioBilletera.Service wsBilletera = new ServicioBilletera.Service();
                    Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    string Token = Encrypt.CreateMD5(ConfigurationManager.AppSettings["PWDBILLETERA"] + unixTimestamp + cargo.ClienteId);
                    string respuesta = wsBilletera.Abono(cargo.ClienteId, unixTimestamp, Token, cargo.NumCuenta, cargo.TipoRecurso, cargo.Referencia, cargo.Descripcion, cargo.Monto);
                    try
                    {
                        response = JsonConvert.DeserializeObject<ResponseCargo>(respuesta);
                    }
                    catch (JsonException ex)
                    {
                        response.ErrorCode = (int)HttpStatusCode.BadGateway;
                        response.ErrorMsg = "Error al realizar cargo al servicio";
                    }

                }
                catch (Exception ex)
                {
                    response.ErrorCode = (int)HttpStatusCode.BadGateway;
                    response.ErrorMsg = "Ocurrio un error al intentar consumir el servicio Wallet: " + ex.Message;
                }
            }
            else
            {
                response.ErrorCode = (int)HttpStatusCode.BadRequest;
                response.ErrorMsg = "ClienteId, NumCuenta, TipoRecurso, Descripcion o Monto invalido, no puede ser 0 o vacio su valor";
            }
            return response;
        }
        public Transport InsertarTransferencia(string key,TransferenciaEntity oTransfer)
        {
            Transport oTransport = new Transport();
            MySqlConnection dbCnn = null;
            MySqlCommand dbCmd = null;
            MySqlDataReader reader = null;
            try
            {
                string cnn = ConfigurationManager.AppSettings["DBTRANSFER"].ToString();
                dbCnn = new MySqlConnection(cnn);
                dbCmd = new MySqlCommand("dbtransfer.sp_insert_transferenciaV2", dbCnn);
                dbCmd.CommandType = System.Data.CommandType.StoredProcedure;
                dbCmd.Parameters.AddWithValue("@CknidRemitente", oTransfer.nidRemitente);
                dbCmd.Parameters.AddWithValue("@CknId", oTransfer.nId);
                dbCmd.Parameters.AddWithValue("@CknFechaOperacion", oTransfer.nFechaOperacion);
                dbCmd.Parameters.AddWithValue("@CknInstitucionOrdenante", oTransfer.nInstitucionOrdenante);
                dbCmd.Parameters.AddWithValue("@CknInstitucionBeneficiaria", oTransfer.nInstitucionBeneficiaria);
                dbCmd.Parameters.AddWithValue("@CksClaveRastreo", oTransfer.sClaveRastreo);
                dbCmd.Parameters.AddWithValue("@CkdMonto", oTransfer.dMonto);
                dbCmd.Parameters.AddWithValue("@CksNombreOrdenante", oTransfer.sNombreOrdenante);
                dbCmd.Parameters.AddWithValue("@CknTipoCuentaOrdenante", oTransfer.nTipoCuentaOrdenante);
                dbCmd.Parameters.AddWithValue("@CksCuentaOrdenante", oTransfer.sCuentaOrdenante);
                dbCmd.Parameters.AddWithValue("@CksRfcCurpOrdenante", oTransfer.sRfcCurpOrdenante);
                dbCmd.Parameters.AddWithValue("@CksNombreBeneficiario", oTransfer.sNombreBeneficiario);
                dbCmd.Parameters.AddWithValue("@CknTipoCuentaBeneficiario", oTransfer.nTipoCuentaBeneficiario);
                dbCmd.Parameters.AddWithValue("@CksCuentaBeneficiario", oTransfer.sCuentaBeneficiario);
                dbCmd.Parameters.AddWithValue("@CksNombreBeneficiario2", oTransfer.sNombreBeneficiario2);
                dbCmd.Parameters.AddWithValue("@CksTipoCuentaBeneficiario2", oTransfer.sTipoCuentaBeneficiario2);
                dbCmd.Parameters.AddWithValue("@CksCuentaBeneficiario2", oTransfer.sCuentaBeneficiario2);
                dbCmd.Parameters.AddWithValue("@CksRfcCurpBeneficiario", oTransfer.sRfcCurpBeneficiario);
                dbCmd.Parameters.AddWithValue("@CksConceptoPago", oTransfer.sConceptoPago);
                dbCmd.Parameters.AddWithValue("@CknReferenciaNumerica", oTransfer.nReferenciaNumerica);
                dbCmd.Parameters.AddWithValue("@CksEmpresa", oTransfer.sEmpresa);
                dbCmd.Parameters.AddWithValue("@CknTipoPago", oTransfer.nTipoPago);
                dbCmd.Parameters.AddWithValue("@CksTsLiquidacion", oTransfer.sTsLiquidacion);
                dbCmd.Parameters.AddWithValue("@CksFolioCodi", oTransfer.sFolioCodi);

                string parametros = string.Empty;
                foreach (MySqlParameter item in dbCmd.Parameters)
                {
                    parametros+=$"SET {item.ParameterName} ={item.Value};";                    

                }
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"InsertarTransferencia()=>Iniciar conección ");
                dbCnn.Open();
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"InsertarTransferencia()=>Conección establecida "); 

                IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"InsertarTransferencia()=>Pepara comando ");
                WaitHandle.WaitAny(HandleResult);                
                reader = dbCmd.EndExecuteReader(DBResult);
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"InsertarTransferencia()=>Ejecuta comando ");
                if (reader.HasRows == true)
                {
                    reader.Read();                    
                    oTransport.ErrorCode = reader.GetInt32("CodigoRespuesta");
                    oTransport.ErrorMessage = reader.GetString("MsgRespuesta");
                    oTransport.Status = (oTransport.ErrorCode == 0 ? true : false);
                    oTransport.SetMessage(new Message(TypeMessage.INFORMATION, oTransport.ErrorCode, oTransport.ErrorMessage));

                    LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"InsertarTransferencia()=>Iserción{ DateTime.Now.ToString("HH: mm:ss: FFFF")}");

                    reader.Close();
                }
                else
                    LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "InsertarTransferencia()=>Iserción fallida");
            }
            catch (Exception ex)
            {
                oTransport.ErrorCode = 1005;
                oTransport.ErrorMessage = ex.Message;
                oTransport.Status = false;
                oTransport.SetMessage(new Message(ex));
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "InsertarTransferencia()=>Error: "+ex.ToString());
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
            return oTransport;
        }
    }
}