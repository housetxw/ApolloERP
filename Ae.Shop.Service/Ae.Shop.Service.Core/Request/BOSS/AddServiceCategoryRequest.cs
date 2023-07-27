using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class AddServiceCategoryRequest
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 英文CODE
        /// </summary>
        [Required(ErrorMessage = "CODE不能为空")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        [Required(ErrorMessage = "创建人不能为空")]
        public string CreateBy { get; set; } = string.Empty;
    }
}
