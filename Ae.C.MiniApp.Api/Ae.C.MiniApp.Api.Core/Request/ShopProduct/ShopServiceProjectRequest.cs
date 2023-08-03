using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.ShopProduct
{
    /// <summary>
    /// 获取门店服务项目
    /// </summary>
    public class ShopServiceProjectRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "门店Id必须大于0")]
        public int ShopId { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "分类Id必须大于0")]
        public int CategoryId { get; set; }
    }
}
