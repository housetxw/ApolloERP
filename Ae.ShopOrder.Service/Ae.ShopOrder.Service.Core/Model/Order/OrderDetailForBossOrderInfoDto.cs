using Ae.ShopOrder.Service.Common.Extension;
using Ae.ShopOrder.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    public class OrderDetailForBossOrderInfoDto
    {
        /// <summary>
        /// 订单号（RGB前缀）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 订单状态（10已提交 20已确认 30已完成 40已取消）
        /// </summary>
        public sbyte OrderStatus { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 渠道（0未设置 1C端 2门店）
        /// </summary>
        public sbyte Channel { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 终端类型（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网）
        /// </summary>
        public sbyte TerminalType { get; set; }
        /// <summary>
        /// 应用版本号
        /// </summary>
        public string AppVersion { get; set; } = string.Empty;
        /// <summary>
        /// 订单备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 支付状态（0未支付 1已支付）
        /// </summary>
        public sbyte PayStatus { get; set; }
        /// <summary>
        /// 是否发生过逆向申请（0否 1是）
        /// </summary>
        public sbyte IsOccurReverse { get; set; }
        /// <summary>
        /// 逆向申请单状态（10已提交 20已确认 30已完成 40已取消）
        /// </summary>
        public sbyte ReverseStatus { get; set; }
        /// <summary>
        /// 退款状态（0未退款 1已退款）
        /// </summary>
        public sbyte RefundStatus { get; set; }

        public long ShopId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 签收状态
        /// </summary>
        public sbyte SignStatus { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string ShowProductType
        {
            get
            {
                return ((ProductTypeEnum)ProduceType).GetDescription();
            }
        }

        public int ProduceType { get; set; }

        /// <summary>
        /// 关联订单号
        /// </summary>
        public string RefOrderNo { get; set; }

    }
}
