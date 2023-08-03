using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    public class ReserveTrackDTO
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
