using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model.Reserve
{
    public class ShopReserveListDto
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
        /// 客户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 状态显示
        /// </summary>
        public string StatusDisplay { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 渠道显示
        /// </summary>
        public string ChannelDisplay { get; set; }

        /// <summary>
        /// 预约渠道
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 预约类型
        /// </summary>
        public string ReserveType { get; set; }

        /// <summary>
        /// 预约类型显示
        /// </summary>
        public string ReserveTypeDisplay { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserveTime { get; set; }

        /// <summary>
        /// 预约技师Id
        /// </summary>
        public string TechId { get; set; }

        /// <summary>
        /// 预约技师
        /// </summary>
        public string TechName { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 车型展示
        /// </summary>
        public string CarType { get; set; }
    }
}
