using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
   public class CreateOrUpdatePullNewConfigRequest
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
        /// 拉新奖励点数
        /// </summary>
        public decimal PullNewPoint { get; set; }
        /// <summary>
        /// 首次消费开关
        /// </summary>
        public sbyte FirstConsumeFlag { get; set; }
        /// <summary>
        /// 消费配置类型(按比例1,固定金额2)
        /// </summary>
        public sbyte FirstConsumeType { get; set; }
        /// <summary>
        /// 绩效点/固定金额
        /// </summary>
        public decimal FirstConsumePoint { get; set; }

        public string CreateBy { get; set; } = string.Empty;
    }
}
