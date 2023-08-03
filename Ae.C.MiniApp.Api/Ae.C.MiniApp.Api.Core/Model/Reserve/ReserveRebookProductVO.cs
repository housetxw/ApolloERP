using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class ReserveRebookProductVO
    {
        /// <summary>
        /// 类目Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }
        /// <summary>
        /// 缩率图
        /// </summary>
        public string Icon { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// 促销活动ID
        /// </summary>
        public string ActivityId { get; set; } = string.Empty;
    }
}
