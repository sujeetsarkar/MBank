using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessLayer
{
    public interface IEmployeeLoan
    {
        IEnumerable<LoanTransactionsEntity> GetLoanTransaction(string pLoanId, DateTime pFromDate, DateTime pToDate);
    }
}
