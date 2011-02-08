using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.wudada.web.util
{
    public class ImageHelper
    {
        private readonly static string[] ALLOW_IMAGE_TYPE = new string[] { "jpg", "jpeg", "gif", "tiff", "png", "bmp" };

        public ImageHelper()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        /// <summary>
        /// 檢查檔案是否為圖檔格式
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsImage(string filePath)
        {
            bool isImage = false;

            foreach (string fileType in ALLOW_IMAGE_TYPE)
            {
                if (filePath.IndexOf(fileType) != -1)
                {
                    return true;
                }
            }
            return isImage;
        }
    }
}
