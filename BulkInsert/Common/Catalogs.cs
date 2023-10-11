using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading;
using Common.Models;
using Common.Class;

namespace Common
{
    public static class Catalogo
    {
        public static int ConsultaPais(MySqlDatabase Database, int IdPais, ref PaisInfo Info, ref string MsgRespuesta)
        {
            int CodigoRespuesta = 99;

            Info = new PaisInfo();

            string sqlQuery = "CALL `td_administracion`.`sp_query_PaisById`(" + IdPais + ")";

            MySqlConnection conn = Database.GetConnection();
            MySqlCommand dbCmd = null;
            MySqlDataReader reader;
            
            using (conn)
            {
                try
                {
                    dbCmd = new MySqlCommand(sqlQuery, conn);
                    conn.Open();

                    IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                    WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                    WaitHandle.WaitAny(HandleResult);
                    reader = dbCmd.EndExecuteReader(DBResult);
                    reader.Read();

                    if (reader.HasRows)
                    {
                        CodigoRespuesta = 0;
                        MsgRespuesta = "Consulta Exitosa";

                        Info.Id = IdPais;
                        Info.Nombre = reader.GetString("sNombre");
                        Info.Nacionalidad = reader.GetString("sNacionalidad");
                        Info.Name = reader.GetString("sName");
                        Info.Code = reader.GetString("sClave");
                    }
                    else
                    {
                        LogClass.LogError("Catalogs", "ConsultaPais", "Consulta sin resultado: "+ IdPais);

                        MsgRespuesta = "El codigo de pais no existe.";
                        CodigoRespuesta = 902;
                    }

                    reader.Close();
                }
                catch (Exception e)
                {
                    LogClass.LogError("Catalogs", "ConsultaPais", "Exception: " + e.Message + "\n\nSql Query: " + dbCmd.CommandText);

                    MsgRespuesta = e.Message;
                    CodigoRespuesta = 901;
                }
                finally
                {
                    conn.Close();
                    LogClass.LogInfo("Catalogs", "ConsultaPais", "Conn Closed", 5);
                    conn.Dispose();
                    LogClass.LogInfo("Catalogs", "ConsultaPais", "Conn Disposed", 5);
                }
            }
            return CodigoRespuesta;
        }
        public static int ConsultaEstado(MySqlDatabase Database, int IdEstado, ref EstadoInfo Info, ref string MsgRespuesta)
        {
            int CodigoRespuesta = 99;

            Info = new EstadoInfo();

            string sqlQuery = "CALL `td_administracion`.`sp_query_EstadoById`(" + IdEstado + ")";

            MySqlConnection conn = Database.GetConnection();
            MySqlCommand dbCmd = null;
            MySqlDataReader reader;

            using (conn)
            {
                try
                {
                    dbCmd = new MySqlCommand(sqlQuery, conn);
                    conn.Open();

                    IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                    WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                    WaitHandle.WaitAny(HandleResult);
                    reader = dbCmd.EndExecuteReader(DBResult);
                    reader.Read();

                    if (reader.HasRows)
                    {
                        CodigoRespuesta = 0;
                        MsgRespuesta = "Consulta Exitosa";

                        Info.Nombre = reader.GetString("sNombre");
                        Info.Codigo = reader.GetString("sClave");
                        Info.Abreviacion = reader.GetString("sAbreviacion");
                    }
                    else
                    {
                        LogClass.LogError("Catalogo", "ConsultaEstado", "Consulta sin resultado: "+ IdEstado);

                        MsgRespuesta = "El codigo de estado no existe.";
                        CodigoRespuesta = 902;
                    }

                    reader.Close();
                }
                catch (Exception e)
                {
                    LogClass.LogError("Catalogo", "ConsultaEstado", "Exception: " + e.Message + "\n\nSql Query: " + dbCmd.CommandText);

                    MsgRespuesta = e.Message;
                    CodigoRespuesta = 901;
                }
                finally
                {
                    conn.Close();
                    LogClass.LogInfo("Catalogo", "ConsultaEstado", "Conn Closed", 5);
                    conn.Dispose();
                    LogClass.LogInfo("Catalogo", "ConsultaEstado", "Conn Disposed", 5);
                }
            }
            return CodigoRespuesta;
        }
        public static int ConsultaCiudad(MySqlDatabase Database, int CodigoPostal, ref ColoniaInfo Info, ref string MsgRespuesta)
        {
            int CodigoRespuesta = 99;

            Info = new ColoniaInfo();

            string sqlQuery = "CALL `td_administracion`.`sp_query_CiudadSeleccionada`(" + CodigoPostal + ")";

            MySqlConnection conn = Database.GetConnection();
            MySqlCommand dbCmd = null;
            MySqlDataReader reader;

            using (conn)
            {
                try
                {
                    dbCmd = new MySqlCommand(sqlQuery, conn);
                    conn.Open();

                    IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                    WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                    WaitHandle.WaitAny(HandleResult);
                    reader = dbCmd.EndExecuteReader(DBResult);
                    reader.Read();

                    if (reader.HasRows)
                    {
                        CodigoRespuesta = 0;
                        MsgRespuesta = "Consulta Exitosa";

                        Info.NombreMunicipio = reader.GetString("sNombreMunicipio");
                        Info.IdMunicipio = Convert.ToInt32(reader.GetString("nNumMunicipio"));
                        
                    }
                    else
                    {
                        LogClass.LogError("Catalogo", "ConsultaCiudad", "Consulta sin resultado: " + CodigoPostal);

                        MsgRespuesta = "El codigo de estado no existe.";
                        CodigoRespuesta = 902;
                    }

                    reader.Close();
                }
                catch (Exception e)
                {
                    LogClass.LogError("Catalogo", "ConsultaCiudad", "Exception: " + e.Message + "\n\nSql Query: " + dbCmd.CommandText);

                    MsgRespuesta = e.Message;
                    CodigoRespuesta = 901;
                }
                finally
                {
                    conn.Close();
                    LogClass.LogInfo("Catalogo", "ConsultaCiudad", "Conn Closed", 5);
                    conn.Dispose();
                    LogClass.LogInfo("Catalogo", "ConsultaCiudad", "Conn Disposed", 5);
                }
            }
            return CodigoRespuesta;
        }
        public static int ConsultaColonia(MySqlDatabase Database, int CodigoPostal, int Estado, int Municipio, int Colonia, ref ColoniaInfo Info, ref string MsgRespuesta)
        {
            int CodigoRespuesta = 99;

            Info = new ColoniaInfo();

            string sqlQuery = $"CALL `td_administracion`.`sp_query_coloniaById`({CodigoPostal}, {Estado}, {Municipio}, {Colonia})";

            MySqlConnection conn = Database.GetConnection();
            MySqlCommand dbCmd = null;
            MySqlDataReader reader;

            using (conn)
            {
                try
                {
                    dbCmd = new MySqlCommand(sqlQuery, conn);
                    conn.Open();

                    IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                    WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                    WaitHandle.WaitAny(HandleResult);
                    reader = dbCmd.EndExecuteReader(DBResult);
                    reader.Read();

                    if (reader.HasRows)
                    {
                        CodigoRespuesta = 0;
                        MsgRespuesta = "Consulta Exitosa";

                        Info.CodigoPostal = CodigoPostal;

                        Info.IdEstado = reader.GetInt32("nIdEstado");
                        Info.NombreEstado = reader.GetString("sNombreEstado");

                        Info.IdMunicipio = reader.GetInt32("nNumMunicipio");
                        Info.NombreMunicipio = reader.GetString("sNombreMunicipio");

                        Info.IdColonia = reader.GetInt32("nNumColonia");
                        Info.NombreColonia = reader.GetString("sNombreColonia");

                    }
                    else
                    {
                        LogClass.LogError("Catalogo", "ConsultaCodigoPostal", "Consulta sin resultado: " + CodigoPostal);

                        MsgRespuesta = "El codigo postal no existe.";
                        CodigoRespuesta = 902;
                    }

                    reader.Close();
                }
                catch (Exception e)
                {
                    LogClass.LogError("Catalogo", "ConsultaCodigoPostal", "Exception: " + e.Message + "\n\nSql Query: " + dbCmd.CommandText);

                    MsgRespuesta = e.Message;
                    CodigoRespuesta = 901;
                }
                finally
                {
                    conn.Close();
                    LogClass.LogInfo("Catalogo", "ConsultaCodigoPostal", "Conn Closed", 5);
                    conn.Dispose();
                    LogClass.LogInfo("Catalogo", "ConsultaCodigoPostal", "Conn Disposed", 5);
                }
            }
            return CodigoRespuesta;
        }
        public static int ConsultaGenero(MySqlDatabase Database, int IdGenero, ref string Descripcion, ref string MsgRespuesta)
        {
            int CodigoRespuesta = 99;
            Descripcion = string.Empty;

            string sqlQuery = "CALL `td_administracion`.`sp_query_GeneroById`(" + IdGenero + ")";

            MySqlConnection conn = Database.GetConnection();
            MySqlCommand dbCmd = null;
            MySqlDataReader reader;

            using (conn)
            {
                try
                {
                    dbCmd = new MySqlCommand(sqlQuery, conn);
                    conn.Open();

                    IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                    WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                    WaitHandle.WaitAny(HandleResult);
                    reader = dbCmd.EndExecuteReader(DBResult);
                    reader.Read();

                    if (reader.HasRows)
                    {
                        CodigoRespuesta = 0;
                        MsgRespuesta = "Consulta Exitosa";

                        Descripcion = reader.GetString("sDescripcion");
                    }
                    else
                    {
                        LogClass.LogError("Catalogo", "ConsultaGenero", "Consulta sin resultado: " + IdGenero);

                        MsgRespuesta = "El codigo de genero no existe.";
                        CodigoRespuesta = 902;
                    }

                    reader.Close();
                }
                catch (Exception e)
                {
                    LogClass.LogError("Catalogo", "ConsultaGenero", "Exception: " + e.Message + "\n\nSql Query: " + dbCmd.CommandText);

                    MsgRespuesta = e.Message;
                    CodigoRespuesta = 901;
                }
                finally
                {
                    conn.Close();
                    LogClass.LogInfo("Catalogo", "ConsultaGenero", "Conn Closed", 5);
                    conn.Dispose();
                    LogClass.LogInfo("Catalogo", "ConsultaGenero", "Conn Disposed", 5);
                }
            }
            return CodigoRespuesta;
        }
        public static int ConsultaOcupacion(MySqlDatabase Database, int IdOcupacion, ref string Descripcion, ref string MsgRespuesta)
        {
            int CodigoRespuesta = 99;
            Descripcion = string.Empty;

            string sqlQuery = "CALL `td_administracion`.`sp_query_OcupacionById`(" + IdOcupacion + ")";

            MySqlConnection conn = Database.GetConnection();
            MySqlCommand dbCmd = null;
            MySqlDataReader reader;

            using (conn)
            {
                try
                {
                    dbCmd = new MySqlCommand(sqlQuery, conn);
                    conn.Open();

                    IAsyncResult DBResult = dbCmd.BeginExecuteReader();
                    WaitHandle[] HandleResult = { DBResult.AsyncWaitHandle };
                    WaitHandle.WaitAny(HandleResult);
                    reader = dbCmd.EndExecuteReader(DBResult);
                    reader.Read();

                    if (reader.HasRows)
                    {
                        CodigoRespuesta = 0;
                        MsgRespuesta = "Consulta Exitosa";

                        Descripcion = reader.GetString("sDescripcion");
                    }
                    else
                    {
                        LogClass.LogError("Catalogo", "ConsultaOcupacion", "Sin resultado: "+ IdOcupacion);

                        MsgRespuesta = "El codigo de ocupacion no existe.";
                        CodigoRespuesta = 902;
                    }

                    reader.Close();
                }
                catch (Exception e)
                {
                    LogClass.LogError("Catalogo", "ConsultaOcupacion", "Exception: " + e.Message + "\n\nSql Query: " + dbCmd.CommandText);

                    MsgRespuesta = e.Message;
                    CodigoRespuesta = 901;
                }
                finally
                {
                    conn.Close();
                    LogClass.LogInfo("Catalogo", "ConsultaOcupacion", "Conn Closed", 5);
                    conn.Dispose();
                    LogClass.LogInfo("Catalogo", "ConsultaOcupacion", "Conn Disposed", 5);
                }
            }
            return CodigoRespuesta;
        }        
    }
}
