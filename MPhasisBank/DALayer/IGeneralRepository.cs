using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace DALayer
{
    public interface IGeneralRepository
    {
        int Login(string pUserId, string pPassword, int pUsrType);
        int PartPayment(float pAmount, LoanEntity pLoanAccount, int pEntityType);

        IEnumerable<SavingTransactionsEntity> GetSavingsTransaction(string pId, DateTime pFromDate, DateTime pToDate);

        IEnumerable<LoanTransactionsEntity> GetLoanTransaction(string pLoanId, DateTime pFromDate, DateTime pToDate);


    }
}
