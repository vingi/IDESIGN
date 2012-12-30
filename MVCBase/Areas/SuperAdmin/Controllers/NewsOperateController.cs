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
    public class NewsOperateController : Controller
    {
        //
        // GET: /SuperAdmin/NewsOperate/

        public ActionResult Index(int? id)
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("NewsManage", "NewsOperate");

            var newstype = new NewsType();
            ViewBag.newstype = newstype.GetList();

            var model = new Ba_News();
            if (id != null)
            {
                News dal = new News();
                model = dal.GetSingleById((int)id);
            }
            if (model == null)
                model = new Ba_News();

            ViewBag.model = model;

            if (model.Ns_ID.Equals(0))
                ViewBag.Title = "新增最新情報";
            else
                ViewBag.Title = "更新最新情報";

            return View();
        }

        [HttpPost,ValidateInput(false)]
        public string Submit(NewsOperate_Form form)
        {
            string result = string.Empty;
            News dal = new News();
            var model = dal.GetSingleById(form.news_id);
            if (model == null)
                model = new Ba_News();
            model.Ns_Title = form.news_title;
            model.Ns_Type = form.news_type;
            model.Ns_Content = form.news_description;
            model.Ns_ImagePath = form.news_imageurl;
            model.Ns_PostTime = DateTime.Now;
            model.Ns_State = true;
            try
            {
                dal.Save(model);
                result = "1";
            }
            catch (System.Exception ex)
            {
                result = ex.ToString();
            }

            return result;
        }

        public string Delete(int id)
        {
            string result = "0";
            News dal = new News();
            var model = dal.GetSingleById(id);
            if (model != null)
            {
                model.Ns_State = false;
                dal.Update(model);
                result = "1";
            }

            return result;
        }

    }

    public class NewsOperate_Form
    {
        public int news_id { get; set; }
        public string news_title { get; set; }
        public int news_type { get; set; }
        public string news_description { get; set; }
        public string news_imageurl { get; set; }
    }
}
