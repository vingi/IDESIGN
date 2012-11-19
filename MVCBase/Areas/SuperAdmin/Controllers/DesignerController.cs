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
            //設計風格
            DAL.DesignStyleType dal_designstyletype = new DAL.DesignStyleType();
            var designstyletype = dal_designstyletype.GetAllModel();
            ViewBag.designstyletype = designstyletype;

            return View(model);
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Submit(Designer_Form form)
        {

            return View();
        }
    }

    public class Designer_Form
    {
        public int designer_id { get; set; }
        public string designer_name { get; set; }
        public string designer_company { get; set; }
        public string designer_tel { get; set; }
        public string designer_email { get; set; }
        public string designer_address { get; set; }
        public string designer_url { get; set; }
        public string designer_local { get; set; }
        public string[] designer_htype { get; set; }
        public string[] designer_dtype { get; set; }
        public string[] designer_stype { get; set; }
        public bool designer_entrust { get; set; }
        public bool designer_decoration { get; set; }
        public string designer_price { get; set; }
        public string designer_strengths { get; set; }
        public string designer_text { get; set; }
        public bool designer_pdesign { get; set; }
        public bool designer_ndesign { get; set; }
        public bool designer_sdesign { get; set; }
    }
}
