using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model
{
    public class AsnDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public long LocationId { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string Location { get; set; } = string.Empty;
        /// <summary>
        /// 关联单号
        /// </summary>
        public long AsnNo { get; set; }
        /// <summary>
        /// 关联类型(1采购单 2退货单)
        /// </summary>
        public string AsnType { get; set; } = string.Empty;
        /// <summary>
        /// 状态(1新建 2关闭 3取消 4部分收货 5已收货)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 发货方编号
        /// </summary>
        public long SenderId { get; set; }
        /// <summary>
        /// 发货方名称
        /// </summary>
        public string Sender { get; set; } = string.Empty;
        /// <summary>
        /// 发货地址
        /// </summary>
        public string SendAddress { get; set; } = string.Empty;
        /// <summary>
        /// 最早到达时间
        /// </summary>
        public DateTime EarliestArrivalTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 最迟到达时间
        /// </summary>
        public DateTime LatestArrivalTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 货主
        /// </summary>
        public int OwnerId { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        public string Channel { get; set; } = string.Empty;
        /// <summary>
        /// 货主类型
        /// </summary>
        public string OwnerType { get; set; } = string.Empty;
        /// <summary>
        /// 货主名称
        /// </summary>
        public string OwnerName { get; set; } = string.Empty;
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


    public class AsnProductDTO
    {
        public string Remark { get; set; }
        public long AsnNo { get; set; }

        public string AsnType { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// asn单号
        /// </summary>
        public long AsnId { get; set; }
        /// <summary>
        /// 产品单号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 成本
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// 状态(1新建 2关闭 3取消 4部分收货 5已收货)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 外联单号(采购子单,退货子单)
        /// </summary>
        public string RefNo { get; set; } = string.Empty;
        /// <summary>
        /// 入库单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 税率
        /// </summary>
        public int TaxRate { get; set; }
        /// <summary>
        /// 无税成本
        /// </summary>
        public decimal NoTaxCost { get; set; }
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
