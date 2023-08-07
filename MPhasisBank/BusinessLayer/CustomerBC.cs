using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DALayer;

namespace BusinessLayer
{
    public class BCustomerLinker
    {
        public ICustomer GetCustomerService()
        {
            CustomerLinker m = new CustomerLinker();
            return new CustomerBC(m.GetCustomerDALInstance());
        }
        public ICustomer GetGeneralService()
        {
            GeneralLinker m = new GeneralLinker();
            return new CustomerBC(m.GetGeneralDALInstance());
        }
    }
    public class CustomerBC : ICustomer
    {
        ICustomerRepository ICustRep;
        IGeneralRepository IGenRep;
        public CustomerBC(ICustomerRepository pObj)
        {
            ICustRep = pObj;
        }
        
        public CustomerBC(IGeneralRepository pObj)
        {
            IGenRep = pObj;
        }
        public CustomerBC()
        {

        }
        public int Login(string pUserId, string pPassword)
        {
            try
            {
                return IGenRep.Login(pUserId, pPassword, 0);//0-customer 1-staff
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public int PartPayment(float pAmount, LoanEntity pLoanCustomer)
        {
            try
            {
                if (pAmount.IsValidPartpaymentAmount(pLoanCustomer))
                    return IGenRep.PartPayment(pAmount, pLoanCustomer, 0);//0-customer 1-staff
            }
            catch(Exception ex)
            {
                throw ex;
            }
            throw new Exception("Invalid Amount");
        }
        
        public int ForeClose(string pLoanId)
        {
            return ICustRep.ForeClose(pLoanId);
        }

        public CustomerHome CustomerHomeDetail(string pCustID)
        {
            try
            {
                return ICustRep.CustomerHomeDetail(pCustID);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }

        public IEnumerable<SavingTransactionsEntity> GetSavingsTransaction(string pId, DateTime pFromDate, DateTime pToDate)
        {
            try
            {
                return IGenRep.GetSavingsTransaction(pId, pFromDate, pToDate);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public IEnumerable<LoanTransactionsEntity> GetLoanTransaction(string pId, DateTime pFromDate, DateTime pToDate)
        {
            try
            {
                return IGenRep.GetLoanTransaction(pId, pFromDate, pToDate);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public bool CheckAccountBalance(string pAID, out float pBal, out float? pOutstanding)
        {
            try
            {
                return ICustRep.CheckAccountBalance(pAID, out pBal, out pOutstanding);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
    }
}
