using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class ReportData
    {
        public string Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public string PAN { get; set; }
        public string Phone { get; set; }
        public string Account_ID { get; set; }
        public string Account_Type { get; set; }
        public Nullable<int> Account_Status { get; set; }
        public float Balance { get; set; }
        public string Savings_Trans_ID { get; set; }
        public System.DateTime Transaction_Date { get; set; }
        public string Transaction_Type { get; set; }
        public float Amount { get; set; }
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
