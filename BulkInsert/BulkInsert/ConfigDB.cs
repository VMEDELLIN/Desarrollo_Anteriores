using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsert
{
    public class ConfigDB
    {
        public bool ConnectioCreate { get; set; }
        public MySqlDatabase Database
        {
            get { return m_database; }
            set { m_database = value; }
        }
        private MySqlDatabase m_database = new MySqlDatabase();

        public ConfigDB()
        {
            ConnectioCreate = false;
        }
    }
}
