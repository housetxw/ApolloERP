using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Client.Response
{
    public class UserAddressClientResponse
    {
        public List<UserAddressDTO> AddressVOs { get; set; }
    }

    public class UserAddressDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        public int AddressId { get; set; }

        public string MobileNumberDes { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
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
        public sbyte DefaultAddress { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
