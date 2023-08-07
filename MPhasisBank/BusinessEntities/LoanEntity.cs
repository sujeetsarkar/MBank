using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public partial class LoanEntity
    {
        public string Loan_Account_ID { get; set; }
        public string Customer_ID { get; set; }
        public float Loan_Amount { get; set; }
        public System.DateTime LStart_Date { get; set; }
        public int Tenure { get; set; }
        public float Loan_ROI { get; set; }
        public float EMI { get; set; }
        public float Outstanding { get; set; }

        public virtual AccountsEntity AccountsEntity { get; set; }
        public virtual CustomerEntity CustomerEntity { get; set; }
    }
}
