using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ae.C.MiniApp.Api.Common.Util
{
   public static class ImageUtil
    {
        /// <summary>
        /// 检查图片格式
        /// </summary>
        /// <param name="imgUrl"></param>
        /// <returns>合法的图片格式返回true，其他格式返回false。</returns>
        public static bool CheckImageFormat(string imgUrl)
        {
            string[] format = { ".jpg", ".png", ".gif", ".jpeg", ".bmp", ".ico", ".log" };
            //文件后缀
            var suffix = imgUrl.Substring(imgUrl.LastIndexOf(".")).ToLower();
            if (format.Contains(suffix))
                return true;
            return false;
        }

        /// <summary>
        /// 添加图片的域名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string AddImageDomain(this string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {

                return string.Empty;
            }
            var imageDomain = "https://m.aerp.com.cn";
            //var imageUrl = url.TrimStart('/');
            var imageUrl = url.Trim();
            int len = imageUrl.IndexOf("://");
            if (len < 0)
            {
                imageUrl = string.Concat(imageDomain, "/", imageUrl);
            }
            return imageUrl;
        }

        /// <summary> 
        /// 取得HTML中所有图片的 URL。 
        /// </summary> 
        /// <param name="sHtmlText">HTML代码</param> 
        /// <returns>图片的URL列表</returns> 
        public static List<string> GetHtmlImageUrlList(this string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表 
            foreach (Match match in matches)
            {
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            }
            return sUrlList?.ToList();
        }
    }
}
