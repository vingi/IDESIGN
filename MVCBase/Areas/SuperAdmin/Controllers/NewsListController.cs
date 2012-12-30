using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MVCBase.Domain.Entity;
using MVCBase.DAL;
using NHibernate;

namespace MVCBase.Areas.SuperAdmin.Controllers
{
    public class NewsListController : Controller
    {
        //
        // GET: /SuperAdmin/NewsList/

        public ActionResult Page(int? id)
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("NewsManage", "NewsList");

            var newstype = new NewsType();
            ViewBag.newstype = newstype.GetList();

            News dal = new News();
            IList<Ba_News> news = dal.GetNews(id.HasValue ? (int)id : 1);

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
            page.HrefPage = "/superadmin/NewsList/page/";
            ViewBag.pageinfo = page.Render();

            return View(news);
        }

    }
}
