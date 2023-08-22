using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.FileUpload.Api.Common.Util
{
   public class ImageUtil
    {
        /// <summary>
        /// 检查图片格式
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns>合法的图片格式返回true，其他格式返回false。</returns>
        public static bool CheckImageFormat(string imgUrl)
        {
            string[] format = { ".jpg", ".png", ".gif", ".jpeg", ".bmp", ".ico", ".log",".pdf" };
            //文件后缀
            var suffix = imgUrl.Substring(imgUrl.LastIndexOf(".")).ToLower();
            if (format.Contains(suffix))
                return true;
            return false;
        }
    }
}
