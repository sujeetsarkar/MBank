using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DALayer;

namespace BusinessLayer
{
    public class BEmployeeLoanLinker
    {
        public IEmployeeLoan GetCustomerService()
        {
            EmployeeLoanLinker m = new EmployeeLoanLinker();
            return new EmployeeLoanBC(m.GetEmployeeLoanDALInstance());
        }
        public ICustomer GetGeneralService()
        {
            GeneralLinker m = new GeneralLinker();
            return new CustomerBC(m.GetGeneralDALInstance());
        }
    }
    

    class EmployeeLoanBC:IEmployeeLoan
    {
        IEmployeeLoanRepository IEmpLoanRep;
        IGeneralRepository IGenRep;
        public EmployeeLoanBC(IEmployeeLoanRepository pObj)
        {
            IEmpLoanRep = pObj;
        }

        public EmployeeLoanBC(IGeneralRepository pObj)
        {
            IGenRep = pObj;
        }

        public IEnumerable<LoanTransactionsEntity> GetLoanTransaction(string pLoanId, DateTime pFromDate, DateTime pToDate)
        {
            try
            {
                return IGenRep.GetLoanTransaction(pLoanId, pFromDate, pToDate);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
    }
}
