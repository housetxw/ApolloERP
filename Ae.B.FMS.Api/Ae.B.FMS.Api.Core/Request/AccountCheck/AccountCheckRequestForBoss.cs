using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Request
{
   public class AccountCheckRequestForBoss
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
            /// 门店编号
            /// </summary>
            public long LocationId { get; set; }
            /// <summary>
            /// 门店名称
            /// </summary>
            public string LocationName { get; set; } = string.Empty;
            /// <summary>
            /// 对账单类型(风险单)
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
            /// 门店对账状态
            /// </summary>
            public string ShopCheckResult { get; set; } = string.Empty;
            /// <summary>
            /// 门店核对时间
            /// </summary>
            public DateTime ShopCheckTime { get; set; } = new DateTime(1900, 1, 1);
            /// <summary>
            /// 总部对账状态
            /// </summary>
            public string RgCheckResult { get; set; } = string.Empty;
            /// <summary>
            /// 总部核对时间
            /// </summary>
            public DateTime RgCheckTime { get; set; } = new DateTime(1900, 1, 1);
            /// <summary>
            /// 对账状态(备用)
            /// </summary>
            public string Status { get; set; } = string.Empty;
            /// <summary>
            /// 提现状态(已申请)
            /// </summary>
            public string WithdrawStatus { get; set; } = string.Empty;
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
