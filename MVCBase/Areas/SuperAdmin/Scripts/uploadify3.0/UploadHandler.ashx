<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;

public class UploadHandler : IHttpHandler {

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
            string NewName = DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + stro;
            if (oFile.FileName.Length > 0)
            {
                string FileExtention = Path.GetExtension(oFile.FileName);
                string fileallname = strUploadPath + NewName + FileExtention;
                oFile.SaveAs(fileallname);
                //异步到image server
                try
                {
                    //string re = Upload_Request("http://vingi.soufun.tw/ReceiveImage.ashx?filename=" + NewName + "_1" + FileExtention, fileallname, NewName + FileExtention, context);
                    string re = uploadtt(fileallname);
                    context.Response.Write(re);
                    FileInfo file = new FileInfo(fileallname);
                    file.Delete();
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

    //原image上传handle,暂时用.
    private string uploadtt(string fileallname)
    {
        string geturl = string.Empty;
        string Para = "http://uimg.twhouses.com.tw/uploads/,120,E:\\uploads ";
        try
        {
            System.Net.WebClient Client = new System.Net.WebClient();
            Client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            Client.Proxy = null;
            byte[] bytes = Client.UploadFile("http://uimg.twhouses.com.tw/imgSave.aspx?Para=" + Para, "post", fileallname);
            string sss = System.Text.Encoding.UTF8.GetString(bytes);

            if (sss.IndexOf("^") != -1)
                geturl = sss.Substring(0, sss.IndexOf("^"));
            else
                geturl = sss;
        }
        catch (Exception ex)
        {
            geturl = "error";
        }
        return geturl;
    }
    
    
    
    
    
    


    /// <summary>
    /// 将本地文件上传到指定的服务器(HttpWebRequest方法)
    /// </summary>
    /// <param name="address">文件上传到的服务器</param>
    /// <param name="fileNamePath">要上传的本地文件（全路径）</param>
    /// <param name="fullfilename">文件名</param>
    /// <param name="progressBar">上传进度条</param>
    /// <returns>成功返回1，失败返回0</returns>
    private string Upload_Request(string address, string fileNamePath, string fullfilename, HttpContext context)
    {
        string returnValue = string.Empty;

        // 要上传的文件
        FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
        BinaryReader r = new BinaryReader(fs);

        //时间戳
        string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
        byte[] boundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");

        //请求头部信息
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("--");
        sb.Append(strBoundary);
        sb.Append("\r\n");
        sb.Append("Content-Disposition: form-data; name=\"");
        sb.Append("file");
        sb.Append("\"; filename=\"");
        sb.Append(fullfilename);
        sb.Append("\"");
        sb.Append("\r\n");
        sb.Append("Content-Type: ");
        sb.Append("application/octet-stream");
        sb.Append("\r\n");
        sb.Append("\r\n");
        string strPostHeader = sb.ToString();
        byte[] postHeaderBytes = System.Text.Encoding.UTF8.GetBytes(strPostHeader);

        // 根据uri创建HttpWebRequest对象
        System.Net.HttpWebRequest httpReq = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(new Uri(address));
        httpReq.Method = "POST";

        //对发送的数据不使用缓存
        httpReq.AllowWriteStreamBuffering = false;

        //设置获得响应的超时时间（300秒）
        httpReq.Timeout = 300000;
        httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
        long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
        long fileLength = fs.Length;
        httpReq.ContentLength = length;
        try
        {
            //每次上传40k
            int bufferLength = 40960;
            byte[] buffer = new byte[bufferLength];

            //已上传的字节数
            long offset = 0;

            //开始上传时间
            DateTime startTime = DateTime.Now;
            int size = r.Read(buffer, 0, bufferLength);
            Stream postStream = httpReq.GetRequestStream();

            //发送请求头部消息
            postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            while (size > 0)
            {
                postStream.Write(buffer, 0, size);
                offset += size;
                //TimeSpan span = DateTime.Now - startTime;
                //double second = span.TotalSeconds;
                size = r.Read(buffer, 0, bufferLength);
            }

            //添加尾部的时间戳
            postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            postStream.Close();

            //获取服务器端的响应
            System.Net.WebResponse webRespon = httpReq.GetResponse();
            Stream s = webRespon.GetResponseStream();
            StreamReader sr = new StreamReader(s);

            //读取服务器端返回的消息
            String sReturnString = sr.ReadLine();
            s.Close();
            sr.Close();
            returnValue = sReturnString;
            //if (sReturnString.IndexOf("http://") > -1)
            //{
            //    returnValue = 1;
            //}
            //else if (sReturnString == "Error")
            //{
            //    returnValue = 0;
            //}stageConnectionStrings.config

        }
        catch
        {
            returnValue = "error";
        }
        finally
        {
            fs.Close();
            r.Close();
        }

        return returnValue;
    }
    
    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}