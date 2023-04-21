using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeerQEQ
{
    class Program
    {
        static void Main(string[] args)
        {
            ////string ConnectionString = "mongodb://thetruthalwaystriumphs:th3truth4lwaystr1umph5@ec2-35-84-37-131.us-west-2.compute.amazonaws.com:26047/?authSource=blacklist&authMechanism=SCRAM-SHA-256;";
            //string ConnectionString = "mongodb://localhost:27017/";
            //string NameBdd = "PLD";
            //string Collection = "PLDLIST";
            ////var client = new MongoClient(ConnectionString);
            ////var db = client.GetDatabase(NameBdd);
            ////var collection = db.GetCollection<BsonDocument>(Collection);

            //List<DatabaseResponse> objDatabaseResponse = new List<DatabaseResponse>();
            //string PathFileMongoDB = @"C:\Log\TransferDirecto\DownloadFileSftpBlackList\toDown\";
            //string File = "BasesV09.txt";
            ////string nameFileUtf8="20230110170109.txt";
            //var nameFileUtf8 = DateTime.Now.ToString("yyyyMMddHHMMss");
            //var convert = ConvertFileUtf8(PathFileMongoDB, PathFileMongoDB, File, nameFileUtf8);
            //if (convert.Split('|')[0] == "1")
            //{

            //    DataTable datatable = new DataTable();
            //    //var pathFile = $"{ ResponseDirectory.First().PathFileMongoDB }{ ResponseDirectory.First().File.Split('|')[1] }";
            //    var pathFile = $"{ PathFileMongoDB }{ nameFileUtf8 }.txt";               

            //    StreamReader streamreader = new StreamReader(@pathFile);
            //    char[] delimiter = new char[] { '\t' };
            //    string[] columnheaders = streamreader.ReadLine().Split(delimiter);


            //    var client = new MongoClient(ConnectionString);
            //    var db = client.GetDatabase(NameBdd);
            //    var collection = db.GetCollection<BsonDocument>(Collection);

            //    foreach (string columnheader in columnheaders)
            //    {
            //        datatable.Columns.Add(columnheader); // I've added the column headers here.
            //    }

            //    while (streamreader.Peek() > 0)
            //    {
            //        DataRow datarow = datatable.NewRow();
            //        datarow.ItemArray = streamreader.ReadLine().Split(delimiter);
            //        datatable.Rows.Add(datarow);
            //    }
            //    streamreader.Close();
            //    streamreader.Dispose();

            //    List<BsonDocument> batch = new List<BsonDocument>();
            //    foreach (DataRow dr in datatable.Rows)
            //    {
            //        var dictionary = dr.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
            //        batch.Add(new BsonDocument(dictionary));
            //    }


            //    DeleteFileUtf8(pathFile);

            //    Console.WriteLine($"Star DeleteMany mongo");
            //    Console.WriteLine("Date and Time with Milliseconds: {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));                
            //    collection.DeleteMany(Builders<BsonDocument>.Filter.Empty);
            //    Console.WriteLine("Date and Time with Milliseconds: {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
            //    Console.WriteLine($"End DeleteMany mongo");

            //    Console.WriteLine($"Star InsertMany mongo");
            //    Console.WriteLine("Date and Time with Milliseconds: {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));


            //    //collection.InsertMany(batch.AsEnumerable());

            //    Console.WriteLine("Date and Time with Milliseconds: {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
            //    Console.WriteLine($"End InsertMany mongo");

            //    objDatabaseResponse.Add(new DatabaseResponse("0", "succeeded"));



            //}

            //string data = "2";
            BulkInsertMongoDb();
            Console.ReadLine();
        }
        public static async Task BulkInsertMongoDb()
        {

            //string ConnectionString = "mongodb://thetruthalwaystriumphs:th3truth4lwaystr1umph5@ec2-35-84-37-131.us-west-2.compute.amazonaws.com:26047/?authSource=blacklist&authMechanism=SCRAM-SHA-256;";
            string ConnectionString = "mongodb://localhost:27017/";
            string NameBdd = "PLD";
            string Collection = "PLDLIST";
            //var client = new MongoClient(ConnectionString);
            //var db = client.GetDatabase(NameBdd);
            //var collection = db.GetCollection<BsonDocument>(Collection);

            List<DatabaseResponse> objDatabaseResponse = new List<DatabaseResponse>();
            string PathFileMongoDB = @"C:\Log\TransferDirecto\DownloadFileSftpBlackList\toDown\";
            string File = "BasesV09.txt";
            //string nameFileUtf8="20230110170109.txt";
            var nameFileUtf8 = DateTime.Now.ToString("yyyyMMddHHMMss");
            var convert = ConvertFileUtf8(PathFileMongoDB, PathFileMongoDB, File, nameFileUtf8);
            if (convert.Split('|')[0] == "1")
            {

                DataTable datatable = new DataTable();
                //var pathFile = $"{ ResponseDirectory.First().PathFileMongoDB }{ ResponseDirectory.First().File.Split('|')[1] }";
                var pathFile = $"{ PathFileMongoDB }{ nameFileUtf8 }.txt";

                StreamReader streamreader = new StreamReader(@pathFile);
                char[] delimiter = new char[] { '\t' };
                string[] columnheaders = streamreader.ReadLine().Split(delimiter);


                var client = new MongoClient(ConnectionString);
                //var db = client.GetDatabase(NameBdd);
                //var collection = db.GetCollection<BsonDocument>(Collection);

                IMongoDatabase db = client.GetDatabase(NameBdd);                
                var collection = db.GetCollection<BsonDocument>(Collection);

                foreach (string columnheader in columnheaders)
                {
                    datatable.Columns.Add(columnheader); // I've added the column headers here.
                }

                while (streamreader.Peek() > 0)
                {
                    DataRow datarow = datatable.NewRow();
                    datarow.ItemArray = streamreader.ReadLine().Split(delimiter);
                    datatable.Rows.Add(datarow);
                }
                streamreader.Close();
                streamreader.Dispose();

                //List<BsonDocument> batch = new List<BsonDocument>();
                var listWrites = new List<WriteModel<BsonDocument>>();
                foreach (DataRow dr in datatable.Rows)
                {
                    var dictionary = dr.Table.Columns.Cast<DataColumn>().ToDictionary(col => col.ColumnName, col => dr[col.ColumnName]);
                    //batch.Add(new BsonDocument(dictionary));
                    listWrites.Add(new InsertOneModel<BsonDocument>((new BsonDocument(dictionary))));
                    //listWrites.Add(new InsertOneModel<BsonDocument>(dictionary))

                }


                DeleteFileUtf8(pathFile);

                Console.WriteLine($"Star DeleteMany mongo");
                Console.WriteLine("Date and Time with Milliseconds: {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
                collection.DeleteMany(Builders<BsonDocument>.Filter.Empty);
                Console.WriteLine("Date and Time with Milliseconds: {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
                Console.WriteLine($"End DeleteMany mongo");

                Console.WriteLine($"Star InsertMany mongo");
                Console.WriteLine("Date and Time with Milliseconds: {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));

                var resultWrites = await collection.BulkWriteAsync(listWrites);

                Console.WriteLine($"OK?: {resultWrites.IsAcknowledged} - Inserted Count: {resultWrites.InsertedCount}");

                //collection.InsertMany(batch.AsEnumerable());

                Console.WriteLine("Date and Time with Milliseconds: {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
                Console.WriteLine($"End InsertMany mongo");

                objDatabaseResponse.Add(new DatabaseResponse("0", "succeeded"));



            }

            string data = "2";




            //IMongoDatabase db = _client.GetDatabase("sample_blog");

            //var listWrites = new List<WriteModel<User>>();
            //var totalNewUsers = 1000;

            //for (int i = 0; i < totalNewUsers; i++)
            //{
            //    var newUser = new User
            //    {
            //        name = $"customName-{i}",
            //        email = $"customEmail-{i}@domain{i}.com",
            //        createdAt = DateTime.Now,
            //        isBlocked = false
            //    };

            //    listWrites.Add(new InsertOneModel<User>(newUser));
            //}

            //var userCollection = db.GetCollection<User>("users");
            //var resultWrites = await userCollection.BulkWriteAsync(listWrites);

            //Console.WriteLine($"OK?: {resultWrites.IsAcknowledged} - Inserted Count: {resultWrites.InsertedCount}");







        }
        public static string ConvertFileUtf8(string pathSourceFile, string pathDesFile, string nameFileSource, string nameFileDes)
        {
            string succeeded = string.Empty;
            string fileSource = $"{ pathSourceFile }{ nameFileSource }";
            string supplementaryFile = $"{ pathSourceFile }BasesVC.txt";
            string remameWithUtf8 = $"{ pathDesFile }{ nameFileDes }.txt";

            try
            {
                Encoding utf8WithoutBom = new UTF8Encoding(false);
                string filetext = File.ReadAllText(fileSource, Encoding.Default);
                string supplementaryFileText = File.ReadAllText(supplementaryFile, Encoding.Default);
                //string fullText = $"{filetext}\n{supplementaryFileText}";

                File.WriteAllText(@remameWithUtf8, filetext + supplementaryFileText, utf8WithoutBom);

                succeeded = $"1|{ nameFileDes }";
            }
            catch (Exception err)
            {
                succeeded = $"0|{ err.Message }";
            }
            return succeeded;
        }
        public static void DeleteFileUtf8(string pathSourceFile)
        {
            string message;
            try
            {
                File.Delete(pathSourceFile);
                message = "ok";
            }
            catch (Exception err)
            {
                message = $"error --> { err.Message }";
            }

        }
    }
    public struct DatabaseResponse
    {
        public string LoadFileSuccess { get; set; }
        public string Error { get; set; }

        public DatabaseResponse(string loadFileSuccess, string error)
        {
            LoadFileSuccess = loadFileSuccess;
            Error = error;
        }
    }
}
