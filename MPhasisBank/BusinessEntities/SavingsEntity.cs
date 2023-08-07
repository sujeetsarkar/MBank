using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public partial class SavingsEntity
    {
        public string Account_ID { get; set; }
        public string Customer_ID { get; set; }
        public float Balance { get; set; }

        public virtual AccountsEntity AccountsEntity { get; set; }
        public virtual CustomerEntity CustomerEntity { get; set; }
    }
}
