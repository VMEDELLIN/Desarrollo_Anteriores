using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WALLET.Controllers;

namespace WALLET
{
    public class FileData : IFileData
    {
        public async Task Create(string path)
        {
            await Task.Delay(100);
            LogClass.LogInfo("AplicaTransferencia", "AplicaPayCash", $"PayCash");
            //using (var sw=new StreamWriter(path))
            //{
            //    await Task.Delay(1000);
            //    await sw.WriteAsync("Pato");
            //}
        }
        public async Task Wallet()
        {
            await Task.Delay(10);
            if (Singleton.Instance.PilaCargo.Count > 0)
                realizarAbono(Singleton.Instance.PilaCargo.Dequeue());
        }
        public ResponseCargo realizarAbono(Cargo cargo)
        {
            string Date = DateTime.Now.ToString("ddMMyyyy");
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(11)
                .ToList().ForEach(e => builder.Append(e));
            string key = Date + builder.ToString() + " - ";

            ResponseCargo response = new ResponseCargo();
            if (cargo.ClienteId != 0 && cargo.NumCuenta != 0 && cargo.TipoRecurso != 0 && !string.IsNullOrEmpty(cargo.Descripcion) && cargo.Monto != 0)
            {
                try
                {
                    LogClass.LogInfo(key + " Desencolar", "WalletBLL Transferencia", $"Procesando petición {cargo.Id}");
                    ServicioBilletera.Service wsBilletera = new ServicioBilletera.Service();
                    Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    string Token = Encrypt.CreateMD5(ConfigurationManager.AppSettings["PWDBILLETERA"] + unixTimestamp + cargo.ClienteId);
                    try
                    {
                        string respuesta = wsBilletera.Abono(cargo.ClienteId, unixTimestamp, Token, cargo.NumCuenta, cargo.TipoRecurso, cargo.Referencia, cargo.Descripcion, cargo.Monto);
                        response = JsonConvert.DeserializeObject<ResponseCargo>(respuesta);
                        LogClass.LogInfo(key + " Desencolar", "WalletBLL Transferencia", $"Procesando petición fin {cargo.Id}");
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

        public async Task Transferencia()
        {
            await Task.Delay(100);
            //LogClass.LogInfo("AplicaTransferencia","AplicaSTP", $"STP");
            if (Singleton.Instance.ColaTrannsferencia.Count > 0)
            {
                Ejecuta("BANCO", Singleton.Instance.ColaTrannsferencia.Dequeue());
                //InsertarTransferencia("BANCO",Singleton.Instance.ColaTrannsferencia.Dequeue());
            }

            //await Task.Delay(10);
            //LogClass.LogInfo("AplicaTransferencia", "AplicaPayCash", $"PayCash");

        }
        private void Ejecuta(string tipo, TransferenciaEntity oTransfer) {

            var aplica = Aplica(3, tipo, oTransfer, InsertarTransferencia);
            aplica();
            aplica();
            aplica();
        }
        private Action Aplica(int Intentos, string tipo, TransferenciaEntity oTransfer, Func<string, int, TransferenciaEntity, bool> InsertTransfer)
        {
            int counter = 0;
            bool  Aplicado = false;
            return () =>
            {
                if (!Aplicado)
                {
                    if (counter < Intentos)
                    {
                        Aplicado = InsertTransfer(tipo, (counter + 1), oTransfer);
                    }
                    else
                        LogClass.LogInfo($"{oTransfer.key} CrossBorderWeb", $"AplicaTransferencia Tipo {tipo} Intento {counter + 1}", $"Se exedio el número de intentos, no se aplico la Transferencia Id {oTransfer.nId} Referencia {oTransfer.nReferenciaNumerica} FolioWallet {oTransfer.FolioWallet}");
                }
                else
                    LogClass.LogInfo($"{oTransfer.key} CrossBorderWeb", $"AplicaTransferencia Tipo {tipo} Intento {counter + 1}", $"La Transferencia Id {oTransfer.nId} Referencia {oTransfer.nReferenciaNumerica} FolioWallet {oTransfer.FolioWallet} ya fue aplicada anteriormente");

                counter++;
            };
        }

        public bool InsertarTransferencia(string tipo, int Intentos, TransferenciaEntity oTransfer)
        {
            string key = oTransfer.key;
            bool exito = false;
            MySqlConnection _sqlCnn = null; ;
            MySqlDataReader _sqlDatRead = null;
            MySqlCommand _sqlCmd;
            //int numIntentos = 1;
            try
            {
                //if (numIntentos > Intentos)
                //    throw new Exception($"Se exedio el número de intentos, no se aplico la Transferencia Id {oTransfer.nId} Referencia {oTransfer.nReferenciaNumerica} FolioWallet {oTransfer.FolioWallet}");

                LogClass.LogInfo($"{key} CrossBorderWeb", $"AplicaTransferencia Tipo {tipo} Intento {Intentos}", $"Inicia Insertar Transferencia Id {oTransfer.nId} Referencia {oTransfer.nReferenciaNumerica} FolioWallet {oTransfer.FolioWallet}");

                _sqlCnn = new MySqlConnection(Singleton.Instance.cadenaConexion);
                _sqlCnn.Open();
                _sqlCmd = new MySqlCommand();
                _sqlCmd.Connection = _sqlCnn;
                _sqlCmd.CommandTimeout = 0;

                if(Intentos==2)
                    _sqlCmd.CommandText = "dbtransfer.sp_insert_transferencia";
                else
                    _sqlCmd.CommandText = "dbtransfer.sp_insert_transferen";
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
                _sqlCmd.Parameters.AddWithValue("@CkFolioWallet", oTransfer.FolioWallet);
                _sqlCmd.Parameters.AddWithValue("@CkdMontoWallet", oTransfer.dMontoWallet);

                LogClass.LogInfo($"{key} CrossBorderWeb", $"AplicaTransferencia Tipo {tipo} Intento {Intentos}", $"sp_insert_transferencia Prepara comando ");
                _sqlDatRead = _sqlCmd.ExecuteReader();
                LogClass.LogInfo($"{key} CrossBorderWeb", $"AplicaTransferencia Tipo {tipo} Intento {Intentos}", $"sp_insert_transferencia Ejecuta comando ");
                if (_sqlDatRead.HasRows == true)
                {
                    _sqlDatRead.Read();
                    exito = !Convert.ToBoolean((Int32)_sqlDatRead["CodigoRespuesta"]);
                    LogClass.LogInfo($"{key} CrossBorderWeb", $"AplicaTransferencia Tipo {tipo} Intento {Intentos}", $"sp_insert_transferencia Resultado {(exito?"Exitoso":"Fallido")} => {_sqlDatRead["CodigoRespuesta"].ToString()} Mensaje {_sqlDatRead["MsgRespuesta"].ToString()}");
                    _sqlDatRead.Close();
                }
            }
            catch (Exception ex)
            {
                LogClass.LogInfo($"{key} CrossBorderWeb", $"AplicaTransferencia Tipo {tipo} Intento {Intentos}", $"sp_insert_transferencia Error=> { ex.ToString()}");

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
            LogClass.LogInfo($"{key} CrossBorderWeb", $"AplicaTransferencia Tipo {tipo} Intento {Intentos}", $"Fin Insertar Transferencia Id {oTransfer.nId} Referencia {oTransfer.nReferenciaNumerica} FolioWallet {oTransfer.FolioWallet}");

            return exito;
        }
    }
}
