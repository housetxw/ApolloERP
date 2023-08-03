using Ae.ConsumerOrder.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Response
{
    public class ScanCodeResponse
    {
        /// <summary>
        /// 码类型 0未设置 1核销码 2安装码
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 码渠道方 RG总部 OT其他（目前默认只有RG返回）
        /// </summary>
        public string Channel { get; set; } = "RG";
        /// <summary>
        /// 如果是安装码有返回此参数为待安装的订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
    }
}
