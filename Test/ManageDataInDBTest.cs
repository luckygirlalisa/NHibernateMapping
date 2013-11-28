using System;
using System.Linq;
using Domain.Entities;
using NHibernate;
using NHibernateMapping;
using NUnit.Framework;


namespace Test
{
    [TestFixture]
    class ManageDataInDBTest
    {
        public SessionManager sessionManager;
        [SetUp]
        public void Init()
        {
            ISession session = new SessionCreator().GetSession();
            sessionManager = new SessionManager(session);
        }

        [Test]
        public void should_save_data_to_DB()
        {
            var customer = new Customer {FirstName = "Ruijie", LastName = "Huang"};

            sessionManager.SaveCustomerInToDB(customer);

            var ruijieHuangs = sessionManager.GetCustomerBySample(customer).ToArray();
            PrintCustomer(ruijieHuangs);
        }

        [Test]
        public void should_delete_customer_in_DB()
        {
            var customer = sessionManager.GetCustomerById(GetLeastId());
            Assert.NotNull(customer);

            sessionManager.DeleteCustomer(customer);    

            var deletedCustomer = sessionManager.GetCustomerById(GetLeastId());
            Assert.Null(deletedCustomer);
        }

        [Test] //need change firstName to update, test update and transaction
        public void should_update_data_in_DB()
        {
            var leastId = GetLeastId();
            var sourceCustomer = sessionManager.GetCustomerById(leastId);
            PrintCustomer(sourceCustomer);
            
            sourceCustomer.FirstName = "Zhanfei";
            sessionManager.UpdateCustomer(sourceCustomer);

            var resultCustomer = sessionManager.GetCustomerById(leastId);
            PrintCustomer(resultCustomer);
        }

        [Test]
        public void should_save_or_update_customer()
        {
            var newCustomer = new Customer {FirstName = "Unique", LastName = "Li"};
            var existCustomer = sessionManager.GetCustomerById(GetLeastId());
            PrintCustomer(existCustomer);
            existCustomer.FirstName = "NonExistFirstName";

            sessionManager.SaveOrUpdateCustomer(newCustomer);
            sessionManager.SaveOrUpdateCustomer(existCustomer);

            var resultNewCustomer = sessionManager.GetCustomerBySample(newCustomer).First();
            var resultExistCustomer = sessionManager.GetCustomerById(existCustomer.CustomerId);
            PrintCustomer(new[] {resultNewCustomer, resultExistCustomer});
        }


        private void PrintCustomer(params Customer[] customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(new { customer.CustomerId, customer.FirstName, customer.LastName });
            }
        }

        private int GetLeastId()
        {
            var customers = sessionManager.GetAllCustomer();
            var leastId = customers.Min(c => c.CustomerId);
            return leastId;
        }
    }
}
