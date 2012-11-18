using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class DesignType
    {
        ISession session;
        public DesignType()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public DesignType GetAdminById(int Dt_Id)
        {
            return session.Get<DesignType>(Dt_Id);
        }

        public IList<DesignType> GetAllModel()
        {
            return session.CreateQuery("from ID_DesignType as dt order by dt.Dt_orderby")
                .List<DesignType>();
        }
    }
}
