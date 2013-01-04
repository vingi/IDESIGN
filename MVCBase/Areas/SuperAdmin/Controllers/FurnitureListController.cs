using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCBase.Domain.Entity;
using MVCBase.DAL;

namespace MVCBase.Areas.SuperAdmin.Controllers
{
    public class FurnitureListController : Controller
    {
        //
        // GET: /SuperAdmin/FurnitureList/

        public ActionResult Page(int? id)
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("FurnitureManage", "FurnitureList");

            Furniture dal = new Furniture();
            IList<Fr_ContentData> furnitures = dal.GetModel(id.HasValue ? (int)id : 1);

            int pagecount = 1;
            int pagestep = 18;
            int objectcount = dal.GetCount();
            if (objectcount % pagestep == 0)
                pagecount = objectcount / pagestep;
            else
                pagecount = objectcount / pagestep + 1;

            int currentpage = id.HasValue ? (int)id : 1;
            Common.HtmlPagerControl page = new Common.HtmlPagerControl(pagecount, 3, objectcount);
            page.CurrentPage = currentpage;
            page.HrefPage = "/superadmin/FurnitureList/page/";
            ViewBag.pageinfo = page.Render();
            return View(furnitures);
        }


        public ActionResult KnowAlls()
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("FurnitureManage", "FurnitureKnowAlls");
            TopKnowAlls dal = new TopKnowAlls();
            IList<Fr_TopKnowAlls> topknowalls = dal.GetAllModel();
            return View(topknowalls);
        }

        [HttpPost]
        public string KnowAllsSubmit(FormCollection form)
        {
            string result = string.Empty;
            TopKnowAlls dal = new TopKnowAlls();
            try
            {
                for (int i = 1; i < 19; i++)
                {
                    if (form["topknowalls" + i.ToString()] != null)
                    {
                        var model = dal.GetSingledataById(i);
                        int tempint = 0;
                        int.TryParse(form["topknowalls" + i.ToString()], out tempint);
                        if (tempint.Equals(0))
                            model.Tp_DcID_FK = null;
                        else
                            model.Tp_DcID_FK = tempint;
                        dal.Update(model);
                    }
                }
                result = "1";
            }
            catch (Exception)
            {
                result = "0";
            }
            return result;
        }

        public ActionResult KindOfFurniture()
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("FurnitureManage", "FurnitureKindOfFurniture");
            TopKindOfFurniture dal = new TopKindOfFurniture();
            IList<Fr_TopKindOfFurniture> topkingoffurniture = dal.GetAllModel();
            return View(topkingoffurniture);
        }

        [HttpPost]
        public string KindOfFurnitureSubmit(FormCollection form)
        {
            string result = string.Empty;
            TopKindOfFurniture dal = new TopKindOfFurniture();
            try
            {
                for (int i = 1; i < 19; i++)
                {
                    if (form["topkindoffurniture" + i.ToString()] != null)
                    {
                        var model = dal.GetSingledataById(i);
                        int tempint = 0;
                        int.TryParse(form["topkindoffurniture" + i.ToString()], out tempint);
                        if (tempint.Equals(0))
                            model.Tp_DcID_FK = null;
                        else
                            model.Tp_DcID_FK = tempint;
                        dal.Update(model);
                    }
                }
                result = "1";
            }
            catch (Exception)
            {
                result = "0";
            }
            return result;
        }

        public ActionResult FrChoose()
        {
            ViewBag.jsInit = Public.SuperAdminCommon.JSInit("FurnitureManage", "FurnitureFrChoose");
            TopFrChoose dal = new TopFrChoose();
            IList<Fr_TopChoose> topfrchoose = dal.GetAllModel();
            return View(topfrchoose);
        }

        [HttpPost]
        public string FrChooseSubmit(FormCollection form)
        {
            string result = string.Empty;
            TopFrChoose dal = new TopFrChoose();
            try
            {
                for (int i = 1; i < 19; i++)
                {
                    if (form["topfrchoose" + i.ToString()] != null)
                    {
                        var model = dal.GetSingledataById(i);
                        int tempint = 0;
                        int.TryParse(form["topfrchoose" + i.ToString()], out tempint);
                        if (tempint.Equals(0))
                            model.Tp_DcID_FK = null;
                        else
                            model.Tp_DcID_FK = tempint;
                        dal.Update(model);
                    }
                }
                result = "1";
            }
            catch (Exception)
            {
                result = "0";
            }
            return result;
        }
    }
}
