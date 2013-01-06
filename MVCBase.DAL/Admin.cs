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
            session = NHibernateHelper.GetSession();
        }

        public void CreateAdmin(ID_Admin admin)
        {
            session.Save(admin);
            session.Flush();
        }

        public ID_Admin GetAdminById(int Ad_ID)
        {
            //Configuration cfg = new Configuration().Configure(path);
            //ISession session = cfg.BuildSessionFactory().OpenSession();
            return session.Get<ID_Admin>(Ad_ID);
        }

        public void Update(ID_Admin admin)
        {
            session.Update(admin);
            session.Flush();
        }

        public IList<ID_Admin> GetModel(string adminname, string adminpwd)
        {
            //session.Get<ID_Admin>()
            return session.CreateQuery("from ID_Admin as ad where ad.Ad_AdminName=:an and ad.Ad_AdminPwd=:ap")
                .SetString("an", adminname).SetString("ap", adminpwd).List<ID_Admin>();
        }
    }
}
