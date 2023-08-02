using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Response
{
    public class CheckPartCodeResultClientResponse
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
