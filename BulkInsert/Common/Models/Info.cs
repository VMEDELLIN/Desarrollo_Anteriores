using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;


namespace Common.Models
{
    public class GatewayPortInfo
    {
        private int _Port = 0;
        private int _Count = 0;

        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        public int Fetch
        {
            get
            {
                _Count++;
                return _Port;
            }
        }

    }

    public class EncryptionKeyInfo
    {
        private int m_idUsb;
        private string m_driveSerial;
        private string m_volumeSerial;
        private string m_volumeName;
        private string m_publicKey;
        private string m_publicKeyHash;

        public int Id
        {
            get { return m_idUsb; }
            set { m_idUsb = value; }
        }
        public string DriveSerial
        {
            get { return m_driveSerial; }
            set { m_driveSerial = value; }
        }
        public string VolumeSerial
        {
            get { return m_volumeSerial; }
            set { m_volumeSerial = value; }
        }
        public string VolumeName
        {
            get { return m_volumeName; }
            set { m_volumeName = value; }
        }
        public string PublicKey
        {
            get { return m_publicKey; }
            set { m_publicKey = value; }
        }
        public string PublicKeyHash
        {
            get { return m_publicKeyHash; }
            set { m_publicKeyHash = value; }
        }
    }

    public class TranTypeInfo
    {
        private int m_idTranType;
        private string m_nameTranType;

        private int m_tipoServicio;
        private int m_flujoTranType;
        private int m_checkFolio;
        private int m_checkTrace;
        private int m_memoria;

        public int Id
        {
            get { return m_idTranType; }
            set { m_idTranType = value; }
        }
        public string Name
        {
            get { return m_nameTranType; }
            set { m_nameTranType = value; }
        }
        public int Tipo
        {
            get { return m_tipoServicio; }
            set { m_tipoServicio = value; }
        }
        public int Flujo
        {
            get { return m_flujoTranType; }
            set { m_flujoTranType = value; }
        }
        public int CheckFolio
        {
            get { return m_checkFolio; }
            set { m_checkFolio = value; }
        }
        public int CheckTrace
        {
            get { return m_checkTrace; }
            set { m_checkTrace = value; }
        }
        public int Memoria
        {
            get { return m_memoria; }
            set { m_memoria = value; }
        }
    }

    public class TranTypeRouteInfo
    {
        private int _idRuta;
        private int _idInterface;
        private int _MessagType;
        private int _TranType;
        private bool _Active;
        private string _TranTypeDesc;

        private int _TipoServicio;
        private int _DestinationEntity;


        public int Id
        {
            get { return _idRuta; }
            set { _idRuta = value; }
        }
        public int Inteface
        {
            get { return _idInterface; }
            set { _idInterface = value; }
        }
        public int MessageType
        {
            get { return _MessagType; }
            set { _MessagType = value; }
        }
        public int TranType
        {
            get { return _TranType; }
            set { _TranType = value; }
        }
        public bool Enabled
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public string Name
        {
            get { return _TranTypeDesc; }
            set { _TranTypeDesc = value; }
        }
        public int Tipo
        {
            get { return _TipoServicio; }
            set { _TipoServicio = value; }
        }
        public int RemoteId
        {
            get { return _DestinationEntity; }
            set { _DestinationEntity = value; }
        }
    }

    public class EntityInfo
    {
        private int m_EntityId = 0;
        private int m_EntityStatus = 1;
        private int m_EntityType = 0;
        private string m_EntityServiceName = "";
        private string m_EntityName = "";
        private bool m_LoadBalancing = false;
        private bool m_RestrictLocation = false;
        private bool m_JumpRestriction = false;

