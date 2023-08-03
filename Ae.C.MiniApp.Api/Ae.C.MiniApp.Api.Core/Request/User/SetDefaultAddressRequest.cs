using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.User
{
    /// <summary>
    /// 设为默认地址
    /// </summary>
    public class SetDefaultAddressRequest
    {
        /// <summary>
        /// 地址Id
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember]
        public string UserId { get; set; }
    }
}
