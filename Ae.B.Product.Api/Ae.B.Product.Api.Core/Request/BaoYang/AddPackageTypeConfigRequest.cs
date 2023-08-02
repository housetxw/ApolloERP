using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// AddPackageTypeConfigRequest
    /// </summary>
    public class AddPackageTypeConfigRequest
    {
        /// <summary>
        /// PackageType
        /// </summary>
        [Required(ErrorMessage = "PackageType不能为空")]
        public string PackageType { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        [Required(ErrorMessage = "DisplayName不能为空")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 建议提示
        /// </summary>
        public string SuggestTip { get; set; }

        /// <summary>
        /// 简要描述
        /// </summary>
        public string BriefDescription { get; set; }

        /// <summary>
        /// 描述链接url
        /// </summary>
        public string DescriptionLink { get; set; }

        /// <summary>
        /// 详细描述
        /// </summary>
        public string DetailDescription { get; set; }

        /// <summary>
        /// 适配展示商品类型：0套餐  1单品  2套餐&单品
        /// </summary>
        public int ProductType { get; set; }

        /// <summary>
        /// 图片logo
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 排序显示
        /// </summary>
        public int DisplayIndex { get; set; }

        /// <summary>
        /// 服务pid
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// BaoYangType
        /// </summary>
        public List<string> BaoYangType { get; set; }

        /// <summary>
        /// 一级分类
        /// </summary>
        public string CategoryAlias { get; set; }
    }
}
