using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    /// <summary>
    /// 查询公司列表
    /// </summary>
    public class GetCompanyListResponse
    {
        /// <summary>
        /// 公司列表
        /// </summary>
        public List<CompanyDTO> CompanyList { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }
    }
}
