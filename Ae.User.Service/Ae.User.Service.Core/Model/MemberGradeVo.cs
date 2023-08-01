using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Model
{
    /// <summary>
    /// 会员等级
    /// </summary>
    public class MemberGradeVo
    {
        /// <summary>
        /// 会员等级
        /// </summary>
        public int MemberGrade { get; set; }

        /// <summary>
        /// 等级显示
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 成长开始值
        /// </summary>
        public int StartValue { get; set; }

        /// <summary>
        /// 成长结束值
        /// </summary>
        public int? EndValue { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
