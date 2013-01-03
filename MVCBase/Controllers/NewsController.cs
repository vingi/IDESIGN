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
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index(int? id)
        {
            var newstype = new NewsType();
            ViewBag.newstype = newstype.GetList();

            News dal = new News();
            var model = dal.GetSingleById(id.HasValue ? id.Value : 0);

            return View(model);
        }

        public ActionResult List(int? newstype, int? id)
        {
            int _newstype = newstype.HasValue ? newstype.Value : 1;
            var newsTypes = new NewsType();
            ViewBag.newstypes = newsTypes.GetList();

            ViewBag.newstype = _newstype;

            var news = new News();
            IList<Ba_News> baNewses = news.GetNews(_newstype, id.HasValue ? id.Value : 1);
            ViewBag.baNewses = baNewses;

            //page info
            int pagecount = 1;
            int pagestep = 5;
            int objectcount = news.GetCount(_newstype);
            if (objectcount % pagestep == 0)
                pagecount = objectcount / pagestep;
            else
                pagecount = objectcount / pagestep + 1;
            //////
            //pagecount = 13;
            int currentpage = id.HasValue ? (int)id : 1;
            Common.HtmlPagerControl page = new Common.HtmlPagerControl(pagecount, 7, objectcount);
            page.CurrentPage = currentpage;
            page.HrefPage = "/news/list/" + newstype + "/";
            page.SimpleTheme = true;
            page.NavigateNext = "&gt;";
            page.NavigatePrevious = "&lt;";
            ViewBag.pageinfo = page.Render();

            return View();
        }

    }
}
