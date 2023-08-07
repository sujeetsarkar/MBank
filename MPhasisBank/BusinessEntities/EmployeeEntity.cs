using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{
    public partial class EmployeeEntity
    {
        public string Employee_ID { get; set; }
        public string Employee_Name { get; set; }
        public string EPassword { get; set; }
        public string Employee_Type { get; set; }
        public string Department_ID { get; set; }
        public string DeptID_Comp { get; set; }

        public virtual DepartmentEntity DepartmentEntity { get; set; }
    }
}
