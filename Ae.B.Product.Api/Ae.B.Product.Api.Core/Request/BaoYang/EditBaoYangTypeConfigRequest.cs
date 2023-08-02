using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// EditBaoYangTypeConfigRequest
    /// </summary>
    public class EditBaoYangTypeConfigRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "Id必须大于0")]
        public int Id { get; set; }

        /// <summary>
        /// 保养类型BaoYangType
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 产品类目
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 最小商品类目 ,分割
        /// </summary>
        public string ChildCategories { get; set; }

        /// <summary>
        /// 组别
        /// </summary>
        public string TypeGroup { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool? IsDeleted { get; set; }
    }
}
