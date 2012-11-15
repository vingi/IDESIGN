using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBase.Areas.SuperAdmin.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /SuperAdmin/Default/

        public ActionResult Index()
        {
            ViewBag.AdminName = "Vingi";
            return View();
        }

    }
}
