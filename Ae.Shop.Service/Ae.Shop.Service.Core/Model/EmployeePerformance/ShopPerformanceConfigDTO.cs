using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class ShopPerformanceConfigDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 绩效类型(0默认， 1单品，2小品类)
        /// </summary>
        public sbyte PerformanceType { get; set; }
        /// <summary>
        /// 类型参数（多个用逗号隔开）
        /// </summary>
        public string TypeValue { get; set; } = string.Empty;
        /// <summary>
        /// 配置类型(比例 1/金额2)
        /// </summary>
        public sbyte ConfigType { get; set; }
        /// <summary>
        /// 比例/金额
        /// </summary>
        public decimal ConfigPoint { get; set; }
        /// <summary>
        /// 配置启用时间(备用字段)
        /// </summary>
        public DateTime ConfigTime { get; set; } = new DateTime(1900, 1, 1);
       
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
   
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}