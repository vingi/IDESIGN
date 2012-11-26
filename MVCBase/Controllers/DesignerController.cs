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
    public class DesignerController : Controller
    {
        //
        // GET: /Designer/

        public ActionResult Index(int? id)
        {
            DAL.Designer dal = new DAL.Designer();
            var model = dal.GetSingledataById(id.HasValue ? id.Value : 0);

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


        public ActionResult List(string designertype, int? id)
        {
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

            Designer dal = new Designer();
            var model = dal.GetPopularList(id.HasValue ? id.Value : 0);
            return View(model);
        }

    }
}
