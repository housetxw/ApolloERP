using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.Arrival
{
    public class ShopArrivalVideoResponse
    {
        /// <summary>
        /// 主键
        /// </summary>


        public long Id { get; set; }

        ///
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
 
        public long Duration { get; set; }
        /// <summary>
        /// 存储空间
        /// </summary>

        public long FileSize { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>

        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>

        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
