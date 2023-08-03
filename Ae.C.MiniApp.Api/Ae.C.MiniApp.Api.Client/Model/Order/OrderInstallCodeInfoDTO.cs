using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Order
{
    /// <summary>
    /// 订单安装码信息
    /// </summary>
    public class OrderInstallCodeInfoDTO
    {
        /// <summary>
        /// 安装码（RGAZ*****）
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 开始使用营业时间，例：08:00
        /// </summary>
        public string StartUseBussinessTime { get; set; } = string.Empty;
        /// <summary>
        /// 开始使用营业时间，例：20:00
        /// </summary>
        public string EndUseBussinessTime { get; set; } = string.Empty;
        /// <summary>
        /// 适用的门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 适用的门店名称
        /// </summary>
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 适用的门店名称
        /// </summary>
        public string ShopAddress { get; set; } = string.Empty;
        /// <summary>
        /// 适用规则描述
        /// </summary>
        public string RuleDesc { get; set; } = string.Empty;
    }
}
