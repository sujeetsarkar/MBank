using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace DALayer
{
    public static class ValidationDAL
    {
        static MPhasisBankEntities Obj = new MPhasisBankEntities();
        public static bool IsExistingCustomer(this string pPAN)
        {
            try
            {
                var _custResult = (from customer in Obj.CustomerEntities
                                   where customer.PAN == pPAN
                                   select customer).ToList();
                if (_custResult.Count > 0)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public static string GetAccountId(this string pId)
        {
            string _strAccountId;
            if(pId.StartsWith("MLA"))
            {
                var _resAccount = (from t in Obj.AccountsEntities
                                   where t.Customer_ID == pId && t.Account_Status == 1
                                   select t).ToList();
                if (_resAccount.Count == 0)
                    throw new Exception("Account is not Active or does not exists");
                _strAccountId = _resAccount.First().Account_ID;
            }
            else
            {
                var _resAccount = (from t in Obj.AccountsEntities
                                   where t.Account_ID == pId && t.Account_Status == 1
                                   select t).ToList();
                _strAccountId = pId;
            }
            return _strAccountId;
        }

        public static string GetCustomerId(this string pId)
        {
            string _strCustId;
            if (pId.StartsWith("MLA"))
            {
                _strCustId = pId;
                var _resAccount = (from t in Obj.AccountsEntities
                                   where t.Customer_ID == pId && t.Account_Status==1
                                   select t).ToList();
                if (_resAccount.Count ==0 )
                    throw new Exception("Account is not Active or does not exists");

            }
            else if (pId.StartsWith("LN")||pId.StartsWith("SB"))
            {
                try
                {
                    var _resAccount = (from t in Obj.AccountsEntities
                                       where t.Account_ID == pId && t.Account_Status == 1
                                       select t).ToList();
                    if (_resAccount.Count == 0)
                        throw new Exception("Account is not Active or does not exists");
                    _strCustId = _resAccount.First().Customer_ID;
                }
                catch (Exception pEx)
                {
                    throw pEx;
                }
            }
            else
                throw new Exception("Invalid id");

            return _strCustId;
        }
        
    }
} 
