using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ConfigHelper 的摘要描述
/// </summary>
public class ConfigHelper
{
    public ConfigHelper()
    {
    }

    /// <summary>
    /// 是否專案測試中Y:測試 N:正式
    /// 在測試中，寄mail測試只會到測試信箱 不會到user手中
    /// </summary>
    public static readonly string OnTest = System.Configuration.ConfigurationManager.AppSettings["OnTest"].ToString();

    /// <summary>
    /// 網址
    /// </summary>
    public static readonly string Host = string.Format("http://{0}/", System.Configuration.ConfigurationManager.AppSettings["Host"].ToString());

    /// <summary>
    /// 測試收件者Email
    /// </summary>
    public static readonly string TestRecMail = System.Configuration.ConfigurationManager.AppSettings["TestRecMail"].ToString();

    /// <summary>
    /// 警示通知Email
    /// </summary>
    public static readonly string AlertRecMail = System.Configuration.ConfigurationManager.AppSettings["AlertRecMail"].ToString();

    /// <summary>
    /// 廣告輪播功能 頁面
    /// </summary>
    public static readonly string AdSlideShowPage = System.Configuration.ConfigurationManager.AppSettings["AdSlideShowPage"].ToString();

    /// <summary>
    /// 廣告圖檔目錄
    /// </summary>
    public static readonly string AdPicURL = System.Configuration.ConfigurationManager.AppSettings["AdPicURL"].ToString();

    /// <summary>
    /// AttachFile目錄
    /// </summary>
    public static readonly string AttachFileURL = System.Configuration.ConfigurationManager.AppSettings["AttachFileURL"].ToString();
}
