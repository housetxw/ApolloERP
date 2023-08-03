using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Core.Model
{
    /// <summary>
    /// token对象
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string TokenContent { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime Expires { get; set; }
    }
}
