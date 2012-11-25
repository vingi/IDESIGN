using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class Designer
    {
                ISession session;
                public Designer()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public void Add(ID_DContentData data)
        {
            session.Save(data);
            session.Flush();
        }

        public void Update(ID_DContentData data)
        {
            session.Update(data);
            session.Flush();
        }

        public void SaveOrUpdate(ID_DContentData data)
        {
            session.SaveOrUpdate(data);
            session.Flush();
        }

        public int GetCount() {
            int count = 0;
            count = session.CreateSQLQuery("select count(0) from ID_DContentData where Dc_display=:st")
                .SetBoolean("st", true).UniqueResult<int>();
            return count;
        }

        public ID_DContentData GetSingledataById(int Dc_Id)
        {
            var model = session.Get<ID_DContentData>(Dc_Id);
            if (model == null)
                model = new ID_DContentData();
            return model;
        }

        public IList<ID_DContentData> GetModel(int pagenum)
        {
            int pagestep = 10;
            return session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st order by ns.Dc_createdate desc")
                .SetBoolean("st", true)
                .SetFirstResult((pagenum - 1) * pagestep)
                .SetMaxResults(pagenum * pagestep)
                .List<ID_DContentData>();
        }

    }
}
