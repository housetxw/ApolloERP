using System;
using System.Collections.Generic;
using System.Text;
using Ae.Order.Service.Core.Enums;

namespace Ae.Order.Service.Dal.Condition.ShopOrder
{
    /// <summary>
    /// 查询主订单信息根据状态 请求对象
    /// </summary>
    public class GetMainOrdersForStatusCondition : BasePageCondition
    {
        /// <summary>
        /// 门店ShopId
        /// </summary>

        public int ShopId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderBusinessStatusEnum OrderBusinessStatus { get; set; }

        /// <summary>
        /// 搜索的类型OrderNo、Telephone、ProductName
        /// </summary>
        public GetOrdersTypeEnum SearchType { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建开始时间
        /// </summary>
        public string StartOrderTime { get; set; }

        /// <summary>
        /// 创建结束时间
        /// </summary>
        public string EndOrderTime { get; set; }

        /// <summary>
        /// 开始安装时间
        /// </summary>
        public string StartInstalledTime { get; set; }

        /// <summary>
        /// 结束安装时间
        /// </summary>
        public string EndInstalledTime { get; set; }


        /// <summary>
        /// 订单渠道
        /// </summary>
        public List<int> Channels { get; set; }

        ///// <summary>
        ///// 订单渠道
        ///// </summary>
        //public List<int> Channels { get; set; }

        public List<int> ProductTypes { get; set; }
    }
}
