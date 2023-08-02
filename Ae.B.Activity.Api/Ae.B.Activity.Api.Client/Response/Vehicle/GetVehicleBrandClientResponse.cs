using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Response.Vehicle
{
  public  class GetVehicleBrandClientResponse
    {
        /// <summary>
        /// 品牌 （Eg:A - 奥迪）
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 品牌前缀（Eg:A）
        /// </summary>
        public string BrandPrefix { get; set; }

        /// <summary>
        /// 品牌后缀（Eg:奥迪）
        /// </summary>
        public string BrandSuffix { get; set; }

        /// <summary>
        /// 品牌图片Url
        /// </summary>
        public string BrandUrl { get; set; }

        /// <summary>
        /// 品牌热度：大于0 为热门品牌
        /// </summary>
        public int HotLevel { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }
    }
}
