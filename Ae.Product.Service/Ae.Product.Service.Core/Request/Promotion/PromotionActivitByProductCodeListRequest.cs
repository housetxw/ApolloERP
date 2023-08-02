using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Promotion
{
    /// <summary>
    /// 根据Pid查询促销活动
    /// </summary>
    public class PromotionActivitByProductCodeListRequest
    {
        /// <summary>
        /// 商品PidList 最大限制50
        /// </summary>
        [MaxLength(50, ErrorMessage = "批量查询最多支持50")]
        public List<string> ProductCodeList { get; set; }

        /// <summary>
        /// 渠道 ：小程序 MiniApp  门店管家 ShopApp
        /// </summary>
        [Required(ErrorMessage ="渠道不能为空")]
        public string PromotionChannel { get; set; }
    }
}
