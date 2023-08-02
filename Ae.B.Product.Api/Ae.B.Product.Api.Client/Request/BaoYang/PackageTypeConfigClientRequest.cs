using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    public class PackageTypeConfigClientRequest
    {
        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 是否禁用 0：所有  1：禁用  2：启用
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// 一级分类
        /// </summary>
        public string CategoryAlias { get; set; }
    }
}
