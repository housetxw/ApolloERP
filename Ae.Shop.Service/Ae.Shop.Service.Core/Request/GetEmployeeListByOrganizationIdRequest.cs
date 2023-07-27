using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetEmployeeListByOrganizationIdRequest
    {
        /// <summary>
        /// 单位ID
        /// </summary>
        public int OrganizationId { get; set; }
        /// <summary>
        /// 单位类型 0：公司；1：门店；2：仓库
        /// </summary>
        public int Type { get; set; }
    }
}
