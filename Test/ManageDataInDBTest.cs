using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using NHibernate;
using NHibernate.Util;
using NHibernateMapping;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class ManageDataInDBTest
    {
        public SessionManager SessionManager;
        [SetUp]
        public void Init()
        {
            ISession session = new SessionCreator().GetSession();
            SessionManager = new SessionManager(session);
        }

        [Test]
        public void should_save_data_to_DB()
        {
            var customer = new Customer {FirstName = "Enyu", LastName = "Zhao"};

            SessionManager.SaveCustomerInToDB(customer);

            IList<Customer> EnyuZhaos = SessionManager.GetCustomerBySample(customer);
            PrintCustomerList(EnyuZhaos);
        }

        [Test]
        public void should_delete_customer_in_DB()
        {
            var customers = SessionManager.GetAllCustomer();
            var leastId = customers.Min(c => c.CustomerId);

            var customer = SessionManager.GetCustomerById(leastId);
            Assert.NotNull(customer);

            SessionManager.DeleteCustomer(customer);

            var deletedCustomer = SessionManager.GetCustomerById(leastId);
            Assert.Null(deletedCustomer);
        }

        private static void PrintCustomerList(IList<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(new { customer.CustomerId, customer.FirstName, customer.LastName });
            }
        }
    }
}
