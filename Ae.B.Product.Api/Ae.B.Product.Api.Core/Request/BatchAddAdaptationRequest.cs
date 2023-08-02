using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    /// <summary>
    /// 批量添加配件
    /// </summary>
    public class BatchAddAdaptationRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "tid不能为空")]
        [MinLength(1, ErrorMessage = "tid不能为空")]
        public List<string> TidList { get; set; }

        /// <summary>
        /// 普通配件
        /// </summary>
        public List<NormalPart> NormalPart { get; set; }

        /// <summary>
        /// 特殊配件
        /// </summary>
        public List<SpecialPart> SpecialPart { get; set; }
    }

    /// <summary>
    /// 普通配件
    /// </summary>
    public class NormalPart
    {
        /// <summary>
        /// 配件名
        /// </summary>
        [Required(ErrorMessage = "配件类型不能为空")]
        public string PartName { get; set; }

        /// <summary>
        /// OE件号
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        [Required(ErrorMessage = "零件号不能为空")]
        [MinLength(1, ErrorMessage = "零件号不能为空")]
        public List<PartCodeModel> PartCodes { get; set; }
    }

    /// <summary>
    /// 特殊配件
    /// </summary>
    public class SpecialPart
    {
        /// <summary>
        /// 配件类型
        /// </summary>
        [Required(ErrorMessage = "配件类型不能为空")]
        public string PartName { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        [Required(ErrorMessage = "零件号不能为空")]
        [MinLength(1, ErrorMessage = "零件号不能为空")]
        public List<SpecialPartCodeModel> PartCodes { get; set; }
    }
}
