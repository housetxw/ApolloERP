using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Client.Model.BaoYang
{
    public class BaoYangTypeConfigDto
    {
        /// <summary>
        /// 保养类型BaoYangType
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
    }
}
