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
    }
}