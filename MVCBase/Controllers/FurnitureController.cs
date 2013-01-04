using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBase.Domain.Entity;
using MVCBase.DAL;
using NHibernate;

namespace MVCBase.Controllers
{
    public class FurnitureController : Controller
    {
        //
        // GET: /Furniture/

        public ActionResult Index(int? id)
        {
            DAL.Furniture dal = new DAL.Furniture();
            var model = dal.GetSingleById(id.HasValue ? id.Value : 0);

            //设定相临建材
            if (id.HasValue)
            {
                ViewBag.Prev = dal.Prev(id.Value);
                ViewBag.Next = dal.Next(id.Value);
            }

            return View(model);
        }

        public ActionResult List(string furnituretype, int? id)
        {
            FurnitureType furnitureType = new FurnitureType();
            ViewBag.furnitureType = furnitureType.GetAllModel();

            furnituretype = furnituretype == null ? "knowalls" : furnituretype;
            //create query
            var query = new FurnitureListQuery();
            query.Pageindex = id.HasValue ? id.Value : 1;
            query.FurnitureType = furnituretype;
            query.Type = Common.common.ConvertInt32(Request.QueryString["ftype"]);

            ViewBag.query = query;

            //Get List
            Designer dal = new Designer();
            IList<ID_DContentData> model;
            model = dal.GetList(query);

            //page info
            int pagecount = 1;
            int pagestep = 18;
            int objectcount = dal.GetCount(query);
            if (objectcount % pagestep == 0)
                pagecount = objectcount / pagestep;
            else
                pagecount = objectcount / pagestep + 1;
            //////
            //pagecount = 13;
            int currentpage = id.HasValue ? (int)id : 1;
            Common.HtmlPagerControl page = new Common.HtmlPagerControl(pagecount, 7, objectcount);
            page.CurrentPage = currentpage;
            page.HrefPage = "/designer/list/" + furnituretype + "/";
            page.SimpleTheme = true;
            page.NavigateNext = "&gt;";
            page.NavigatePrevious = "&lt;";
            ViewBag.pageinfo = page.Render();

            return View(model);
        }

    }
}
