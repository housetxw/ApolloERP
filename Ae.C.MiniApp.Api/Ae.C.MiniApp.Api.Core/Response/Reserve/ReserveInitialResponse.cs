using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class ReserveInitialResponse
    {
        /// <summary>
        /// 可预约订单
        /// </summary>
        public CanReserveOrderVO ReserveOrder { get; set; }
        /// <summary>
        /// 车型信息
        /// </summary>
        public MyCarInfoVO CarInfo { get; set; }
        /// <summary>
        /// 门店信息
        /// </summary>
        public ReserveShopInfoVO ShopInfo { get; set; }
        /// <summary>
        /// 可预约日期
        /// </summary>
        public List<ReserveDateVO> CanReserveDate { get; set; }
        /// <summary>
        /// 时间节点
        /// </summary>
        public List<ReserveTimeVO> ReserveTimeList { get; set; }
        /// <summary>
        /// 门店服务项目
        /// </summary>
        public List<ShopServiceVO> ShopServiceItems { get; set; }
        /// <summary>
        /// 预约说明
        /// </summary>
        public string ReserveExplain { get; set; }
    }
}
