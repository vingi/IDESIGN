using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVCBase.DAL
{
    public class News
    {
        ISession session;
        public News()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public void Add(Ba_News news)
        {
            session.Save(news);
            session.Flush();
        }

        public void Update(Ba_News news)
        {
            session.Update(news);
            session.Flush();
        }

        public void SaveOrUpdate(Ba_News news)
        {
            session.SaveOrUpdate(news);
            session.Flush();
        }

        public Ba_News GetSingleNewsById(int Ns_ID)
        {
            return session.Get<Ba_News>(Ns_ID);
        }

        public IList<Ba_News> GetNews()
        {
            return session.CreateQuery("from Ba_News as ns where ns.Ns_State=:st")
                .SetBoolean("st", true).List<Ba_News>();
        }

        //public IList<Ba_Admin> GetModel(string adminname, string adminpwd)
        //{
        //    //session.Get<Ba_Admin>()
        //    return session.CreateQuery("from Ba_Admin as ad where ad.Ad_AdminName=:an and ad.Ad_AdminPwd=:ap")
        //        .SetString("an", adminname).SetString("ap", adminpwd).List<Ba_Admin>();
        //}
    }
}
