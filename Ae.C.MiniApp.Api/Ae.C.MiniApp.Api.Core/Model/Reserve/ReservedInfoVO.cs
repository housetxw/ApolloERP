using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class ReservedInfoVO : MyCarInfoVO
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
        /// 预约状态 0待确认 1已确认 2已完成 3已取消
        /// </summary>
        public string ReserveStatus { get; set; }
    }
}
