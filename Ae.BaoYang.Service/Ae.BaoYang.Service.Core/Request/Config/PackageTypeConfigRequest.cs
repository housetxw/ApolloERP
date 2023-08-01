using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// PackageTypeConfigRequest
    /// </summary>
    public class PackageTypeConfigRequest : BasePageRequest
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
        /// 一级分类
        /// </summary>
        public string CategoryAlias { get; set; }
    }
}
