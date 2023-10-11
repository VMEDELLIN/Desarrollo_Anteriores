using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkInsert
{
    public class SaveBulkTransactionAction
    {
        private readonly ConfigDB _configDB;
        public SaveBulkTransactionAction(ConfigDB configDB)
        {
            _configDB = configDB;
        }
        public DataTable GetTable()
        {

            DataTable dataTableTransactionAction = new DataTable();
            dataTableTransactionAction.Columns.Add("nIdReference", typeof(System.Int64));
            dataTableTransactionAction.Columns.Add("nIdActionCode", typeof(System.Int32));
            dataTableTransactionAction.Columns.Add("dFecDateOfPayment", typeof(System.DateTime));
            dataTableTransactionAction.Columns.Add("nBranchCode", typeof(System.String));
            dataTableTransactionAction.Columns.Add("sBeneficiaryId", typeof(System.String));
            dataTableTransactionAction.Columns.Add("nBeneficiaryIdType", typeof(System.Int32));
            dataTableTransactionAction.Columns.Add("sNotes", typeof(System.String));
            dataTableTransactionAction.Columns.Add("bNotifiedToMaxi", typeof(System.Boolean));
            dataTableTransactionAction.Columns.Add("nIdUsuarioAlta", typeof(System.Int32));
            dataTableTransactionAction.Columns.Add("dFecAlta", typeof(System.DateTime));
            dataTableTransactionAction.Columns.Add("nIdUsuarioNotified", typeof(System.Int32));
            dataTableTransactionAction.Columns.Add("dFecNotified", typeof(System.DateTime));
            return dataTableTransactionAction;
        }
        
    }
}
