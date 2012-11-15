using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class Admin
    {
        ISession session;
        public Admin()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public void CreateAdmin(Ba_Admin admin)
        {
            session.Save(admin);
            session.Flush();
        }

        public Ba_Admin GetAdminById(int Ad_ID)
        {
            //Configuration cfg = new Configuration().Configure(path);
            //ISession session = cfg.BuildSessionFactory().OpenSession();
            return session.Get<Ba_Admin>(Ad_ID);
        }

        public void Update(Ba_Admin admin)
        {
            session.Update(admin);
            session.Flush();
        }

        public IList<Ba_Admin> GetModel(string adminname, string adminpwd)
        {
            //session.Get<Ba_Admin>()
            return session.CreateQuery("from Ba_Admin as ad where ad.Ad_AdminName=:an and ad.Ad_AdminPwd=:ap")
                .SetString("an", adminname).SetString("ap", adminpwd).List<Ba_Admin>();
        }
    }
}
