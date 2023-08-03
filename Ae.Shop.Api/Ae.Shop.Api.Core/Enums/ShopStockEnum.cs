using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Enums
{
    public enum OrderQueueStatusEnum
    {
        /// <summary>
        /// 新建
        /// </summary>
        New = 1,

        /// <summary>
        /// 未占用
        /// </summary>
        UnOccupy = 2,

        /// <summary>
        /// 已占用
        /// </summary>
        Occupy = 3,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancel = 4
    }

    public enum OrderQueueProcessStatusEnum
    {
        /// <summary>
        /// 处理中
        /// </summary>
        Processing = 1,

        /// <summary>
        /// 未处理
        /// </summary>
        Processed = 2
    }

    public enum OrderQueueTypeEnum
    {
        /// <summary>
        /// 订单服务
        /// </summary>
        OrderService = 1,

        /// <summary>
        /// 订单Queue通知
        /// </summary>
        OrderQueue = 2

    }

    public enum OccupyTypeEnum
    {
        Order = 1,
        Return
    }
}
