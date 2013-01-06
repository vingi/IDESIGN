using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;
using System;
using System.Collections.Generic;

namespace MVCBase.DAL
{
    public class NewsType
    {
        ISession session;
        public NewsType()
        {
            session = NHibernateHelper.GetSession();
        }

        public IList<Ba_NewsType> GetList()
        {
            return session.CreateQuery("from Ba_NewsType as ns order by ns.Nt_ListOrder")
                .List<Ba_NewsType>();
        }
    }
}
