using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class NewAccountDetails
    {
        public string Customer_ID { get; set; }
        public string Account_ID { get; set; }
        public string Customer_Name { get; set; }
        public string Account_Type { get; set; }
        public string Password { get; set; }
        public DateTime DOB { get; set; }
        public string PAN { get; set; }
        public float Balance { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
