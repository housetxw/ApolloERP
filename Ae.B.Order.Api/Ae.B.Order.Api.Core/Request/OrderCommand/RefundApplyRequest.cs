using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Request
{
    public class RefundApplyRequest : CreateReverseOrderBaseRequest
    {
        /// <summary>
        /// 上传的凭证图片资源路径集合
        /// </summary>
        public List<string> CredentialImageUrls { get; set; }
    }
}
