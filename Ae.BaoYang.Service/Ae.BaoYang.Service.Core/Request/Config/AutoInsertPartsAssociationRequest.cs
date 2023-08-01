using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// AutoInsertPartsAssociationRequest
    /// </summary>
    public class AutoInsertPartsAssociationRequest
    {
        /// <summary>
        /// Pid
        /// </summary>
        [Required(ErrorMessage = "Pid不能为空")]
        public string Pid { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; } = string.Empty;
    }
}
