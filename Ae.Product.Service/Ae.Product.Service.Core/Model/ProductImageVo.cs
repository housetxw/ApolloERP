using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model
{
    public class ProductImageVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商品id
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 标记删除
        /// </summary>
        public sbyte IsDeleted { get; set; }
    
    }
}
