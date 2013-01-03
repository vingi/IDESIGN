using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBase.Domain.Entity;
using MVCBase.DAL;

namespace MVCBase.Areas.SuperAdmin.Controllers
{
    public class FurnitureListController : Controller
    {
        //
        // GET: /SuperAdmin/FurnitureList/

        public ActionResult Page(int? id)
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("FurnitureManage", "FurnitureList");

            Furniture dal = new Furniture();
            IList<Fr_ContentData> furnitures = dal.GetModel(id.HasValue ? (int)id : 1);

            int pagecount = 1;
            int pagestep = 18;
            int objectcount = dal.GetCount();
            if (objectcount % pagestep == 0)
                pagecount = objectcount / pagestep;
            else
                pagecount = objectcount / pagestep + 1;

            int currentpage = id.HasValue ? (int)id : 1;
            Common.HtmlPagerControl page = new Common.HtmlPagerControl(pagecount, 3, objectcount);
            page.CurrentPage = currentpage;
            page.HrefPage = "/superadmin/FurnitureList/page/";
            ViewBag.pageinfo = page.Render();
            return View(furnitures);
        }

    }
}
