using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Product
{
    public class GrouponProductPageListClientRequest
    {
        /// <summary>
        /// 是否上架
        /// </summary>
        public sbyte? OnSale { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Key { get; set; }

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
