using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Promotion
{
    /// <summary>
    /// 
    /// </summary>
    public class PromotionDetailVo : PromotionBriefVo
    {
        /// <summary>
        /// 可售数量
        /// </summary>
        public int AvailQuantity { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 状态：0进行中 1已结束
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 默认一个商品Pid
        /// </summary>
        public string DefaultPid { get; set; }
    }
}
