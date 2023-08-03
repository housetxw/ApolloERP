using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.User
{
    /// <summary>
    /// 添加用户地址
    /// </summary>
    public class AddUserAddressRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember]
        public string UserId { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        [Required(ErrorMessage = "收货人姓名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 收货人手机
        /// </summary>
        [Required(ErrorMessage = "收货人手机不能为空")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// 地址标签
        /// </summary>
        public string AddressTag { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [Required(ErrorMessage = "请选择省")]
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [Required(ErrorMessage = "请选择市")]
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        [Required(ErrorMessage = "请选择区")]
        public string District { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        [Required(ErrorMessage = "街道地址必须填")]
        public string AddressLine { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        [Required(ErrorMessage = "请选择省")]
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        [Required(ErrorMessage = "请选择市")]
        public string CityId { get; set; }

        /// <summary>
        /// 区Id
        /// </summary>
        [Required(ErrorMessage = "请选择区")]
        public string DistrictId { get; set; }

        /// <summary>
        /// 默认地址
        /// </summary>
        public bool DefaultAddress { get; set; }
    }
}
