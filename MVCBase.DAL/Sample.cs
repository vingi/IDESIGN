using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class Sample
    {
        ISession session;
        public Sample()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public void CreateCustomer(Customer customer)
        {
            //Configuration cfg = new Configuration().Configure(path);
            //ISession session = (new NHibernateHelper()).GetSession();
            session.Save(customer);
            session.Flush();
        }

        public void UpdateCustomer(Customer customer)
        {
            //ISession session = (new NHibernateHelper()).GetSession();
            session.SaveOrUpdate(customer);
            session.Flush();
        }
        public Customer GetCustomerById(int customerId)
        {
            //Configuration cfg = new Configuration().Configure(path);
            //ISession session = cfg.BuildSessionFactory().OpenSession();
            return session.Get<Customer>(customerId);
        }
    }
}
