//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DALayer
{
    using System;
    using System.Collections.Generic;
    
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