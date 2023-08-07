using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessEntities;
using BusinessLayer;

namespace MPhasisUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SeniorCitizenTestCase1()
        {
            CustomerEntity _custEntity = new CustomerEntity() { Customer_Name = "Rama", CPassword = "abcd@123", Customer_ID = "MLA12345", DOB = Convert.ToDateTime("1995-10-08"), PAN = "ABCD9824" };
            bool _boolResult = BankProperties.IsSeniorCitizen(_custEntity);
            Assert.AreEqual(false, _boolResult);
        }
        [TestMethod]
        public void SeniorCitizenTestCase2()
        {
            CustomerEntity _custEntity = new CustomerEntity() { Customer_Name = "Rama", CPassword = "abcd@123", Customer_ID = "MLA12345", DOB = Convert.ToDateTime("1950-10-08"), PAN = "ABCD9824" };
            bool _boolResult = BankProperties.IsSeniorCitizen(_custEntity);
            Assert.AreEqual(true, _boolResult);
        }
        [TestMethod]
        public void ValidTransactiontestCase1()
        {
            float _floatAmount = 116;
            bool _boolResult = BankProperties.IsValidTransaction(_floatAmount);
            Assert.AreEqual(true, _boolResult);
        }
        [TestMethod]
        public void ValidTransactiontestCase2()
        {
            float _floatAmount = 95.62F;
            bool _boolResult = BankProperties.IsValidTransaction(_floatAmount);
            Assert.AreEqual(false, _boolResult);
        }
        [TestMethod]
        public void AgeTestCase1()
        {
            CustomerEntity _custEntity = new CustomerEntity() { Customer_Name = "Rama", CPassword = "abcd@123", Customer_ID = "MLA12345", DOB = Convert.ToDateTime("1995-10-08"), PAN = "ABCD9824" };
            int _intResult = BankProperties.Age(_custEntity);
            Assert.AreEqual(22, _intResult);
        }
        [TestMethod]
        public void AgeTestCase2()
        {
            CustomerEntity _custEntity = new CustomerEntity() { Customer_Name = "Rama", CPassword = "abcd@123", Customer_ID = "MLA12345", DOB = Convert.ToDateTime("1950-10-08"), PAN = "ABCD9824" };
            int _intResult = BankProperties.Age(_custEntity);
            Assert.AreEqual(67, _intResult);
        }
        [TestMethod]
        public void ValidDOBTestCase1()
        {
            CustomerEntity _custEntity = new CustomerEntity() { Customer_Name = "Rama", CPassword = "abcd@123", Customer_ID = "MLA12345", DOB = Convert.ToDateTime("1950-10-08"), PAN = "ABCD9824" };
            bool _boolResult = BankProperties.IsValidDOB(_custEntity);
            Assert.AreEqual(true, _boolResult);
        }
        [TestMethod]
        public void ValidDOBTestCase2()
        {
            CustomerEntity _custEntity = new CustomerEntity() { Customer_Name = "Rama", CPassword = "abcd@123", Customer_ID = "MLA12345", DOB = Convert.ToDateTime("2055-10-08"), PAN = "ABCD9824" };
            bool _boolResult = BankProperties.IsValidDOB(_custEntity);
            Assert.AreEqual(false, _boolResult);
        }
        [TestMethod]
        public void SufficientBalanceTestCase1()
        {
            SavingsEntity _saveEntity = new SavingsEntity() { Customer_ID = "MLA12345", Account_ID = "SB12345", Balance = 2000 };
            int _floatAmount = 956;
            bool _boolResult = _saveEntity.IsSufficientBalance(_floatAmount);
            Assert.AreEqual(true, _boolResult);
        }
        [TestMethod]
        public void SufficientBalanceTestCase2()
        {
            SavingsEntity _saveEntity = new SavingsEntity() { Customer_ID = "MLA12345", Account_ID = "SB12345", Balance = 2000 };
            int _floatAmount = 1200;
            bool _boolResult = _saveEntity.IsSufficientBalance(_floatAmount);
            Assert.AreEqual(false, _boolResult);
        }
        [TestMethod]
        public void ValidPartPaymenttestcase1()
        {
            LoanEntity _LoanEntity = new LoanEntity() { Loan_Account_ID = "LN12345", Customer_ID = "MLA12345", Loan_Amount = 200000, Loan_ROI = 12.5F, LStart_Date = Convert.ToDateTime("2017 - 01 - 01"), EMI = 4499.59F, Tenure = 24 };
            float _floatPartialAmt = 11234.24F;
            bool _boolResult = BankProperties.IsValidPartpaymentAmount(_floatPartialAmt, _LoanEntity);
            Assert.AreEqual(true, _boolResult);
        }
        [TestMethod]
        public void ValidPartPaymenttestcase2()
        {
            LoanEntity _LoanEntity = new LoanEntity() { Loan_Account_ID = "LN12345", Customer_ID = "MLA12345", Loan_Amount = 200000, Loan_ROI = 12.5F, LStart_Date = Convert.ToDateTime("2017 - 01 - 01"), EMI = 4499.59F, Tenure = 24 };
            float _floatPartialAmt = 150000;
            bool _boolResult = BankProperties.IsValidPartpaymentAmount(_floatPartialAmt, _LoanEntity);
            Assert.AreEqual(false, _boolResult);
        }
    }
}
