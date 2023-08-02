using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Product
{
    public class EditPackageCardProductClientRequest
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

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
