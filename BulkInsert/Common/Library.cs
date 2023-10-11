using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Globalization;
using Newtonsoft.Json;

namespace Common
{
    public static class Library
    {
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string getRegistryValue(RegistryKey root, string keyName)
        {
            string tmp = "";
            tmp = (string)root.GetValue(keyName);

            if (tmp == null) { return ""; }

            return tmp;
        }


        public static IPAddress resolveHostName(string host)
        {
            IPAddress ipAddress = IPAddress.Loopback;

            try
            {
                IPAddress[] ipAddressList = Dns.GetHostAddresses(host);

                for (int i = 0; i < ipAddressList.Length; i++)
                {
                    if (ipAddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ipAddressList[i];
                        break;
                    }
                }
            }
            catch
            {
                ipAddress = IPAddress.Loopback;
            }

            return ipAddress;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string ConvertToBase64(string UTF8Str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(UTF8Str));
        }
        public static string ConvertToUTF8(string Base64Str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(Base64Str));
        }

        public static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static bool IsDateValid(string StrDate)
        {
            int Year, Month, Day = 0;

            string[] Datos = StrDate.Split('-');

            if (!Datos.Length.Equals(3)) { return false; }

            if (!int.TryParse(Datos[0], out Year)) { return false; }
            if (!int.TryParse(Datos[1], out Month)) { return false; }
            if (!int.TryParse(Datos[2], out Day)) { return false; }

            if (Year < (DateTime.Today.Year - 100) || Year > DateTime.Today.Year) { return false; }
            if (Month < 1 || Month > 12) { return false; }
            if (Day < 1 || Day > DateTime.DaysInMonth(Year, Month)) { return false; }

            return true;
        }

        public static bool IsDateValid(string StrDate, ref DateTime dDate, bool BirthDate = true)
        {
            int Year, Month, Day = 0;

            string[] Datos = StrDate.Split('-');

            if (!Datos.Length.Equals(3)) { return false; }

            if (!int.TryParse(Datos[0], out Year)) { return false; }
            if (!int.TryParse(Datos[1], out Month)) { return false; }
            if (!int.TryParse(Datos[2], out Day)) { return false; }

            if (Year < (DateTime.Today.Year - 100) || (BirthDate && Year > DateTime.Today.Year)) { return false; }
            //if (Year < (DateTime.Today.Year - 100)) { return false; }
            if (Month < 1 || Month > 12) { return false; }
            if (Day < 1 || Day > DateTime.DaysInMonth(Year, Month)) { return false; }

            dDate = new DateTime(Year, Month, Day);

            return true;
        }




        public static string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }

        public static string getJsonString(Object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        public static string checkDir(string Drive, string Path)
        {
            bool DIRECTORY = false;
            string tmpPath = Drive + ":\\" + Path + "\\";

            DirectoryInfo directoryInfo = new DirectoryInfo(tmpPath);

            try
            {
                if (!directoryInfo.Exists)
                {
                    directoryInfo = Directory.CreateDirectory(directoryInfo.FullName);
                }

                DIRECTORY = true;
            }
            catch (Exception ex)
            {
                DIRECTORY = false;
            }

            return (DIRECTORY) ? (Path) : ("");
        }
        public static string checkDir(string Path)
        {
            bool DIRECTORY = false;
            //string tmpPath = Drive + ":\\" + Path + "\\";

            DirectoryInfo directoryInfo = new DirectoryInfo(Path);

            try
            {
                if (!directoryInfo.Exists)
                {
                    directoryInfo = Directory.CreateDirectory(directoryInfo.FullName);
                }

                DIRECTORY = true;
            }
            catch (Exception ex)
            {
                DIRECTORY = false;
            }

            return (DIRECTORY) ? (Path) : ("");
        }



    }
}
