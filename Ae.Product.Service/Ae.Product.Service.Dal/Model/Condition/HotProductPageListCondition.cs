using Ae.Product.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Condition
{
    public class HotProductPageListCondition
    {
        /// <summary>
        /// 终端类型
        /// </summary>
        public TerminalType? TerminalType { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 类目
        /// </summary>
        public List<int> CategoryId { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public sbyte? OnSale { get; set; }

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
