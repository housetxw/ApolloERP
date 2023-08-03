using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model.Reserve
{
    public class ReserveListDto
    {
        /// <summary>
        /// 预约Id
        /// </summary>
        public long ReserveId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 预约渠道
        /// </summary>
        public string ReserveChannel { get; set; }

        /// <summary>
        /// 预约类型
        /// </summary>
        public string ReserveTypeName { get; set; }

        /// <summary>
        /// 预约技师
        /// </summary>
        public string Technician { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 车型展示
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 已取消Canceled/已到店Completed/逾期未到店OverdueNotArrive/逾期已到店OverdueAndArrive/有效状态Valid
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 状态显示
        /// </summary>
        public string StatusDisplay { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserveTime { get; set; }
    }
}
