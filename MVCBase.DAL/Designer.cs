﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;
using System.Collections;

namespace MVCBase.DAL
{
    public class Designer
    {
        private int pagestep = 18;

        ISession session;
        public Designer()
        {
            session = NHibernateHelper.GetSession();
        }

        public void Add(ID_DContentData data)
        {
            session.Save(data);
            session.Flush();
        }

        public void Update(ID_DContentData data)
        {
            session.Update(data);
            session.Flush();
        }

        public void SaveOrUpdate(ID_DContentData data)
        {
            session.SaveOrUpdate(data);
            session.Flush();
        }

        public int GetCount()
        {
            int count = 0;
            count = session.CreateSQLQuery("select count(0) from ID_DContentData where Dc_display=:st")
                .SetBoolean("st", true).UniqueResult<int>();
            return count;
        }

        public int GetCount(DesignerListQuery query) {
            string tsql = "select count(0) from ID_DContentData where Dc_display=:st ";
            switch (query.DeesignerType)
            {
                case "popular":
                    tsql += " and Dc_pdesign=1 ";
                    break;
                case "star":
                    tsql += " and Dc_ndesign=1 ";
                    break;
                case "choose":
                    tsql += " and Dc_sdesign=1 ";
                    break;
                default:
                    tsql += " and Dc_pdesign=1 ";
                    break;
            }

            if (!query.Housetype.Equals(0))
                tsql += " and Dc_htype like '%," + query.Housetype.ToString() + ",%' ";
            if (!query.Designtype.Equals(0))
                tsql += " and Dc_dtype like '%," + query.Designtype.ToString() + ",%' ";
            if (!query.Designstyletype.Equals(0))
                tsql += " and Dc_stype like '%," + query.Designstyletype.ToString() + ",%' ";

            return session.CreateSQLQuery(tsql)
                .SetBoolean("st", true).UniqueResult<int>();
        }

        public ID_DContentData GetSingledataById(int Dc_Id)
        {
            var model = session.Get<ID_DContentData>(Dc_Id);
            if (model == null)
                model = new ID_DContentData();
            return model;
        }

        public IList<ID_DContentData> GetModel(int pagenum)
        {
            int pagestep = 18;
            return session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st order by ns.Dc_createdate desc")
                .SetBoolean("st", true)
                .SetFirstResult((pagenum - 1) * pagestep)
                .SetMaxResults(pagenum * pagestep)
                .List<ID_DContentData>();
        }

        public ID_DContentData Prev(int Dc_Id)
        {
            IList<ID_DContentData> data = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_Id < :did order by ns.Dc_Id desc")
                .SetBoolean("st", true)
                .SetInt32("did", Dc_Id)
                .SetMaxResults(1)
                .List<ID_DContentData>();
            return data.FirstOrDefault();
        }

        public ID_DContentData Next(int Dc_Id)
        {
            IList<ID_DContentData> data = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_Id > :did order by ns.Dc_Id asc")
                .SetBoolean("st", true)
                .SetInt32("did", Dc_Id)
                .SetMaxResults(1)
                .List<ID_DContentData>();
            return data.FirstOrDefault();
        }

        public IList<ID_DContentData> GetListBasic(DesignerListQuery query)
        {
            string tsql = "from ID_DContentData as ns where ns.Dc_display=:st ";
            switch (query.DeesignerType.ToLower())
            {
                case "popular":
                    tsql += " and Dc_pdesign=1 ";
                    break;
                case "star":
                    tsql += " and Dc_ndesign=1 ";
                    break;
                case "choose":
                    tsql += " and Dc_sdesign=1 ";
                    break;
            }
            if (!query.Housetype.Equals(0))
                tsql += " and Dc_htype like '%," + query.Housetype.ToString() + ",%' ";
            if (!query.Designtype.Equals(0))
                tsql += " and Dc_dtype like '%," + query.Designtype.ToString() + ",%' ";
            if (!query.Designstyletype.Equals(0))
                tsql += " and Dc_stype like '%," + query.Designstyletype.ToString() + ",%' ";

            IList<ID_DContentData> data = session.CreateQuery(tsql)
                .SetBoolean("st", true)
                .SetFirstResult((query.Pageindex - 1) * pagestep)
                .SetMaxResults(query.Pageindex * pagestep)
                .List<ID_DContentData>();

            return data;
        }

