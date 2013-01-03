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

        public void Save(Ba_News news)
        {
            session.SaveOrUpdate(news);
            session.Flush();
        }

        public Ba_News GetSingleById(int Ns_ID)
        {
            var model = session.Get<Ba_News>(Ns_ID);
            if (model == null)
                model = new Ba_News();
            return model;
        }

        public IList<Ba_News> GetNews(int pagenum)
        {
            int pagestep = 18;
            return session.CreateQuery("from Ba_News as ns where ns.Ns_State=:st order by ns.Ns_PostTime desc")
                .SetBoolean("st", true)
                .SetFirstResult((pagenum - 1) * pagestep)
                .SetMaxResults(pagenum * pagestep)
                .List<Ba_News>();
        }

        public IList<Ba_News> GetNews(int newstype,int pagenum)
        {
            //return session.CreateQuery("from Ba_News as ns where ns.Ns_State=:st")
            //    .SetBoolean("st", true).List<Ba_News>();

            int pagestep = 5;
            return session.CreateQuery("from Ba_News as ns where ns.Ns_State=:st and ns.Ns_Type=:ty order by ns.Ns_PostTime desc")
                .SetBoolean("st", true)
                .SetInt32("ty", newstype)
                .SetFirstResult((pagenum - 1) * pagestep)
                .SetMaxResults(pagenum * pagestep)
                .List<Ba_News>();
        }

        public int GetCount()
        {
            int count = 0;
            count = session.CreateSQLQuery("select count(0) from Ba_News where Ns_State=:st")
                .SetBoolean("st", true).UniqueResult<int>();
            return count;
        }

        public int GetCount(int newstype)
        {
            int count = 0;
            count = session.CreateSQLQuery("select count(0) from Ba_News where Ns_Type=:ty and Ns_State=:st")
                .SetInt32("ty", newstype).SetBoolean("st", true).UniqueResult<int>();
            return count;
        }


        //public IList<Ba_Admin> GetModel(string adminname, string adminpwd)
        //{
        //    //session.Get<Ba_Admin>()
        //    return session.CreateQuery("from Ba_Admin as ad where ad.Ad_AdminName=:an and ad.Ad_AdminPwd=:ap")
        //        .SetString("an", adminname).SetString("ap", adminpwd).List<Ba_Admin>();
        //}
    }
}
