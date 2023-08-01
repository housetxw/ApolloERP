﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ae.User.Service.Core.Model
{
    /// <summary>
    /// 用户地址
    /// </summary>
    public class UserAddressVO
    {

        public int Id { get; set; }

        /// <summary>
        /// 地址Id
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 收货人手机
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// 收货人手机[脱敏]
        /// </summary>
        public string MobileNumberDes { get; set; }

        /// <summary>
        /// 地址标签
        /// </summary>
        public string AddressTag { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string AddressLine { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 区Id
        /// </summary>
        public string DistrictId { get; set; }

        /// <summary>
        /// 默认地址
        /// </summary>
        public bool DefaultAddress { get; set; }
    }
}
