using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 获取车型信息Request
    /// </summary>
    public class UserVehicleByCarIdRequest: BaseGetRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember]
        public string UserId { get; set; }

        /// <summary>
        /// 车Id
        /// </summary>
        [Required(ErrorMessage = "CarId不能为空")]
        public string CarId { get; set; }
    }
}
