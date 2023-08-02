using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Core.Model.User
{
    /// <summary>
    /// 用户地址
    /// </summary>
    public class UserAddressVo
    {
        /// <summary>
        /// 地址Id
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// 手机号脱敏
        /// </summary>
        public string MobileNumberDes { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 地址标签
        /// </summary>
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 收货人手机号
        /// </summary>
        public string MobileNumber { get; set; } = string.Empty;
        /// <summary>
        /// 收货人座机
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 街道
        /// </summary>
        public string AddressLine { get; set; } = string.Empty;
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
        /// 默认地址
        /// </summary>
        public sbyte DefaultAddress { get; set; }
    }
}
