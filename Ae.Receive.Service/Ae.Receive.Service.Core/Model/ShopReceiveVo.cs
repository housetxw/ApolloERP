using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    /// <summary>
    /// 到店记录Model
    /// </summary>
    public class ShopReceiveVo
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
