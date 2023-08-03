using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Enums
{
    /// <summary>
    /// 搜索订单枚举
    /// </summary>
    public enum GetOrdersTypeEnum
    {
        /// <summary>
        /// 0不参与搜索类型
        /// </summary>
        NotSearch = 0,
        /// <summary>
        /// 1订单号
        /// </summary>
        OrderNo = 1,
        /// <summary>
        /// 2电话号码
        /// </summary>
        Telephone = 2,
        /// <summary>
        /// 3商品名称
        /// </summary>
        ProductName = 3,
    }
}
