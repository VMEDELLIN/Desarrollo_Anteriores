using Airpak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LeerXML
{
    class Program
    {
        static void Main(string[] args)
        {
            string StrRequest = string.Empty;
            string ErrorMsg = string.Empty;
            int ErrorCode = 299;
            string StrResponse = string.Empty;
            StrResponse= @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'><soapenv:Header /><soapenv:Body><pay-money-search-reply><payment_transactions><transaction><sender><name><first_name1 /><first_name2 /><last_name1 /><last_name2 /></name><address><addr_line1 /><addr_line2 /><city /><state /><country /><postal_code /></address><phone_number><number /><country_code /><city_code /><phone_type /></phone_number></sender><receiver><name><first_name1 /><first_name2 /><last_name1 /><last_name2 /></name><address><addr_line1 /><addr_line2 /><city /><state /><country /><postal_code /></address><phone_number><number /><country_code /><city_code /><phone_type /></phone_number></receiver><transaction_details><origin><country /><currency /></origin><destination><country /><currency /><city /><state /><location /><namelocation /></destination><transaction_date /><transaction_type /></transaction_details><financial_details><origin_amount /><exchange_rate /><destination_amount /><charges /><service_charges /><total_tax /><total_amount /></financial_details><transfer_key /><transfer_number>94221910220004</transfer_number><transfer_TransferID /><transfer_found>False</transfer_found><transfer_response_code>ED04</transfer_response_code><transfer_response_text>Giro no disponible, Bloqueado</transfer_response_text></transaction></payment_transactions></pay-money-search-reply></soapenv:Body></soapenv:Envelope>";

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.LoadXml(StrResponse);
            }
            catch (Exception e)
            {
                StrRequest = string.Empty;
                ErrorMsg = e.Message;
                ErrorCode = 299;
            }

            string ResponseType = doc.DocumentElement.Name;
            object Response = null; ;
            switch (ResponseType)
            {
                case "status":
                    //Response = XMLUtilities.ObjectToXML(StrResponse, typeof(status));
                    Response = XMLUtilities.GetObjectFromSOAPXML<status>(StrResponse);

                    var StatusResponse = (status)Response;

                    ErrorCode = 202;
                    ErrorMsg = $"{StatusResponse.code}:{StatusResponse.description}";

                    //LogClass.LogError(Id, "Airpak.Lib.SendRequest", StatusResponse.type);

                    break;
                case "soapenv:Envelope":
                    //Response = XMLUtilities.ObjectToXML(StrResponse, typeof(Envelope));
                    Response = XMLUtilities.GetObjectFromSOAPXML<SOAPEnvelope>(StrResponse);

                    var EnvelopeResponse = Response;

                    ErrorCode = 0;
                    ErrorMsg = "OK";

                    break;
                case "Fault":
                    //Response = XMLUtilities.ObjectToXML(StrResponse, typeof(Fault));

                    //var FaultResponse = (Fault)Response;

                    //var ComplianceError = (FaultResponse.detail.airpak_error != null) ? (FaultResponse.detail.airpak_error.error + " - ") : (string.Empty);


                    //ErrorCode = 1;
                    //ErrorMsg = $"{ComplianceError}{FaultResponse.faultstring}";

                    //LogClass.LogError(Id, "Airpak.Lib.SendRequest", FaultResponse.faultactor);

                    break;
                default:
                    ErrorCode = 202;
                    ErrorMsg = "Error de comunicacion. Respuesta invalida.";
                    break;
            }



             
            var vSearchReply = ((SOAPEnvelope)Response).body.pay_money_search_reply;

            if (vSearchReply == null)
            {
                ErrorCode = 203;
                ErrorMsg = "Error de comunicacion, invalid pay_money_search_reply";
                
            }

            var vTransaction = vSearchReply.payment_transactions.transaction;
            if (vTransaction != null)
            {

                string TRANSFER_FOUND = (vTransaction != null) ? (vTransaction.transfer_found) : (string.Empty);

                bool.TryParse(TRANSFER_FOUND, out bool Found);

                if (Found)
                {
                    ErrorCode = 0;
                    ErrorMsg = "Consulta Exitosa";

                    //string TRANSFER_NUMBER = (vTransaction != null) ? (vTransaction.transfer_number) : (string.Empty);
                    //string TRANSFER_TRANSFERID = (vTransaction != null) ? (vTransaction.transfer_TransferID) : (string.Empty);
                    ////string TRANSFER_SUBTYPE = (vTransaction != null) ? (vTransaction.transaction_details.) : (string.Empty);
                    //string TRANSFER_RESPONSE_CODE = (vTransaction != null) ? (vTransaction.transfer_response_code) : (string.Empty);
                    //string TRANSFER_RESPONSE_TEXT = (vTransaction != null) ? (vTransaction.transfer_response_text) : (string.Empty);

                    //var vRemitente = vTransaction.sender;
                    //string F_NAME = (vRemitente != null) ? (vRemitente.name.first_name1) : (string.Empty);
                    //string M_NAME = (vRemitente != null) ? (vRemitente.name.first_name2) : (string.Empty);
                    //string S_NAME = (vRemitente != null) ? (vRemitente.name.last_name1) : (string.Empty);
                    //string L_NAME = (vRemitente != null) ? (vRemitente.name.last_name2) : (string.Empty);

                    //var vBemeficiario = vTransaction.receiver;
                    //string F_NAME_B = (vBemeficiario != null) ? (vBemeficiario.name.first_name1) : (string.Empty);
                    //string M_NAME_B = (vBemeficiario != null) ? (vBemeficiario.name.first_name2) : (string.Empty);
                    //string S_NAME_B = (vBemeficiario != null) ? (vBemeficiario.name.last_name1) : (string.Empty);
                    //string L_NAME_B = (vBemeficiario != null) ? (vBemeficiario.name.last_name2) : (string.Empty);

                    //ArrayList alRemitente = new ArrayList();
                    //if (!string.IsNullOrEmpty(F_NAME)) { alRemitente.Add(F_NAME); }
                    //if (!string.IsNullOrEmpty(M_NAME)) { alRemitente.Add(M_NAME); }
                    //if (!string.IsNullOrEmpty(S_NAME)) { alRemitente.Add(S_NAME); }
                    //if (!string.IsNullOrEmpty(L_NAME)) { alRemitente.Add(L_NAME); }

                    //Remitente.NombreCompleto = string.Join(" ", alRemitente.ToArray());
                    //Remitente.Nombre = (F_NAME != string.Empty ? F_NAME : "");
                    //Remitente.Nombre += (Remitente.Nombre != string.Empty ? (M_NAME != string.Empty ? " " + M_NAME : "") : "");
                    //Remitente.Paterno = (S_NAME != string.Empty ? S_NAME : "");
                    //Remitente.Materno = (L_NAME != string.Empty ? L_NAME : "");


                    //ArrayList alBeneficiario = new ArrayList();
                    //if (!string.IsNullOrEmpty(F_NAME_B)) { alBeneficiario.Add(F_NAME_B); }
                    //if (!string.IsNullOrEmpty(M_NAME_B)) { alBeneficiario.Add(M_NAME_B); }
                    //if (!string.IsNullOrEmpty(S_NAME_B)) { alBeneficiario.Add(S_NAME_B); }
                    //if (!string.IsNullOrEmpty(L_NAME_B)) { alBeneficiario.Add(L_NAME_B); }
                    //Beneficiario.NombreCompleto = string.Join(" ", alBeneficiario.ToArray());
                    //Beneficiario.Nombre = (F_NAME_B != string.Empty ? F_NAME_B : "");
                    //Beneficiario.Nombre += (Beneficiario.Nombre != string.Empty ? (M_NAME_B != string.Empty ? " " + M_NAME_B : "") : string.Empty);
                    //Beneficiario.Paterno = (S_NAME_B != string.Empty ? S_NAME_B : "");
                    //Beneficiario.Materno = (L_NAME_B != string.Empty ? L_NAME_B : "");



                    //var vTransactionDetails = vTransaction.transaction_details;

                    //string LOCATION = (vTransactionDetails != null) ? (vTransactionDetails.location) : (string.Empty);
                    //string ORIGIN_COUNTRY = (vTransactionDetails != null) ? (vTransactionDetails.origin.country) : (string.Empty);
                    //string ORIGIN_CURRENCY = (vTransactionDetails != null) ? (vTransactionDetails.origin.currency) : (string.Empty);
                    //string DESTINATION_COUNTRY = (vTransactionDetails != null) ? (vTransactionDetails.destination.country) : (string.Empty);
                    //string DESTINATION_CURRENCY = (vTransactionDetails != null) ? (vTransactionDetails.destination.currency) : (string.Empty);
                    //string TRANSACTION_DATE = (vTransactionDetails != null) ? (vTransactionDetails.transaction_date) : (string.Empty);
                    //string TRANSACTION_TYPE = (vTransactionDetails != null) ? (vTransactionDetails.transaction_type) : (string.Empty);




                    //string Ori = "Ciudad/Estado, Pais: " + ORIGIN_COUNTRY;
                    //Remesa.Origen = Ori;

                    //string Dest = "Ciudad/Estado, Pais: " + Header.Agencia.Direccion.NombreCiudad + "/ " + Header.Agencia.Direccion.NombreEstado + ", " + Header.Agencia.Direccion.NombrePais;
                    //Remesa.Destino = Dest;


                    //var vFinantialsDetails = vTransaction.financial_details;

                    //string CHARGES = (vFinantialsDetails != null) ? (vFinantialsDetails.charges) : (string.Empty);
                    //string SERVIGE_CHANGES = (vFinantialsDetails != null) ? (vFinantialsDetails.service_charges) : (string.Empty);
                    //string EXCHANGE_RATE = (vFinantialsDetails != null) ? (vFinantialsDetails.exchange_rate) : (string.Empty);
                    //string TOTAL_TAX = (vFinantialsDetails != null) ? (vFinantialsDetails.total_tax) : (string.Empty);
                    //string TOTAL_AMOUNT = (vFinantialsDetails != null) ? (vFinantialsDetails.total_amount) : (string.Empty);
                    //string ORIGIN_AMOUNT = (vFinantialsDetails != null) ? (vFinantialsDetails.origin_amount) : (string.Empty);
                    //string DESTINATION_AMOUNT = (vFinantialsDetails != null) ? (vFinantialsDetails.destination_amount) : (string.Empty);

                    //decimal.TryParse(TOTAL_TAX, out TotalTax);
                    //decimal.TryParse(TOTAL_AMOUNT, out TotalAmount);
                    //decimal.TryParse(ORIGIN_AMOUNT, out OriginAmount);
                    //decimal.TryParse(DESTINATION_AMOUNT, out DestinationAmount);

                }
                else
                {
                    ErrorCode = 205;
                    ErrorMsg = "Clave de envio invalido o no se ha encontrado.";
                }

            }
            else
            {
                ErrorCode = 203;
                ErrorMsg = "Error de comunicacion, invalid pay_money_search_reply";
            }
        }
        public class status
        {

            public string code { get; set; }

            public string type { get; set; }

            public string description { get; set; }
        }
    }
}
