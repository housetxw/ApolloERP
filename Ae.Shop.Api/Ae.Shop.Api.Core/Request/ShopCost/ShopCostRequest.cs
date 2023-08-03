using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopCost
{
    public class ShopCostRequest: BasePageRequest
    {
        /// <summary>
        /// 费用类别
        /// </summary>
        public string CostType { get; set; }
        /// <summary>
        /// 支付渠道
        /// </summary>
        public string PayChannel { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }


    }
}
