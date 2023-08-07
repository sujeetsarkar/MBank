using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace DALayer
{
    public interface IEmployeeLoanRepository
    {
        //Get Loan transaction in GeneralDAL
         NewAccountDetails OpenLoanAccount(string pCustID, float pMonthly, float pReqsLoanAmount, int pTenure);
    }
}
