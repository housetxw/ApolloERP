using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Model
{
    public class GetOrderCommentForTechDTO: GetOrderCommentBaseDTO
    {
        /// <summary>
        /// 技师员工Id
        /// </summary>
        public string EmployeeId { get; set; } = string.Empty;

        /// <summary>
        /// 技师名称
        /// </summary>
        public string TechName { get; set; } = string.Empty;

        /// <summary>
        /// 技师级别
        /// </summary>
        public int TechLevel { get; set; }

        public string TechLevelStr { get; set; }

        /// <summary>
        /// 分值
        /// </summary>
        public int Score { get; set; }

    }
}
