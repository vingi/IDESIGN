using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class TopFrChoose
    {
        ISession session;
        public TopFrChoose()
        {
            session = (new NHibernateHelper()).GetSession();
        }
        public void Update(Fr_TopChoose data)
        {
            session.Update(data);
            session.Flush();
        }

        public Fr_TopChoose GetSingledataById(int Dc_Id)
        {
            var model = session.Get<Fr_TopChoose>(Dc_Id);
            if (model == null)
                model = new Fr_TopChoose();
            return model;
        }

        public IList<Fr_TopChoose> GetAllModel()
        {
            ICriteria crt = session.CreateCriteria(typeof(Fr_TopChoose));
            crt.AddOrder(new NHibernate.Criterion.Order("Tp_place", true));
            return crt.List<Fr_TopChoose>();
        }
    }
}
