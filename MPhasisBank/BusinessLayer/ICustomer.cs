using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DALayer;

namespace BusinessLayer
{
    public interface ICustomer
    {
        int Login(string pUserId, string pPassword);
        int PartPayment(float pAmount, LoanEntity pLoanCustomer);

        int ForeClose(string pLoanId);
        CustomerHome CustomerHomeDetail(string pCustID);

        IEnumerable<SavingTransactionsEntity> GetSavingsTransaction(string pId, DateTime pFromDate, DateTime pToDate);
        IEnumerable<LoanTransactionsEntity> GetLoanTransaction(string pId, DateTime pFromDate, DateTime pToDate);

        bool CheckAccountBalance(string pAID, out float pBal, out float? pOutstanding);
    }
}
