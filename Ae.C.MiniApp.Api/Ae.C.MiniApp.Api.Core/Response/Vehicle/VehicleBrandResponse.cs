using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    /// <summary>
    /// 车型品牌接口
    /// </summary>
    public class VehicleBrandResponse
    {
        /// <summary>
        /// 品牌前缀（Eg:A）
        /// </summary>
        public string BrandPrefix { get; set; }

        /// <summary>
        /// 分类显示文案
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 车品牌
        /// </summary>
        public List<VehicleBrandModel> BrandList { get; set; }
    }

    /// <summary>
    /// 车品牌model
    /// </summary>
    public class VehicleBrandModel
    {
        /// <summary>
        /// 品牌 （Eg:A - 奥迪）
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 品牌后缀（Eg:奥迪）
        /// </summary>
        public string BrandSuffix { get; set; }

        /// <summary>
        /// 品牌图片Url
        /// </summary>
        public string BrandUrl { get; set; }
    }
}