        public int Id
        {
            get { return m_EntityId; }
            set { m_EntityId = value; }
        }
        public int Status
        {
            get { return m_EntityStatus; }
            set { m_EntityStatus = value; }
        }
        public int Type
        {
            get { return m_EntityType; }
            set { m_EntityType = value; }
        }
        public bool Enabled
        {
            get { return (m_EntityStatus.Equals(0)) ? (true) : (false); }
            set { m_EntityStatus = (value) ? (0) : (1); }
        }
        public string ServiceName
        {
            get { return m_EntityServiceName; }
            set { m_EntityServiceName = value; }
        }
        public string Name
        {
            get { return m_EntityName; }
            set { m_EntityName = value; }
        }
        public bool LoadBalancing
        {
            get { return m_LoadBalancing; }
            set { m_LoadBalancing = value; }
        }
        public bool RestrictLocation
        {
            get { return m_RestrictLocation; }
            set { m_RestrictLocation = value; }
        }
        public bool JumpRestriction
        {
            get { return m_JumpRestriction; }
            set { m_JumpRestriction = value; }
        }

    }

    public class ServerInfo
    {
        private int m_id = 0;
        private int m_idEntity = 0;
        private int m_idServerPriority = 0;
        private int m_idLocation = 0;
        private int m_idServerStatus = 0;
        private string m_serverServiceName = string.Empty;
        private string m_serverDescription = "";
        private string m_serverHost = "";
        private int m_serverPort = 0;
        private int m_serverTimeOut = 0;
        private DateTime m_LastUse = DateTime.Now;
        private DateTime m_LastCheck = DateTime.Now;
        private DateTime m_LastUpdate = DateTime.Now;
        private IPEndPoint m_RemoteEndPoint;

        public int Id
        {
            set { m_id = value; }
            get { return m_id; }
        }
        public int IdEntity
        {
            set { m_idEntity = value; }
            get { return m_idEntity; }
        }
        public int Priority
        {
            set { m_idServerPriority = value; }
            get { return m_idServerPriority; }
        }
        public int Location
        {
            set { m_idLocation = value; }
            get { return m_idLocation; }
        }
        public int Status
        {
            set { m_idServerStatus = value; }
            get { return m_idServerStatus; }
        }
        public string Name
        {
            set { m_serverDescription = value; }
            get { return m_serverDescription; }
        }
        public string Host
        {
            set { m_serverHost = value; }
            get { return m_serverHost; }
        }
        public string ServiceName
        {
            set { m_serverServiceName = value; }
            get { return m_serverServiceName; }
        }
        public int Port
        {
            set { m_serverPort = value; }
            get { return m_serverPort; }
        }
        public int TimeOut
        {
            set { m_serverTimeOut = value; }
            get { return m_serverTimeOut; }
        }
        public DateTime UseDate
        {
            set { m_LastUse = value; }
            get { return m_LastUse; }
        }
        public DateTime CheckDate
        {
            set { m_LastCheck = value; }
            get { return m_LastCheck; }
        }
        public DateTime UpdateDate
        {
            set { m_LastUpdate = value; }
            get { return m_LastUpdate; }
        }
        public IPEndPoint RemoteEndPoint
        {
            set { m_RemoteEndPoint = value; }
            get { return m_RemoteEndPoint; }
        }
    }

    public class DataInfo
    {
        private int m_Key = 0;
        private int m_DataType = 0;
        private string m_Desc = string.Empty;
        private string m_Value = string.Empty;

