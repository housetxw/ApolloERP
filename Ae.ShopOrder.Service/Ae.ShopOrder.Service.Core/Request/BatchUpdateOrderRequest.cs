using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request
{
    /// <summary>
    /// 批次更新订单通用
    /// </summary>
    public class BatchUpdateOrderRequest
    {
        [Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> OrderNos { get; set; } 

        /// <summary>
        ///  订单日期,一次最多更新一天数据
        /// </summary>
        [Required(ErrorMessage = "日期不能为空")]
        public string OrderDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
    }

}
