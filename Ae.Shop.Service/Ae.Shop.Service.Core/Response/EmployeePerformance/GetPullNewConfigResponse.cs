using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    /// <summary>
    /// 拉新配置
    /// </summary>
  public  class GetPullNewConfigResponse
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 拉新开关
        /// </summary>
        public bool PullNewFlag { get; set; }

        /// <summary>
        /// 拉新奖励点数
        /// </summary>
        public decimal PullNewPoint { get; set; }
        /// <summary>
        /// 首次消费开关
        /// </summary>
        public bool FirstConsumeFlag { get; set; }
        /// <summary>
        /// 消费配置类型(按比例1,固定金额2)
        /// </summary>
        public sbyte FirstConsumeType { get; set; }
        /// <summary>
        /// 绩效点/固定金额
        /// </summary>
        public decimal FirstConsumePoint { get; set; }
   }
}
