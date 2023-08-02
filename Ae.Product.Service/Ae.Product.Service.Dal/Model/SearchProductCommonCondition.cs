using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model
{
    public class SearchProductCommonCondition
    {
        /// <summary>
        /// 产品名称Or产品Pid
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 是否上下架 默认上架：1
        /// </summary>
        public int OnSale { get; set; } = 1;

        /// <summary>
        /// 是否零售
        /// </summary>
        public int? IsRetail { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public int? IsShow { get; set; }

        /// <summary>
        /// 产品属性  默认所有
        /// 分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品
        /// </summary>
        public List<int> ProductAttribute { get; set; }

        /// <summary>
        /// 类目
        /// </summary>
        public List<int> CategoryId { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
