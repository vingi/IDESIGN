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
            session = NHibernateHelper.GetSession();
        }

        public Fr_Type GetAdminById(int Ft_Id)
        {
            return session.Get<Fr_Type>(Ft_Id);
        }

        public IList<Fr_Type> GetAllModel()
        {
            //ICriteria crt = session.CreateCriteria(typeof(ID_FurnitureType));
            //crt.AddOrder(new NHibernate.Criterion.Order("Ft_orderby", true));
            //return crt.List<ID_FurnitureType>();
            return session.CreateQuery("from Fr_Type as ft order by ft.Fr_ListOrder")
            .List<Fr_Type>();
        }
    }
}
