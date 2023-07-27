using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request.Company
{
    public class UpdateCompanyStatusRequest
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 公司状态 0待提交 1审核中 2正常 3注销 4审核未通过
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 未通过审核原因
        /// </summary>
        public string FailedExaminedReason { get; set; } = string.Empty;
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
