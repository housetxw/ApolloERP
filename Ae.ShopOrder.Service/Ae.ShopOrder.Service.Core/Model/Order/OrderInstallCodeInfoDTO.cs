using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
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
        public string RuleDesc { get; set; } = "支持预约，消费高峰期可能需要等位。发票问题请询问平台。不兑现、不找零。具体解释权归平台所有。";//TODO 暂时写死，以后可能走配置
    }
}
