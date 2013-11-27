using System.Collections.Generic;

namespace Domain.Entities
{
    public class Customer
    {
        public virtual int CustomerId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual IList<Customer> Orders { get; set; }
    }
}
