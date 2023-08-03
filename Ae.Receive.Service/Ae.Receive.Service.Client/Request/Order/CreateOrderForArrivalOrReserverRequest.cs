using Ae.Receive.Service.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Order
{
    public class CreateOrderForArrivalOrReserverRequest
    {
        /// <summary>
        /// 入口类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///车辆Id
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        public string PromotionChannel { get; set; }
        /// <summary>
        /// 项目内容
        /// </summary>
        public List<ReserveProductClientDTO> Items { get; set; }
    }
}
