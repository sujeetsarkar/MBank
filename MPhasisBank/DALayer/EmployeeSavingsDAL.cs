using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace DALayer
{
    public class EmployeeSavingsLinker
    {
        public IEmployeeSavingsRepository GetEmployeeSavingsDALInstance()
        {
            return new EmployeeSavingsDAL();
        }
    }
    public class EmployeeSavingsDAL:IEmployeeSavingsRepository
    {
        IEmployeeSavingsRepository _empSavRep = new ManagerDAL();

        public bool Deposit(string pAccountID, float pAmount)
        {
            try
            {
                return _empSavRep.Deposit(pAccountID, pAmount);
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }
        
        public NewAccountDetails OpenSavingsAccount(CustomerEntity pCustomer, float pAmount)
        {
            try
            {
                return _empSavRep.OpenSavingsAccount(pCustomer, pAmount);
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
                return _empSavRep.EmpHomeDetail(pEmpID);
            }
            catch(Exception pEx)
            {
                throw pEx;
            }
        }

        public bool Withdraw(string pAccountId, float pAmount)
        {
            try
            {
                return _empSavRep.Withdraw(pAccountId, pAmount);
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
            
        }

        ////Get Savings transaction in GeneralDAL
    }
}
