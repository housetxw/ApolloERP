using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Product
{
    public class DoorProductPageListClientRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 一级类目
        /// </summary>
        public int MainCategoryId { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        public int SecondCategoryId { get; set; }

        /// <summary>
        /// 三级分类
        /// </summary>
        public int ChildCategoryId { get; set; }

        /// <summary>
        /// 是否上门产品
        /// </summary>
        public bool? IsDoorProduct { get; set; }

        /// <summary>
        /// 是否包上门
        /// </summary>
        public int? FreeDoorFee { get; set; }

        /// <summary>
        /// 是否上下架
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
