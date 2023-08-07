using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using BusinessLayer;

namespace MPhasisBank
{
    class Program
    {
        static void Main(string[] args)
        {
            #region //experiment
            //BCustomerLinker bc = new BCustomerLinker();
            //ICustomer ic = bc.GetCustomerService();
            //ic.Login("", "");

            //Console.WriteLine(DateTime.Today);

            //string str = "MLA12345";
            //Console.WriteLine(str.Substring(3));

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine(GenerateTransID());
            //}
            //Guid guid = Guid.NewGuid();
            //string str = guid.ToString();
            //for (int i = 0; i < 10; i++)
            //{


            //}
            //Console.WriteLine(str);
            #endregion
            //Console.WriteLine("click a button 0 or 1");
            //int btn = int.Parse(Console.ReadLine());
            //if (btn == 1)
            //{
            //    CustomerEntity ce = new CustomerEntity();
            //    ce.Customer_Name = "Manideep G";
            //    ce.DOB = Convert.ToDateTime("1994-10-21");
            //    ce.PAN = "ABCD1234";
            //    ce.Phone = "9618715433";
            //    ce.Email = "gunthamanideep@gmail.com";
            //    BManagerLinker mgrObj = new BManagerLinker();
            //    IManager im = mgrObj.GetGeneralService();
            //    Console.WriteLine("Enter User Id");
            //    string UserId = Console.ReadLine();
            //    Console.WriteLine("Password");
            //    string Pass = Console.ReadLine();
            //    int res1 = im.Login(UserId, Pass);
            //    if (res1 == 2)
            //    {
            //        Console.WriteLine("Login successful and Logged in as Employee=" + res1);
            //        BEmployeeSavingsLinker ob = new BEmployeeSavingsLinker();
            //        IEmployeeSavings ies = ob.GetEmployeeSavingsService();
            //        Console.WriteLine("opening Savings account for" +ce.Customer_Name);
            //        ies.OpenSavingsAccount(ce, 2000);
            //        Console.WriteLine("Account opened Successfully");
            //    }
            //    else if (res1 == 3)
            //    {
            //        Console.WriteLine("Login successful and Logged in as Manager=" + res1);
            //        IManager ims = mgrObj.GetManagerService();
            //        Console.WriteLine("opening Savings account for" + ce.Customer_Name);
            //        ims.OpenSavingsAccount(ce, 3000);
            //        Console.WriteLine("Account opened Successfully");
            //    }
            //}
            //else
            //{
            //    BCustomerLinker ob = new BCustomerLinker();
            //    ICustomer ic = ob.GetGeneralService();
            //    Console.WriteLine("Enter User Id");
            //    string UserId = Console.ReadLine();
            //    string Pass = Console.ReadLine();
            //    int res1 = ic.Login(UserId, Pass);
            //    if (res1 == 1)
            //        Console.WriteLine("Login Successful as Customer", +res1);
            //    else
            //        Console.WriteLine("Invalid Credential");

            //}
            //double t = (DateTime.Today - Convert.ToDateTime("2018-03-01")).TotalDays;
            //float Outstanding = 100000;
            //double totaldays= (new DateTime(DateTime.Now.Year + 1, 1, 1) - new DateTime(DateTime.Now.Year, 1, 1)).Days;
            //double _fPaymentSI = Outstanding * 10 * t / (100 * totaldays);

            //Console.WriteLine("t="+t);
            //Console.WriteLine("yeardays="+totaldays);
            //Console.WriteLine("SI="+_fPaymentSI);





            //object ob = null;
            //Console.WriteLine(Convert.ToString(ob));

            //if(Convert.ToString(ob) is null)
            //{
            //    Console.WriteLine("I am null");
            //}
            //else
            //    Console.WriteLine("Not null");

            //Console.WriteLine(DateTime.Now.Day);
            //Console.WriteLine(DateTime.Now.DayOfYear);
            //Console.WriteLine(DateTime.Now.Year);
            //Console.WriteLine(DateTime.Now.Date.Year);
            ////var thisYear = new DateTime(DateTime.Now.Year, 1, 1);
            ////var nextYear = new DateTime(DateTime.Now.Year + 1, 1, 1);

            //Console.WriteLine(( new DateTime(DateTime.Now.Year + 1, 1, 1) - new DateTime(DateTime.Now.Year, 1, 1)).Days);
            //int count = 0;
            //int i = 0;
            //for(;i<10;)
            //{
            //    i++;
            //    for(;i>9;i--)
            //    {
            //        count++;
            //        Console.WriteLine(i);
            //    }
            //}
            //Console.WriteLine("count="+count);

            try
            {
                Console.WriteLine("stmt1");
                try
                {
                    Console.WriteLine("stmt2");
                    goto Exit;
                }
                finally
                {
                    Console.WriteLine("stmt3");
                }
            }
            finally
            {
                Console.WriteLine("stmt2");
            }

            Exit: Console.WriteLine("EXIT");
            Console.ReadLine();
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
        private static string generateID(string url_add)
        {
            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);

            return number;
        }
    }
}
