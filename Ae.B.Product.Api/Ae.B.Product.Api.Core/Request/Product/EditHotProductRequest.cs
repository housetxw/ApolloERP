using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    /// <summary>
    /// EditHotProductRequest
    /// </summary>
    public class EditHotProductRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Rank { get; set; }

        /// <summary>
        /// 标记删除
        /// </summary>
        public sbyte? IsDeleted { get; set; }
    }
}
