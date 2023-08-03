using Ae.C.MiniApp.Api.Client.Model.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.User
{
    public class MemberLevelClientResponse
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
        public int? NextMemberGrade { get; set; }

        /// <summary>
        /// 成长明细
        /// </summary>
        public List<UserPointRecordDto> GrowthDetail { get; set; }

        /// <summary>
        /// 成长等级
        /// </summary>
        public List<MemberGradeDto> MemberGrades { get; set; }
    }
}
