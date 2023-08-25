using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Ae.Vehicle.Service.Common.Helper
{
    public static class ImageHelper
    {

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>System.String.</returns>
        public static string GetImageUrl(this string url) => GetImageUrl(url, 0, 0, false);

        /// <summary>处理图片地址</summary>
        /// <param name="url">图片地址。绝对地址或相对地址</param>
        /// <param name="maxWidth">最大宽</param>
        /// <param name="maxHeight">最大高</param>
        /// <param name="mobile">是否是用于手机显示</param>
        /// <returns>处理后的图片地址</returns>
        public static string GetImageUrl(this string url, int maxWidth, int maxHeight, bool mobile)
        {
            if (string.IsNullOrWhiteSpace(url))
                return null;

            UriBuilder ub;
            if (url.Contains("://"))
            {
                ub = new UriBuilder(url);
                if (!ub.Host.EndsWith(".aerp.com.cn") &&
                    !ub.Host.EndsWith(".aerp.cn") &&
                    !ub.Host.EndsWith(".alikunlun.com") &&
                    !ub.Host.EndsWith(".qiniucdn.com"))
                    return url;
            }
            else
            {
                if (url[0] != '/')
                    url = "/" + url;

                ub = new UriBuilder { Path = url };
            }

            if (maxWidth > 0 || maxHeight > 0 || mobile)
                ub.Path = ub.Path.SubstringBefore('@');

            ub.Scheme = "https";
            ub.Port = 443;
            ub.Host = "m.aerp.com.cn";
            //ub.Host = $"img{Math.Abs(ub.Path.GetHashCode() % 4) + 1}.aerp.com.cn";
            ub.Query = "";

            string format;
            if (mobile)
                format = ".webp";
            else
            {
                format = GetImageFormat(url);
                if (format != null)
                    format = "." + format;
            }

            if (maxWidth < 1)
                ub.Path += maxHeight < 1 ? $"@100Q{format}" : $"@{maxHeight}h_100Q{format}";
            else
                ub.Path += maxHeight < 1 ? $"@{maxWidth}w_100Q{format}" : $"@{maxWidth}w_{maxHeight}h_100Q{format}";

            return ub.Uri.ToString();
        }

        /// <summary>Substring到IndexOf的位置，如果不匹配则到最后</summary>
        public static string SubstringBefore(this string str, char value, StringComparison comparisonType = StringComparison.Ordinal)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            var index = str.IndexOf(value);
            return index <= 0 ? str : str.Substring(0, index);
        }

        /// <summary>将输入格式转换成标准输出格式</summary>
        /// <param name="url"></param>
        /// <returns>bmp gif psd tiff转换成png</returns>
        private static string GetImageFormat(string url)
        {
            var format = Path.GetExtension(url);
            if (format == null)
                return null;

            switch (format.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "jpg";

                case ".bmp":
                case ".png":
                case ".psd":
                case ".tiff":
                    return "png";

                case ".gif":
                    return "gif";

                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取Logo图片地址(汉字转化为拼音)
        /// </summary>
        /// <param name="brand">品牌名称</param>
        /// <returns>Url</returns>
        public static string GetLogoUrlByName(string brand)
        {
            if (brand == null || brand.Length < 4)
                return null;

            return "/Images/Logo/" + WebUtility.UrlEncode(brand.Substring(4).Trim().Pinyin().ToLower()) + ".png";
        }
    }
}
