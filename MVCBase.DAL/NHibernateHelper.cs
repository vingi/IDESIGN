using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace MVCBase.DAL
{
    class NHibernateHelper
    {
        private ISessionFactory _sessionFactory;
        private string path = System.Web.HttpContext.Current.Server.MapPath("~/hibernate.cfg.xml");
        public NHibernateHelper()
        {
            _sessionFactory = GetSessionFactory();
        }
        private ISessionFactory GetSessionFactory()
        {
            return (new Configuration()).Configure(path).BuildSessionFactory();
        }
        public ISession GetSession()
        {
            Configuration cfg = new Configuration().Configure(path);
            ISession session = cfg.BuildSessionFactory().OpenSession();
            return session;
        }
    }
}
