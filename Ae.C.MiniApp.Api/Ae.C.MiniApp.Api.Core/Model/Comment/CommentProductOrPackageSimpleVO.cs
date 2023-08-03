﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    /// <summary>
    /// 评论单品、服务或套餐简单视图类
    /// </summary>
    public class CommentProductOrPackageSimpleVO
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 售价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 单位（个/升/斤等）
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
    }
}
