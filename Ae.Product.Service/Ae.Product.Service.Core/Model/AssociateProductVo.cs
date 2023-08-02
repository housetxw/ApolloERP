using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model
{
    /// <summary>
    /// 关联商品
    /// </summary>
    public class AssociateProductVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; } = string.Empty;
        /// <summary>
        /// 类目Id
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 分组属性值
        /// </summary>
        public List<string> Attributes { get; set; } 

        /// <summary>
        /// 关联商品明细
        /// </summary>
        public List<AssociateProductDetailVo> Details { get; set; }
    }
}
