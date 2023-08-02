using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// AddBaoYangTypeConfigRequest
    /// </summary>
    public class AddBaoYangTypeConfigRequest
    {
        /// <summary>
        /// 保养类型BaoYangType
        /// </summary>
        [Required(ErrorMessage = "BaoYangType不能为空")]
        public string BaoYangType { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        [Required(ErrorMessage = "DisplayName不能为空")]
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
        [Required(ErrorMessage = "TypeGroup不能为空")]
        public string TypeGroup { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
