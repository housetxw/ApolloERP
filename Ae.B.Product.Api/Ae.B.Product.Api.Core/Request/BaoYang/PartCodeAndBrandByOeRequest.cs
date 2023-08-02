using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// 根据OE号查询零件号
    /// </summary>
    public class PartCodeAndBrandByOeRequest
    {
        /// <summary>
        /// OE号
        /// </summary>
        [Required(ErrorMessage = "Oe件号不能为空")]
        public string OeCode { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        [Required(ErrorMessage = "配件类型不能为空")]
        public string PartName { get; set; }
    }
}