        public IList<ID_DContentData> GetPopularList(DesignerListQuery query)
        {
            if (!(query.Housetype.Equals(0) && query.Designtype.Equals(0) && query.Designstyletype.Equals(0)))
                //basic mode
                return GetListBasic(query);

            // special mode
            ICriteria crt = session.CreateCriteria(typeof(ID_TopPopularData));
            crt.AddOrder(new NHibernate.Criterion.Order("Tp_place", true));
            IList<ID_TopPopularData> pop = crt.List<ID_TopPopularData>();
            ArrayList ar = new ArrayList();
            Dictionary<int, int> di = new Dictionary<int, int>();
            foreach (var item in pop)
            {
                if (item.Tp_DcID_FK.HasValue)
                {
                    ar.Add(item.Tp_DcID_FK.Value.ToString());
                    di.Add(item.Tp_place, item.Tp_DcID_FK.Value);
                }
            }
            string specialid = string.Join(",", ar.ToArray());

            // specialid 为空时...正常查询
            IList<ID_DContentData> data = new List<ID_DContentData>();

            // specialid 有指定位置时
            if (specialid.Length > 0)
            {
                var commonquery = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_pdesign=:st and ns.Dc_Id not in (" + specialid + ")")
                    .SetBoolean("st", true);
                //commonquery.SetString("specialid", specialid);
                if (((query.Pageindex - 1) * pagestep) - ar.Count > -1)
                    commonquery.SetFirstResult((query.Pageindex - 1) * pagestep - ar.Count);
                else
                    commonquery.SetFirstResult(0);
                commonquery.SetMaxResults(query.Pageindex * pagestep - ar.Count);
                var commonlist = commonquery.List<ID_DContentData>();

                var commlist_index = 0;
                if (query.Pageindex.Equals(1))
                {
                    var speciallist = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_pdesign=:st and ns.Dc_Id in (" + specialid + ")")
                        .SetBoolean("st", true)
                        //.SetString("specialid", specialid)
                        .List<ID_DContentData>();

                    for (int i = 1; i < pagestep + 1; i++)
                    {
                        if (di.ContainsKey(i))
                        {
                            var te = speciallist.Where(it => it.Dc_Id.Equals(di[i]));
                            if (te.Any())
                                data.Add(te.Single());
                        }
                        else
                        {
                            if (commonlist.Count > commlist_index)
                            {
                                data.Add(commonlist[commlist_index]);
                                commlist_index++;
                            }
                        }
                    }
                }
                else
                {
                    data = commonquery.List<ID_DContentData>();
                }
            }
            else
            {
                data = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_pdesign=:st")
                .SetBoolean("st", true)
                .SetFirstResult((query.Pageindex - 1) * pagestep)
                .SetMaxResults(query.Pageindex * pagestep)
                .List<ID_DContentData>();
            }
            return data;
        }

        public IList<ID_DContentData> GetStarList(DesignerListQuery query)
        {
            if (!(query.Housetype.Equals(0) && query.Designtype.Equals(0) && query.Designstyletype.Equals(0)))
                //basic mode
                return GetListBasic(query);

            // special mode
            ICriteria crt = session.CreateCriteria(typeof(ID_TopStarData));
            crt.AddOrder(new NHibernate.Criterion.Order("Ts_place", true));
            IList<ID_TopStarData> pop = crt.List<ID_TopStarData>();
            ArrayList ar = new ArrayList();
            Dictionary<int, int> di = new Dictionary<int, int>();
            foreach (var item in pop)
            {
                if (item.Ts_DcID_FK.HasValue)
                {
                    ar.Add(item.Ts_DcID_FK.Value.ToString());
                    di.Add(item.Ts_place, item.Ts_DcID_FK.Value);
                }
            }
            string specialid = string.Join(",", ar.ToArray());

            // specialid 为空时...正常查询
            IList<ID_DContentData> data = new List<ID_DContentData>();

            // specialid 有指定位置时
            if (specialid.Length > 0)
            {
                data.Clear();
                var commonquery = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_ndesign=:st and ns.Dc_Id not in (" + specialid + ")")
                    .SetBoolean("st", true);
                //commonquery.SetString("specialid", specialid);
                if (((query.Pageindex - 1) * pagestep) - ar.Count > -1)
                    commonquery.SetFirstResult((query.Pageindex - 1) * pagestep - ar.Count);
                else
                    commonquery.SetFirstResult(0);
                commonquery.SetMaxResults(query.Pageindex * pagestep - ar.Count);
                var commonlist = commonquery.List<ID_DContentData>();

                var commlist_index = 0;
                if (query.Pageindex.Equals(1))
                {
                    var speciallist = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_ndesign=:st and ns.Dc_Id in (" + specialid + ")")
                        .SetBoolean("st", true)
                        //.SetString("specialid", specialid)
                        .List<ID_DContentData>();

                    for (int i = 1; i < pagestep + 1; i++)
                    {
                        if (di.ContainsKey(i))
                        {
                            var te = speciallist.Where(it => it.Dc_Id.Equals(di[i]));
                            if (te.Any())
                                data.Add(te.Single());
                        }
                        else
                        {
                            if (commonlist.Count > commlist_index)
                            {
                                data.Add(commonlist[commlist_index]);
                                commlist_index++;
                            }
                        }
                    }
                }
                else
                {
                    data = commonquery.List<ID_DContentData>();
                }
            }
            else
            {
                data = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_ndesign=:st")
                .SetBoolean("st", true)
                .SetFirstResult((query.Pageindex - 1) * pagestep)
                .SetMaxResults(query.Pageindex * pagestep)
                .List<ID_DContentData>();
            }
            return data;
        }

