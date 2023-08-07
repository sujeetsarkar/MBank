using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
namespace BusinessLayer
{
    public class Validation
    {
        
        #region //Deposit >100 validation---want some changes

        public int ValidAmount(int Amount)
        {
            if (Amount > 100)
                return 1;
            else
                throw new Exception("Deposit Amount Can not be Less than 100");
        }
        #endregion
    }
}
