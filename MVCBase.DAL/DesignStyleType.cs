using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;

namespace MVCBase.DAL
{
    public class DesignStyleType
    {
        ISession session;
        public DesignStyleType()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public ID_DesignStyleType GetAdminById(int St_Id)
        {
            return session.Get<ID_DesignStyleType>(St_Id);
        }

        public IList<ID_DesignStyleType> GetAllModel()
        {
            return session.CreateQuery("from ID_DesignStyleType as ds order by ds.St_orderby")
                .List<ID_DesignStyleType>();
        }
    }
}
