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
            Sransform(form);
            return View();
        }

        public MVCBase.Domain.Entity.ID_DContentData Sransform(Designer_Form form)
        {
            DAL.Designer dal = new DAL.Designer();
            var model = dal.GetSingledataById(form.designer_id == null ? 0 : form.designer_id);
            if (model == null)
                model = new MVCBase.Domain.Entity.ID_DContentData();
            model.Dc_name = form.designer_name;
            model.Dc_company = form.designer_company;
            model.Dc_tel = form.designer_tel;
            model.Dc_email = form.designer_email;
            model.Dc_address = form.designer_address;
            model.Dc_url = form.designer_url;
            model.Dc_local = form.designer_local;
            model.Dc_htype = form.designer_htype.Length > 0 ? "," + string.Join(",", form.designer_htype) + "," : string.Empty;
            model.Dc_dtype = form.designer_dtype.Length > 0 ? "," + string.Join(",", form.designer_dtype) + "," : string.Empty;
            model.Dc_stype = form.designer_stype.Length > 0 ? "," + string.Join(",", form.designer_stype) + "," : string.Empty;
            model.Dc_Entrust = form.designer_entrust;

            return model;
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
        public string designer_imageurl { get; set; }
        public bool designer_pdesign { get; set; }
        public bool designer_ndesign { get; set; }
        public bool designer_sdesign { get; set; }
    }
}
