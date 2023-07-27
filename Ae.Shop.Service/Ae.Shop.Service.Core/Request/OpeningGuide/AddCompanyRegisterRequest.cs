using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request.OpeningGuide
{
    /// <summary>
    /// AddCompanyRegisterRequest
    /// </summary>
    public class AddCompanyRegisterRequest
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string Mobile { get; set; } = string.Empty;

        /// <summary>
        /// 公司所在省Id
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;

        /// <summary>
        /// 公司所在市Id
        /// </summary>
        public string CityId { get; set; } = string.Empty;

        /// <summary>
        /// 公司所在区Id
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;

        /// <summary>
        /// 省名
        /// </summary>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// 市名
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 区名
        /// </summary>
        public string District { get; set; } = string.Empty;

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 加盟等级
        /// </summary>
        public string ServiceLevel { get; set; } = string.Empty;

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; } = string.Empty;
    }
}
