using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using NHibernateMapping;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    class CompositeMappingTest
    {
        private SessionManager sessionManager;
      
        [SetUp]
        public void Init()
        {
            var session = new SessionCreator().GetSession();
            sessionManager = new SessionManager(session);
        }

        [Test]
        public void should_get_Order()
        {
            var order = new Order {Version = 1, OrderDate = "2013-11-27"};
            sessionManager.SaveOrderInToDB(order);
            var resultOrders = sessionManager.GetOrderFromDBByExample(order).ToArray();
            PrintOrders(resultOrders);
        }

        [Test]
        public void should_get_orders_from_customer()
        {
            IList<Order> orders = new List<Order> { new Order {Version = 1, OrderDate = "2013-11-27" }};
            var customerWithOrders = new Customer {FirstName = "Tuhao", LastName = "Li", Orders = orders};

            sessionManager.SaveCustomerInToDB(customerWithOrders);

            var customer = sessionManager.GetCustomerBySample(customerWithOrders).ToArray();
            PrintCustomer(customer);
        }

        private void PrintOrders(params Order[] orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine(new {order.OrderId, order.Version, order.OrderDate});
            }
        }

        private void PrintCustomer(params Customer[] customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(new { customer.CustomerId, customer.FirstName, customer.LastName, customer.Orders });
            }
        }
    }
}
