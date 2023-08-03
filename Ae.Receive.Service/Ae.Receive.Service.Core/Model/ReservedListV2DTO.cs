using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    public class ReservedListV2DTO
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
        /// 车辆信息  
        /// </summary>
        public string VehicleName { get; set; }
        /// <summary>
        /// 预约状态 Unconfirmed：待确认 Confirmed：已确认 Completed：已完成 Canceled：已取消
        /// </summary>
        public string ReserveStatus { get; set; }
        /// <summary>
        /// 取消按钮
        /// </summary>
        public bool IsCancelButton { get; set; }
        /// <summary>
        /// 重新预约按钮
        /// </summary>
        public bool IsReserveButton { get; set; }
        /// <summary>
        /// 支付按钮
        /// </summary>
        public bool DisplayPayButton { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}
