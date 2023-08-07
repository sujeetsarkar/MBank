using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BusinessEntities
{
    [DataContract]
    public partial class SavingTransactionsEntity
    {
        [DataMember]
        public string Savings_Trans_ID { get; set; }
        [DataMember]
        public string Account_ID { get; set; }
        [DataMember]
        public System.DateTime Transaction_Date { get; set; }
        [DataMember]
        public string Transaction_Type { get; set; }
        [DataMember]
        public float Amount { get; set; }

        public virtual AccountsEntity AccountsEntity { get; set; }
    }
}
