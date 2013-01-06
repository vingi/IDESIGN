using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class HouseType
    {
                ISession session;
                public HouseType()
        {
            session = NHibernateHelper.GetSession();
        }

                public ID_HouseType GetAdminById(int Ht_Id)
        {
            return session.Get<ID_HouseType>(Ht_Id);
        }

                public IList<ID_HouseType> GetAllModel()
        {
            //ICriteria crt = session.CreateCriteria(typeof(ID_FurnitureType));
            //crt.AddOrder(new NHibernate.Criterion.Order("Ft_orderby", true));
            //return crt.List<ID_FurnitureType>();
            return session.CreateQuery("from ID_HouseType as ht order by ht.Ht_orderby")
            .List<ID_HouseType>();
        }
    }
}
