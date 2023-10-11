using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
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
}
