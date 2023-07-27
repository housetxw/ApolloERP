using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Model.Receive
{
    public class ShopReceiveDto
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public long RecId { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 到店时间
        /// </summary>
        public DateTime ArriveTime { get; set; }
    }
}
