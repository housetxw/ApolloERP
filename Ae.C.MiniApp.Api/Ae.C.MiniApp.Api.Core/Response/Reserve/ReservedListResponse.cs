using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class ReservedListResponse: MyCarInfoVO
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }
        /// <summary>
        /// 预约编号
        /// </summary>
        public int ReserveNO { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserveTime { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopSimpleName { get; set; }
        /// <summary>
        /// 预约项目
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNO { get; set; }
        /// <summary>
        /// 预约状态 Unconfirmed：待确认 Confirmed：已确认 Completed：已完成 Canceled：已取消
        /// </summary>
        public string ReserveStatus { get; set; }
        /// <summary>
        /// 是否可展开
        /// </summary>
        public bool IsDisplay { get; set; }
        /// <summary>
        /// 0到店服务  1上门服务
        /// </summary>
        public sbyte Type { get; set; }
    }
}
