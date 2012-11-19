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

        public ID_DContentData GetSingledataById(int Dc_Id)
        {
            var model = session.Get<ID_DContentData>(Dc_Id);
            if (model == null)
                model = new ID_DContentData();
            return model;
        }

    }
}
