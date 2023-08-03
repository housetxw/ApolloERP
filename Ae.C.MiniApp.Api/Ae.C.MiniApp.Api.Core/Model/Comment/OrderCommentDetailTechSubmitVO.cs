using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class OrderCommentDetailTechSubmitVO : BaseOrderCommentDetailSubmitVO
    {
        /// <summary>
        /// 技师员工Id
        /// </summary>
        public string EmployeeId { get; set; }
        /// <summary>
        /// 技师头像
        /// </summary>
        public string TechHeadUrl { get; set; }
        /// <summary>
        /// 技师名称
        /// </summary>
        public string TechName { get; set; }
        /// <summary>
        /// 技师级别
        /// </summary>
        public string TechLevel { get; set; }
    }
}
