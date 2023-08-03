using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Core.Model
{
    /// <summary>
    /// 解密unionid
    /// </summary>
    public class GetWxUnionid
    {
        /// <summary>
        /// 开放ID
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 联合ID
        /// </summary>
        public string UnionId { get; set; }
    }
}
