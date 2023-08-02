using Ae.Product.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
{
    /// <summary>
    /// AddFrontCategoryRequest
    /// </summary>
    public class AddFrontCategoryRequest
    {
        /// <summary>
        /// 分类code
        /// </summary>
        [Required(ErrorMessage = "分类code不能为空")]
        public string Category { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 图片url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 二级标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 终端类型
        /// </summary>
        public TerminalType TerminalType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; } = 1;

        /// <summary>
        /// 扩展参数
        /// </summary>
        public string ExtendParam { get; set; }

        /// <summary>
        /// 关联产品
        /// </summary>
        public List<FrontCategoryProductRequest> Product { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// FrontCategoryProductRequest
    /// </summary>
    public class FrontCategoryProductRequest
    {
        /// <summary>
        /// 类型
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 对应产品/类目/品牌等id
        /// </summary>
        public string ProductId { get; set; }
    }
}
