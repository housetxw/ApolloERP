using Ae.Receive.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    public class GetReserveInfoResponse
    {
        /// <summary>
        /// 可预约订单
        /// </summary>
        public CanReserveOrderDTO ReserveOrders { get; set; }
        /// <summary>
        /// 车型信息
        /// </summary>
        public MyCarInfoDTO CarInfo { get; set; }
        /// <summary>
        /// 门店信息
        /// </summary>
        public ReserveShopInfoDTO ShopInfo { get; set; }
        /// <summary>
        /// 可预约日期
        /// </summary>
        public List<ReserveDateDTO> CanReserveDate { get; set; }
        /// <summary>
        /// 时间节点
        /// </summary>
        public List<ReserveTimeDTO> ReserveTimeList { get; set; }
        /// <summary>
        /// 门店服务项目
        /// </summary>
        public List<ShopServiceDTO> ShopServiceItems { get; set; }
        /// <summary>
        /// 预约说明
        /// </summary>
        public string ReserveExplain { get; set; }
    }
}
