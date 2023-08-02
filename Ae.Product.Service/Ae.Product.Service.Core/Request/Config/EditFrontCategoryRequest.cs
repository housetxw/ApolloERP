using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
{
    /// <summary>
    /// EditFrontCategoryRequest
    /// </summary>
    public class EditFrontCategoryRequest
    {
        /// <summary>
        /// id
        /// </summary>
        [Required(ErrorMessage = "类目Id必须大于0")]
        public long CategoryId { get; set; }

        /// <summary>
        /// 分类code
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
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
        public int? ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Rank { get; set; }

        /// <summary>
        /// 扩展参数
        /// </summary>
        public string ExtendParam { get; set; }

        /// <summary>
        /// 关联产品
        /// </summary>
        public List<FrontCategoryProductRequest> Product { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public sbyte? IsDeleted { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
    }
}
