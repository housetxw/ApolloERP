using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBaoYangProductRefRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "Id必须大于0")]
        public int Id { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; } = string.Empty;
    }
}
