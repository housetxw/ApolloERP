using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserAttentionProductsRequest: BasePageRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember]
        public string UserId { get; set; }
    }
}
