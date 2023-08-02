using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ae.B.Product.Api.Core.Request
{
    /// <summary>
    /// 添加品牌请求实体
    /// </summary>
    public class AddBrandRequest
    {
        /// <summary>
        /// 品牌名
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 品牌图片Url
        /// </summary>
        public string BrandImg { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public Boolean IsForbid { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
