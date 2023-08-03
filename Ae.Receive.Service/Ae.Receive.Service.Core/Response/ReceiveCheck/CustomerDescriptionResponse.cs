using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.ReceiveCheck
{
    /// <summary>
    /// 客户描述
    /// </summary>
    public class CustomerDescriptionResponse
    {
        /// <summary>
        /// 客户描述内容
        /// </summary>
        public string Narration { get; set; }

        /// <summary>
        /// 检查项Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 常用语
        /// </summary>
        public List<string> CommonSense { get; set; }
    }
}
