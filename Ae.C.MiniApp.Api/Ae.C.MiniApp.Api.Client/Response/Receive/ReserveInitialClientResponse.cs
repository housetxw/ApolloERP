using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class ReserveInitialClientResponse
    {
        /// <summary>
        /// 可预约订单
        /// </summary>
        public CanReserveOrderDTO ReserveOrder { get; set; }
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
