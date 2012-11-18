using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class FurnitureType
    {
        ISession session;
        public FurnitureType()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public ID_FurnitureType GetAdminById(int Ft_Id)
        {
            return session.Get<ID_FurnitureType>(Ft_Id);
        }

        public IList<ID_FurnitureType> GetAllModel()
        {
            //ICriteria crt = session.CreateCriteria(typeof(ID_FurnitureType));
            //crt.AddOrder(new NHibernate.Criterion.Order("Ft_orderby", true));
            //return crt.List<ID_FurnitureType>();
            return session.CreateQuery("from ID_FurnitureType as ft order by ft.Ft_orderby")
            .List<ID_FurnitureType>();
        }
    }
}
