using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.User.Service.Core.Enums;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// 操作用户成长值request
    /// </summary>
    public class OperateUserGrowthValueRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public UserGrowthOperateTypeEnum OperateType { get; set; }

        /// <summary>
        /// 积分值
        /// </summary>
        public int GrowthValue { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "操作人不能为空")]
        public string SubmitBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
