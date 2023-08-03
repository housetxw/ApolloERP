using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.UserAddress
{
   public class UpdateUserAddressClientRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// 地址类型
        /// </summary>
        public int AddressType { get; set; }
        /// <summary>
        /// 地址名称
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
        public bool DefaultAddress { get; set; }


        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
    }
}
