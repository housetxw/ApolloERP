using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Service
{
    /// <summary>
    /// 维修保养记录信息
    /// </summary>
    public class MaintenanceRecordVo
    {
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// 标题名称
        /// </summary>
        public string TitleName { get; set; } = string.Empty;

        /// <summary>
        /// 到店的日期
        /// </summary>
        public string ArriveDate { get; set; } = string.Empty;

        /// <summary>
        /// 到店记录Id
        /// </summary>
        public string ArriveShopRecordId { get; set; } = string.Empty;

        /// <summary>
        /// 服务的名称
        /// </summary>
        public string ServiceName { get; set; } = string.Empty;

        /// <summary>
        /// 服务的门店
        /// </summary>
        public string ServiceShop { get; set; } = string.Empty;

        /// <summary>
        /// 预约编号
        /// </summary>
        public string ReserveNumber { get; set; } = string.Empty;

        /// <summary>
        /// 排队编号
        /// </summary>
        public string QueueNumber { get; set; } = string.Empty;

        /// <summary>
        /// 服务的技师
        /// </summary>
        public string ServiceTechnician { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 维修保养订单信息
        /// </summary>
        public List<MaintenanceRecordVo> MaintenanceOrderInfo { get; set; } = null;
    }
}
