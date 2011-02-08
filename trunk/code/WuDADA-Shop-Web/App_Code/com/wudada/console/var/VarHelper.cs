using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 常用參數
/// </summary>
namespace com.wudada.console.var
{
    public class VarHelper
    {
        public enum SLIDESHOW_FX { fade, scrollDown, zoom };

        /// <summary>
        /// 系統設定 Id
        /// </summary>
        public static readonly int WuDADA_SYSTEMPARAM_ID = 1;

        /// <summary>
        /// 廣告輪播-效果
        /// </summary>
        public static readonly string DISPLAY_AD_SLIDESHOW_FX = Enum.GetName(SLIDESHOW_FX.fade.GetType(), SLIDESHOW_FX.fade);

        /// <summary>
        /// 廣告輪播-速度
        /// </summary>
        public static readonly string DISPLAY_AD_SLIDESHOW_SPEED = "5000";

        /// <summary>
        /// 公司簡介-關於新明
        /// </summary>
        public static readonly int WuDADA_ITEMPARAM_ID_COM_INFO1 = 1;

        /// <summary>
        /// 公司簡介-聯絡資訊
        /// </summary>
        public static readonly int WuDADA_ITEMPARAM_ID_COM_INFO2 = 2;

        /// <summary>
        /// 資訊管理-最新消息
        /// </summary>
        public static readonly int WuDADA_ITEMPARAM_ID_NEWS1 = 5;

        /// <summary>
        /// 資訊管理-眼鏡與我
        /// </summary>
        public static readonly int WuDADA_ITEMPARAM_ID_INFO1 = 6;

        /// <summary>
        /// 廣告管理-首頁廣告
        /// </summary>
        public static readonly int WuDADA_ITEMPARAM_ID_AD_INFO1 = 7;

        /// <summary>
        /// 廣告管理-最新商品
        /// </summary>
        public static readonly int WuDADA_ITEMPARAM_ID_AD_INFO2 = 8;

        /// <summary>
        /// 廣告管理-最新消息
        /// </summary>
        public static readonly int WuDADA_ITEMPARAM_ID_AD_INFO3 = 9;

        /// <summary>
        /// 廣告管理-眼鏡與我
        /// </summary>
        public static readonly int WuDADA_ITEMPARAM_ID_AD_INFO4 = 10;
    }
}