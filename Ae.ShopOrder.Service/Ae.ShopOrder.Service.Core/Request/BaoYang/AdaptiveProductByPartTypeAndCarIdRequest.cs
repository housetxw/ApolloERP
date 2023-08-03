using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.BaoYang
{
    /// <summary>
    /// 根据carId 和 parType 适配默认商品request
    /// </summary>
    public class AdaptiveProductByPartTypeAndCarIdRequest
    {
        /// <summary>
        /// CarId
        /// </summary>
        [Required(ErrorMessage = "CarId不能为空")]
        public string CarId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }

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
