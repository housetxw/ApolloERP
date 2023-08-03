using Ae.Shop.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Order
{
    /// <summary>
    /// 业务状态搜索订单请求对象
    /// </summary>
    public class GetOrderBaseInfoForBusinessStatusRequest : BasePageRequest
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        [Required(ErrorMessage = "业务状态不能为空")]
        public OrderBusinessStatusEnum OrderBusinessStatus { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 订单渠道
        /// </summary>
        public List<int> Channels { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 创建开始时间
        /// </summary>
        public string StartOrderTime { get; set; }

        /// <summary>
        /// 创建结束时间
        /// </summary>
        public string EndOrderTime { get; set; }

        /// <summary>
        /// 开始安装时间
        /// </summary>
        public string StarInstallTime { get; set; }

        /// <summary>
        /// 结束安装时间
        /// </summary>
        public string EndInstallTime { get; set; }
    }
}
