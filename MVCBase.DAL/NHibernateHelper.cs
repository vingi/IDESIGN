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
        private static ISessionFactory _sessionFactory;
        private static string path = System.Web.HttpContext.Current.Server.MapPath("~/hibernate.cfg.xml");
        static NHibernateHelper()
        {
            _sessionFactory = GetSessionFactory();
        }
        private static ISessionFactory GetSessionFactory()
        {
            return (new Configuration()).Configure(path).BuildSessionFactory();
        }
        public static ISession GetSession()
        {
            //Configuration cfg = new Configuration().Configure(path);
            //ISession session = cfg.BuildSessionFactory().OpenSession();
            return GetSessionFactory().OpenSession();
        }
    }
}
