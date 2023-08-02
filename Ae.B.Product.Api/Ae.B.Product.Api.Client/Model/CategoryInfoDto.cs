using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model
{
    /// <summary>
    /// 类目信息
    /// </summary>
    public class CategoryInfoDto
    {

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 类目Code
        /// </summary>
        public string CategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 类目Code缩写
        /// </summary>
        public string CategoryCodeShort { get; set; } = string.Empty;
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 父级id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 类型 1 后台类目 2 前台类目
        /// </summary>
        public int CategoryType { get; set; }
        /// <summary>
        /// 类型基本 
        /// </summary>
        public int CategoryLevel { get; set; }



    }
}
