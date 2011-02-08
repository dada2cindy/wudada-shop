<%@ WebHandler Language="C#" Class="PictureShow" %>

using System;
using System.Web;
using Common.Logging;
using System.IO;
using com.wudada.web.util;

public class PictureShow : IHttpHandler 
{
    ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private static readonly string AD_PIC = ConfigHelper.AdPicURL;
    private static readonly string FJX_FILE = ConfigHelper.FjxFileURL;
    
    public void ProcessRequest (HttpContext context) 
    {
        string imgFile = "";
        string imgPath = "";
        string type = "";

        //防止自動程式連結
        //if (!configHelper.OnTest.Equals("Y"))
        //{
        //    if (context.Request.UrlReferrer == null || context.Request.UrlReferrer.ToString().IndexOf("hsinmin") == -1)
        //    {
        //        return;
        //    }
        //}

        imgFile = context.Request.QueryString["fileName"];
        type = context.Request.QueryString["type"];

        //   context.Response.ContentType = "image/jpg";
        if (string.IsNullOrEmpty(imgFile) || string.IsNullOrEmpty(type))
        {
            return;
        }
        else
        {
            switch (type)
            {
                case "adpic":
                    imgPath = context.Server.MapPath(AD_PIC);
                    imgPath = string.Format("{0}{1}", imgPath, HttpUtility.UrlDecode(imgFile));
                    break;
                case "fjx":
                    imgPath = context.Server.MapPath(FJX_FILE);
                    imgPath = string.Format("{0}{1}", imgPath, HttpUtility.UrlDecode(imgFile));
                    break;
            }
        }


        if (File.Exists(imgPath))
        {
            context.Response.WriteFile(imgPath);
        }
        else
        {
            log.Info("檔案不存在:" + imgPath);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}