        public int Key
        {
            get { return m_Key; }
            set { m_Key = value; }
        }
        public int DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
        }
        public string Desc
        {
            get { return m_Desc; }
            set { m_Desc = value; }
        }
        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }
    }

    public class serverConfigurationInfo
    {
        private int m_ServerId = 0;
        private int m_EntityId = 0;
        private int m_ServerType = 0;
        private int m_ServerPriority = 0;
        private int m_LocationId = 0;
        private int m_ServerStatus = 1;
        private int m_ComputerId = 0;
        private string m_serverName = "";

        private int m_DebugLog = 0;
        private string m_LogFileName = "";
        private string m_LogFilePath = "";

        private string m_LocalHost = "";
        private int m_LocalPort = 0;
        //private int m_LocalTimeout = 0;
        private int m_BufferSize = 1024;
        private int m_SimulatenousConnections = 0;
        private int m_MaxConnections = 0;

        private int m_ConfigurationTime = 0;
        private int m_LogRefreshTime = 0;
        private int m_ServerListTime = 0;
        private int m_DatabaseTime = 0;
        private int m_DuplicateTime = 0;
        private int m_DisconnectTime = 0;
        private int m_SystemTime = 5;

        public int ServerId
        {
            get { return m_ServerId; }
            set { m_ServerId = value; }
        }
        public int EntityId
        {
            get { return m_EntityId; }
            set { m_EntityId = value; }
        }
        public int ServerType
        {
            get { return m_ServerType; }
            set { m_ServerType = value; }
        }
        public int ServerPriority
        {
            get { return m_ServerPriority; }
            set { m_ServerPriority = value; }
        }
        public int LocationId
        {
            get { return m_LocationId; }
            set { m_LocationId = value; }
        }
        public int Status
        {
            get { return m_ServerStatus; }
            set { m_ServerStatus = value; }
        }
        public int ComputerId
        {
            get { return m_ComputerId; }
            set { m_ComputerId = value; }
        }
        public string Name
        {
            get { return m_serverName; }
            set { m_serverName = value; }
        }

        public int DebugLevel
        {
            get { return m_DebugLog; }
            set { m_DebugLog = value; }
        }
        public string LogFileName
        {
            get { return m_LogFileName; }
            set { m_LogFileName = value; }
        }
        public string PathFileName
        {
            get { return m_LogFilePath; }
            set { m_LogFilePath = value; }
        }

        public string Host
        {
            get { return m_LocalHost; }
            set { m_LocalHost = value; }
        }
        public int Port
        {
            get { return m_LocalPort; }
            set { m_LocalPort = value; }
        }
        public int BufferSize
        {
            get { return m_BufferSize; }
            set { m_BufferSize = value; }
        }
        public int SimultaneousConn
        {
            get { return m_SimulatenousConnections; }
            set { m_SimulatenousConnections = value; }
        }
        public int MaximunConn
        {
            get { return m_MaxConnections; }
            set { m_MaxConnections = value; }
        }

        public int ConfigRefreshTime
        {
            get { return m_ConfigurationTime; }
            set { m_ConfigurationTime = value; }
        }
        public int LogRefreshTime
        {
            get { return m_LogRefreshTime; }
            set { m_LogRefreshTime = value; }
        }
        public int ServerRefreshTime
        {
            get { return m_ServerListTime; }
            set { m_ServerListTime = value; }
        }
        public int DBRefreshTime
        {
            get { return m_DatabaseTime; }
            set { m_DatabaseTime = value; }
        }
        public int DuplicateTime
        {
            get { return m_DuplicateTime; }
            set { m_DuplicateTime = value; }
        }
        public int DisconnectTime
        {
            get { return m_DisconnectTime; }
            set { m_DisconnectTime = value; }
        }
        public int SystemTime
        {
            get { return m_SystemTime; }
            set { m_SystemTime = value; }
        }
    }

    public class EquipoInfo
    {
        private int m_Equipod = 0;
        private int m_idEstatus = 99;
        private int m_EquipoType = 0;
        private double m_Version = 0;
        private double m_Revision = 0;
        private string m_FingerPrint = string.Empty;
        private string m_NombreEquipo = string.Empty;
        private string m_NombreTerm = string.Empty;
        private IPAddress m_IpAddress = IPAddress.Loopback;

        public int Id
        {
            get { return m_Equipod; }
            set { m_Equipod = value; }
        }
        public int idEstatus
        {
            get { return m_idEstatus; }
            set { m_idEstatus = value; }
        }
        public int Type
        {
            get { return m_EquipoType; }
            set { m_EquipoType = value; }
        }
        public double Ver
        {
            get { return m_Version; }
            set { m_Version = value; }
        }
        public double Rev
        {
            get { return m_Revision; }
            set { m_Revision = value; }
        }
        public string FingerPrint
        {
            get { return m_FingerPrint; }
            set { m_FingerPrint = value; }
        }
        public string Name
        {
            get { return m_NombreEquipo; }
            set { m_NombreEquipo = value; }
        }
        public string Terminal
        {
            get { return m_NombreTerm; }
            set { m_NombreTerm = value; }
        }
        public IPAddress IPHost
        {
            get { return m_IpAddress; }
            set { m_IpAddress = value; }
        }
    }

    
}

