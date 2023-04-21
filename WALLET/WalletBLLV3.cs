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
    public class WalletBLLV3
    {
       
        public string  cadenaConexion { get; set; }
        public WalletBLLV3() {
            cadenaConexion= ConfigurationManager.AppSettings["DBTRANSFER"];
        }
    
        #region Atributos



        #endregion
        public bool Transferencia(string key, TransferenciaRequest Request, ref TransferenciaResponse Response)
        {

            try
            {
               
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "ObtenerRemitenteSTP()");
                RemitenteEntity oRem = ObtenerRemitenteSTP(key,Request.cuentaBeneficiario);
                if (oRem != null)
                {
                    Cargo cargo = new Cargo();
                    Singleton.Instance.contadorWallet= Singleton.Instance.contadorWallet+1;
                    cargo.Id = Singleton.Instance.contadorWallet;
                    cargo.ClienteId = Convert.ToInt32(ConfigurationManager.AppSettings["ClienteIdWallet"]);
                    cargo.TipoRecurso = Convert.ToInt32(ConfigurationManager.AppSettings["TipoRecursoWallet"]);
                    cargo.Descripcion = ConfigurationManager.AppSettings["DescripcionEnvio"];
                    cargo.Monto = Request.monto;
                    cargo.NumCuenta = Convert.ToInt32(oRem.nNumCuenta);

                    ////Realiza abono en la wallet        
                    string request = JsonConvert.SerializeObject(cargo);
                    ResponseCargo responseDataCargo = realizarAbono(key,cargo);
                    string response = JsonConvert.SerializeObject(responseDataCargo);
                    if (responseDataCargo.ErrorCode != 0)
                    {
                        Response.mensaje = responseDataCargo.ErrorMsg;
                        return false;
                    }
                    //Inserta los datos de Transferencia
                    TransferenciaEntity oTransfer = new TransferenciaEntity();
                    oTransfer.FolioWallet = responseDataCargo.Folio;
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

                    LogClass.LogInfo(key + "Encolar", "WalletBLL Transferencia", $"Encolar Transferencia Folio Wallet {oTransfer.FolioWallet}");
                    Singleton.Instance.EncolarTransferencia(oTransfer);
                    LogClass.LogInfo(key + "Encolar", "WalletBLL Transferencia", $"Total Transferencias {Singleton.Instance.ColaTrannsferencia.Count}");

                }
                else {
                    Response.mensaje = "Remitente no encontrado";
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

        
        public RemitenteEntity ObtenerRemitenteSTP(string key, string CksReferencia)
        {

            RemitenteEntity rem = null;
            MySqlConnection _sqlCnn = null; ;
        MySqlDataAdapter _sqlDatAdapt;
        MySqlDataReader _sqlDatRead=null;
         MySqlCommand _sqlCmd;
         MySqlTransaction _sqlTran;
         Exception _errorOcurred;
         DataSet dsDatos;
            try
            {

                _sqlCnn = new MySqlConnection(cadenaConexion);
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Iniciar conección ");
                _sqlCnn.Open();
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Conección establecida ");
                _sqlCmd = new MySqlCommand();
                _sqlCmd.Connection = _sqlCnn;
                _sqlCmd.CommandTimeout = 0;

                _sqlCmd.CommandText = "dbtransfer.sp_select_cuentastp";
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Clear();
                _sqlCmd.Parameters.AddWithValue("@CksReferencia", CksReferencia);
                _sqlCmd.Prepare();
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Prepara comando ");
                _sqlDatRead = _sqlCmd.ExecuteReader();
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Ejecuta comando ");
                if (_sqlDatRead.HasRows == true)
                {
                    _sqlDatRead.Read();
                    rem = new RemitenteEntity();
                    rem.nIdRemitente = Convert.ToInt32(_sqlDatRead["nIdRemitente"].ToString());
                    rem.nNumCuenta = Convert.ToInt32(_sqlDatRead["nNumCuenta"].ToString());
                    LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "ObtenerRemitenteSTP()=> con resultados");
                    _sqlDatRead.Close();
                }  
            }
            catch (Exception ex)
            {
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "ObtenerRemitenteSTP()=>Error: "+ ex.ToString());
            }
            finally
            {
                if ((_sqlDatRead != null) && !_sqlDatRead.IsClosed)
                    _sqlDatRead.Close();
                if ((_sqlCnn != null) && _sqlCnn.State == ConnectionState.Open)
                {
                    _sqlCnn.Close();
                    _sqlCnn.Dispose();
                }
            }
            return rem;
        }
        public ResponseCargo realizarAbono(string key,Cargo cargo)
        {
            ResponseCargo response = new ResponseCargo();
            if (cargo.ClienteId != 0 && cargo.NumCuenta != 0 && cargo.TipoRecurso != 0 && !string.IsNullOrEmpty(cargo.Descripcion) && cargo.Monto != 0)
            {
                try
                {
                    ServicioBilletera.Service wsBilletera = new ServicioBilletera.Service();
                    Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    string Token = Encrypt.CreateMD5(ConfigurationManager.AppSettings["PWDBILLETERA"] + unixTimestamp + cargo.ClienteId);
                    LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"realizarAbono()=>Inicia abono Wallet ");
                    string respuesta = wsBilletera.Abono(cargo.ClienteId, unixTimestamp, Token, cargo.NumCuenta, cargo.TipoRecurso, cargo.Referencia, cargo.Descripcion, cargo.Monto);
                    
                    try
                    {                        
                        response = JsonConvert.DeserializeObject<ResponseCargo>(respuesta);
                        LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"realizarAbono()=>Fin abono Wallet Exitoso");
                    }
                    catch (JsonException ex)
                    {
                        response.ErrorCode = (int)HttpStatusCode.BadGateway;
                        response.ErrorMsg = "Error al realizar cargo al servicio";
                        LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"realizarAbono()=>Fin abono Wallet Fallido ");
                    }                    
                }
                catch (Exception ex)
                {
                    response.ErrorCode = (int)HttpStatusCode.BadGateway;
                    response.ErrorMsg = "Ocurrio un error al intentar consumir el servicio Wallet: " + ex.Message;
                    LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"realizarAbono()=>Fin abono Wallet ");
                }
            }
            else
            {
                response.ErrorCode = (int)HttpStatusCode.BadRequest;
                response.ErrorMsg = "ClienteId, NumCuenta, TipoRecurso, Descripcion o Monto invalido, no puede ser 0 o vacio su valor";
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"realizarAbono()=>Fin abono Wallet ");

            }
            LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"realizarAbono()=>Fin abono Wallet ");
            return response;
        }
        public bool InsertarTransferencia(string key,TransferenciaEntity oTransfer)
        {
            bool exito = false;
            RemitenteEntity rem = null;
            MySqlConnection _sqlCnn = null; ;
            MySqlDataAdapter _sqlDatAdapt;
            MySqlDataReader _sqlDatRead = null;
            MySqlCommand _sqlCmd;
            MySqlTransaction _sqlTran;
            Exception _errorOcurred;
            DataSet dsDatos;
            try
            {

                _sqlCnn = new MySqlConnection(cadenaConexion);
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Iniciar conección ");
                _sqlCnn.Open();
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Conección establecida ");
                _sqlCmd = new MySqlCommand();
                _sqlCmd.Connection = _sqlCnn;
                _sqlCmd.CommandTimeout = 0;

                _sqlCmd.CommandText = "dbtransfer.sp_insert_transferenciaV2";
                _sqlCmd.CommandType = CommandType.StoredProcedure;
                _sqlCmd.Parameters.Clear();
                _sqlCmd.Parameters.AddWithValue("@CknidRemitente", oTransfer.nidRemitente);
                _sqlCmd.Parameters.AddWithValue("@CknId", oTransfer.nId);
                _sqlCmd.Parameters.AddWithValue("@CknFechaOperacion", oTransfer.nFechaOperacion);
                _sqlCmd.Parameters.AddWithValue("@CknInstitucionOrdenante", oTransfer.nInstitucionOrdenante);
                _sqlCmd.Parameters.AddWithValue("@CknInstitucionBeneficiaria", oTransfer.nInstitucionBeneficiaria);
                _sqlCmd.Parameters.AddWithValue("@CksClaveRastreo", oTransfer.sClaveRastreo);
                _sqlCmd.Parameters.AddWithValue("@CkdMonto", oTransfer.dMonto);
                _sqlCmd.Parameters.AddWithValue("@CksNombreOrdenante", oTransfer.sNombreOrdenante);
                _sqlCmd.Parameters.AddWithValue("@CknTipoCuentaOrdenante", oTransfer.nTipoCuentaOrdenante);
                _sqlCmd.Parameters.AddWithValue("@CksCuentaOrdenante", oTransfer.sCuentaOrdenante);
                _sqlCmd.Parameters.AddWithValue("@CksRfcCurpOrdenante", oTransfer.sRfcCurpOrdenante);
                _sqlCmd.Parameters.AddWithValue("@CksNombreBeneficiario", oTransfer.sNombreBeneficiario);
                _sqlCmd.Parameters.AddWithValue("@CknTipoCuentaBeneficiario", oTransfer.nTipoCuentaBeneficiario);
                _sqlCmd.Parameters.AddWithValue("@CksCuentaBeneficiario", oTransfer.sCuentaBeneficiario);
                _sqlCmd.Parameters.AddWithValue("@CksNombreBeneficiario2", oTransfer.sNombreBeneficiario2);
                _sqlCmd.Parameters.AddWithValue("@CksTipoCuentaBeneficiario2", oTransfer.sTipoCuentaBeneficiario2);
                _sqlCmd.Parameters.AddWithValue("@CksCuentaBeneficiario2", oTransfer.sCuentaBeneficiario2);
                _sqlCmd.Parameters.AddWithValue("@CksRfcCurpBeneficiario", oTransfer.sRfcCurpBeneficiario);
                _sqlCmd.Parameters.AddWithValue("@CksConceptoPago", oTransfer.sConceptoPago);
                _sqlCmd.Parameters.AddWithValue("@CknReferenciaNumerica", oTransfer.nReferenciaNumerica);
                _sqlCmd.Parameters.AddWithValue("@CksEmpresa", oTransfer.sEmpresa);
                _sqlCmd.Parameters.AddWithValue("@CknTipoPago", oTransfer.nTipoPago);
                _sqlCmd.Parameters.AddWithValue("@CksTsLiquidacion", oTransfer.sTsLiquidacion);
                _sqlCmd.Parameters.AddWithValue("@CksFolioCodi", oTransfer.sFolioCodi);

                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Prepara comando ");
                _sqlDatRead = _sqlCmd.ExecuteReader();
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", $"ObtenerRemitenteSTP()=>Ejecuta comando ");
                if (_sqlDatRead.HasRows == true)
                {
                    _sqlDatRead.Read();
                    exito = !Convert.ToBoolean((Int32)_sqlDatRead["CodigoRespuesta"]);
                    LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "ObtenerRemitenteSTP()=> con resultados");
                    _sqlDatRead.Close();
                }
            }
            catch (Exception ex)
            {
                LogClass.LogInfo(key + "CrossBorderWeb", "WalletBLL Transferencia", "ObtenerRemitenteSTP()=>Error: " + ex.ToString());

            }
            finally
            {
                if ((_sqlDatRead != null) && !_sqlDatRead.IsClosed)
                    _sqlDatRead.Close();
                if ((_sqlCnn != null) && _sqlCnn.State == ConnectionState.Open)
                {
                    _sqlCnn.Close();
                    _sqlCnn.Dispose();
                }
            }
            return exito;
        }
    }
}