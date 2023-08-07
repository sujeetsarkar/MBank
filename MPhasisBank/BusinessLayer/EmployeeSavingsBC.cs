using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALayer;

namespace BusinessLayer
{
    public class BEmployeeSavingsLinker
    {
        public IEmployeeSavings GetEmployeeSavingsService()
        {
            EmployeeSavingsLinker m = new EmployeeSavingsLinker();
            return new EmployeeSavingsBC(m.GetEmployeeSavingsDALInstance());
        }
        public IEmployeeSavings GetGeneralService()
        {
            GeneralLinker m = new GeneralLinker();
            return new EmployeeSavingsBC(m.GetGeneralDALInstance());
        }
    }
    public class EmployeeSavingsBC:IEmployeeSavings
    {
        IEmployeeSavingsRepository IEmpSavingsRep;
        IGeneralRepository IGenRep;
        public EmployeeSavingsBC(IEmployeeSavingsRepository pObj)
        {
            IEmpSavingsRep = pObj;
        }

        public EmployeeSavingsBC(IGeneralRepository pObj)
        {
            IGenRep = pObj;
        }

        public EmployeeSavingsBC()
        {

        }

        public bool Deposit(String pAccountID,float pAmount)
        {
            if(pAmount>100)
            {
                try
                {
                    return IEmpSavingsRep.Deposit(pAccountID, pAmount);                      
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            throw new Exception("Opps!!!! Invalid Amount--Amount can not be less than 100");
        }

        public NewAccountDetails OpenSavingsAccount(CustomerEntity pCustomer, float pOpeningAmount)
        {
            if(pOpeningAmount<1000)
            {
                throw new Exception("Invalid Amount!!!! Minimum Opening Account Balance is 1000");
            }
            pCustomer.Customer_ID = "MLA" + BankProperties.GenerateID();
            pCustomer.CPassword = BankProperties.CreateRandomPassword(8);
            try
            {
                pCustomer.PAN.IsExistingCustomer();
                return IEmpSavingsRep.OpenSavingsAccount(pCustomer, pOpeningAmount);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }

        public int Login(string pUserId, string pPassword)
        {
            try
            {
                return IGenRep.Login(pUserId, pPassword, 1);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }

        public bool Withdraw(string pAccountId, float pAmount)
        {
            if (!(pAmount.IsValidTransaction()))
                throw new Exception("Invalid Amount");
            try
            {
                return IEmpSavingsRep.Withdraw(pAccountId, pAmount);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }

        public IEnumerable<SavingTransactionsEntity> GetSavingsTransaction(string pAccountId, DateTime pFromDate, DateTime pToDate)
        {
            try
            {
                return IGenRep.GetSavingsTransaction(pAccountId, pFromDate, pToDate);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }

        public StaffHome SaveEmpHomeDetail(string pEmpID)
        {
            try
            {
                return IEmpSavingsRep.EmpHomeDetail(pEmpID);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
    }
}
