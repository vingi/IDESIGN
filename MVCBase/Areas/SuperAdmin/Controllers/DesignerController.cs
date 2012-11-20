using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Submit(Designer_Form form)
        {
            string result = string.Empty;
            if (ModelState.IsValid)
            {
                var model = Sransform(form);
                DAL.Designer dal = new DAL.Designer();
                try
                {
                    dal.SaveOrUpdate(model);
                    result = "1";
                }
                catch (System.Exception ex)
                {
                    result = ex.ToString();
                }
            }

            return result;
        }

        public MVCBase.Domain.Entity.ID_DContentData Sransform(Designer_Form form)
        {
            var model = new MVCBase.Domain.Entity.ID_DContentData();
            if (!form.designer_id.Equals(0))
            {
                DAL.Designer dal = new DAL.Designer();
                model = dal.GetSingledataById(form.designer_id);
            }
            model.Dc_Id = form.designer_id;
            model.Dc_name = form.designer_name;
            model.Dc_company = form.designer_company;
            model.Dc_tel = form.designer_tel;
            model.Dc_email = form.designer_email;
            model.Dc_address = form.designer_address;
            model.Dc_url = form.designer_url;
            model.Dc_local = form.designer_local;
            model.Dc_htype = form.designer_htype != null && form.designer_htype.Length > 0 ? "," + string.Join(",", form.designer_htype) + "," : string.Empty;
            model.Dc_dtype = form.designer_dtype != null && form.designer_dtype.Length > 0 ? "," + string.Join(",", form.designer_dtype) + "," : string.Empty;
            model.Dc_stype = form.designer_stype != null && form.designer_stype.Length > 0 ? "," + string.Join(",", form.designer_stype) + "," : string.Empty;
            model.Dc_Entrust = Common.MVCCommon.BindCheckBox_Entity(form.designer_entrust);
            model.Dc_Decoration = Common.MVCCommon.BindCheckBox_Entity(form.designer_decoration);
            model.Dc_price = form.designer_price;
            model.Dc_Strengths = form.designer_strengths;
            model.Dc_Text = form.designer_text;
            model.Dc_ImageUrl = form.designer_imageurl;
            model.Dc_pdesign = Common.MVCCommon.BindCheckBox_Entity(form.designer_pdesign);
            model.Dc_ndesign = Common.MVCCommon.BindCheckBox_Entity(form.designer_ndesign);
            model.Dc_sdesign = Common.MVCCommon.BindCheckBox_Entity(form.designer_sdesign);
            model.Dc_display = model.Dc_Id.Equals(0) ? true : model.Dc_display;
            model.Dc_createdate = model.Dc_createdate.HasValue ? model.Dc_createdate : DateTime.Now;

            return model;
        }
    }

    public class Designer_Form
    {
        public int designer_id { get; set; }
        [Required]
        public string designer_name { get; set; }
        public string designer_company { get; set; }
        public string designer_tel { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string designer_email { get; set; }
        public string designer_address { get; set; }
        [Required]
        [DataType(DataType.Url)]
        public string designer_url { get; set; }
        public string designer_local { get; set; }
        public string[] designer_htype { get; set; }
        public string[] designer_dtype { get; set; }
        public string[] designer_stype { get; set; }
        public string designer_entrust { get; set; }
        public string designer_decoration { get; set; }
        public string designer_price { get; set; }
        public string designer_strengths { get; set; }
        public string designer_text { get; set; }
        public string designer_imageurl { get; set; }
        public string designer_pdesign { get; set; }
        public string designer_ndesign { get; set; }
        public string designer_sdesign { get; set; }
    }
}
