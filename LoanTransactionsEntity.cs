//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DALayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class LoanTransactionsEntity
    {
        public string Loan_Trans_ID { get; set; }
        public string Loan_Account_ID { get; set; }
        public System.DateTime EMI_Payment_Date { get; set; }
        public float Amount { get; set; }
    
        public virtual AccountsEntity AccountsEntity { get; set; }
    }
}
