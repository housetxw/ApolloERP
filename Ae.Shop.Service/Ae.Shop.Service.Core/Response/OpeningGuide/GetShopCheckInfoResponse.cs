using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response.OpeningGuide
{
    public class GetShopCheckInfoResponse
    {
        /// <summary>
        /// 门店审核状态  0新建 1待审核 2已通过审核 3未通过审核
        /// </summary>
        public int CheckStatus { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Examiner { get; set; } = string.Empty;
        /// <summary>
        /// 审核人电话
        /// </summary>
        public string ExaminerTel { get; set; } = string.Empty;
        /// <summary>
        /// 未通过审核原因
        /// </summary>
        public string FailedExaminedReason { get; set; } = string.Empty;
    }
}
