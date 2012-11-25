using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class TopPopular
    {
        ISession session;
        public TopPopular()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public void Update(ID_TopPopularData data)
        {
            session.Update(data);
            session.Flush();
        }

        public ID_TopPopularData GetSingledataById(int Dc_Id)
        {
            var model = session.Get<ID_TopPopularData>(Dc_Id);
            if (model == null)
                model = new ID_TopPopularData();
            return model;
        }

        public IList<ID_TopPopularData> GetAllModel()
        {
            ICriteria crt = session.CreateCriteria(typeof(ID_TopPopularData));
            crt.AddOrder(new NHibernate.Criterion.Order("Tp_place", true));
            return crt.List<ID_TopPopularData>();
        }
    }
}
