using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request
{
   public class CreateAccountCheckRequest
    {
        public List<CreateAccountCheckDTO> Accounts { get; set; } = new List<CreateAccountCheckDTO>();
    }

    public class CreateAccountCheckDTO {

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 门店编号
        /// </summary>
        public long LocationId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string LocationName { get; set; } = string.Empty;
        /// <summary>
        /// 对账单类型()
        /// </summary>
        public string AccountType { get; set; } = string.Empty;
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime InstallTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PayMethod { get; set; } = string.Empty;
        /// <summary>
        /// 到账状态
        /// </summary>
        public string MoneyArriveStatus { get; set; } = string.Empty;
        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; } = string.Empty;
        /// <summary>
        /// 销售总价
        /// </summary>
        public decimal SaleTotalAmount { get; set; }
        /// <summary>
        /// 成本
        /// </summary>
        public decimal TotalCost { get; set; }
        /// <summary>
        /// 服务费
        /// </summary>
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 手续费(前期是固定的)
        /// </summary>
        public decimal CommissionFee { get; set; }
        /// <summary>
        /// 其他费用
        /// </summary>
        public decimal OtherFee { get; set; }
        /// <summary>
        /// 结算价
        /// </summary>
        public decimal SettlementFee { get; set; }

        /// <summary>
        /// 门店核对结果
        /// </summary>
        public string ShopCheckResult { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
    }
}
