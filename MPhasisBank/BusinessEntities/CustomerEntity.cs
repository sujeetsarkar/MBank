using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace BusinessEntities
{
    public partial class CustomerEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerEntity()
        {
            this.AccountsEntities = new HashSet<AccountsEntity>();
            this.LoanEntities = new HashSet<LoanEntity>();
            this.SavingsEntities = new HashSet<SavingsEntity>();
        }

        public string Customer_ID { get; set; }
        public string CPassword { get; set; }
        public string Customer_Name { get; set; }
        public System.DateTime DOB { get; set; }
        public string PAN { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountsEntity> AccountsEntities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanEntity> LoanEntities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SavingsEntity> SavingsEntities { get; set; }
    }
}