        public IList<ID_DContentData> GetChooseList(DesignerListQuery query)
        {
            if (!(query.Housetype.Equals(0) && query.Designtype.Equals(0) && query.Designstyletype.Equals(0)))
                //basic mode
                return GetListBasic(query);

            // special mode
            ICriteria crt = session.CreateCriteria(typeof(ID_TopChooseData));
            crt.AddOrder(new NHibernate.Criterion.Order("Tc_place", true));
            IList<ID_TopChooseData> pop = crt.List<ID_TopChooseData>();
            ArrayList ar = new ArrayList();
            Dictionary<int, int> di = new Dictionary<int, int>();
            foreach (var item in pop)
            {
                if (item.Tc_DcID_FK.HasValue)
                {
                    ar.Add(item.Tc_DcID_FK.Value.ToString());
                    di.Add(item.Tc_place, item.Tc_DcID_FK.Value);
                }
            }
            string specialid = string.Join(",", ar.ToArray());

            // specialid 为空时...正常查询
            IList<ID_DContentData> data = new List<ID_DContentData>();

            // specialid 有指定位置时
            if (specialid.Length > 0)
            {
                var commonquery = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_sdesign=:st and ns.Dc_Id not in (" + specialid + ")")
                    .SetBoolean("st", true);
                //commonquery.SetString("specialid", specialid);
                if (((query.Pageindex - 1) * pagestep) - ar.Count > -1)
                    commonquery.SetFirstResult((query.Pageindex - 1) * pagestep - ar.Count);
                else
                    commonquery.SetFirstResult(0);
                commonquery.SetMaxResults(query.Pageindex * pagestep - ar.Count);
                var commonlist = commonquery.List<ID_DContentData>();

                var commlist_index = 0;
                if (query.Pageindex.Equals(1))
                {
                    var speciallist = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_sdesign=:st and ns.Dc_Id in (" + specialid + ")")
                        .SetBoolean("st", true)
                        //.SetString("specialid", specialid)
                        .List<ID_DContentData>();

                    for (int i = 1; i < pagestep + 1; i++)
                    {
                        if (di.ContainsKey(i))
                        {
                            var te = speciallist.Where(it => it.Dc_Id.Equals(di[i]));
                            if (te.Any()) 
                                data.Add(te.Single());
                        }
                        else
                        {
                            if (commonlist.Count > commlist_index)
                            {
                                data.Add(commonlist[commlist_index]);
                                commlist_index++;
                            }
                        }
                    }
                }
                else
                {
                    data = commonquery.List<ID_DContentData>();
                }
            }
            else
            {
                data = session.CreateQuery("from ID_DContentData as ns where ns.Dc_display=:st and ns.Dc_sdesign=:st")
                .SetBoolean("st", true)
                .SetFirstResult((query.Pageindex - 1) * pagestep)
                .SetMaxResults(query.Pageindex * pagestep)
                .List<ID_DContentData>();
            }
            return data;
        }

        public IList<ID_DContentData> GetList(DesignerListQuery query)
        {
            IList<ID_DContentData> model = new List<ID_DContentData>();
            if (query.DeesignerType == null){
                model = this.GetPopularList(query);
                return model;
            }
            switch (query.DeesignerType.ToLower())
            {
                case "popular":
                    model = this.GetPopularList(query);
                    break;
                case "star":
                    model = this.GetStarList(query);
                    break;
                case "choose":
                    model = this.GetChooseList(query);
                    break;
            }
            return model;
        }
    }

    public class DesignerListQuery
    {
        public int Pageindex { get; set; }
        public string DeesignerType { get; set; }
        public string Type { get; set; }
        public int Housetype { get; set; }
        public int Designtype { get; set; }
        public int Designstyletype { get; set; }
    }
}
