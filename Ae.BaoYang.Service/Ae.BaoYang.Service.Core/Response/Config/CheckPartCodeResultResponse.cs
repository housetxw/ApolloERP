using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Response.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckPartCodeResultResponse
    {
        /// <summary>
        /// 是否验证成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }
    }
}
