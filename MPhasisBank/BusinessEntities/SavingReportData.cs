using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class SavingReportData
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
        public float Balance { get; set; }
        public string Savings_Trans_ID { get; set; }
        public System.DateTime Transaction_Date { get; set; }
        public string Transaction_Type { get; set; }
        public float Amount { get; set; }
    }
}