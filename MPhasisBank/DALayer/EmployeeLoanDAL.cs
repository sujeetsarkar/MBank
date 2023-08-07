using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace DALayer
{
    public class EmployeeLoanLinker
    {
        public IEmployeeLoanRepository GetEmployeeLoanDALInstance()
        {
            return new EmployeeLoanDAL();
        }
    }
    public class EmployeeLoanDAL:IEmployeeLoanRepository
    {
        IEmployeeLoanRepository _empLoanRep = new ManagerDAL();
        //Get Loan transaction in GeneralDAL

        public NewAccountDetails OpenLoanAccount(string pCustID, float pMonthly, float pReqsLoanAmount, int pTenure)
        {
            try
            {
                return _empLoanRep.OpenLoanAccount(pCustID, pMonthly, pReqsLoanAmount, pTenure);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
    }
}
