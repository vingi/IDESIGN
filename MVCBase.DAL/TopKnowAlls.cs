using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class TopKnowAlls
    {
        ISession session;
        public TopKnowAlls()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public void Update(Fr_TopKnowAlls data)
        {
            session.Update(data);
            session.Flush();
        }

        public Fr_TopKnowAlls GetSingledataById(int Dc_Id)
        {
            var model = session.Get<Fr_TopKnowAlls>(Dc_Id);
            if (model == null)
                model = new Fr_TopKnowAlls();
            return model;
        }

        public IList<Fr_TopKnowAlls> GetAllModel()
        {
            ICriteria crt = session.CreateCriteria(typeof(Fr_TopKnowAlls));
            crt.AddOrder(new NHibernate.Criterion.Order("Tp_place", true));
            return crt.List<Fr_TopKnowAlls>();
        }
    }
}
