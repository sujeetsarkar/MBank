using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using System.Data.Linq;
using System.Transactions;
using System.Data.SqlClient;

namespace DALayer
{
    public class CustomerLinker
    {
        public ICustomerRepository GetCustomerDALInstance()
        {
            return new CustomerDAL();
        }
    }
    public class CustomerDAL : ICustomerRepository
    {
        MPhasisBankEntities Obj = new MPhasisBankEntities();

        //GetSavingsTransaction-------in GeneralDAL
        //GetLoanTransactions----------in GeneralDAL

        #region //Fore close (Modification required)

        public int ForeClose(string pLoanId)
        {
            //    var _loanRes = from LoanAccount in Obj.LoanEntities
            //               where LoanAccount.Loan_Account_ID == pLoanId
            //               select LoanAccount;
            //    var _totalPaid=from LoanTrans in Obj.LoanTransactionsEntities
            //                   where LoanTrans.Loan_Account_ID==pLoanId
            //                   select LoanTrans.
            //    int _intLoanPeriod = (DateTime.Now - _loanRes.First().LStart_Date).Days;
            //    double _doubleROIPerDay = _loanRes.First().Loan_ROI / 30;
            //    double _doubleTotalAmount=_loanRes.First().Loan_Ammount
            return 0;
        }
        #endregion

        public CustomerHome CustomerHomeDetail(string pCustID)
        {
            try
            {
                var _checkAccount = (from t in Obj.AccountsEntities
                                     where t.Customer_ID == pCustID && t.Account_Type == "SB" && t.Account_Status == 1
                                     select t).ToList();
                if (_checkAccount.Count == 0)
                    throw new Exception("Account is not Active");

                var _resCustHome = from t1 in Obj.CustomerEntities
                                   join t2 in Obj.SavingsEntities on t1.Customer_ID equals t2.Customer_ID
                                   select new
                                   {
                                       t1.Customer_ID,
                                       t1.Customer_Name,
                                       t2.Account_ID,
                                       t2.Balance
                                   };
                var _res = from t in _resCustHome
                           where t.Customer_ID == pCustID
                           select t;
                CustomerHome _custHome = new CustomerHome();
                _custHome.Customer_ID = pCustID;
                _custHome.Customer_Name = _res.First().Customer_Name;
                _custHome.SB_Account_Number = _res.First().Account_ID;
                _custHome.SB_Balance = _res.First().Balance;

                return _custHome;
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }

        public bool CheckAccountBalance(string pAID, out float pBal, out float? pOutstanding)
        {
            string _strCustId;
            if (pAID.StartsWith("MLA"))
            {
                _strCustId = pAID;
            }
            else
            {
                try
                {
                    var _resAccount = (from t in Obj.AccountsEntities
                                       where t.Account_ID == pAID && t.Account_Status == 1
                                       select t).ToList();
                    if (_resAccount.Count > 0)
                        _strCustId = _resAccount.First().Customer_ID;
                    else
                        throw new Exception("Account is not Active");
                }
                catch (Exception pEx)
                {
                    throw pEx;
                }
            }
            try
            {
                var _res = (from t in Obj.AccountsEntities
                            where t.Customer_ID == _strCustId && t.Account_Status == 1
                            select t).ToList();

                if (_res.Count > 0)
                {
                    var _saveRes = from t in Obj.SavingsEntities
                                   where t.Customer_ID == _strCustId
                                   select t;
                    pBal = _saveRes.First().Balance;
                    if (_res.Count > 1)
                    {
                        var _loanRes = from t in Obj.LoanEntities
                                       where t.Customer_ID == _strCustId
                                       select t;
                        pOutstanding = _loanRes.First().Outstanding;
                    }
                    else
                        pOutstanding = null;
                    return true;
                }

                else
                    throw new Exception("Account is not Active");
            }
            catch(Exception pEx)
            {
                throw pEx;
            }
        }


    }
}
