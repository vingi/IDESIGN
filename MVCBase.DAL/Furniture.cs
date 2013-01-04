using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class Furniture
    {
        ISession session;
        public Furniture()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public void Add(Fr_ContentData data)
        {
            session.Save(data);
            session.Flush();
        }

        public void Update(Fr_ContentData data)
        {
            session.Update(data);
            session.Flush();
        }

        public void Save(Fr_ContentData data)
        {
            session.SaveOrUpdate(data);
            session.Flush();
        }

        public int GetCount()
        {
            int count = 0;
            count = session.CreateSQLQuery("select count(0) from Fr_ContentData where Fc_display=:st")
                .SetBoolean("st", true).UniqueResult<int>();
            return count;
        }

        public Fr_ContentData GetSingleById(int Fc_ID)
        {
            var model = session.Get<Fr_ContentData>(Fc_ID);
            if (model == null)
                model = new Fr_ContentData();
            return model;
        }

        public IList<Fr_ContentData> GetModel(int pagenum)
        {
            int pagestep = 18;
            return session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st order by ns.Fc_createtime desc")
                .SetBoolean("st", true)
                .SetFirstResult((pagenum - 1) * pagestep)
                .SetMaxResults(pagenum * pagestep)
                .List<Fr_ContentData>();
        }

        public Fr_ContentData Prev(int Fc_ID)
        {
            IList<Fr_ContentData> data = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_ID < :did order by ns.Fc_ID desc")
                .SetBoolean("st", true)
                .SetInt32("did", Fc_ID)
                .SetMaxResults(1)
                .List<Fr_ContentData>();
            return data.FirstOrDefault();
        }

        public Fr_ContentData Next(int Fc_ID)
        {
            IList<Fr_ContentData> data = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_ID > :did order by ns.Fc_ID asc")
                .SetBoolean("st", true)
                .SetInt32("did", Fc_ID)
                .SetMaxResults(1)
                .List<Fr_ContentData>();
            return data.FirstOrDefault();
        }
    }

    public class FurnitureListQuery
    {
        public int Pageindex { get; set; }
        public string FurnitureType { get; set; }
        public int Type { get; set; }
    }
}
