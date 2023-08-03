using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.User
{
    /// <summary>
    /// 用户积分
    /// </summary>
    public class UserPointResponse
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
        public List<UserPointRecordVO> PointList { get; set; }
    }
}
