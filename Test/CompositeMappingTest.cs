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

//        [Test]
//        public void should_get_orders_from_customer()
//        {
//            IList<Order> orders = new List<Order>{ };
//            var customerWithOrders = new Customer {FirstName = "Tuhao", LastName = "Li", Orders = orders};
//        }

        [Test]
        public void should_get_Order()
        {
            var order = new Order {Version = 1, OrderDate = "2013-11-27", CustomerId = 5};
            sessionManager.SaveOrderInToDB(order);
            var resultOrders = sessionManager.GetOrderFromDBByExample(order).ToArray();
            PrintOrders(resultOrders);
        }

        private void PrintOrders(params Order[] orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine(new {order.OrderId, order.Version, order.OrderDate, order.CustomerId});
            }
        }
    }
}
