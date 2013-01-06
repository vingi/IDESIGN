using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class TopChoose
    {
        ISession session;
        public TopChoose()
        {
            session = NHibernateHelper.GetSession();
        }

        public void Update(ID_TopChooseData data)
        {
            session.Update(data);
            session.Flush();
        }

        public ID_TopChooseData GetSingledataById(int Dc_Id)
        {
            var model = session.Get<ID_TopChooseData>(Dc_Id);
            if (model == null)
                model = new ID_TopChooseData();
            return model;
        }

        public IList<ID_TopChooseData> GetAllModel()
        {
            ICriteria crt = session.CreateCriteria(typeof(ID_TopChooseData));
            crt.AddOrder(new NHibernate.Criterion.Order("Tc_place", true));
            return crt.List<ID_TopChooseData>();
        }
    }
}
