using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Request
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
        public string UserId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
