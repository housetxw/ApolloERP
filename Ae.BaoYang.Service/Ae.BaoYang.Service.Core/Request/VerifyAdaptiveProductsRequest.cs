using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// 校验商品是否适配
    /// </summary>
    public class VerifyAdaptiveProductsRequest
    {
        /// <summary>
        /// 车型
        /// </summary>
        [Required(ErrorMessage = "Vehicle不能为空")]
        public VehicleRequest Vehicle { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        [Required(ErrorMessage = "商品不能为空")]
        [MinLength(1, ErrorMessage = "商品不能为空")]
        public List<ProductRequest> Products { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ProductRequest
    {
        /// <summary>
        /// 商品pid
        /// </summary>
        [Required(ErrorMessage = "商品Pid不能为空")]
        public string Pid { get; set; }

        /// <summary>
        /// 最小类目Id
        /// </summary>
        [Required(ErrorMessage = "商品类目Id不能为空")]
        public string CategoryId { get; set; }
    }
}
