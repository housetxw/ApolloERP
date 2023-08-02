using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Config
{
    public class ConfigShopPackageCardDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long CardId { get; set; }
        /// <summary>
        /// shopIds集合，分割
        /// </summary>
        public string ShopIds { get; set; } = string.Empty;
        /// <summary>
        /// 标记删除
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
