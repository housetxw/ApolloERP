using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    /// <summary>
    /// 物流包裹信息
    /// </summary>
    public class GetBatchWarehouseTransferPackagesDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 移库单ID
        /// </summary>
        public string TransferId { get; set; }

        /// <summary>
        /// 移库类型
        /// </summary>
        public string TransferType { get; set; }

        /// <summary>
        /// 物流方式
        /// </summary>
        public string DeliveryType { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        public  string DeliveryCode { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string DeliveryCompany { get; set; }


        /// <summary>
        /// 运费
        /// </summary>
        public string DeliveryFee { get; set; }

        /// <summary>
        /// 物重量
        /// </summary>
        public string DeliveryWeight { get; set; }

        /// <summary>
        /// 物流电话
        /// </summary>
        public string DeliveryTel { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string UpdateTime { get; set; }
    }
}
