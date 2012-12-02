using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBase.Domain.Entity;
using MVCBase.DAL;
using NHibernate;

namespace MVCBase.Areas.SuperAdmin.Controllers
{
    public class DesignerListController : Controller
    {
        //
        // GET: /SuperAdmin/DesignerList/

        public ActionResult Page(int? id)
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("DesignerManage", "DesignerList");

            Designer dal = new Designer();
            IList<ID_DContentData> designers = dal.GetModel(id.HasValue ? (int)id : 1);

            int pagecount = 1;
            int pagestep = 18;
            int objectcount = dal.GetCount();
            if (objectcount % pagestep == 0)
                pagecount = objectcount / pagestep;
            else
                pagecount = objectcount / pagestep + 1;
            //////
            /////pagecount = 13;
            int currentpage = id.HasValue ? (int)id : 1;
            Common.HtmlPagerControl page = new Common.HtmlPagerControl(pagecount, 3, objectcount);
            page.CurrentPage = currentpage;
            page.HrefPage = "/superadmin/DesignerList/page/";
            ViewBag.pageinfo = page.Render();
            return View(designers);
        }

        public ActionResult Popular()
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("DesignerManage", "DesignerPopular");
            TopPopular dal = new TopPopular();
            IList<ID_TopPopularData> toppopular = dal.GetAllModel();
            return View(toppopular);
        }

        [HttpPost]
        public string PopularSubmit(FormCollection form) 
        {
            string result = string.Empty;
            TopPopular dal = new DAL.TopPopular();
            try
            {
                for (int i = 1; i < 19; i++)
                {
                    if (form["toppopular" + i.ToString()] != null)
                    {
                        var model = dal.GetSingledataById(i);
                        int tempint = 0;
                        int.TryParse(form["toppopular" + i.ToString()], out tempint);
                        if (tempint.Equals(0))
                            model.Tp_DcID_FK = null;
                        else
                            model.Tp_DcID_FK = tempint;
                        dal.Update(model);
                    }
                }
                result = "1";
            }
            catch (Exception)
            {
                result = "0";
            }
            return result;
        }

        public ActionResult Star()
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("DesignerManage", "DesignerStar");
            TopStar dal = new TopStar();
            IList<ID_TopStarData> topstar = dal.GetAllModel();
            return View(topstar);
        }

        [HttpPost]
        public string StarSubmit(FormCollection form)
        {
            string result = string.Empty;
            TopStar dal = new DAL.TopStar();
            try
            {
                for (int i = 1; i < 19; i++)
                {
                    if (form["topstar" + i.ToString()] != null)
                    {
                        var model = dal.GetSingledataById(i);
                        int tempint = 0;
                        int.TryParse(form["topstar" + i.ToString()], out tempint);
                        if (tempint.Equals(0))
                            model.Ts_DcID_FK = null;
                        else
                            model.Ts_DcID_FK = tempint;
                        dal.Update(model);
                    }
                }
                result = "1";
            }
            catch (Exception)
            {
                result = "0";
            }
            return result;
        }

        public ActionResult Choose()
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("DesignerManage", "DesignerChoose");
            TopChoose dal = new TopChoose();
            IList<ID_TopChooseData> topchoose = dal.GetAllModel();
            return View(topchoose);
        }

        [HttpPost]
        public string ChooseSubmit(FormCollection form)
        {
            string result = string.Empty;
            TopChoose dal = new DAL.TopChoose();
            try
            {
                for (int i = 1; i < 19; i++)
                {
                    if (form["topchoose" + i.ToString()] != null)
                    {
                        var model = dal.GetSingledataById(i);
                        int tempint = 0;
                        int.TryParse(form["topchoose" + i.ToString()], out tempint);
                        if (tempint.Equals(0))
                            model.Tc_DcID_FK = null;
                        else
                            model.Tc_DcID_FK = tempint;
                        dal.Update(model);
                    }
                }
                result = "1";
            }
            catch (Exception)
            {
                result = "0";
            }
            return result;
        }
    }
}
