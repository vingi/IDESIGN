using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBase.Domain.Entity;
using MVCBase.DAL;

namespace MVCBase.Areas.SuperAdmin.Controllers
{
    public class FurnitureOperateController : Controller
    {
        //
        // GET: /SuperAdmin/FurnitureOperate/

        public ActionResult Index(int? id)
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("FurnitureManage", "FurnitureOperate");

            FurnitureType furnitureType = new FurnitureType();
            ViewBag.furnitureType = furnitureType.GetAllModel();

            Furniture furniture = new Furniture();
            var model = furniture.GetSingleById(id.HasValue ? id.Value : 0);




            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public string Submit(Furniture_Form form)
        {
            string result = string.Empty;
            if (ModelState.IsValid)
            {
                var model = Sransform(form);
                DAL.Furniture dal = new DAL.Furniture();
                try
                {
                    dal.Save(model);
                    result = "1";
                }
                catch (System.Exception ex)
                {
                    result = ex.ToString();
                }
            }

            return result;
        }

        public string Delete(int id)
        {
            string result = "0";
            DAL.Furniture dal = new DAL.Furniture();
            var model = dal.GetSingleById(id);
            if (model != null)
            {
                model.Fc_display = false;
                dal.Update(model);
                result = "1";
            }

            return result;
        }

        public MVCBase.Domain.Entity.Fr_ContentData Sransform(Furniture_Form form)
        {
            var model = new MVCBase.Domain.Entity.Fr_ContentData();
            if (!form.furniture_id.Equals(0))
            {
                DAL.Furniture dal = new DAL.Furniture();
                model = dal.GetSingleById(form.furniture_id);
            }
            model.Fc_ID = form.furniture_id;
            model.Fc_company = form.furniture_company;
            model.Fc_type_FK = form.furniture_type;
            model.Fc_tel = form.furniture_tel;
            model.Fc_email = form.furniture_email;
            model.Fc_address = form.furniture_address;
            model.Fc_url = form.furniture_url;
            model.Fc_local = form.furniture_local;
            model.Fc_summary = form.furniture_summary;
            model.Fc_history = form.furniture_history;
            model.Fc_concept = form.furniture_concept;
            model.Fc_description = form.furniture_description;
            model.Fc_ImageUrl = form.furniture_imageurl;
            model.Fc_pdesign = Common.MVCCommon.BindCheckBox_Entity(form.furniture_pdesign);
            model.Fc_ndesign = Common.MVCCommon.BindCheckBox_Entity(form.furniture_ndesign);
            model.Fc_sdesign = Common.MVCCommon.BindCheckBox_Entity(form.furniture_sdesign);
            model.Fc_display = model.Fc_ID.Equals(0) ? true : model.Fc_display;
            model.Fc_createtime = model.Fc_createtime.HasValue ? model.Fc_createtime : DateTime.Now;
            model.Fc_color = form.furniture_color;

            return model;
        }

    }

    public class Furniture_Form
    {
        public int furniture_id { get; set; }
        [Required]
        public string furniture_company { get; set; }
        public int furniture_type { get; set; }
        public string furniture_tel { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string furniture_email { get; set; }
        public string furniture_address { get; set; }
        [Required]
        [DataType(DataType.Url)]
        public string furniture_url { get; set; }
        public string furniture_local { get; set; }
        public string furniture_summary { get; set; }
        public string furniture_history { get; set; }
        public string furniture_concept { get; set; }
        public string furniture_description { get; set; }
        public string furniture_imageurl { get; set; }
        public string furniture_pdesign { get; set; }
        public string furniture_ndesign { get; set; }
        public string furniture_sdesign { get; set; }
        public string furniture_color { get; set; }
    }
}
