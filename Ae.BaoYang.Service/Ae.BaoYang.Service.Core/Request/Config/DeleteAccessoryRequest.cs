using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 删除辅料配置
    /// </summary>
    public class DeleteAccessoryRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        public string Tid { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Required(ErrorMessage = "类型不能为空")]
        public string AccessoryName { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
