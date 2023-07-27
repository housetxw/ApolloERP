using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    /// <summary>
    /// 查询公司列表
    /// </summary>
    public class GetPageCompanyListRequest : BasePageRequest
    {
        /// <summary>
        /// 一级公司ID
        /// </summary>
        public long ParentId { get; set; } = 0;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 市Id
        /// </summary>
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 区Id
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 公司状态 0待提交 1审核中 2正常 3注销
        /// </summary>
        public sbyte Status { get; set; }
    }
}
