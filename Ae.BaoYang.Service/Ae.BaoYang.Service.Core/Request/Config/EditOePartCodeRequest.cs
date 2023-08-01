using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 编辑OE件号
    /// </summary>
    public class EditOePartCodeRequest
    {
        /// <summary>
        /// Oe件号
        /// </summary>
        [Required(ErrorMessage = "OE号不能为空")]
        public string OePartCode { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        [Required(ErrorMessage = "配件类型不能为空")]
        public string PartName { get; set; }

        /// <summary>
        /// 车型品牌
        /// </summary>
        public List<string> VehicleBrand { get; set; }

        /// <summary>
        /// 配件号
        /// </summary>
        public List<ParCodeDetailRequest> PartCode { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
