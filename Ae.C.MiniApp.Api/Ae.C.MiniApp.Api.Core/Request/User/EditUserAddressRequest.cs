using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.User
{
    /// <summary>
    /// 编辑地址
    /// </summary>
    public class EditUserAddressRequest
    {
        /// <summary>
        /// 地址Id
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember]
        public string UserId { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 收货人手机
        /// </summary>
        public string MobileNumber { get; set; }

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
