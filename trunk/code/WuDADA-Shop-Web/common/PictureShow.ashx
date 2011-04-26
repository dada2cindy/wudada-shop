<%@ WebHandler Language="C#" Class="PictureShow" %>

using System;
using System.Web;
using Common.Logging;
using System.IO;
using com.wudada.web.util;
using com.wudada.console.generic.util;

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
        string auto = ""; //自動大小 高:h 寬:w
        int size = 0;
        int size2 = 0;

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
        auto = context.Request.QueryString["auto"];
        if (!string.IsNullOrEmpty(context.Request.QueryString["size"]))
        {
            size = ConvertUtil.ToInt32(context.Request.QueryString["size"]);
        }
        if (!string.IsNullOrEmpty(context.Request.QueryString["size2"]))
        {
            size2 = ConvertUtil.ToInt32(context.Request.QueryString["size2"]);
        }

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

        if (!string.IsNullOrEmpty(auto) & size > 0) //判斷是否要自動調整大小
        {
            log.Info("auto:" + auto);
            log.Info("size:" + size);
            string showPath = System.IO.Path.GetTempFileName();

            switch (auto)
            {
                case "w":
                    //縮圖不成功 就不縮了
                    try
                    {
                        ImageUtil.ToSmallPicInWidth(imgPath, showPath, size, ImageUtil.GetImageType(imgPath));
                    }
                    catch (Exception e)
                    {
                        showPath = imgPath;
                        log.Error(e);
                    }
                    break;
                case "h":
                    //縮圖不成功 就不縮了
                    try
                    {
                        ImageUtil.ToSmallPicInHeight(imgPath, showPath, size, ImageUtil.GetImageType(imgPath));
                    }
                    catch (Exception e)
                    {
                        showPath = imgPath;
                        log.Error(e);
                    }
                    break;
                case "wh":
                    //縮圖不成功 就不縮了
                    try
                    {
                        ImageUtil.ToSmallPicInWidthAndHeight(imgPath, showPath, size, size2, ImageUtil.GetImageType(imgPath));
                    }
                    catch (Exception e)
                    {
                        showPath = imgPath;
                        log.Error(e);
                    }
                    break;
                case "hw":
                    //縮圖不成功 就不縮了
                    try
                    {
                        ImageUtil.ToSmallPicInHeightAndWidth(imgPath, showPath, size, size2, ImageUtil.GetImageType(imgPath));
                    }
                    catch (Exception e)
                    {
                        showPath = imgPath;
                        log.Error(e);
                    }
                    break;
                case "same":
                    //縮圖不成功 就不縮了
                    try
                    {
                        ImageUtil.ToSmallPic(imgPath, showPath, size, size, ImageUtil.GetImageType(imgPath));
                    }
                    catch (Exception e)
                    {
                        showPath = imgPath;
                        log.Error(e);
                    }
                    break;
            }
            context.Response.WriteFile(showPath);
            context.Response.Flush();
        }
        else
        {
            if (File.Exists(imgPath))
            {
                context.Response.WriteFile(imgPath);
                context.Response.Flush();
            }
            else
            {
                log.Info("檔案不存在:" + imgPath);
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}