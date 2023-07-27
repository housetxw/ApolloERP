using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class ModifyShopConfigInfoRequest
    {
        /// <summary>
        /// 门店id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        /// 营业开始时间
        /// </summary>
        public DateTime StartWorkTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 营业结束时间
        /// </summary>
        public DateTime EndWorkTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 正式上线时间
        /// </summary>
        public DateTime OnlineTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 租赁开始日期
        /// </summary>
        public DateTime LeaseStartDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 租赁结束日期
        /// </summary>
        public DateTime LeaseEndDate { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 汽车故障诊断仪
        /// </summary>
        public sbyte CarFaultDiagnosticTool { get; set; }
        /// <summary>
        /// 是否有休息室
        /// </summary>
        public sbyte IsLoungeRoom { get; set; }
        /// <summary>
        /// 是否有卫生间
        /// </summary>
        public sbyte IsRestRoom { get; set; }
        /// <summary>
        /// 是否免费wifi
        /// </summary>
        public sbyte IsFreeWifi { get; set; }
        /// <summary>
        /// 是否柱式举升机
        /// </summary>
        public sbyte IsPostLift { get; set; }
        /// <summary>
        /// 类型 0:BOSS 1:H5
        /// </summary>
        public int Type { get; set; } = 0;
        /// <summary>
        /// 暂停营业 开始时间
        /// </summary>
        public DateTime SuspendStartDateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 暂停营业 结束时间
        /// </summary>
        public DateTime SuspendEndDateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
