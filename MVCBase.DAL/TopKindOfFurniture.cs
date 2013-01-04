using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class TopKindOfFurniture
    {
        ISession session;
        public TopKindOfFurniture()
        {
            session = (new NHibernateHelper()).GetSession();
        }
        public void Update(Fr_TopKindOfFurniture data)
        {
            session.Update(data);
            session.Flush();
        }

        public Fr_TopKindOfFurniture GetSingledataById(int Dc_Id)
        {
            var model = session.Get<Fr_TopKindOfFurniture>(Dc_Id);
            if (model == null)
                model = new Fr_TopKindOfFurniture();
            return model;
        }

        public IList<Fr_TopKindOfFurniture> GetAllModel()
        {
            ICriteria crt = session.CreateCriteria(typeof(Fr_TopKindOfFurniture));
            crt.AddOrder(new NHibernate.Criterion.Order("Tp_place", true));
            return crt.List<Fr_TopKindOfFurniture>();
        }
    }
}
