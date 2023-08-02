﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Client.Request.Vehicle
{
    public class DeleteUserCarClientRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string Submitter { get; set; }
    }
}
