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

        public ActionResult Index()
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("NewsManage", "NewsList");

            News dal = new News();
            IList<Ba_News> news = dal.GetNews();
            ViewBag.news = news;

            return View(news);
        }

    }
}
