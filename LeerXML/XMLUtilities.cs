using Microsoft.XmlDiffPatch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LeerXML
{
    public class XMLUtilities
    {
        public static bool Compare(string XmlA, string XmlB)
        {
            XmlDiff Diff = new XmlDiff();

            Diff.IgnoreChildOrder = true;
            Diff.IgnoreComments = true;
            Diff.IgnoreDtd = true;
            Diff.IgnoreNamespaces = true;
            Diff.IgnorePI = true;
            Diff.IgnorePrefixes = true;
            Diff.IgnoreWhitespace = true;
            Diff.IgnoreXmlDecl = true;

            XmlDocument DocA = new XmlDocument();
            DocA.LoadXml(XmlA);

            XmlDocument DocB = new XmlDocument();
            DocB.LoadXml(XmlB);

            XmlNode NodeA = DocA.FirstChild;
            XmlNode NodeB = DocB.FirstChild;

            return Diff.Compare(NodeA, NodeB);
        }

        public static bool isValidXml(string StrXml)
        {
            XmlDocument XmlDoc = new XmlDocument();

            try
            {
                XmlDoc.LoadXml(StrXml);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static string FormatedXmlString(string StrXml)
        {
            return XElement.Parse(StrXml).ToString();
        }

        public static string GetXMLFromObject(object o)
        {
            bool IsException = false;
            Exception ErrorException = new Exception();

            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
                IsException = true;
                ErrorException = ex;
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }

            if (IsException)
            {
                throw ErrorException;
            }

            return sw.ToString();
        }

        public static string GetSOAPXMLFromObject(object o)
        {
            bool IsException = false;
            Exception ErrorException = new Exception();

            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;

            try
            {
                var soapserializer = new XmlSerializer(o.GetType());

                //XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                soapserializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
                IsException = true;
                ErrorException = ex;
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }

            if (IsException)
            {
                throw ErrorException;
            }

            return sw.ToString();
        }

        public static Object ObjectToXML(string xml, Type objectType)
        {
            bool IsException = false;
            Exception ErrorException = new Exception();

            System.IO.StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception exp)
            {
                //Handle Exception Code
                IsException = true;
                ErrorException = exp;
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }

            if (IsException)
            {
                throw ErrorException;
            }

            return obj;
        }

        public static T GetObjectFromSOAPXML<T>(string SoapXml)
        {
            var rawXML = XDocument.Parse(SoapXml);

            T deserializedObject;

            using (var reader = rawXML.CreateReader(System.Xml.Linq.ReaderOptions.None))
            {
                var ser = new XmlSerializer(typeof(T));
                deserializedObject = (T)ser.Deserialize(reader);
            }

            return deserializedObject;
        }
    }
}
