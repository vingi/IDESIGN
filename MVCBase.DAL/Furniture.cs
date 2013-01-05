using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using MVCBase.Domain.Entity;
using System.Collections;

namespace MVCBase.DAL
{
    public class Furniture
    {
        private int pagestep = 18;
        ISession session;
        public Furniture()
        {
            session = (new NHibernateHelper()).GetSession();
        }

        public void Add(Fr_ContentData data)
        {
            session.Save(data);
            session.Flush();
        }

        public void Update(Fr_ContentData data)
        {
            session.Update(data);
            session.Flush();
        }

        public void Save(Fr_ContentData data)
        {
            session.SaveOrUpdate(data);
            session.Flush();
        }

        public int GetCount()
        {
            int count = 0;
            count = session.CreateSQLQuery("select count(0) from Fr_ContentData where Fc_display=:st")
                .SetBoolean("st", true).UniqueResult<int>();
            return count;
        }

        public int GetCount(FurnitureListQuery query)
        {
            string tsql = "select count(0) from Fr_ContentData where Fc_display=:st ";
            switch (query.FurnitureType.ToLower())
            {
                case "knowalls":
                    tsql += " and Fc_pdesign=1 ";
                    break;
                case "kindoffurniture":
                    tsql += " and Fc_ndesign=1 ";
                    break;
                case "frchoose":
                    tsql += " and Fc_sdesign=1 ";
                    break;
            }
            if (!query.Type.Equals(0))
                tsql += " and Fc_type_FK = " + query.Type.ToString() + " ";

            return session.CreateSQLQuery(tsql)
                .SetBoolean("st", true).UniqueResult<int>();
        }

        public Fr_ContentData GetSingleById(int Fc_ID)
        {
            var model = session.Get<Fr_ContentData>(Fc_ID);
            if (model == null)
                model = new Fr_ContentData();
            return model;
        }

        public IList<Fr_ContentData> GetModel(int pagenum)
        {
            int pagestep = 18;
            return session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st order by ns.Fc_createtime desc")
                .SetBoolean("st", true)
                .SetFirstResult((pagenum - 1) * pagestep)
                .SetMaxResults(pagenum * pagestep)
                .List<Fr_ContentData>();
        }

        public Fr_ContentData Prev(int Fc_ID)
        {
            IList<Fr_ContentData> data = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_ID < :did order by ns.Fc_ID desc")
                .SetBoolean("st", true)
                .SetInt32("did", Fc_ID)
                .SetMaxResults(1)
                .List<Fr_ContentData>();
            return data.FirstOrDefault();
        }

        public Fr_ContentData Next(int Fc_ID)
        {
            IList<Fr_ContentData> data = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_ID > :did order by ns.Fc_ID asc")
                .SetBoolean("st", true)
                .SetInt32("did", Fc_ID)
                .SetMaxResults(1)
                .List<Fr_ContentData>();
            return data.FirstOrDefault();
        }


        public IList<Fr_ContentData> GetListBasic(FurnitureListQuery query)
        {
            string tsql = "from Fr_ContentData as ns where ns.Fc_display=:st ";
            switch (query.FurnitureType.ToLower())
            {
                case "knowalls":
                    tsql += " and Fc_pdesign=1 ";
                    break;
                case "kindoffurniture":
                    tsql += " and Fc_ndesign=1 ";
                    break;
                case "frchoose":
                    tsql += " and Fc_sdesign=1 ";
                    break;
            }
            if (!query.Type.Equals(0))
                tsql += " and Fc_type_FK = " + query.Type.ToString() + " ";

            IList<Fr_ContentData> data = session.CreateQuery(tsql)
                .SetBoolean("st", true)
                .SetFirstResult((query.Pageindex - 1) * pagestep)
                .SetMaxResults(query.Pageindex * pagestep)
                .List<Fr_ContentData>();

            return data;
        }

