using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBase.Areas.SuperAdmin.Controllers
{
    public class DesignerListController : Controller
    {
        //
        // GET: /SuperAdmin/DesignerList/

        public ActionResult Index()
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("DesignerManage", "DesignerOperate");
            return View();
        }

    }
}
