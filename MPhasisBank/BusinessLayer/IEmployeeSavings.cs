using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessLayer;

namespace BusinessLayer
{
    public interface IEmployeeSavings
    {
        int Login(string pUserId, string pPassword);
        StaffHome SaveEmpHomeDetail(string pEmpID);
        bool Deposit(String pAccountID, float pAmount);


        NewAccountDetails OpenSavingsAccount(CustomerEntity pCustomer, float pOpeningAmount);

        

        bool Withdraw(string pAccountId, float pAmount);

        IEnumerable<SavingTransactionsEntity> GetSavingsTransaction(string pAccountId, DateTime pFromDate, DateTime pToDate);
    }
}
