using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Order
{
    public class OrderInsuranceCompanyDTO
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单号（RGC或RGS前缀）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 保险公司名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 关联单号
        /// </summary>
        public string RefNo { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
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
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
