using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 删除车辆request
    /// </summary>
    public class DeleteUserCarRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember]
        public string UserId { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        [Required(ErrorMessage = "CarId不能为空")]
        public string CarId { get; set; }
    }
}
