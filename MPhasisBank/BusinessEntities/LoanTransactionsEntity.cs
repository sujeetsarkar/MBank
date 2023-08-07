using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BusinessEntities
{
    [DataContract]
    public partial class LoanTransactionsEntity
    {
        [DataMember]
        public string Loan_Trans_ID { get; set; }
        [DataMember]
        public string Loan_Account_ID { get; set; }
        [DataMember]
        public System.DateTime EMI_Payment_Date { get; set; }
        [DataMember]
        public float Amount { get; set; }

        public virtual AccountsEntity AccountsEntity { get; set; }
    }
}
