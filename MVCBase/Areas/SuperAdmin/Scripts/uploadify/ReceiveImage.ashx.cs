using System;
using System.Collections.Generic;
using System.Web;

namespace HouseDataManageSystem.Views.API.uploadify
{
    /// <summary>
    /// ReceiveImage 的摘要说明
    /// </summary>
    public class ReceiveImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");

            foreach (string f in context.Request.Files.AllKeys)
            {

                HttpPostedFile file = context.Request.Files[f];

                string aab = "";
                try
                {
                    aab = context.Request["Para"].ToString();
                }
                catch (Exception ex)
                {

                }

                string[] Para = aab.Split(',');
                try
                {
                    //string aa = images.Upload(file, Para[0].ToString(), Para[2].ToString(), Convert.ToInt32(Para[1])) + "^";
                    //context.Response.Write(aa);
                    //context.Response.End();
                }
                catch (OutOfMemoryException exmemory)
                {
                    context.Response.Write("outofspace");
                    context.Response.End();
                }
                catch (Exception ex)
                {

                }


            }
            if (context.Request["name"] != null)
            {
                //images.DeleteImg(Request["name"].ToString(), Request["urlpath"].ToString(), Request["filepath"].ToString());

                context.Response.Write("");
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}