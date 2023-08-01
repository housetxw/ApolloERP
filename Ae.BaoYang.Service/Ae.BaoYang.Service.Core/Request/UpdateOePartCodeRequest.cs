using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// 修改OE件号
    /// </summary>
    public class UpdateOePartCodeRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        public string Tid { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        [Required(ErrorMessage = "PartName不能为空")]
        public string PartName { get; set; }

        /// <summary>
        /// 原OE号
        /// </summary>
        public string OriginalOePartCode { get; set; }

        /// <summary>
        /// Oe号
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Required(ErrorMessage = "操作人不能为空")]
        public string SubmitBy { get; set; }
    }
}
