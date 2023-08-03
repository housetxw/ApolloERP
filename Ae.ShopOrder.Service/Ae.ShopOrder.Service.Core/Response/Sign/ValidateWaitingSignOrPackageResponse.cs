using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Model;

namespace Ae.ShopOrder.Service.Core.Response.Sign
{
    /// <summary>
    /// 验证签收码返回对象
    /// </summary>
    public class ValidateWaitingSignOrPackageResponse
    {
    
        /// <summary>
        /// 签收类型
        /// </summary>
        public string SignType { get; set; }

        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 签收的Code
        /// </summary>
        public string SignCode { get; set; }

        /// <summary>
        /// 待签收的物流单号
        /// </summary>
        public List<WaitingSignPackageVO> WaitingPackage { get; set; }
    }
}
