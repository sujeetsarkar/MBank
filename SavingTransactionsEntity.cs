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
    
    public partial class SavingTransactionsEntity
    {
        public string Savings_Trans_ID { get; set; }
        public string Account_ID { get; set; }
        public System.DateTime Transaction_Date { get; set; }
        public string Transaction_Type { get; set; }
        public float Amount { get; set; }
    
        public virtual AccountsEntity AccountsEntity { get; set; }
    }
}
