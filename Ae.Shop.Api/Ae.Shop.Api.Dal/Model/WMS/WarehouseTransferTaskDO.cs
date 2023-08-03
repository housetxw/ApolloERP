using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Api.Dal.Model
{
    [Table("warehouse_transfer_task")]
    public class WarehouseTransferTaskDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 源仓库编号
        /// </summary>
        [Column("source_warehouse")]
        public long SourceWarehouse { get; set; }
        /// <summary>
        /// 源仓库名称
        /// </summary>
        [Column("source_warehouse_name")]
        public string SourceWarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 目标仓编号
        /// </summary>
        [Column("target_warehouse")]
        public long TargetWarehouse { get; set; }
        /// <summary>
        /// 目标仓名称
        /// </summary>
        [Column("target_warehouse_name")]
        public string TargetWarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 转移单号
        /// </summary>
        [Column("transfer_id")]
        public string TransferId { get; set; } = string.Empty;
        /// <summary>
        /// 转移类型
        /// </summary>
        [Column("transfer_type")]
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// 任务状态(1等待发出 2任务接收 3已发出 4已送达 5部分收货 6已收货 7已取消 8揽件失败)
        /// </summary>
        [Column("task_status")]
        public string TaskStatus { get; set; } = string.Empty;
        /// <summary>
        /// 配送类型(1快递 2自配)
        /// </summary>
        [Column("delivery_type")]
        public string DeliveryType { get; set; } = string.Empty;
        /// <summary>
        /// 期望发货时间
        /// </summary>
        [Column("expect_send_time")]
        public DateTime ExpectSendTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 期望送达时间
        /// </summary>
        [Column("expect_arrive_time")]
        public DateTime ExpectArriveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 货物配送人
        /// </summary>
        [Column("accept_by")]
        public string AcceptBy { get; set; } = string.Empty;
        /// <summary>
        /// 货物配送接收时间
        /// </summary>
        [Column("accept_time")]
        public DateTime AcceptTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 对账状态
        /// </summary>
        [Column("check_state")]
        public string CheckState { get; set; } = string.Empty;
        /// <summary>
        /// 干线编号
        /// </summary>
        [Column("delivery_line_id")]
        public long DeliveryLineId { get; set; }
        /// <summary>
        /// 干线名称
        /// </summary>
        [Column("delivery_line")]
        public string DeliveryLine { get; set; } = string.Empty;
        /// <summary>
        /// 发出时间
        /// </summary>
        [Column("send_time")]
        public DateTime SendTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 到达时间
        /// </summary>
        [Column("arrive_time")]
        public DateTime ArriveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 货物类型(1良品 2不良品)
        /// </summary>
        [Column("stock_type")]
        public sbyte StockType { get; set; }
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
    }
}