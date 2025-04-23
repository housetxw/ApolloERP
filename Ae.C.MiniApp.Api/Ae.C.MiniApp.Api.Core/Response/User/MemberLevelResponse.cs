using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.User
{
    /// <summary>
    /// 会员等级
    /// </summary>
    public class MemberLevelResponse
    {
        /// <summary>
        /// 当前成长值
        /// </summary>
        public int CurrentGrowthValue { get; set; }

        /// <summary>
        /// 当前会员等级
        /// </summary>
        public int MemberGrade { get; set; }

        /// <summary>
        /// 会员等级名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 下一个会员等级
        /// </summary>
        public int NextMemberGrade { get; set; }
        /// <summary>
        /// 距离下一个等级需要的成长值
        /// </summary>
        public int NextNeedGrade { get; set; } = 0;
        /// <summary>
        /// 下一个会员等级名称
        /// </summary>
        public string NextDisplayName { get; set; } = string.Empty;

        /// <summary>
        /// 成长明细
        /// </summary>
        public List<UserPointRecordVO> GrowthDetail { get; set; }

        /// <summary>
        /// 成长等级
        /// </summary>
        public List<MemberGradeVO> MemberGrades { get; set; }
    }
}
