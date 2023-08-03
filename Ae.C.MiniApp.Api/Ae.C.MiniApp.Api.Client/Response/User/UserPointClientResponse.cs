using System;
using System.Collections.Generic;
using System.Text;
using Ae.C.MiniApp.Api.Client.Model.User;

namespace Ae.C.MiniApp.Api.Client.Response.User
{
    public class UserPointClientResponse
    {
        /// <summary>
        /// 当前积分
        /// </summary>
        public int CurrentPoint { get; set; }

        /// <summary>
        /// 积分用途
        /// </summary>
        public string PointPurpose { get; set; }

        /// <summary>
        /// 积分详情
        /// </summary>
        public List<UserPointRecordDto> PointList { get; set; }
    }
}
