using System;
using System.Collections.Generic;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using System.Text;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("warehouse_transfer_package")]
    public class WarehouseTransferPackageDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        [Column("transfer_id")]
        public string TransferId { get; set; }
        /// <summary>
        /// 出库类型(1订单 2移库)
        /// </summary>
        [Column("transfer_type")]
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// 配送类型(快递 自配)
        /// </summary>
        [Column("delivery_type")]
        public string DeliveryType { get; set; } = string.Empty;
        /// <summary>
        /// 快递单号
        /// </summary>
        [Column("delivery_code")]
        public string DeliveryCode { get; set; } = string.Empty;
        /// <summary>
        /// 快递公司
        /// </summary>
        [Column("delivery_company")]
        public string DeliveryCompany { get; set; } = string.Empty;
        /// <summary>
        /// 快递费用
        /// </summary>
        [Column("delivery_fee")]
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// 快递重量
        /// </summary>
        [Column("delivery_weight")]
        public string DeliveryWeight { get; set; } = string.Empty;
        /// <summary>
        /// 状态(新建 已发出 已签收 已清点)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 快递电话
        /// </summary>
        [Column("delivery_tel")]
        public string DeliveryTel { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);

        [NotMapped]
        public List<string> PackageNos { get; set; } = new List<string>();
    }

}
