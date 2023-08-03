using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
   public class ProductClaimRecordDTO
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
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 技师编号
        /// </summary>
        public string TechId { get; set; } = string.Empty;
        /// <summary>
        /// 技师名称
        /// </summary>
        public string TechName { get; set; } = string.Empty;
        /// <summary>
        /// 领取人
        /// </summary>
        public string ReceiveName { get; set; } = string.Empty;
        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime ReceiveTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 状态(新建/部分退货/已退货)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
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

        public List<TechClaimProductDTO> Products { get; set; } = new List<TechClaimProductDTO>();

        public string JoinText { get; set; } = string.Empty;

    }
}
