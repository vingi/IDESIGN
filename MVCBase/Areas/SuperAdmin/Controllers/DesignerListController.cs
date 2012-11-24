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

        public ActionResult Index(int? id)
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("DesignerManage", "DesignerList");

            Designer dal = new Designer();
            IList<ID_DContentData> designers = dal.GetModel(id.HasValue ? (int)id : 1);
            return View(designers);
        }

    }
}
