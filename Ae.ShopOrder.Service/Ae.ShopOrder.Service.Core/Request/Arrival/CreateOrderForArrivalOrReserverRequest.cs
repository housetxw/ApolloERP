using Ae.ShopOrder.Service.Core.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Arrival
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderForArrivalOrReserverRequest
    {
        /// <summary>
        /// 入口类型（0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他）
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
        /// 促销类型MiniApp,ShopApp
        /// </summary>
        public string PromotionChannel { get; set; }

        /// <summary>
        /// 项目内容
        /// </summary>
        public List<ProjectItemVo> Items { get; set; }
    }
}
