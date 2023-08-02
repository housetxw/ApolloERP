using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    /// <summary>
    /// 添加特殊配件号
    /// </summary>
    public class AddSpecialPartRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "tid不能为空")]
        [MinLength(1, ErrorMessage = "tid不能为空")]
        public List<string> TidList { get; set; }

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

    /// <summary>
    /// 
    /// </summary>
    public class SpecialPartCodeModel
    {
        /// <summary>
        /// 零件号
        /// </summary>
        [Required(ErrorMessage = "零件号不能为空")]
        public string PartCode { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 种类
        /// </summary>
        [Required(ErrorMessage = "种类不能为空")]
        public string PartType { get; set; }
    }
}
