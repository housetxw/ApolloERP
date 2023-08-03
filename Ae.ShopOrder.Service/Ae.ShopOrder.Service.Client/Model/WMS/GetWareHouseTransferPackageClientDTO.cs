using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Model.WMS
{
    /// <summary>
    /// 包裹信息
    /// </summary>
    public class GetWareHouseTransferPackageClientDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        public string TransferId { get; set; }
        /// <summary>
        /// 出库类型(1订单 2移库)
        /// </summary>
        public string TransferType { get; set; } = string.Empty;
        /// <summary>
        /// 配送类型(快递 自配)
        /// </summary>
        public string DeliveryType { get; set; } = string.Empty;
        /// <summary>
        /// 快递单号
        /// </summary>
        public string DeliveryCode { get; set; } = string.Empty;
        /// <summary>
        /// 快递公司
        /// </summary>
        public string DeliveryCompany { get; set; } = string.Empty;
        /// <summary>
        /// 快递费用
        /// </summary>
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// 快递重量
        /// </summary>
        public string DeliveryWeight { get; set; } = string.Empty;
        /// <summary>
        /// 快递电话
        /// </summary>
        public string DeliveryTel { get; set; } = string.Empty;
        /// <summary>
        /// 状态(新建 已发出 已签收 已收货)
        /// </summary>
        public string Status { get; set; } = string.Empty;
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
