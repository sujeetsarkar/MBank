using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace DALayer
{
    public class GeneralLinker
    {
        public IGeneralRepository GetGeneralDALInstance()
        {
            return new GeneralDAL();
        }
    }
    public class GeneralDAL : IGeneralRepository
    {
        MPhasisBankEntities Obj = new MPhasisBankEntities();
        public int PartPayment(float pAmount, LoanEntity pLoanAccount, int pEntityType)//0-customer 1-staff
        {
            if (pEntityType == 0)
            {
                try
                {
                    //Finding Saving Account id from customer-id of pLoanAccount
                    var tempSavings = from savAcc in Obj.SavingsEntities
                                      where savAcc.Customer_ID == pLoanAccount.Customer_ID
                                      select savAcc;

                    if (tempSavings.Count() <= 0)
                        throw new Exception("No Saving Account Found");


                    string tempId = null;
                    foreach (var item in tempSavings)
                    {
                        //fetching savings Account-id
                        tempId = item.Account_ID;


                        //updating SavingsEntity Balance
                        item.Balance -= pAmount;
                    }
                    //Updateing Saving Transaction
                    SavingTransactionsEntity _newSaveTransaction = new SavingTransactionsEntity();
                    _newSaveTransaction.Account_ID = tempId;
                    _newSaveTransaction.Transaction_Date = DateTime.Now;
                    _newSaveTransaction.Transaction_Type = "DR";
                    _newSaveTransaction.Amount -= pAmount;

                    Obj.SavingTransactionsEntities.Add(_newSaveTransaction);

                    //Updating LoanEntity (Loan-amount)
                    pLoanAccount.Loan_Amount -= pAmount;

                    //Updating Loan Transaction
                    LoanTransactionsEntity _newLoanTransaction = new LoanTransactionsEntity();
                    _newLoanTransaction.Loan_Account_ID = pLoanAccount.Loan_Account_ID;
                    _newLoanTransaction.EMI_Payment_Date = DateTime.Now;
                    _newLoanTransaction.Amount = pAmount;

                    Obj.LoanTransactionsEntities.Add(_newLoanTransaction);

                    Obj.SaveChanges();

                    return 1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else if (pEntityType == 1)
            {
                try
                {
                    //Updating LoanEntity (Loan-amount)
                    pLoanAccount.Loan_Amount -= pAmount;

                    //Updating Loan Transaction
                    LoanTransactionsEntity _newLoanTransaction = new LoanTransactionsEntity();
                    _newLoanTransaction.Loan_Account_ID = pLoanAccount.Loan_Account_ID;
                    _newLoanTransaction.EMI_Payment_Date = DateTime.Now;
                    _newLoanTransaction.Amount = pAmount;

                    Obj.LoanTransactionsEntities.Add(_newLoanTransaction);

                    Obj.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return 0;
        }

        public int Login(string pUserId, string pPassword, int pUsrType)
        {
            if (pUsrType == 0)
            {
                try
                {
                    var _usrResult = (from t in Obj.CustomerEntities
                                      where t.Customer_ID == pUserId && t.CPassword == pPassword
                                      select t).ToList();
                    if (_usrResult.Count > 0)
                        return 1;//1=customer
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                try
                {
                    var _usrResult = (from t in Obj.EmployeeEntities
                                      where t.Employee_ID == pUserId && t.EPassword == pPassword
                                      select t).ToList();
                    if (_usrResult.Count > 0)
                    {
                        foreach (var item in _usrResult)
                        {
                            if (item.Employee_Type == "E" && item.Department_ID=="DEPT01")
                                return 2;//2= Savings Employee
                            else if(item.Employee_Type == "E" && item.Department_ID == "DEPT02")
                                return 3;//3 = Loan Employee
                            else if (item.Employee_Type == "M")
                                return 4;//4 = Manager
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            throw new Exception("Credential not valid");
        }




        public IEnumerable<SavingTransactionsEntity> GetSavingsTransaction(string pId, DateTime pFromDate, DateTime pToDate)
        {
            if (pFromDate > pToDate || pFromDate > DateTime.Now || pToDate > DateTime.Now)
                throw new Exception("Invalid Date");
            try
            {
                string pAccountId;
                if (pId.StartsWith("MLA"))
                {
                    pAccountId = "SB" + pId.Substring(3);
                }
                else if (pId.StartsWith("SB") || pId.StartsWith("LN"))
                {
                    pAccountId = "SB" + pId.Substring(2);
                }
                else
                    throw new Exception("Invalid ID");
                pToDate = pToDate.AddDays(1);
                var _transResult = (from t in Obj.SavingTransactionsEntities
                                    where t.Transaction_Date >= pFromDate && t.Transaction_Date <= pToDate && t.Account_ID == pAccountId
                                    select t).ToList();
                return _transResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<LoanTransactionsEntity> GetLoanTransaction(string pId, DateTime pFromDate, DateTime pToDate)
        {
            try
            {
                string pAccountId;
                if (pId.StartsWith("MLA"))
                {
                    pAccountId = "LN" + pId.Substring(3);
                }
                else if (pId.StartsWith("SB") || pId.StartsWith("LN"))
                {
                    pAccountId = "LN" + pId.Substring(2);
                }
                else
                    throw new Exception("Invalid ID");
                if (pFromDate > pToDate || pFromDate > DateTime.Now || pToDate > DateTime.Now)
                    throw new Exception("Invalid Date");
                pToDate = pToDate.AddDays(1);
                var _transResult = (from t in Obj.LoanTransactionsEntities
                                    where t.EMI_Payment_Date >= pFromDate && t.EMI_Payment_Date <= pToDate && t.Loan_Account_ID == pAccountId
                                    select t).ToList();
                return _transResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
