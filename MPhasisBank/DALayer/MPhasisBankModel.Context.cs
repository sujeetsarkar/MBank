﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using BusinessEntities;
    
    public partial class MPhasisBankEntities : DbContext
    {
        public MPhasisBankEntities()
            : base("name=MPhasisBankEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccountsEntity> AccountsEntities { get; set; }
        public virtual DbSet<CustomerEntity> CustomerEntities { get; set; }
        public virtual DbSet<DepartmentEntity> DepartmentEntities { get; set; }
        public virtual DbSet<EmployeeEntity> EmployeeEntities { get; set; }
        public virtual DbSet<LoanEntity> LoanEntities { get; set; }
        public virtual DbSet<LoanTransactionsEntity> LoanTransactionsEntities { get; set; }
        public virtual DbSet<SavingsEntity> SavingsEntities { get; set; }
        public virtual DbSet<SavingTransactionsEntity> SavingTransactionsEntities { get; set; }
    }
}
