using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.Coupon
{
    /// <summary>
    /// DecapAwardDetailVo
    /// </summary>
    public class DecapAwardDetailVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 领取状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 领取状态 - 显示
        /// </summary>
        public string StatusDisplay
        {
            get
            {
                if (Status == 1)
                {
                    return "已领取";
                }
                else
                {
                    return "待领取";
                }
            }
        }

        /// <summary>
        /// 付款状态
        /// </summary>
        public int PayStatus { get; set; }

        /// <summary>
        /// 付款状态 - 显示
        /// </summary>
        public string PayStatusDisplay
        {
            get
            {
                if (PayStatus == 1)
                {
                    return "已打款";
                }
                else
                {
                    return "待打款";
                }
            }
        }

        /// <summary>
        /// 领取时间
        /// </summary>
        public string DrawTime { get; set; }

        /// <summary>
        /// 领取人OpenId
        /// </summary>
        public string DrawOpenId { get; set; }

        /// <summary>
        /// 领取人UserId
        /// </summary>
        public string DrawUserId { get; set; }

        /// <summary>
        /// 打款时间
        /// </summary>
        public string PayTime { get; set; }

        /// <summary>
        /// 付款渠道
        /// </summary>
        public string ClientChannel { get; set; }

        /// <summary>
        /// 付款渠道-显示
        /// </summary>
        public string ClientChannelDisplay
        {
            get
            {
                string display = string.Empty;
                if (ClientChannel == "WeChat")
                {
                    display = "微信";
                }
                else if (ClientChannel == "AliPay")
                {
                    display = "支付宝";
                }

                return display;
            }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string UserTel { get; set; }
    }
}
