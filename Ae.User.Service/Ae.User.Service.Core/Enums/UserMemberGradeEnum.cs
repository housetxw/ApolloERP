using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.User.Service.Core.Enums
{
    /// <summary>
    /// 用户会员等级
    /// </summary>
    public enum UserMemberGradeEnum
    {
        /// <summary>
        /// 注册会员
        /// </summary>
        [Description("注册会员")] RegisterMember = 0,

        /// <summary>
        /// 铜牌
        /// </summary>
        [Description("铜牌")] BrassPlate = 10,

        /// <summary>
        /// 银牌
        /// </summary>
        [Description("银牌")] SilverMedal = 20,

        /// <summary>
        /// 金牌
        /// </summary>
        [Description("金牌")] GoldMedal = 30,

        /// <summary>
        /// 铂金
        /// </summary>
        [Description("铂金")] Platinum = 40,

        /// <summary>
        /// 钻石
        /// </summary>
        [Description("钻石")] Diamonds = 50
    }
}
