using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessEntities;
using BusinessLayer;
using System.Web.Security;
using System.Text.RegularExpressions;
using iTextSharp;
using RazorPDF;

namespace MPhasisBankUI.Controllers
{
    public class MainController : Controller
    {
        BManagerLinker _bManager;
        IManager _mgrObj;
        public MainController()
        {
            _bManager = new BManagerLinker();
            _mgrObj = _bManager.GetManagerService();
        }

        // GET: Main
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string submit)
        {
            switch (submit)
            {
                case "customer":
                    Session["user"] = "customer";
                    break;
                case "staff":
                    Session["user"] = "staff";
                    break;
                default:
                    Session["user"] = "Invalid";
                    break;
            }

            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Login()
        {
            //Response.Write(Session["user"]);
            return View();
        }
        [HttpPost]
        public void Login(string pUsrName, string pPwd, string pType)
        {
            Session["id"] = pUsrName;
            if (pType == "customer")
            {
                Session["user"] = "customer";
                BCustomerLinker _bCust = new BCustomerLinker();
                ICustomer _custObj = _bCust.GetGeneralService();
                try
                {
                    int _intLoginRes = _custObj.Login(pUsrName, pPwd);
                    if (_intLoginRes == 1)
                    {
                        FormsAuthentication.SetAuthCookie(pUsrName, true);
                        Session["type"] = 1;
                        Response.Redirect("CustomerHome");
                    }
                    else
                    {
                        ViewBag.Msg = "Invalid Customer Credential";
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                //return View();
            }
            else if (pType == "staff")
            {
                Session["user"] = "staff";
                BEmployeeSavingsLinker _bEmpSave = new BEmployeeSavingsLinker();
                IEmployeeSavings _empSaveObj = _bEmpSave.GetGeneralService();
                try
                {
                    int _intLoginRes = _empSaveObj.Login(pUsrName, pPwd);
                    if (_intLoginRes == 2)
                    {
                        FormsAuthentication.SetAuthCookie(pUsrName, true);
                        Session["type"] = 2;
                         Response.Redirect("ManagerHome");
                    }
                    else if (_intLoginRes == 3)
                    {
                        FormsAuthentication.SetAuthCookie(pUsrName, true);
                        Session["type"] = 3;
                         Response.Redirect("ManagerHome");
                    }
                    else if (_intLoginRes == 4)
                    {
                        FormsAuthentication.SetAuthCookie(pUsrName, true);
                        Session["type"] = 4;
                        Response.Redirect("ManagerHome");
                    }
                    else
                        ViewBag.Msg = "Invalid Staff Credential";
                }
                catch (Exception pEx)
                {
                    Response.Write(pEx.Message);
                }
                //return View();
            }
            Session["user"] = "Invalid";
            //Response.Write(Session["user"]);
            //return View();
        }
        public ActionResult CustomerHome()
        {
            BCustomerLinker _bCust = new BCustomerLinker();
            ICustomer _custObj = _bCust.GetCustomerService();
            try
            {
                if (Session["id"] == null)
                    Response.Redirect("Login");
                CustomerHome _cust = new CustomerHome();
                _cust = _custObj.CustomerHomeDetail(Convert.ToString(Session["id"]));
                ViewBag.id = _cust;
            }
            catch (Exception pEx)
            {
                Response.Write(pEx.Message);
            }
            return View();
        }
        public ActionResult SuccessMgr()
        {
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(Session["user"].ToString(), false);
            Session.Abandon();
            //Session.Clear();
            Response.Redirect("Login");
            //return View();
        }

        public ActionResult SavingServices()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SBTransaction()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SBTransaction(string pId, string FromDate, string ToDate)
        {
            //Response.Redirect("http://localhost:4200");
            BCustomerLinker _bCust = new BCustomerLinker();
            ICustomer _custObj = _bCust.GetGeneralService();
            try
            {
                ViewData["msg"] = null;
                IEnumerable<SavingTransactionsEntity> li;
                li = _custObj.GetSavingsTransaction(pId, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
                ViewBag.Trans = li;
            }
            catch (Exception pEx)
            {
                ViewData["msg"] = pEx.Message;
            }
            //return new RazorPDF.PdfResult(ViewBag.Trans, "http://localhost:4200");
            return View();
        }
        [HttpGet]
        public ActionResult LNTransaction()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LNTransaction(string AccountID, string FromDate, string ToDate)
        {
            BCustomerLinker _bCust = new BCustomerLinker();
            ICustomer _custObj = _bCust.GetGeneralService();
            try
            {
                ViewData["msg"] = null;
                    IEnumerable<LoanTransactionsEntity> li;
                    li = _custObj.GetLoanTransaction(AccountID, Convert.ToDateTime(FromDate), Convert.ToDateTime(ToDate));
                    ViewBag.Trans = li;
            }
            catch (Exception pEx)
            {
                ViewData["msg"] = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult AccountBalance()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AccountBalance(string pID)
        {
            //ViewData["msg"] = null;
            float pSavingBal = 0F;
            float? pOutstanding = null;
            BCustomerLinker _bCust = new BCustomerLinker();
            ICustomer _custObj = _bCust.GetCustomerService();
            try
            {
                _custObj.CheckAccountBalance(pID, out pSavingBal, out pOutstanding);
                ViewData["SavBal"] = pSavingBal;
                ViewData["LoanB"] = pOutstanding;
            }
            catch (Exception pEx)
            {
                ViewData["msg"] = pEx.Message;
            }
            return View();
        }

        public ActionResult SavingsEmployeeHome()
        {
            BEmployeeSavingsLinker _bSavingsEmp = new BEmployeeSavingsLinker();
            IEmployeeSavings _empSaveObj = _bSavingsEmp.GetEmployeeSavingsService();

            try
            {
                if (Session["id"] == null)
                    Response.Redirect("Login");
                    //RedirectToAction("Login");
                StaffHome _newStaff = new StaffHome();
                _newStaff = _empSaveObj.SaveEmpHomeDetail(Convert.ToString(Session["id"]));
                ViewBag.id = _newStaff;
            }
            catch (Exception pEx)
            {
                Response.Write(pEx.Message);
            }

            return View();
        }

        public ActionResult ManagerHome()
        {
            try
            {
                if (Session["id"] == null)
                    Response.Redirect("Login");
                StaffHome _newStaff = new StaffHome();
                _newStaff = _mgrObj.EmpHomeDetail(Convert.ToString(Session["id"]));
                ViewBag.id = _newStaff;
            }
            catch (Exception pEx)
            {
                Response.Write(pEx.Message);
            }

            return View();
        }

        [HttpGet]
        public ActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDepartment(string pDeptId, string pDeptName)
        {
            DepartmentEntity _dDept = new DepartmentEntity();
            _dDept.Department_ID = pDeptId;
            _dDept.Department_Name = pDeptName;
            ViewBag.res = false;
            try
            {
                bool _res = _mgrObj.AddDepartment(_dDept);
                ViewBag.res = _res;
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult RemoveDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RemoveDepartment(string pDeptID)
        {
            try
            {
                ViewBag.res = false;
                bool _res = _mgrObj.RemoveDepartment(pDeptID);
                ViewBag.res = _res;
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }

        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(string pDeptId, string pEmpName, string pEmpType)
        {
            EmployeeEntity _eEmp = new EmployeeEntity();
            try
            {
                if (pEmpType == "M")
                {
                    _eEmp.Department_ID = null;
                }
                else if (pEmpType == "E" && pDeptId == "")
                {
                    throw new Exception("Department for Employee Can't be empty");
                }
                else if (pEmpType == "E" && Regex.IsMatch(pDeptId, @"^(DEPT)\d+$"))
                {
                    _eEmp.Department_ID = pDeptId;
                }
                else
                {
                    throw new Exception("Invalid Deapartment Id");
                }
                _eEmp.Employee_Name = pEmpName;
                _eEmp.Employee_Type = pEmpType;

                ViewBag.res = null;
                EmployeeEntity _res = _mgrObj.AddEmployee(_eEmp);
                ViewBag.res = _res;
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }


        [HttpGet]
        public ActionResult RemoveEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RemoveEmployee(string pEmpID)
        {
            try
            {
                if (Session["id"] == null)
                    Response.Redirect("Login");
                ViewBag.res = false;
                if (Session["id"].ToString() == pEmpID)
                    throw new Exception("Can't Delete self");
                bool _res = _mgrObj.RemoveEmployee(pEmpID);
                ViewBag.res = _res;
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult OpenSavingAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OpenSavingAccount(string pCustName, string pDOB, string pPAN, string pEmail, string pPhone, string pAmount)
        {
            CustomerEntity pCust = new CustomerEntity();
            pCust.Customer_Name = pCustName;
            pCust.DOB = Convert.ToDateTime(pDOB);
            pCust.PAN = pPAN;
            pCust.Email = pEmail;
            pCust.Phone = pPhone;
            try
            {
                NewAccountDetails _newDetail = _mgrObj.OpenSavingsAccount(pCust, float.Parse(pAmount));
                ViewBag.Account = _newDetail;
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult CheckCustomerExistance()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckCustomerExistance(string pPAN)
        {
            try
            {
                ViewBag.res = _mgrObj.CheckExistingCustomer(pPAN);
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult CloseSavingAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CloseSavingAccount(string pId)
        {
            try
            {
                ViewBag.res = _mgrObj.CloseSavingsAccount(pId);
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }

        [HttpGet]
        public ActionResult DepositORWithdraw()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepositORWithdraw(string pType, string pId, string pAmount)
        {
            try
            {
                if (pType == "withdraw")
                {
                    ViewBag.res = _mgrObj.Withdraw(pId, float.Parse(pAmount));
                }
                else if (pType == "deposit")
                {
                    ViewBag.res = _mgrObj.Deposit(pId, float.Parse(pAmount));
                }
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult OpenLoanAccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OpenLoanAccount(string pId, string pMonthly, string pReqsLoanAmount, string pTenure)
        {
            try
            {
                ViewBag.Account = _mgrObj.OpenLoanAccount(pId, float.Parse(pMonthly), float.Parse(pReqsLoanAmount), int.Parse(pTenure));
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }

        [HttpGet]
        public ActionResult ForeClose()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForeClose(string pId,string pAmount)
        {
            try
            {
                ViewBag.res = null;
                ViewBag.res = _mgrObj.ForeClose(pId, float.Parse(pAmount));
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult FetchForeClose()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FetchForeClose(string pId)
        {
            try
            {
                ViewBag.res = null;
                ViewBag.res = _mgrObj.FetchForeClose(pId);
            }
            catch (Exception pEx)
            {
                ViewBag.msg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult PartPayment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PartPayment(string pId,string pTenure,string pAmount)
        {
            try
            {
                LoanEntity _newLoan = new LoanEntity();
                _newLoan = _mgrObj.PartPayment(pId, int.Parse(pTenure), float.Parse(pAmount));
                ViewBag.Account = _newLoan;
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }





        [HttpGet]
        public ActionResult GetEMI()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetEMI(string pId)
        {
            try
            {
                ViewBag.res = _mgrObj.GetEMI(pId);
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult GetAccountLoanDetail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAccountLoanDetail(string pId)
        {
            try
            {
                ViewBag.Account = _mgrObj.GetLoanDetail(pId);
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult CalculateEMIAndROI()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CalculateEMIAndROI(string pDOB,string pAmount, string pTenure)
        {
            try
            {
                ViewBag.amt = pAmount;
                ViewBag.ten = pTenure;
                CustomerEntity _custCustomer = new CustomerEntity();
                _custCustomer.DOB = Convert.ToDateTime(pDOB);
                ViewBag.emi = null;
                ViewBag.emi = _mgrObj.CalculateEMI(float.Parse(pAmount), int.Parse(pTenure), _custCustomer);
                ViewBag.roi = null;
                ViewBag.roi = _mgrObj.ROI(float.Parse(pAmount), _custCustomer);
                ViewBag.resmsg = "error";
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult LoanStatement()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoanStatement(string pId)
        {
            IEnumerable<LoanTransactionsEntity> li;
            try
            {
                ViewBag.res = null;
                li = _mgrObj.LoanMiniStatement(pId);
                ViewBag.res = li;
                ViewBag.resmsg = "error";
            }
            catch (Exception pEx)
            {

                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult SavingStatement()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SavingStatement(string pId)
        {
            IEnumerable<SavingTransactionsEntity> li;
            try
            {
                ViewBag.res = null;
                li = _mgrObj.SavingsMiniStatement(pId);
                ViewBag.res = li;
                ViewBag.resmsg = "error";
            }
            catch (Exception pEx)
            {

                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult PayEMI()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PayEMI(string pId)
        {
            try
            {
                ViewBag.res = null;
                ViewBag.res= _mgrObj.PayEmi(pId);
                ViewBag.resmsg = "error";
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult GetCustomerDetail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetCustomerDetail(string pId)
        {
            try
            {
                ViewBag.res = null;
                CustomerEntity _newCust = new CustomerEntity();
                _newCust = _mgrObj.GetCustomerDetail(pId);
                ViewBag.res = _newCust;
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        [HttpGet]
        public ActionResult GetSavingsDetail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetSavingsDetail(string pId)
        {
            try
            {
                SavingsEntity _newSave = new SavingsEntity();
                ViewBag.res = null;
                _newSave = _mgrObj.GetSavingsDetail(pId);
                ViewBag.res = _newSave;
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult GetAccountsDetail() //fetch all account
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult GetAccountsDetail(string pId)
        {
            try
            {
                ViewBag.res = null;
                IEnumerable<AccountsEntity> li;
                li = _mgrObj.GetAccounts(pId);
                ViewBag.res = li;
                ViewBag.resmsg = "Error";
            }
            catch (Exception pEx)
            {
                ViewBag.resmsg = pEx.Message;
            }
            return View();
        }        
    }
}