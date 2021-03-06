﻿using System;
using System.Collections.Generic;
using Domain.Entities;
using NHibernate;
using NHibernate.Criterion;
using Order = Domain.Entities.Order;

namespace NHibernateMapping
{
    public class SessionManager
    {
        private ISession session;
        public SessionManager(ISession session)
        {
            this.session = session;
        }

        #region get data from DB
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

        public IList<Customer> GetCustomerByLastNameWithCriteria(string lastName)
        {
            return session.CreateCriteria<Customer>().Add(Restrictions.Eq("LastName", lastName)).List<Customer>();
        }

        public IList<Customer> GetCustomerBySample(Customer customerSample)
        {
            Example example = Example.Create(customerSample).IgnoreCase();
            return session.CreateCriteria<Customer>().Add(example).List<Customer>();
        }


        public Customer GetCustomerById(int id)
        {
            return session.Get<Customer>(id);
        }
#endregion

        #region modify data in DB
        public void SaveCustomerInToDB(Customer customer)
        {
            session.Save(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            session.Delete(customer);
            session.Flush();
        }

        public void UpdateCustomer(Customer customer)
        {
            using (ITransaction tx = session.BeginTransaction())
            {
                try
                {
                    session.Update(customer);
//                    session.Flush();
                    tx.Commit();
                }
                catch (HibernateException)
                {
                    tx.Rollback();
                    throw;
                }
            }
            
        }

        public void UpdateCustomer1(Customer sourceCustomer)
        {
            session.Update(sourceCustomer);
            session.Flush();
        }

        public void SaveOrUpdateCustomer(Customer customer)
        {
            using (ITransaction tx = session.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(customer);
                    tx.Commit();
                }
                catch (HibernateException)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        #endregion

        public void SaveOrderInToDB(Order order)
        {
            session.Save(order);
        }

        public IList<Order> GetOrderFromDBByExample(Order exampleOrder)
        {
            var example = Example.Create(exampleOrder);
            return session.CreateCriteria<Order>().Add(example).List<Order>();
        }
    }
}
