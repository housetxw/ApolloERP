using System;
using System.Collections.Generic;
using System.Text;
using Ae.Order.Service.Core.Enums;

namespace Ae.Order.Service.Dal.Condition.ShopOrder
{
    public class GetMainOrdersForSearchCondition : BasePageCondition
    {
        /// <summary>
        /// 门店ShopId
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 搜索的类型
        /// </summary>
        public GetOrdersTypeEnum SearchType { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 订单渠道
        /// </summary>
        public List<int> Channels { get; set; }
    }
}
