using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;


namespace DALayer
{
    public class ManagerLinker
    {
        public IManagerRepository GetManagerDALInstance()
        {
            return new ManagerDAL();
        }
    }
    public class ManagerDAL : IManagerRepository, IEmployeeSavingsRepository, IEmployeeLoanRepository
    {
        MPhasisBankEntities Obj = new MPhasisBankEntities();
        public bool CheckExistingCustomer(string pPAN)
        {
            try
            {
                return pPAN.IsExistingCustomer();
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
                var _resMCheck = from t in Obj.EmployeeEntities
                                 where t.Employee_ID == pEmpID
                                 select t;
                if (_resMCheck.First().Employee_Type == "M")
                {
                    StaffHome _newStaffHome = new StaffHome();
                    _newStaffHome.Employee_ID = pEmpID;
                    _newStaffHome.Emp_Name = _resMCheck.First().Employee_Name;
                    _newStaffHome.Emp_Type = "Manager";
                    _newStaffHome.Dept_ID = "NA";
                    _newStaffHome.Dept_Name = "NA";
                    return _newStaffHome;
                }
                var _resEmp = from t1 in Obj.EmployeeEntities
                              join t2 in Obj.DepartmentEntities on t1.Department_ID equals t2.Department_ID
                              select new
                              {
                                  t1.Employee_ID,
                                  t1.Employee_Name,
                                  t1.Employee_Type,
                                  t2.Department_ID,
                                  t2.Department_Name
                              };
                if (_resEmp.Count() > 0)
                {
                    var _res = (from t in _resEmp
                                where t.Employee_ID == pEmpID
                                select t).ToList();
                    StaffHome _newStaffHome = new StaffHome();
                    _newStaffHome.Employee_ID = _res.First().Employee_ID;
                    _newStaffHome.Emp_Name = _res.First().Employee_Name;
                    _newStaffHome.Emp_Type = _res.First().Employee_Type;
                    _newStaffHome.Dept_ID = _res.First().Department_ID;
                    _newStaffHome.Dept_Name = _res.First().Department_Name;
                    return _newStaffHome;
                }
                else
                    throw new Exception("No record Found");
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }
        public NewAccountDetails OpenSavingsAccount(CustomerEntity pCustomer, float pAmount)
        {
            if (pCustomer.PAN.IsExistingCustomer())
            {
                throw new Exception("Customer Already Exist");
            }
            try
            {
                string pAccountId;
                Obj.CustomerEntities.Add(pCustomer);

                if (AddAccount(pCustomer.Customer_ID, pAmount, out pAccountId))
                {

                    NewAccountDetails _newDetail = new NewAccountDetails();
                    _newDetail.Customer_ID = pCustomer.Customer_ID;
                    _newDetail.Account_ID = pAccountId;
                    _newDetail.Account_Type = "SB";
                    _newDetail.Customer_Name = pCustomer.Customer_Name;
                    _newDetail.Balance = pAmount;
                    _newDetail.DOB = pCustomer.DOB;
                    _newDetail.Password = pCustomer.CPassword;
                    _newDetail.PAN = pCustomer.PAN;
                    _newDetail.Email = pCustomer.Email;
                    _newDetail.Phone = pCustomer.Phone;
                    Contact(_newDetail);
                    Obj.SaveChanges();

                    return _newDetail;
                }
                else
                    throw new Exception("Internal Erro");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool AddAccount(string pCustomerId, float pAmount, out string pAccountId)
        {
            try
            {
                AccountsEntity _newAccount = new AccountsEntity();
                _newAccount.Account_ID = "SB" + pCustomerId.Substring(3);
                _newAccount.Customer_ID = pCustomerId;
                _newAccount.Account_Type = "SB";
                _newAccount.Account_Status = 1;
                Obj.AccountsEntities.Add(_newAccount);
                pAccountId = _newAccount.Account_ID;
                if (AddSavingsAccount(_newAccount, pAmount))
                {
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }
        public bool AddSavingsAccount(AccountsEntity pAccount, float pAmount)
        {
            try
            {
                SavingsEntity _newSavingsAccount = new SavingsEntity();
                _newSavingsAccount.Account_ID = pAccount.Account_ID;
                _newSavingsAccount.Customer_ID = pAccount.Customer_ID;
                _newSavingsAccount.Balance = pAmount;
                Obj.SavingsEntities.Add(_newSavingsAccount);
                if (AddSavingstransaction(_newSavingsAccount, pAmount))
                    return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        public bool AddSavingstransaction(SavingsEntity pSavingsAccount, float pAmount)
        {
            try
            {
                SavingTransactionsEntity _newSBTransaction = new SavingTransactionsEntity();
                _newSBTransaction.Savings_Trans_ID = "SBTR" + GenerateTransID();
                _newSBTransaction.Account_ID = pSavingsAccount.Account_ID;
                _newSBTransaction.Transaction_Date = DateTime.Now;
                if (pAmount < 0)
                {
                    _newSBTransaction.Transaction_Type = "DR";
                }
                else
                {
                    _newSBTransaction.Transaction_Type = "CR";
                }
                _newSBTransaction.Amount = Math.Abs(pAmount);
                Obj.SavingTransactionsEntities.Add(_newSBTransaction);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool CloseSavingsAccount(string pId)//enter either account id or customer id
        {
            try
            {
                string _tempId = null;
                if (pId.StartsWith("MLA"))
                {
                    _tempId = "LN" + pId.Substring(3);
                }
                else if (pId.StartsWith("LN") || pId.StartsWith("SB"))
                {
                    _tempId = "LN" + pId.Substring(2);
                }
                else
                    throw new Exception("Invalid Account id");
                var _checkLN = (from t in Obj.AccountsEntities
                                where t.Account_ID == _tempId && t.Account_Status == 1
                                select t).ToList();
                if (_checkLN.Count > 0)
                    throw new Exception("First Close Loan Account to close Savings Account");

                var _temp = (from t in Obj.SavingsEntities
                             where t.Customer_ID == pId
                             select t).ToList();

                if (pId.StartsWith("MLA"))
                {
                    var _res = (from t in Obj.AccountsEntities
                                where t.Customer_ID == pId && t.Account_Status == 1
                                select t).ToList();
                    if (_res.Count > 0)
                    {
                        AddSavingstransaction(_temp.First(), -_temp.First().Balance);
                        _temp.First().Balance = 0;
                        _res.First().Account_Status = 0;
                        Obj.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Invalid Customer Id");
                    }
                }
                else if (pId.StartsWith("SB"))
                {
                    var _resAccount = (from t in Obj.AccountsEntities
                                       where t.Account_ID == pId && t.Account_Status == 1
                                       select t).ToList();
                    if (_resAccount.Count > 0)
                    {
                        AddSavingstransaction(_temp.First(), -_temp.First().Balance);
                        _temp.First().Balance = 0;
                        _resAccount.First().Account_Status = 0;
                        Obj.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Invalid Customer Id");
                    }
                }
                else
                    throw new Exception("Invalid Savings Account id");
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }


        private static string GenerateTransID()
        {
            string allowedChars = "0123456789";

            char[] chars = new char[5];
            Random rd = new Random(DateTime.Now.Millisecond);


            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
            //In calling func append this id with respective Id type using concatination

        }
        public bool Deposit(String pId, float pAmount)
        {
            try
            {
                string _strCustId = pId.GetCustomerId();
                var _res = (from t in Obj.AccountsEntities
                            where t.Customer_ID == _strCustId && t.Account_Status == 1
                            select t).ToList();
                if (_res.Count > 0)
                {
                    var _resSBAccount = (from t in Obj.SavingsEntities
                                         where t.Customer_ID == _strCustId
                                         select t).ToList();
                    if (_resSBAccount.Count > 0)
                    {
                        _resSBAccount.First().Balance += pAmount;
                        if (AddSavingstransaction(_resSBAccount.First(), pAmount))
                        {
                            Obj.SaveChanges();
                            return true;
                        }
                    }
                    else
                        throw new Exception("Account Does not exists");
                }
                else
                    throw new Exception("Account in Inactive");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        public bool Withdraw(string pId, float pAmount)
        {
            try
            {
                string _strCustId = pId.GetCustomerId();
                var _accountRes = (from t in Obj.AccountsEntities
                                   where t.Customer_ID == _strCustId && t.Account_Status == 1
                                   select t).ToList();
                if (_accountRes.Count > 0)
                {
                    var _saveAccount = from t in Obj.SavingsEntities
                                       where t.Customer_ID == _strCustId
                                       select t;
                    if ((_saveAccount.First().Balance - pAmount) >= 1000)
                    {
                        _saveAccount.First().Balance -= pAmount;
                        if (AddSavingstransaction(_saveAccount.First(), -pAmount))
                        {
                            Obj.SaveChanges();
                            return true;
                        }
                    }
                    else
                        throw new Exception("Account Balance is not Sufficient");
                }
                else
                    throw new Exception("Account does not exist or Inactive");
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
            return false;
        }

        public IEnumerable<ReportData> GetCustomerReport(string pCustID)
        {
            List<ReportData> li = new List<ReportData>();
            var _report = from t1 in Obj.CustomerEntities
                          join t2 in Obj.AccountsEntities on t1.Customer_ID equals t2.Customer_ID
                          join t3 in Obj.SavingsEntities on t2.Account_ID equals t3.Account_ID
                          join t4 in Obj.SavingTransactionsEntities on t2.Account_ID equals t4.Account_ID
                          join t5 in Obj.LoanEntities on t2.Account_ID equals t5.Loan_Account_ID
                          join t6 in Obj.LoanTransactionsEntities on t2.Account_ID equals t6.Loan_Account_ID
                          select new
                          {
                              t1.Customer_ID,
                              t2.Account_ID,
                              t2.Account_Type,
                              t2.Account_Status,
                              t1.Customer_Name,
                              t1.Phone,
                              t1.PAN,
                              t3.Balance,
                              t4.Savings_Trans_ID,
                              t4.Transaction_Type,
                              t4.Transaction_Date,
                              t4.Amount,
                              t5.Loan_Amount,
                              t5.LStart_Date,
                              t5.Tenure,
                              t5.Loan_ROI,
                              t5.EMI,
                              t6.Loan_Trans_ID,
                              t6.EMI_Payment_Date,
                              pay = t6.Amount,
                              t5.Outstanding
                          };

            foreach (var item in _report)
            {
                ReportData _repData = new ReportData();
                _repData.Customer_ID = item.Customer_ID;
                _repData.Customer_Name = item.Customer_Name;
                _repData.PAN = item.PAN;
                _repData.Account_ID = item.Account_ID;
                _repData.Account_Type = item.Account_Type;
                _repData.Account_Status = item.Account_Status;
                _repData.Amount = item.Amount;
                _repData.Balance = item.Balance;
                _repData.EMI = item.EMI;
                _repData.EMI_Payment_Date = item.EMI_Payment_Date;
                _repData.Loan_Amount = item.Loan_Amount;
                _repData.Loan_Trans_ID = item.Loan_Trans_ID;
                _repData.LStart_Date = item.LStart_Date;
                _repData.Outstanding = item.Outstanding;
                _repData.Phone = item.Phone;
                _repData.LAmount = item.pay;
                _repData.Savings_Trans_ID = item.Savings_Trans_ID;
                _repData.Tenure = item.Tenure;
                _repData.Transaction_Date = item.Transaction_Date;
                _repData.Transaction_Type = item.Transaction_Type;
                _repData.Loan_ROI = item.Loan_ROI;
                li.Add(_repData);
            }

            return li.ToList();
        }

        public IEnumerable<SavingReportData> GetSavingReport(string pCustID)
        {
            throw new Exception();
        }

        public IEnumerable<LoanReportData> GetLoanReport(string pCustID)
        {
            throw new Exception();
        }

        //Get Savings transaction in GeneralDAL
        //Get Loan transaction in GeneralDAL
        //IsExisting Customer in DAL Validation

        public IEnumerable<AccountsEntity> GetAccounts(string pId)
        {
            try
            {
                var _res = (from t in Obj.AccountsEntities
                            where t.Customer_ID == pId
                            select t).ToList();
                if (_res.Count > 0)
                    return _res;
                else
                    throw new Exception("Invalid Customer Id");
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
                string _strCustId = pId.GetCustomerId();
                var _resCust = from t in Obj.CustomerEntities
                               where t.Customer_ID == _strCustId
                               select t;
                return _resCust.First();
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
                string pCustID = pId.GetCustomerId();
                var _resAccount = (from t in Obj.AccountsEntities
                                   where t.Customer_ID == pCustID && t.Account_Status == 1
                                   select t).ToList();
                if (_resAccount.Count == 2)
                {
                    throw new Exception("Loan Account Already Exists");
                }
                else if (_resAccount.Count == 0)
                {
                    throw new Exception("OPPS!!!!!No Savings Account Found or Savings Account is not Active!!!!Can't open Loan Account");
                }

                var _res = from t in Obj.CustomerEntities
                           where t.Customer_ID == pCustID
                           select t;
                double EMI = CalculateEMI(pReqsLoanAmount, pTenure, _res.First());
                if (EMI > pMonthly * 0.6)
                {
                    throw new Exception("Requested Amount Can not be Sanctioned");
                }

                //string pLoanAccount;
                if (AddLAccount(pCustID, out string pLoanAccount))
                {
                    if (AddLoanAccount(pCustID, pReqsLoanAmount, pTenure, _res.First()))
                    {
                        if (AddLoanTransaction(pCustID, -pReqsLoanAmount))
                        {
                            Obj.SaveChanges();
                            NewAccountDetails _newAccount = new NewAccountDetails();
                            _newAccount.Account_ID = pLoanAccount;
                            _newAccount.Account_Type = "LN";
                            _newAccount.Balance = -pReqsLoanAmount;
                            _newAccount.Customer_ID = pCustID;
                            _newAccount.Customer_Name = _res.First().Customer_Name;
                            return _newAccount;
                        }
                    }
                }
                throw new Exception("Error Occured while opening Loan Account");
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }

        public bool AddLAccount(string pCustID, out string pLoanAccount)
        {
            try
            {
                AccountsEntity _newLNAccount = new AccountsEntity();
                _newLNAccount.Account_ID = "LN" + pCustID.Substring(3);
                _newLNAccount.Customer_ID = pCustID;
                _newLNAccount.Account_Type = "LN";
                _newLNAccount.Account_Status = 1;
                Obj.AccountsEntities.Add(_newLNAccount);
                pLoanAccount = _newLNAccount.Account_ID;
                return true;
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }

        public bool AddLoanAccount(string pCustID, float pReqsLoanAmount, int pTenure, CustomerEntity pCustomer)
        {
            try
            {
                LoanEntity _newLoanAccount = new LoanEntity();
                _newLoanAccount.Loan_Account_ID = "LN" + pCustID.Substring(3);
                _newLoanAccount.Customer_ID = pCustID;
                _newLoanAccount.Loan_Amount = pReqsLoanAmount;
                _newLoanAccount.LStart_Date = DateTime.Now;
                _newLoanAccount.Tenure = pTenure;
                _newLoanAccount.Loan_ROI = ROI(pReqsLoanAmount, pCustomer.Age());
                _newLoanAccount.EMI = (float)CalculateEMI(pReqsLoanAmount, pTenure, pCustomer);
                _newLoanAccount.Outstanding = pReqsLoanAmount;
                Obj.LoanEntities.Add(_newLoanAccount);
                return true;
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }

        public bool AddLoanTransaction(string pCustID, float pReqsLoanAmount)
        {
            try
            {
                LoanTransactionsEntity _newLnTrans = new LoanTransactionsEntity();
                _newLnTrans.Loan_Trans_ID = "LNTR" + GenerateTransID();
                _newLnTrans.Loan_Account_ID = "LN" + pCustID.Substring(3);
                _newLnTrans.EMI_Payment_Date = DateTime.Now;
                _newLnTrans.Amount = pReqsLoanAmount;
                Obj.LoanTransactionsEntities.Add(_newLnTrans);
                return true;
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
                string _strCust = pId.GetCustomerId();
                var _loanResult = (from t in Obj.LoanEntities
                                   where t.Customer_ID == _strCust
                                   select t).ToList();
                return _loanResult.First();
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
                string _strCust = pId.GetCustomerId();
                var _res = (from t in Obj.LoanEntities
                            where t.Customer_ID == _strCust
                            select t).ToList();
                return _res.First().EMI;
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
                string _strCust = pId.GetCustomerId();
                string _strAcc = null;
                string _strCustId = pId.GetCustomerId();
                if (pId.StartsWith("MLA"))
                {
                    _strAcc = "LN" + pId.Substring(3);
                }
                else if (pId.StartsWith("SB") || pId.StartsWith("LN"))
                {
                    _strAcc = "LN" + pId.Substring(2);
                }
                else
                    throw new Exception("Invalid Account Id");
                var _res = (from t in Obj.LoanTransactionsEntities
                            where t.Loan_Account_ID == _strAcc
                            select t).ToList();
                return _res;
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
                string _strAcc = null;
                string _strCustId = pId.GetCustomerId();
                if (pId.StartsWith("MLA"))
                {
                    _strAcc = "SB" + pId.Substring(3);
                }
                else if (pId.StartsWith("SB") || pId.StartsWith("LN"))
                {
                    _strAcc = "SB" + pId.Substring(2);
                }
                else
                    throw new Exception("Invalid Account Id");
                var _res = (from t in Obj.SavingTransactionsEntities
                            where t.Account_ID == _strAcc
                            select t).ToList();
                return _res;
            }
            catch (Exception pEx)
            {

                throw pEx;
            }
        }

        //Validation
        public float ROI(float amount, int age)
        {
            float temp;
            if (amount < 10000)
            {
                throw new Exception("Amount can not be lesser than");
            }
            if (age < 60)
            {
                if (amount >= 10000 && amount <= 500000)
                {
                    temp = 10;
                    return temp;
                }
                else if (amount > 500000 && amount <= 1000000)
                {
                    temp = 9.5F;
                    return temp;
                }
                else
                {
                    temp = 9;
                    return temp;
                }
            }
            else
                return 9.5F;
        }

        public float ROI(float pAmount, CustomerEntity pCustomer)
        {
            return ROI(pAmount, pCustomer.Age());
        }
        public double CalculateEMI(float pAmount, int pTenure, CustomerEntity pCustomer)
        {
            int age = pCustomer.Age();
            double Rate = ROI(pAmount, age);
            double MonthlyRate = Rate / 12 / 100;
            double EMI = pAmount * MonthlyRate * Math.Pow(1 + MonthlyRate, pTenure) / (Math.Pow(1 + MonthlyRate, pTenure) - 1);

            return EMI;
        }

        public LoanEntity PartPayment(string pId, int pTenure, float pAmount)
        {
            try
            {
                string _strCustId = pId.GetCustomerId();
                var _resCust = (from t in Obj.CustomerEntities
                                where t.Customer_ID == _strCustId
                                select t).ToList();
                var _resLoan = (from t in Obj.LoanEntities
                                where t.Customer_ID == _strCustId
                                select t).ToList();
                string _temp = _resLoan.First().Loan_Account_ID;
                var _resLoanTransDate = (from t in Obj.LoanTransactionsEntities
                                         where t.Loan_Account_ID == _temp
                                         select t.EMI_Payment_Date).ToList();
                float _fTotalInterest = (_resLoan.First().EMI * _resLoan.First().Tenure);
                //float _fMonthlyInterest = _fTotalInterest / _resLoan.First().Tenure;
                double _fPartPaymentSI = Math.Abs(_resLoan.First().Outstanding * _resLoan.First().Loan_ROI * (DateTime.Today - _resLoanTransDate.First()).TotalDays / (100 * (new DateTime(DateTime.Now.Year + 1, 1, 1) - new DateTime(DateTime.Now.Year, 1, 1)).Days));
                float _fAmt = _resLoan.First().Outstanding = _resLoan.First().Outstanding - pAmount + (float)_fPartPaymentSI;

                _resLoan.First().Tenure = pTenure;
                _resLoan.First().EMI = (float)CalculateEMI(_fAmt, pTenure, _resCust.First());
                Obj.SaveChanges();
                return _resLoan.First();
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
                string _strCustId = pId.GetCustomerId();
                var _resLoan = (from t in Obj.LoanEntities
                                where t.Customer_ID == _strCustId
                                select t).ToList();
                string _temp = _resLoan.First().Loan_Account_ID;
                var _resLoanTransDate = (from t in Obj.LoanTransactionsEntities
                                         where t.Loan_Account_ID == _temp
                                         select t.EMI_Payment_Date).Max();
                var _resAccount = (from t in Obj.AccountsEntities
                                   where t.Customer_ID == _strCustId
                                   select t).ToList();
                double _fPaymentSI = Math.Abs(_resLoan.First().Outstanding * _resLoan.First().Loan_ROI * (DateTime.Today - _resLoanTransDate).TotalDays / (100 * (new DateTime(DateTime.Now.Year + 1, 1, 1) - new DateTime(DateTime.Now.Year, 1, 1)).Days));
                return _resLoan.First().Outstanding + (float)_fPaymentSI;
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
                string _strCustId = pId.GetCustomerId();
                var _resLoan = (from t in Obj.LoanEntities
                                where t.Customer_ID == _strCustId
                                select t).ToList();
                string _temp = _resLoan.First().Loan_Account_ID;
                var _resLoanTransDate = (from t in Obj.LoanTransactionsEntities
                                         where t.Loan_Account_ID == _temp
                                         select t.EMI_Payment_Date).ToList();
                var _resAccount = (from t in Obj.AccountsEntities
                                   where t.Customer_ID == _strCustId
                                   select t).ToList();
                double _fPaymentSI = (-1) * (_resLoan.First().Outstanding * _resLoan.First().Loan_ROI * (DateTime.Today - _resLoanTransDate.First()).TotalDays / (100 * (new DateTime(DateTime.Now.Year + 1, 1, 1) - new DateTime(DateTime.Now.Year, 1, 1)).Days));

                if (Math.Floor(_resLoan.First().Outstanding + _fPaymentSI) == Math.Floor(pAmount))
                {
                    if (AddLoanTransaction(_strCustId, _resLoan.First().Loan_Amount))//instead pAmount
                    {
                        _resLoan.First().Outstanding = 0;
                        _resLoan.First().Loan_ROI = 0;
                        _resLoan.First().EMI = 0;
                        _resLoan.First().Tenure = 0;
                        _resLoan.First().Loan_Amount = 0;
                        _resAccount.First().Account_Status = 0;
                        Obj.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    throw new Exception("Invalid Amount");
                }
                return false;
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
                string _strCustId = pId.GetCustomerId();
                var _resLoan = (from t in Obj.LoanEntities
                                where t.Customer_ID == _strCustId
                                select t).ToList();
                float _fAmt = _resLoan.First().EMI;
                _resLoan.First().Outstanding -= _resLoan.First().EMI;

                if (_resLoan.First().Outstanding == 0)
                {
                    var _resAccounts = (from t in Obj.AccountsEntities
                                        where t.Customer_ID == _strCustId
                                        select t).ToList();
                    _resAccounts.First().Account_Status = 0;
                    _resLoan.First().Loan_ROI = 0;
                    _resLoan.First().EMI = 0;
                    _resLoan.First().Tenure = 0;
                    _resLoan.First().Loan_Amount = 0;
                }

                if (AddLoanTransaction(_strCustId, -_resLoan.First().EMI))
                {
                    Obj.SaveChanges();
                    return _fAmt;
                }
                throw new Exception("!!Failed...Error occured while updating Transaction");
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
                Obj.DepartmentEntities.Add(pDept);
                int _count = Obj.SaveChanges();
                if (_count == 1)
                    return true;
                else
                    return false;
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
                DepartmentEntity _dept = new DepartmentEntity();
                _dept = Obj.DepartmentEntities.Find(pDeptID);
                Obj.DepartmentEntities.Remove(_dept);
                int _count = Obj.SaveChanges();
                if (_count == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }

        public EmployeeEntity AddEmployee(EmployeeEntity pEmployee)
        {
            try
            {
                Obj.EmployeeEntities.Add(pEmployee);
                int _count = Obj.SaveChanges();
                return pEmployee;
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
                EmployeeEntity _newEmp = new EmployeeEntity();
                _newEmp = Obj.EmployeeEntities.Find(pEmpID);
                Obj.EmployeeEntities.Remove(_newEmp);
                int _count = Obj.SaveChanges();
                if (_count == 1)
                    return true;
                else
                    return false;
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
                string _strCustId = pId.GetCustomerId();
                var _res = (from t in Obj.SavingsEntities
                            where t.Customer_ID == _strCustId
                            select t).ToList();
                return _res.First();
            }
            catch (Exception pEx)
            {
                throw pEx;
            }
        }


        //sending Email
        public void Contact(NewAccountDetails pNewAccount)//EmailFormModel model)
        {
            //var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(pNewAccount.Email));  // replace with valid value 
            message.From = new MailAddress("Sujeet.Kumar03@mphasis.com");  // replace with valid value
            message.Subject = "M-Bank:Welcome to MBank Family";//Your email subject
            message.Body = string.Format("<table align='center' border='1'>" +
    "<tr>" +
        "<td colspan='2' align='center'>Customer Detail</td>" +
        "</tr>" +
    "<tr>" +
        "<td>Customer_ID</td>" +
        "<td>" + pNewAccount.Customer_ID + "</td>" +
    "</tr>" +
    "<tr>" +
        "<td>AccountId</td>" +
        "<td>" + pNewAccount.Account_ID + "</td>" +
    "</tr>" +
    "<tr>" +
        "<td>Account Type</td>" +
        "<td>" + pNewAccount.Account_Type + "</td>" +
    "</tr>" +
    "<tr>" +
        "<td>Name</td>" +
        "<td>" + pNewAccount.Customer_Name + "</td>" +
    "</tr>" +
    "<tr>" +
        "<td>Balance</td>" +
        "<td>" + pNewAccount.Balance + "</td>" +
    "</tr>" +
    "<tr>" +
        "<td>DOB</td>" +
        "<td>" + pNewAccount.DOB + "</td>" +
    "</tr>" +
        "<tr>" +
        "<td>Password</td>" +
        "<td>" + pNewAccount.Password + "</td>" +
    "</tr>" +
    "<tr>" +
        "<td>PAN</td>" +
        "<td>" + pNewAccount.PAN + "</td>" +
    "</tr>" +
    "<tr>" +
        "<td>Email</td>" +
        "<td>"+pNewAccount.Email+"</td>" +
    "</tr>" +
    "<tr>" +
        "<td>+Mobile</td>" +
        "<td>" + pNewAccount.Phone + "</td>" +
    "</tr>" +
"</table>");
            message.IsBodyHtml = true;

            try
            {
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "Sujeet.Kumar03@mphasis.com",  // replace with valid value
                        #region  //password-- pls don't open
                        Password = "sumit@9001827240"  // replace with valid value
                        #endregion
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    //smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                    //await smtp.SendMailAsync(message);
                    //return RedirectToAction("Sent");
                }
            }
            catch (Exception pEx)
            {
                throw pEx;
            }

            

            //return View("Index");
        }






    }
}