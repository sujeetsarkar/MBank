using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace DALayer
{
    public interface IEmployeeSavingsRepository
    {
        StaffHome EmpHomeDetail(string pEmpID);
        NewAccountDetails OpenSavingsAccount(CustomerEntity pCustomer, float pAmount);
        bool Deposit(String pAccountID, float pAmount);
        bool Withdraw(string pAccountId, float pAmount);

        ////Get Savings transaction in GeneralDAL
    }
}
