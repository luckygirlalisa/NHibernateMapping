using System.Collections.Generic;
using Domain.Entities;
using NHibernate; 

namespace NHibernateMapping
{
    public class SessionManager
    {
        private ISession session;
        public SessionManager(ISession session)
        {
            this.session = session;
        }

        public IList<Customer> GetAllCustomer()
        {
            return session.CreateQuery("from Customer").List<Customer>();
        }

        public IList<Customer> GetCustomerByLastName1(string lastName)
        {
            return session.CreateQuery("from Customer where LastName = '"+lastName+"' ").List<Customer>();
        }

        public IList<Customer> GetCustomerByLastName2(string lastName)
        {
            return session.CreateQuery("from Customer where LastName = ? ")
                .SetParameter(0, lastName)
                .List<Customer>();
        }

        public IList<Customer> GetCustomerByLastName3(string lastName)
        {
            return session.CreateQuery("from Customer where LastName = :lastName")
                          .SetParameter("lastName", lastName)
                          .List<Customer>();
        }
    }
}
