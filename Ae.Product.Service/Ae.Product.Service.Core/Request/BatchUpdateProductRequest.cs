using Ae.Product.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class BatchUpdateProductRequest
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public List<ProductBaseInfoVo> Products { get; set; }
        /// <summary>
        /// 更新字段
        /// </summary>
        public List<string> UpdateFilds { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
