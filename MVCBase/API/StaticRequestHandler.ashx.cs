using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCBase.API
{
    /// <summary>
    /// StaticRequestHandler 的摘要说明
    /// </summary>
    public class StaticRequestHandler : IHttpHandler
    {
        /// <summary>
        /// 静态文件合并处理程式
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //该处理程式因含有敏感字符,故不经过XSS过滤
            string type = !string.IsNullOrWhiteSpace(context.Request.QueryString["type"]) ? context.Request.QueryString["type"] : string.Empty;
            switch (type)
            {
                case "css":
                    context.Response.ContentType = "text/css";
                    break;
                case "js":
                    context.Response.ContentType = "text/javascript";
                    break;
                case "javascript":
                    context.Response.ContentType = "text/javascript";
                    break;
                default:
                    context.Response.ContentType = "text/css";
                    break;
            }

            //该处理程式因含有敏感字符,故不经过XSS过滤
            string files_str = !string.IsNullOrWhiteSpace(context.Request.QueryString["f"]) ? context.Request.QueryString["f"] : string.Empty;
            string[] files = files_str.Split(',');

            for (int i = 0; i < files.Length; ++i)
            {
                if (!string.IsNullOrWhiteSpace(files[i]) && (files[i].EndsWith(".css") || files[i].EndsWith(".js")))
                {
                    try
                    {
                        string path = System.Web.HttpContext.Current.Server.MapPath(files[i].StartsWith("~/") ? files[i] : context.Request.ApplicationPath + "/" + files[i]);
                        StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8"), false);
                        context.Response.Write(sr.ReadToEnd() + Environment.NewLine);
                        context.Response.Expires = 60 * 24 * 30; //30天过期
                        sr.Close();
                        sr.Dispose();
                    }
                    catch (Exception) { }

                }
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