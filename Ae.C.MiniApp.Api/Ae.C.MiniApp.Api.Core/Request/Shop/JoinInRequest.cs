using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class JoinInRequest
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string Phone { get; set; } = string.Empty;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 省id
        /// </summary>
        [Required(ErrorMessage = "省ID不能为空")]
        public int ProvinceId { get; set; }
        /// <summary>
        /// 市id
        /// </summary>
        [Required(ErrorMessage = "市ID不能为空")]
        public int CityId { get; set; }
        /// <summary>
        /// 区id
        /// </summary>
        [Required(ErrorMessage = "区ID不能为空")]
        public int DistrictId { get; set; }
        /// <summary>
        /// 省市区
        /// </summary>
        [Required(ErrorMessage = "省市区不能为空")]
        public string ShortAddress { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
