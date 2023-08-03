using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Promotion
{
    /// <summary>
    /// GrouponProductPageVo
    /// </summary>
    public class GrouponProductPageVo
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 平台售价
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// 团购价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 最小价格
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// 最大价格
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态显示
        /// </summary>
        public string StatusDisplay
        {
            get
            {
                if (Status == 0)
                {
                    return "未上架";
                }
                else
                {
                    return "已上架";
                }
            }
        }
    }
}
