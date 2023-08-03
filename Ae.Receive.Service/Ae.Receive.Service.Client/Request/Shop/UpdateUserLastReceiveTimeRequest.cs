using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Shop
{
    public class UpdateUserLastReceiveTimeRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 到店记录Id
        /// </summary>
        public long ArriveId { get; set; }

        /// <summary>
        /// 到店时间
        /// </summary>
        public string ArriveTime { get; set; }

        /// <summary>
        /// 提交者
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
