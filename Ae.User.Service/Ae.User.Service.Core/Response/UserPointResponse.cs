using Ae.User.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Response
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
        public string PointPurpose { get; set; } = "积分可兑换优惠券等商品。";

        /// <summary>
        /// 积分详情
        /// </summary>
        public List<UserPointRecordVo> PointList { get; set; } = new List<UserPointRecordVo>();
    }
}
