using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.Extend
{
    public class ReceiveListWithCondition
    {
        public int ShopId { get; set; }

        /// <summary>
        /// 预约类型
        /// </summary>
        public string ReserveType { get; set; }

        /// <summary>
        /// 是否隐藏已取消的
        /// </summary>
        public bool HideCanceled { get; set; }

        public DateTime? StarTime { get; set; }

        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 技师Id
        /// </summary>
        public string TechId { get; set; }
    }
}
