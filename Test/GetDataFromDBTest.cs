using System;
using System.Collections.Generic;
using Domain.Entities;
using NHibernate.Util;
using NHibernateMapping;
using NUnit.Framework;

namespace MappingTest
{
    [TestFixture]
    class GetCustomerFromDBTest
    {
        SessionManager sessionManager;
        [SetUp]
        public void Setup()
        {
            var session = new SessionCreator().GetSession();
            sessionManager = new SessionManager(session);
        }

        [Test]
        public void should_get_all_customers_in_customer_table_using_HQL()
        {
            var allCurrentCustomer = sessionManager.GetAllCustomer();

            Assert.IsInstanceOfType(typeof(Customer), allCurrentCustomer.First());    

            PrintCustomerList(allCurrentCustomer);
        }
  
        [Test]
        public void should_get_customers_in_customer_table_using_HQL_with_parameters1()
        {
            var allCustomerWithLastNameXiao = sessionManager.GetCustomerByLastName1("Xiao");

            PrintCustomerList(allCustomerWithLastNameXiao);
        }

        [Test]
        public void should_get_customers_in_customer_table_using_HQL_with_parameters2()
        {
            var allCustomerWithLastNameXiao = sessionManager.GetCustomerByLastName2("Xiao");

            PrintCustomerList(allCustomerWithLastNameXiao);
        }

        [Test]
        public void should_get_customers_in_customer_table_using_HQL_with_parameters3()
        {
            var allCustomerWithLastNameXiao = sessionManager.GetCustomerByLastName3("Xiao");

            PrintCustomerList(allCustomerWithLastNameXiao);
        }
//
//        [Test]
//        public void should_get_customers_in_customer_table_using_criteria()
//        {
//            var allCustomWithLastNameXiao = sessionManager.GetCustomerByLastNameWithCriteria("Xiao");
//
//            PrintCustomerList(allCustomWithLastNameXiao);
//        }
//
//        [Test]
//        public void should_get_customers_in_customer_table_using_criteria_between()
//        {
//            var allCustomWithLastNameXiao = sessionManager.GetCustomerByIdRuleWithCriteria();
//
//            PrintCustomerList(allCustomWithLastNameXiao);
//        }
//
//        [Test]
//        public void should_get_customer_with_same_properties_with_sample_using_criteria()
//        {
//            var customerSample = new Customer { CustomerId = 10, FirstName = "Zhanfei", LastName = "Xiao" };
//
//            var customer = sessionManager.GetCustomerBySample(customerSample);
//
//            PrintCustomerList(customer);
//
//            //            Assert.That(customer.FirstName, Is.EqualTo("Zhanfei"));
//            //            Assert.That(customer.LastName, Is.EqualTo("Xiao"));
//            //            Assert.That(customer.Id, Is.EqualTo(10));
//
//            //            Console.WriteLine(new {customer.FirstName, customer.LastName, customer.Id});
//        }
//

        private static void PrintCustomerList(IList<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(new { customer.CustomerId, customer.FirstName, customer.LastName });
            }
        }
    }
}
