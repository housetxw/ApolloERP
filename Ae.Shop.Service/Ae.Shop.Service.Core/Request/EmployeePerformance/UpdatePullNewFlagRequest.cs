using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
   public class UpdatePullNewFlagRequest
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 拉新开关
        /// </summary>
        public sbyte PullNewFlag { get; set; }

        /// <summary>
        /// 首次消费开关
        /// </summary>
        public sbyte FirstConsumeFlag { get; set; }


        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 1 更新新客flag  2 更新消费flag
        /// </summary>
        public int Type { get; set; }

    }
}
