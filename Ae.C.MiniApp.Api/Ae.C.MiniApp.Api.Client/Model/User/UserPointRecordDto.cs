using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.User
{
    public class UserPointRecordDto
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 积分值
        /// </summary>
        public int PointValue { get; set; }
        /// <summary>
        /// 关联编号
        /// </summary>
        public string ReferrerNo { get; set; }
        /// <summary>
        /// 关联编号(脱敏)
        /// </summary>
        public string ReferrerNoDes { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}
