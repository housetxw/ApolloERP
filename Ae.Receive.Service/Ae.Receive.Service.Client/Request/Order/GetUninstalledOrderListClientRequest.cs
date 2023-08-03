using Ae.Receive.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request
{
    public class GetUninstalledOrderListClientRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
