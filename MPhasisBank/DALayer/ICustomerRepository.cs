using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace DALayer
{
    public interface ICustomerRepository
    {
        //Login in GeneralDAL
        //part payment in GeneralDAL
        //GetSavingsTransaction in GeneralDAL
        //GetLoanTransaction in GeneralDAL
        int ForeClose(string pLoanId);

        CustomerHome CustomerHomeDetail(string pCustID);

        bool CheckAccountBalance(string pAID, out float pBal, out float? pOutstanding);
    }
}
