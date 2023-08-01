using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class VerifyAdaptiveProductForBuyRequest
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        [Required(ErrorMessage = "商品Pid不能为空")]
        public string Pid { get; set; }

        /// <summary>
        /// 最小类目Id
        /// </summary>
        [Required(ErrorMessage = "商品类目Id不能为空")]
        public int CategoryId { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        [Required(ErrorMessage = "Vehicle不能为空")]
        public VehicleRequest Vehicle { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }
    }
}
