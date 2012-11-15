using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MVCBase.Areas.SuperAdmin.Public
{
    /// <summary>
    /// UploadHandler 的摘要说明
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";
            HttpPostedFile oFile = context.Request.Files["Filedata"];
            string strUploadPath = HttpContext.Current.Server.MapPath("/ImageUpload") + "\\";
            if (oFile != null)
            {
                if (!Directory.Exists(strUploadPath))
                {
                    Directory.CreateDirectory(strUploadPath);
                }
                Random ro = new Random();
                string stro = ro.Next(100, 100000000).ToString();//产生一个随机数用于新命名的图片
                string NewName = DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + stro;
                if (oFile.FileName.Length > 0)
                {
                    string FileExtention = Path.GetExtension(oFile.FileName);
                    string fileallname = strUploadPath + NewName + FileExtention;
                    oFile.SaveAs(fileallname);
                    //异步到image server
                    try
                    {
                        //string re = Upload_Request("http://vingi.soufun.tw/ReceiveImage.ashx?filename=" + NewName + "_1" + FileExtention, fileallname, NewName + FileExtention, context);
                        //string re = uploadtt(fileallname);
                        string re = "/ImageUpload/" + NewName + FileExtention;
                        if (string.IsNullOrEmpty(Common.common.requestQueryString("immediate")))
                        {
                            context.Response.Write(re);
                        }
                        else
                        {
                            context.Response.Write("{\"err\":\"\",\"msg\":\"" + re + "\"}"﻿﻿);
                        }
                        //FileInfo file = new FileInfo(fileallname);
                        //file.Delete();
                        context.Response.End();
                    }
                    catch (Exception ex) { }

                }
            }
            else
            {
                context.Response.Write("0");
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