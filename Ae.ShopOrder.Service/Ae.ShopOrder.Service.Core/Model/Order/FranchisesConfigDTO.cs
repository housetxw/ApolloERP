using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    public class FranchisesConfigDTO
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 押金所属分类（0 未设置 1：公司  2：门店）
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 加盟费
        /// </summary>
        public string Category { get; set; } = string.Empty;
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// chan
        /// </summary>
        public string Pid { get; set; } = string.Empty;
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
