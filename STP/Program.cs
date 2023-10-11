using ConsultaSTP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace STP
{
    class Program
    {
        static  void  Main(string[] args)
        {

            //TRANSFERDIRECTO 646180204300000000
            //aLkFxNmduaJLx2Jd6vCbp3kOx/ISv8pE3r8yW6JDF3gWO8BY1BBDaD5p2SLgfz6GqfALwVv2gUGY4B4YqEIBaxz2PNRUIcLnjonTmljGAbZ5WqCt+oHI8OinR31f7r7IJ5+hkyZ7eVPOdk6+hc87Ju0e/buOpRY2Y+Ak1v7/sGX4dDe8kTmKnNqsdsOLuhW8+CEDiicG+8Lo/QFmL9IYjmtUQvP3x3nnyfEBdAerfEM81UBUdKB1HJenMatPhI/6Lh3tTHIy5fPqt8Z/rqrSY8MOpddrkca7q7eFeSlWS+iYvGMIDUW5+lLiwl0/vpYEOz7xj1n0ZlxHpjIVEGmXFA\u003d\u003d
            //TRANSPAY_MEXICO 646180204303000007
            //iJ7wwlmVxsRq4M7mya3HUCWikeMQHmcrWEiqgiq4isVkNiYtQr8SZp9Sox7R/sObz7piH0jh/q+CGyc2eZ04vBkNEExhxryHFg4OsgTML9LTJSy2iDgle3FK5RL1gCMBBbxqtHsh+pkX+aPzl8Vy3aF8BL1QnEziYuBpvGDzJ8iZNDQSWWRar4acZwx5M1z3mX3dX4iu3znItoRSIrQeg+4yvMteOM36AMmomWQvXM2fCYM9D8y5xCEcp+7LqsMRHlZXuhRQ5RNkUrOwFzxxE4Ka+zwDvQ6nF0+qkqnm5w9BRW2I9DMbzfw1Gzx/V/ERi34KLt4bvguUG/UpyrQ02Q\u003d\u003d
            //Iniciar();
            var urlBusqueda = "https://efws-dev.stpmex.com/efws/API/consultaSaldoCuenta";
            CryptoHandler cryptoHandler = new CryptoHandler();
            ConsultaSaldoCuentaRequest oConsultaSaldoRequest = new ConsultaSaldoCuentaRequest() { empresa = "TRANSPAY_MEXICO", cuentaOrdenante = "646180204303000007", firma = "" };            
            string firmaa= cryptoHandler.cadenaOriginal("TRANSPAY_MEXICO", "646180204303000007","");
            oConsultaSaldoRequest.firma = firmaa;            

            var jsonDatos = JsonConvert.SerializeObject(oConsultaSaldoRequest);
            HttpWebRequest request = GeneraRequest(urlBusqueda, "POST", jsonDatos, "");
            Envoltura oEnvoltura = new Envoltura();
            oEnvoltura = GeneraResponse<ConsultaSaldoCuentaResponse>(request);


            cryptoHandler = new CryptoHandler();
            oConsultaSaldoRequest = new ConsultaSaldoCuentaRequest() { empresa = "TRANSFERDIRECTO", cuentaOrdenante = "646180204300000000", firma = "" };
            firmaa = cryptoHandler.cadenaOriginal("TRANSFERDIRECTO", "646180204300000000", "");
            oConsultaSaldoRequest.firma = firmaa;

            jsonDatos = JsonConvert.SerializeObject(oConsultaSaldoRequest);
            request = GeneraRequest(urlBusqueda, "POST", jsonDatos, "");
            oEnvoltura = new Envoltura();
            oEnvoltura = GeneraResponse<ConsultaSaldoCuentaResponse>(request);
            Console.ReadLine();
        }
        public static async void Iniciar()
        {

            try
            {
                ConsultaSaldoCuentaResponse json = new ConsultaSaldoCuentaResponse();
                //json = await 
                Consulta();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        public static async Task<ConsultaSaldoCuentaResponse> Consulta()
        {
            ConsultaSaldoCuentaResponse res = new ConsultaSaldoCuentaResponse();

            
            CryptoHandler cryptoHandler = new CryptoHandler();
            ConsultaSaldoCuentaRequest oConsultaSaldoRequest = new ConsultaSaldoCuentaRequest() { empresa = "TRANSPAY_MEXICO", cuentaOrdenante = "646180204303000007", firma = "" };
            oConsultaSaldoRequest.firma = "iJ7wwlmVxsRq4M7mya3HUCWikeMQHmcrWEiqgiq4isVkNiYtQr8SZp9Sox7R/sObz7piH0jh/q+CGyc2eZ04vBkNEExhxryHFg4OsgTML9LTJSy2iDgle3FK5RL1gCMBBbxqtHsh+pkX+aPzl8Vy3aF8BL1QnEziYuBpvGDzJ8iZNDQSWWRar4acZwx5M1z3mX3dX4iu3znItoRSIrQeg+4yvMteOM36AMmomWQvXM2fCYM9D8y5xCEcp+7LqsMRHlZXuhRQ5RNkUrOwFzxxE4Ka+zwDvQ6nF0+qkqnm5w9BRW2I9DMbzfw1Gzx/V/ERi34KLt4bvguUG/UpyrQ02Q\u003d\u003d";
            //cryptoHandler.FirmaConsultaSaldoCuenta("TRANSPAY_MEXICO", "646180204303000007","");

            var url = "https://efws-dev.stpmex.com/efws/API/consultaSaldoCuenta";
            var client = new HttpClient();
            CryptoHandler dssv = new CryptoHandler();
            var dataS = JsonConvert.SerializeObject(oConsultaSaldoRequest);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpContent content = new StringContent(dataS, System.Text.Encoding.UTF8, "application/json");
            
            var httpResponse = await client.PostAsync(url, content);

            if (httpResponse.IsSuccessStatusCode)
            {
                var result = await httpResponse.Content.ReadAsStringAsync();
                ConsultaSaldoCuentaResponse postResult = JsonConvert.DeserializeObject<ConsultaSaldoCuentaResponse>(result);
                // if (postResult.resultado.id < 1)
                Console.WriteLine("Fin=>"+ result);
            }
            else
            {
                
            }

            return res;
        }
        private static HttpWebRequest GeneraRequest(string url, string metodo, string data, string token)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            //ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Ssl3;

            var buffer = System.Text.Encoding.UTF8.GetBytes(data);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = metodo;
            //request.Headers.Add("Authorization", token);
            request.ContentType = "application/json";
            request.Accept = "application/json";
            //request.ContentLength = buffer.Length;





            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(data);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        private static Envoltura GeneraResponse<T>(HttpWebRequest request)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)(0xc0 | 0x300 | 0xc00);
            //ServicePointManager.SecurityProtocol &= ~SecurityProtocolType.Ssl3;
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // Equivalente a SecurityProtocolType.Tls12

            Envoltura oEnvoltura = new Envoltura();
            oEnvoltura.Status = false;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null)
                    {
                        oEnvoltura.Status = false;
                        oEnvoltura.Mensajes.Add(new Mensajes() { Codigo = 401, Mensaje = "No se pudo realizar la petición." });
                    }
                    else
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            //dynamic result = JObject.Parse(responseBody);
                            T oResponse = JsonConvert.DeserializeObject<T>(responseBody);
                            oEnvoltura.Status = true;
                            oEnvoltura.Contenedor.Add(oResponse);
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            return oEnvoltura;
        }
    }
    public class Envoltura
    {
        public bool Status { get; set; }
        public IList<object> Contenedor { get; set; }
        public IList<Mensajes> Mensajes { get; set; }
        public Envoltura() : this(false, new List<object>())
        {

        }
        public Envoltura(bool status, List<object> list) : this(status, list, new List<Mensajes>())
        {

        }
        public Envoltura(bool status, List<object> list, IList<Mensajes> mensajes)
        {
            Status = status;
            Contenedor = list;
            Mensajes = mensajes;
        }

    }
    public class Mensajes
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; }
    }
    public class ConsultaSaldoCuentaRequest
    { 
        public string empresa { get; set; }
        public string cuentaOrdenante { get; set; }
        public string firma { get; set; }
    }
    public class ConsultaSaldoCuentaResponse
    {
        public string estado { get; set; }
        public string mensaje { get; set; }
        public ConsultaSaldoCuentaDetalleResponse respuesta { get; set; }
    }
    public class ConsultaSaldoCuentaDetalleResponse
    {
        public string cargosPendientes { get; set; }
        public string saldo { get; set; }
        public string firma { get; set; }
    }
}
