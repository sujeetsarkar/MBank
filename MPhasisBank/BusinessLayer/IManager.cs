using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessLayer
{
    public interface IManager
    {
        int Login(string pUserId, string pPassword);
        StaffHome EmpHomeDetail(string pEmpID);
        bool CheckExistingCustomer(string pPAN);
        bool AddDepartment(DepartmentEntity pDept);
        bool RemoveDepartment(string pDeptID);
        EmployeeEntity AddEmployee(EmployeeEntity pEmployee);
        bool RemoveEmployee(string pEmpID);
        bool Deposit(String pId, float pAmount);
        NewAccountDetails OpenSavingsAccount(CustomerEntity pCustomer, float pOpeningAmount);
        CustomerEntity GetCustomerDetail(string pId);
        NewAccountDetails OpenLoanAccount(string pId, float pMonthly, float pReqsLoanAmount, int pTenure);
        float FetchForeClose(string pId);
        bool ForeClose(string pId, float pAmount);
        LoanEntity PartPayment(string pId, int pTenure, float pAmount);
        double CalculateEMI(float pAmount, int pTenure, CustomerEntity pCustomer);
        LoanEntity GetLoanDetail(string pId);
        float GetEMI(string pId);
        float PayEmi(String pId);
        float ROI(float pAmount, CustomerEntity pCustomer);
        IEnumerable<LoanTransactionsEntity> LoanMiniStatement(string pId);
        SavingsEntity GetSavingsDetail(string pId);
        IEnumerable<SavingTransactionsEntity> SavingsMiniStatement(string pId);
        IEnumerable<AccountsEntity> GetAccounts(string pId);
        bool CloseSavingsAccount(string pId);
        bool Withdraw(string pId, float pAmount);
        IEnumerable<SavingTransactionsEntity> GetSavingsTransaction(string pAccountId, DateTime pFromDate, DateTime pToDate);
        IEnumerable<LoanTransactionsEntity> GetLoanTransaction(string pLoanId, DateTime pFromDate, DateTime pToDate);

    }
}
