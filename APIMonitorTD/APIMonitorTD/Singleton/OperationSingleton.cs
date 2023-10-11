using APIMonitorTD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIMonitorTD.Singleton
{
    public class OperationSingleton
    {
        private static OperationSingleton instance = null;
        private List<Operation> OperationCollection = null;
        private OperationSingleton()
        {
            OperationCollection = new List<Operation>();
        }
        public static OperationSingleton Instance
        {
            get
            {
                if (instance == null)
                    instance = new OperationSingleton();
                return instance;
            }
        }
        public void Add(Operation oOperation)
        {

            Operation oOperationSelect = OperationCollection.Where(x => x.Referencia == oOperation.Referencia).FirstOrDefault();
            if (oOperationSelect != null)
                OperationCollection.Remove(oOperationSelect);

            OperationCollection.Add(oOperation);
        }
        public string GetAllToJson()
        {
            //JsonSerializerOptions options = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault,
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase                
            //};
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string jsonString = JsonSerializer.Serialize(OperationCollection, options);
            return jsonString;
        }
        public int GetTryToJson()
        {
            int reusult = OperationCollection.Where(x => x.IdEstatus == 1).Count();
            return reusult;
        }
        public int GetPayToJson()
        {
            int reusult = OperationCollection.Where(x => x.IdEstatus == 0).Count();
            return reusult;
        }
        public int GetCalcellToJson()
        {
            int reusult = OperationCollection.Where(x => x.IdEstatus == 4).Count();
            return reusult;
        }
        //public string GetTryToJson()
        //{

        //    JsonSerializerOptions options = new JsonSerializerOptions
        //    {
        //        WriteIndented = true,
        //        IgnoreNullValues = false,
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //    };

        //    string jsonString = JsonSerializer.Serialize(OperationCollection.Select(x => x.IdEstatus == 0), options);
        //    return jsonString;
        //}
        //public string GetPayToJson()
        //{

        //    JsonSerializerOptions options = new JsonSerializerOptions
        //    {
        //        WriteIndented = true,
        //        IgnoreNullValues = false,
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //    };

        //    string jsonString = JsonSerializer.Serialize(OperationCollection.Select(x=>x.IdEstatus==0), options);
        //    return jsonString;
        //}
        //public string GetCalcellToJson()
        //{

        //    JsonSerializerOptions options = new JsonSerializerOptions
        //    {
        //        WriteIndented = true,
        //        IgnoreNullValues = false,
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //    };

        //    string jsonString = JsonSerializer.Serialize(OperationCollection.Select(x => x.IdEstatus == 4), options);
        //    return jsonString;
        //}
    }
}
