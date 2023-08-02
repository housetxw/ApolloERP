using Ae.B.Activity.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Request
{
   public class GetPromoteContentsClientRequest: BasePageRequest
    {
        public long Id { get; set; }

        /// <summary>
        /// 类型（0未设置 1文章 2海报 3段子）
        /// </summary>
        public sbyte Type { get; set; }

        /// <summary>
        /// 上下架状态（0下架 1上架 2所有）
        /// </summary>
        public sbyte OnlineStatus { get; set; }

        /// <summary>
        /// 审核状态（0待审核 1审核通过 2审核不通过 3所有）
        /// </summary>
        public sbyte CheckStatus { get; set; } = 1;

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }


        public string Title { get; set; }
             
    }
}
