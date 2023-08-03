using System;
using System.Collections.Generic;
using System.Text;
using Ae.ShopOrder.Service.Core.Model.Order;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    public class GetWareHouseTransferResponse
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 源仓库编号
        /// </summary>
        public long SourceWarehouse { get; set; }
        /// <summary>
        /// 源仓库名称
        /// </summary>
        public string SourceWarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 目标仓编号
        /// </summary>
        public long TargetWarehouse { get; set; }
        /// <summary>
        /// 目标仓名称
        /// </summary>
        public string TargetWarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 转移单号
        /// </summary>
        public long TransferId { get; set; }
        /// <summary>
        /// 转移类型
        /// </summary>
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// 任务状态(1等待发出 2任务接收 3已发出 4已送达 5部分收货 6已收货 7已取消 8揽件失败)
        /// </summary>
        public string TaskStatus { get; set; } = string.Empty;
        /// <summary>
        /// 配送类型(1快递 2自配)
        /// </summary>
        public string DeliveryType { get; set; } = string.Empty;
        /// <summary>
        /// 期望发货时间
        /// </summary>
        public DateTime ExpectSendTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 期望送达时间
        /// </summary>
        public DateTime ExpectArriveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 货物配送人
        /// </summary>
        public string AcceptBy { get; set; } = string.Empty;
        /// <summary>
        /// 货物配送接收时间
        /// </summary>
        public DateTime AcceptTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 对账状态
        /// </summary>
        public string CheckState { get; set; } = string.Empty;
        /// <summary>
        /// 干线编号
        /// </summary>
        public long DeliveryLineId { get; set; }
        /// <summary>
        /// 干线名称
        /// </summary>
        public string DeliveryLine { get; set; } = string.Empty;
        /// <summary>
        /// 发出时间
        /// </summary>
        public DateTime SendTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 到达时间
        /// </summary>
        public DateTime ArriveTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 货物类型(1良品 2不良品)
        /// </summary>
        public sbyte StockType { get; set; }
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
        /// 产品的信息
        /// </summary>
        public List<GetWareHouseTransferProductVO> Products { get; set; }

        /// <summary>
        /// 包裹的信息
        /// </summary>
        public List<GetWareHouseTransferPackageVO> Packages { get; set; }
    }
}
