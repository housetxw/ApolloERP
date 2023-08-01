using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// 根据配件类型查询适配商品
    /// </summary>
    public class AdaptiveProductByPartTypeRequest
    {
        /// <summary>
        /// 车型
        /// </summary>
        [Required(ErrorMessage = "Vehicle不能为空")]
        public VehicleRequest Vehicle { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 配件数据
        /// </summary>
        public List<BaoYangPartRequest> BaoYangPart { get; set; }
    }

    /// <summary>
    /// 配件类型
    /// </summary>
    public class BaoYangPartRequest
    {
        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartType { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public List<string> BrandList { get; set; }
    }
}
