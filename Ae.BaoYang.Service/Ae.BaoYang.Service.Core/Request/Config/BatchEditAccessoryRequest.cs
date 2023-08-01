using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 批量编辑辅料配置
    /// </summary>
    public class BatchEditAccessoryRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        [MinLength(1, ErrorMessage = "Tid不能为空")]
        public List<string> TidList { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required(ErrorMessage = "类型不能为空")]
        public string AccessoryName { get; set; }

        /// <summary>
        /// 属性配置
        /// </summary>
        [Required(ErrorMessage = "Attribute不能为空")]
        [MinLength(1, ErrorMessage = "Attribute不能为空")]
        public List<PartAttribute> Attribute { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PartAttribute
    {
        /// <summary>
        /// 属性名
        /// </summary>
        [Required(ErrorMessage = "属性名不能为空")]
        public string AttributeName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string AttributeValue { get; set; }
    }
}
