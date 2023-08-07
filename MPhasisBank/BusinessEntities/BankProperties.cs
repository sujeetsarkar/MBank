using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public static class BankProperties
    {
        public static bool IsValidTransaction(this float Amount)
        {
            if (Amount >= 100 )
                return true;
            else
                return false;
        }
        public static bool IsSeniorCitizen(this CustomerEntity Customer)
        {
            //teTime Current = ;//(YYYY-MM-DD) or (MM-DD-YYYY)
            //int age = 0;
            //age = DateTime.Now.Year - Customer.DOB.Year;
            //if (DateTime.Now.DayOfYear < Customer.DOB.DayOfYear)
            //    age--;
            int age = Customer.Age();
            if (age > 60)
                return true;
            else
                return false;
        }

        public static int Age(this CustomerEntity Customer)
        {
            int age = 0;
            age = DateTime.Now.Year - Customer.DOB.Year;
            if (DateTime.Now.DayOfYear < Customer.DOB.DayOfYear)
                age--;
            return age;
        }

        public static bool IsValidDOB(this CustomerEntity Customer)
        {
            //double i = (DateTime.Now - Customer.DOB).TotalDays;//(YYYY-MM-DD) or (MM-DD-YYYY)
            if (Customer.DOB < DateTime.Now)
                return true;
            else
                return false;
        }

        public static bool IsSufficientBalance(this SavingsEntity Account, int Amount)
        {
            if (Account.Balance - Amount >= 1000)
                return true;
            else
                return false;
        }

        public static bool IsValidPartpaymentAmount(this float pAmount,LoanEntity pLoanCustomer)
        {
            double _doubleTemp = pLoanCustomer.Loan_Amount * 0.6;
            if (pAmount < _doubleTemp)
                return true;
            return false;
        }

        #region //generate Id
        public static string GenerateID()
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
        #endregion



        #region //Generate Password

        public static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";

            char[] chars = new char[passwordLength];
            Random rd = new Random();


            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        #endregion
    }
}
