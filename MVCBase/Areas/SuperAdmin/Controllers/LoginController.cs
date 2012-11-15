using MVCBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBase.Domain.Entity;
using MVCBase.DAL;
using System.Configuration;

namespace MVCBase.Areas.SuperAdmin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /SuperAdmin/Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Login(SuperAdmin superadmin)
        {
            string str = string.Empty;
            if (superadmin.Verify())
            {
                str = Login(superadmin.LoginName, superadmin.Password) ? "1" : "2";
            }

            return str;
        }
                
        [NonAction]
        public bool Login(string LoginName, string Password)
        {
            bool istrue = false;

            Admin dal = new Admin();
            IList<Ba_Admin> admins = dal.GetModel(LoginName, Common.Encrypt.MD5Encrypt(Password));
            istrue = admins.Count > 0 ? true : false;

            if (istrue)
            {
                //更新最后login时间
                admins[0].Ad_LastLoginTime = DateTime.Now;
                dal.Update(admins[0]);

                //写入cookies
                setcookie(admins[0]);
            }

            return istrue;
        }

        //设置cookies
        public void setcookie(Ba_Admin entity)
        {
            string _domain = ConfigurationManager.AppSettings["WebDomain"];
            HttpCookieCollection cookiecollect = new HttpCookieCollection();
            HttpCookie Account = null;
            HttpCookie Username = null;

            if (!string.IsNullOrEmpty(entity.Ad_AdminName))
            {
                Account = new HttpCookie("SuperAccount", Encrypt.DESEncrypt(entity.Ad_AdminName + System.Configuration.ConfigurationManager.AppSettings["AccountKey"]));
                Username = new HttpCookie("Superusername", entity.Ad_AdminName);
            }
            cookiecollect.Add(Account);
            cookiecollect.Add(Username);

            for (int i = 0; i < cookiecollect.Count; i++)
            {
                if (!_domain.Equals("localhost"))
                    cookiecollect[i].Domain = _domain;
                System.Web.HttpContext.Current.Response.Cookies.Add(cookiecollect[i]);
            }
        }
    }

    public class SuperAdmin
    {
        public string LoginName { get; set; }
        public string Password { get; set; }

        //验证返回数据的正确性
        public bool Verify()
        {
            bool istrue = true;

            //验证数据是否异常
            if (string.IsNullOrEmpty(this.LoginName) ||
                string.IsNullOrEmpty(this.Password))
            {
                istrue = false;
            }

            return istrue;
        }
    }
}
