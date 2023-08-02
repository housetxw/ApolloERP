using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Product
{
    public class ProductInstallServiceClientRequest
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
        /// 是否配置安装服务
        /// </summary>
        public int? HasInstallService { get; set; }

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
