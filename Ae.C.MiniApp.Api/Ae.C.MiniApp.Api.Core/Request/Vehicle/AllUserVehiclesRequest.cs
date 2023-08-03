using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 查用户所有车型request
    /// </summary>
    public class AllUserVehiclesRequest: BaseGetRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
