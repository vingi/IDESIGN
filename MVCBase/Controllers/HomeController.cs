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
    public class HomeController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger("Quote");
        public ActionResult Index()
        {
            ViewBag.Message = "欢迎使用 ASP.NET MVC!";

            //Sample dal = new Sample();
            //var customer = dal.GetCustomerById(3);
            //customer.FirstName = "Vingi";
            //customer.LastName = "Chen";
            //dal.UpdateCustomer(customer);

            Admin dal = new Admin();
            var model = dal.GetAdminById(1);
            ViewBag.Message = model.Ad_AdminPwd;
            ViewBag.Model = model;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
