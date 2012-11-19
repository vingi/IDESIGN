using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCBase.Areas.SuperAdmin.Controllers
{
    public class DesignerController : Controller
    {
        //
        // GET: /SuperAdmin/Designer/

        public ActionResult Index(int? id)
        {
            DAL.Designer dal = new DAL.Designer();
            var model = dal.GetSingledataById(id.HasValue ? int.Parse(id.ToString()) : 0);

            //房屋類型
            DAL.HouseType dal_housetype = new DAL.HouseType();
            var housetype = dal_housetype.GetAllModel();
            ViewBag.housetype = housetype;
            //設計型式
            DAL.DesignType dal_designtype = new DAL.DesignType();
            var designtype = dal_designtype.GetAllModel();
            ViewBag.designtype = designtype;


            return View(model);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Submit()
        {
            return View();
        }
    }

    public class Designer_Form
    {
            public int designer_id { get; set; }
            public string designer_title { get; set; }
            public string designer_subtitle { get; set; }
            public string designer_description { get; set; }
    }
}
