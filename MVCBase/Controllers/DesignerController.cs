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
    public class DesignerController : Controller
    {
        //
        // GET: /Designer/

        public ActionResult Index(int? id)
        {
            DAL.Designer dal = new DAL.Designer();
            var model = dal.GetSingledataById(id.HasValue ? id.Value : 0);

            //房屋類型
            DAL.HouseType dal_housetype = new DAL.HouseType();
            var housetype = dal_housetype.GetAllModel();
            ViewBag.housetype = housetype;
            //設計型式
            DAL.DesignType dal_designtype = new DAL.DesignType();
            var designtype = dal_designtype.GetAllModel();
            ViewBag.designtype = designtype;
            //設計風格
            DAL.DesignStyleType dal_designstyletype = new DAL.DesignStyleType();
            var designstyletype = dal_designstyletype.GetAllModel();
            ViewBag.designstyletype = designstyletype;

            return View(model);
        }


        public ActionResult List(string designertype, int? id)
        {
            //房屋類型
            DAL.HouseType dal_housetype = new DAL.HouseType();
            var housetype = dal_housetype.GetAllModel();
            ViewBag.housetype = housetype;
            //設計型式
            DAL.DesignType dal_designtype = new DAL.DesignType();
            var designtype = dal_designtype.GetAllModel();
            ViewBag.designtype = designtype;
            //設計風格
            DAL.DesignStyleType dal_designstyletype = new DAL.DesignStyleType();
            var designstyletype = dal_designstyletype.GetAllModel();
            ViewBag.designstyletype = designstyletype;

            //create query
            var query = new DesignerListQuery();
            query.Pageindex = id.HasValue ? id.Value : 0;
            query.DeesignerType = designertype;
            query.Housetype = Common.common.ConvertInt32(Request.QueryString["housetype"]);
            query.Designtype = Common.common.ConvertInt32(Request.QueryString["designtype"]);
            query.Designstyletype = Common.common.ConvertInt32(Request.QueryString["designstyletype"]);

            //Get List
            Designer dal = new Designer();
            var model = dal.GetPopularList(id.HasValue ? id.Value : 0);

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
            page.HrefPage = "/designer/list/" + designertype + "/";
            page.SimpleTheme = true;
            page.NavigateNext = "&gt;";
            page.NavigatePrevious = "&lt;";
            ViewBag.pageinfo = page.Render();

            return View(model);
        }

    }
}
