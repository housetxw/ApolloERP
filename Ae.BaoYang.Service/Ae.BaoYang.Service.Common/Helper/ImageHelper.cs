using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Common.Helper
{
    public class ImageHelper
    {
        /// <summary>
        /// 添加图片的域名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string AddImageDomain(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {

                return string.Empty;
            }

            if (url.StartsWith("https://") || url.StartsWith("http://"))
            {
                return url;
            }

            var imageDomain = "https://m.aerp.com.cn";
            var imageUrl = url.TrimStart('/');
            imageUrl = string.Concat(imageDomain, "/", imageUrl);
            return imageUrl;
        }
    }
}
