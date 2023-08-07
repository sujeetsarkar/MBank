using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessEntities
{
    public partial class AccountsEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountsEntity()
        {
            this.SavingTransactionsEntities = new HashSet<SavingTransactionsEntity>();
            this.LoanTransactionsEntities = new HashSet<LoanTransactionsEntity>();
            this.SavingsEntities = new HashSet<SavingsEntity>();
        }

        public string Account_ID { get; set; }
        public string Customer_ID { get; set; }
        public string Account_Type { get; set; }
        public Nullable<int> Account_Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SavingTransactionsEntity> SavingTransactionsEntities { get; set; }
        public virtual CustomerEntity CustomerEntity { get; set; }
        public virtual LoanEntity LoanEntity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoanTransactionsEntity> LoanTransactionsEntities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SavingsEntity> SavingsEntities { get; set; }
    }
}
