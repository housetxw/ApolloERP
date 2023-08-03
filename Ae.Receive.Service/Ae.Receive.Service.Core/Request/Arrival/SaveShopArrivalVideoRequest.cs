using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class SaveShopArrivalVideoRequest
    {
        /// <summary>
        /// 到店id
        /// </summary>

        public long ArrivalId { get; set; }
        /// <summary>
        /// 视频路径
        /// </summary>

        public string VideoPath { get; set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>

        public string VideoName { get; set; } = string.Empty;
        /// <summary>
        /// 分钟
        /// </summary>

        public int Duration { get; set; }

        /// <summary>
        /// 存储空间
        /// </summary>

        public long FileSize { get; set; } = 0;

        public string CreateBy { get; set; }
    }
}
