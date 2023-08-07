using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class LoanReportData
    {
        public string Customer_ID { get; set; }
        public string CPassword { get; set; }
        public string Customer_Name { get; set; }
        public System.DateTime DOB { get; set; }
        public string PAN { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Account_ID { get; set; }
        public string Account_Type { get; set; }
        public Nullable<int> Account_Status { get; set; }
        public string Loan_Account_ID { get; set; }
        public float Loan_Amount { get; set; }
        public System.DateTime LStart_Date { get; set; }
        public int Tenure { get; set; }
        public float Loan_ROI { get; set; }
        public float EMI { get; set; }
        public string Loan_Trans_ID { get; set; }
        public System.DateTime EMI_Payment_Date { get; set; }
        public float LAmount { get; set; }
        public float Outstanding { get; set; }
    }
}
