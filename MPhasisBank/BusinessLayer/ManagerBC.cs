using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DALayer;

namespace BusinessLayer
{
    public class BManagerLinker
    {
        public IManager GetManagerService()
        {
            ManagerLinker m = new ManagerLinker();
            return new ManagerBC(m.GetManagerDALInstance());
        }
        public IManager GetGeneralService()
        {
            GeneralLinker m = new GeneralLinker();
            return new ManagerBC(m.GetGeneralDALInstance());
        }
    }
    public class ManagerBC:IManager
    {
        IManagerRepository IManagerRep;
        IGeneralRepository IGenRep;
        public ManagerBC(IManagerRepository pObj)
        {
            IManagerRep = pObj;
        }

        public ManagerBC(IGeneralRepository pObj)
        {
            IGenRep = pObj;
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

        public bool CheckExistingCustomer(string pPAN)
        {
            try
            {
                return IManagerRep.CheckExistingCustomer(pPAN);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public bool AddDepartment(DepartmentEntity pDept)
        {
            try
            {
                if (pDept.Department_ID == "DEPT01" || pDept.Department_ID == "DEPT02" || pDept.Department_ID == "")
                   return IManagerRep.AddDepartment(pDept);
                else
                    throw new Exception("!!Invalid Department id--should be one of(DEPT01,DEPT02)");
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public bool RemoveDepartment(string pDeptID)
        {
            try
            {
                return IManagerRep.RemoveDepartment(pDeptID);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public EmployeeEntity AddEmployee(EmployeeEntity pEmployee)
        {
            pEmployee.Employee_ID = BankProperties.GenerateID();
            pEmployee.EPassword = BankProperties.CreateRandomPassword(8);
            try
            {
                return IManagerRep.AddEmployee(pEmployee);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public bool RemoveEmployee(string pEmpID)
        {
            try
            {
                return IManagerRep.RemoveEmployee(pEmpID);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public NewAccountDetails OpenSavingsAccount(CustomerEntity pCustomer, float pOpeningAmount)
        {
            try
            {
                pCustomer.Customer_ID = "MLA" + BankProperties.GenerateID();
                pCustomer.CPassword = BankProperties.CreateRandomPassword(8);
                return IManagerRep.OpenSavingsAccount(pCustomer, pOpeningAmount);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }

        public CustomerEntity GetCustomerDetail(string pId)
        {
            try
            {
                return IManagerRep.GetCustomerDetail(pId);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public bool CloseSavingsAccount(string pId)
        {
            try
            {
                return IManagerRep.CloseSavingsAccount(pId);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }

        public bool ForeClose(string pId, float pAmount)
        {
            try
            {
                return IManagerRep.ForeClose(pId, pAmount);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public float FetchForeClose(string pId)
        {
            try
            {
                return IManagerRep.FetchForeClose(pId);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public double CalculateEMI(float pAmount, int pTenure, CustomerEntity pCustomer)
        {
            try
            {
                return IManagerRep.CalculateEMI(pAmount, pTenure, pCustomer);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public LoanEntity GetLoanDetail(string pId)
        {
            try
            {
                return IManagerRep.GetLoanDetail(pId);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public float GetEMI(string pId)
        {
            try
            {
                return IManagerRep.GetEMI(pId);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public float PayEmi(String pId)
        {
            try
            {
                return IManagerRep.PayEmi(pId);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public float ROI(float pAmount, CustomerEntity pCustomer)
        {
            try
            {
                return IManagerRep.ROI(pAmount, pCustomer);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public SavingsEntity GetSavingsDetail(string pId)
        {
            try
            {
                return IManagerRep.GetSavingsDetail(pId);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public IEnumerable<LoanTransactionsEntity> LoanMiniStatement(string pId)
        {
            try
            {
                return IManagerRep.LoanMiniStatement(pId);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public IEnumerable<AccountsEntity> GetAccounts(string pId)
        {
            try
            {
                return IManagerRep.GetAccounts(pId);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public IEnumerable<SavingTransactionsEntity> SavingsMiniStatement(string pId)
        {
            try
            {
                return IManagerRep.SavingsMiniStatement(pId);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public LoanEntity PartPayment(string pId, int pTenure, float pAmount)
        {
            try
            {
                return IManagerRep.PartPayment(pId, pTenure, pAmount);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        public bool Deposit(String pId, float pAmount)
        {
            if (!(pAmount.IsValidTransaction()))
                throw new Exception("Invalid Amount");
            try
            {
                return IManagerRep.Deposit(pId, pAmount);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public bool Withdraw(string pId, float pAmount)
        {
            if (!(pAmount.IsValidTransaction()))
                throw new Exception("Invalid Amount");
            try
            {
                return IManagerRep.Withdraw(pId, pAmount);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public NewAccountDetails OpenLoanAccount(string pId, float pMonthly, float pReqsLoanAmount, int pTenure)
        {
            try
            {
                return IManagerRep.OpenLoanAccount(pId, pMonthly, pReqsLoanAmount, pTenure);
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

        public StaffHome EmpHomeDetail(string pEmpID)
        {
            try
            {
                return IManagerRep.EmpHomeDetail(pEmpID);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
    }
}
