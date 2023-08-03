using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request
{
    /// <summary>
    /// 多条件查询订单
    /// </summary>
    public class GetOrderListRequest
    {
        /// <summary>
        /// 门店ShopId
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 订单号集合
        /// </summary>
        public List<string> OrderNos { get; set; }

        /// <summary>
        ///  开始日期
        /// </summary>
        //[Required(ErrorMessage = "开始日期不能为空")]
        public DateTime StartDate { get; set; }

        /// <summary>
        ///  结束日期
        /// </summary>
        //[Required(ErrorMessage = "结束日期不能为空")]
        public DateTime EndDate { get; set; }
    }
}
