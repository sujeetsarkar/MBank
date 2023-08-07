using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace DALayer
{
    public interface IManagerRepository
    {
        bool CheckExistingCustomer(string pPAN);
        CustomerEntity GetCustomerDetail(string pId);
        StaffHome EmpHomeDetail(string pEmpID);
        bool AddDepartment(DepartmentEntity pDept);
        bool RemoveDepartment(string pDeptID);
        EmployeeEntity AddEmployee(EmployeeEntity pEmployee);
        bool RemoveEmployee(string pEmpID);
        NewAccountDetails OpenSavingsAccount(CustomerEntity pCustomer, float pAmount);
        bool CloseSavingsAccount(string pId);

        bool Deposit(String pId, float pAmount);

        bool Withdraw(string pId, float pAmount);
        double CalculateEMI(float pAmount, int pTenure, CustomerEntity pCustomer);

        IEnumerable<ReportData> GetCustomerReport(string pCustID);

        IEnumerable<SavingReportData> GetSavingReport(string pCustID);

        IEnumerable<LoanReportData> GetLoanReport(string pCustID);

        //Get Loan transaction in GeneralDAL
        //Get Savings transaction in GeneralDAL
        NewAccountDetails OpenLoanAccount(string pId, float pMonthly, float pReqsLoanAmount, int pTenure);
        bool ForeClose(string pId, float pAmount);
        float FetchForeClose(string pId);

        LoanEntity PartPayment(string pId, int pTenure, float pAmount);
        LoanEntity GetLoanDetail(string pId);
        float GetEMI(string pId);
        float PayEmi(String pId);
        float ROI(float pAmount, CustomerEntity pCustomer);
        IEnumerable<LoanTransactionsEntity> LoanMiniStatement(string pId);

        SavingsEntity GetSavingsDetail(string pId);
        IEnumerable<SavingTransactionsEntity> SavingsMiniStatement(string pId);

        IEnumerable<AccountsEntity> GetAccounts(string pId);
    }
}
