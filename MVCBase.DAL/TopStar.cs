using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class TopStar
    {
        ISession session;
        public TopStar()
        {
            session = NHibernateHelper.GetSession();
        }

        public void Update(ID_TopStarData data)
        {
            session.Update(data);
            session.Flush();
        }

        public ID_TopStarData GetSingledataById(int Dc_Id)
        {
            var model = session.Get<ID_TopStarData>(Dc_Id);
            if (model == null)
                model = new ID_TopStarData();
            return model;
        }

        public IList<ID_TopStarData> GetAllModel()
        {
            ICriteria crt = session.CreateCriteria(typeof(ID_TopStarData));
            crt.AddOrder(new NHibernate.Criterion.Order("Ts_place", true));
            return crt.List<ID_TopStarData>();
        }
    }
}