        public IList<Fr_ContentData> GetKnowAllsList(FurnitureListQuery query)
        {
            if (!(query.FurnitureType.Equals(0)))
                //basic mode
                return GetListBasic(query);

            // special mode
            ICriteria crt = session.CreateCriteria(typeof(Fr_TopKnowAlls));
            crt.AddOrder(new NHibernate.Criterion.Order("Tp_place", true));
            IList<Fr_TopKnowAlls> pop = crt.List<Fr_TopKnowAlls>();
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
            IList<Fr_ContentData> data = new List<Fr_ContentData>();

            // specialid 有指定位置时
            if (specialid.Length > 0)
            {
                var commonquery = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_pdesign=:st and ns.Fc_ID not in (" + specialid + ")")
                    .SetBoolean("st", true);
                //commonquery.SetString("specialid", specialid);
                if (((query.Pageindex - 1) * pagestep) - ar.Count > -1)
                    commonquery.SetFirstResult((query.Pageindex - 1) * pagestep - ar.Count);
                else
                    commonquery.SetFirstResult(0);
                commonquery.SetMaxResults(query.Pageindex * pagestep - ar.Count);
                var commonlist = commonquery.List<Fr_ContentData>();

                var commlist_index = 0;
                if (query.Pageindex.Equals(1))
                {
                    var speciallist = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_pdesign=:st and ns.Fc_ID in (" + specialid + ")")
                        .SetBoolean("st", true)
                        //.SetString("specialid", specialid)
                        .List<Fr_ContentData>();

                    for (int i = 1; i < pagestep + 1; i++)
                    {
                        if (di.ContainsKey(i))
                        {
                            var te = speciallist.Where(it => it.Fc_ID.Equals(di[i]));
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
                    data = commonquery.List<Fr_ContentData>();
                }
            }
            else
            {
                data = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_pdesign=:st")
                .SetBoolean("st", true)
                .SetFirstResult((query.Pageindex - 1) * pagestep)
                .SetMaxResults(query.Pageindex * pagestep)
                .List<Fr_ContentData>();
            }
            return data;
        }

        public IList<Fr_ContentData> GetKindOfFurnitureList(FurnitureListQuery query)
        {
            if (!(query.FurnitureType.Equals(0)))
                //basic mode
                return GetListBasic(query);

            // special mode
            ICriteria crt = session.CreateCriteria(typeof(Fr_TopKindOfFurniture));
            crt.AddOrder(new NHibernate.Criterion.Order("Tp_place", true));
            IList<Fr_TopKindOfFurniture> pop = crt.List<Fr_TopKindOfFurniture>();
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
            IList<Fr_ContentData> data = new List<Fr_ContentData>();

            // specialid 有指定位置时
            if (specialid.Length > 0)
            {
                data.Clear();
                var commonquery = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_ndesign=:st and ns.Fc_ID not in (" + specialid + ")")
                    .SetBoolean("st", true);
                //commonquery.SetString("specialid", specialid);
                if (((query.Pageindex - 1) * pagestep) - ar.Count > -1)
                    commonquery.SetFirstResult((query.Pageindex - 1) * pagestep - ar.Count);
                else
                    commonquery.SetFirstResult(0);
                commonquery.SetMaxResults(query.Pageindex * pagestep - ar.Count);
                var commonlist = commonquery.List<Fr_ContentData>();

                var commlist_index = 0;
                if (query.Pageindex.Equals(1))
                {
                    var speciallist = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_ndesign=:st and ns.Fc_ID in (" + specialid + ")")
                        .SetBoolean("st", true)
                        //.SetString("specialid", specialid)
                        .List<Fr_ContentData>();

                    for (int i = 1; i < pagestep + 1; i++)
                    {
                        if (di.ContainsKey(i))
                        {
                            var te = speciallist.Where(it => it.Fc_ID.Equals(di[i]));
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
                    data = commonquery.List<Fr_ContentData>();
                }
            }
            else
            {
                data = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_ndesign=:st")
                .SetBoolean("st", true)
                .SetFirstResult((query.Pageindex - 1) * pagestep)
                .SetMaxResults(query.Pageindex * pagestep)
                .List<Fr_ContentData>();
            }
            return data;
        }

        public IList<Fr_ContentData> GetFrChooseList(FurnitureListQuery query)
        {
            if (!(query.FurnitureType.Equals(0)))
                //basic mode
                return GetListBasic(query);

            // special mode
            ICriteria crt = session.CreateCriteria(typeof(Fr_TopChoose));
            crt.AddOrder(new NHibernate.Criterion.Order("Tp_place", true));
            IList<Fr_TopChoose> pop = crt.List<Fr_TopChoose>();
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
            IList<Fr_ContentData> data = new List<Fr_ContentData>();

            // specialid 有指定位置时
            if (specialid.Length > 0)
            {
                var commonquery = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_sdesign=:st and ns.Fc_ID not in (" + specialid + ")")
                    .SetBoolean("st", true);
                //commonquery.SetString("specialid", specialid);
                if (((query.Pageindex - 1) * pagestep) - ar.Count > -1)
                    commonquery.SetFirstResult((query.Pageindex - 1) * pagestep - ar.Count);
                else
                    commonquery.SetFirstResult(0);
                commonquery.SetMaxResults(query.Pageindex * pagestep - ar.Count);
                var commonlist = commonquery.List<Fr_ContentData>();

                var commlist_index = 0;
                if (query.Pageindex.Equals(1))
                {
                    var speciallist = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_sdesign=:st and ns.Fc_ID in (" + specialid + ")")
                        .SetBoolean("st", true)
                        //.SetString("specialid", specialid)
                        .List<Fr_ContentData>();

                    for (int i = 1; i < pagestep + 1; i++)
                    {
                        if (di.ContainsKey(i))
                        {
                            var te = speciallist.Where(it => it.Fc_ID.Equals(di[i]));
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
                    data = commonquery.List<Fr_ContentData>();
                }
            }
            else
            {
                data = session.CreateQuery("from Fr_ContentData as ns where ns.Fc_display=:st and ns.Fc_sdesign=:st")
                .SetBoolean("st", true)
                .SetFirstResult((query.Pageindex - 1) * pagestep)
                .SetMaxResults(query.Pageindex * pagestep)
                .List<Fr_ContentData>();
            }
            return data;
        }

        public IList<Fr_ContentData> GetList(FurnitureListQuery query)
        {
            IList<Fr_ContentData> model = new List<Fr_ContentData>();
            if (query.FurnitureType == null)
            {
                model = this.GetKnowAllsList(query);
                return model;
            }
            switch (query.FurnitureType.ToLower())
            {
                case "knowalls":
                    model = this.GetKnowAllsList(query);
                    break;
                case "kindoffurniture":
                    model = this.GetKindOfFurnitureList(query);
                    break;
                case "frchoose":
                    model = this.GetFrChooseList(query);
                    break;
            }
            return model;
        }
    
    }

    public class FurnitureListQuery
    {
        public int Pageindex { get; set; }
        public string FurnitureType { get; set; }
        public int Type { get; set; }
    }
}
