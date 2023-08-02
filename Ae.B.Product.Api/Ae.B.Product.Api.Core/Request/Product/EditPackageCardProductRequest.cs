using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    public class EditPackageCardProductRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Rank { get; set; }

        /// <summary>
        /// 套餐卡类型
        /// </summary>
        public sbyte? Type { get; set; }

        /// <summary>
        /// 标记删除
        /// </summary>
        public sbyte? IsDeleted { get; set; }
    }
}
