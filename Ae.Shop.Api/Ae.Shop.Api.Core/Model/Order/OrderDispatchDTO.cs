using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Order
{
    public class OrderDispatchDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 技师Id
        /// </summary>
        public string TechId { get; set; } = string.Empty;
        /// <summary>
        /// 技师姓名
        /// </summary>
        public string TechName { get; set; } = string.Empty;
        /// <summary>
        /// 职级
        /// </summary>
        public string Level { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
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

        /// <summary>
        /// 技师电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 比例
        /// </summary>
        public Decimal Percent { get; set; }
    }
}
