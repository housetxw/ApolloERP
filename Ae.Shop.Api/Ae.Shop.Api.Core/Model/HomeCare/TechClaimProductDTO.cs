using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
   public class TechClaimProductDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 记录单号
        /// </summary>
        public long RecordId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品类型
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// 结算成本价
        /// </summary>
        public decimal CostPrice { get; set; } = 0;
        /// <summary>
        /// 领取数量
        /// </summary>
        public int Num { get; set; } = 0;
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public byte IsDeleted { get; set; }
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
