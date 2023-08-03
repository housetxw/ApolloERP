using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.OrderQuery
{
    public class GetOrderPackageCardsResponse
    {
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserPhone { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string SourceOrderNo { get; set; }

        // public string ProductName { get; set; }

        /// <summary>
        /// 渠道（RG）
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// 核销码（RGHX*****）
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 状态（0未使用 1已使用 2已过期）
        /// </summary>
        public string Status { get; set; }

        public string ShowStatus
        {
            get
            {
                if (Status == "未使用")
                {
                    return "立即使用";
                }
                return Status;
            }
        }

        /// <summary>
        /// 起始有效时间
        /// </summary>
        public DateTime StartValidTime { get; set; }

        /// <summary>
        /// 截止有效时间
        /// </summary>
        public DateTime EndValidTime { get; set; }

        public string ShowStartValidTime
        {
            get
            {
                return  StartValidTime.ToString("yyyy-MM-dd");

            }
        }

        public string ShowEndValidTime
        {
            get
            {
                return EndValidTime.ToString("yyyy-MM-dd");

            }
        }

        /// <summary>
        /// 核销生成的订单号
        /// </summary>
        public string VerifyOrderNo { get; set; }

        /// <summary>
        /// 核销门店
        /// </summary>
        public string VerifyShopName { get; set; } = string.Empty;

        /// <summary>
        /// 核销时间
        /// </summary>
        public DateTime VerifyTime { get; set; }

        /// <summary>
        /// 产品id
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 核销服务商品名称
        /// </summary>
        public string ProductName { get; set; }
    }
}
