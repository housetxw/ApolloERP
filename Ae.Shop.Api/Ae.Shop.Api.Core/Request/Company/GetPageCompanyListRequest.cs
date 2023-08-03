using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Company
{
    public class GetPageCompanyListRequest : BasePageRequest
    {
        /// <summary>
        /// 一级公司ID
        /// </summary>
        //[Range(1, long.MaxValue, ErrorMessage = "公司ID必须大于0")]
        public long ParentId { get; set; }
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
        /// 公司状态 0待提交 1审核中 2正常 3注销 4审核未通过
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 类型；0公司，2仓库，3拓展，4供应商
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 公司等级 1一级公司 2二级公司
        /// </summary>
        public sbyte Level { get; set; }
        /// <summary>
        /// 系统类型；1平台公司，2普通公司，3区域合伙公司
        /// </summary>
        public sbyte SystemType { get; set; }
    }
}
