using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.User.Service.Dal.Model
{
    [Table("user_address")]
    public class UserAddressDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }
        /// <summary>
        /// 地址类型
        /// </summary>
        [Column("address_type")]
        public int AddressType { get; set; }
        /// <summary>
        /// 地址名称
        /// </summary>
        [Column("address_tag")]
        public string AddressTag { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; }
        /// <summary>
        /// 收货人手机号
        /// </summary>
        [Column("mobile_number")]
        public string MobileNumber { get; set; }
        /// <summary>
        /// 收货人座机
        /// </summary>
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [Column("province")]
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        [Column("city")]
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        [Column("district")]
        public string District { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        [Column("address_line")]
        public string AddressLine { get; set; }
        /// <summary>
        /// 省Id
        /// </summary>
        [Column("province_id")]
        public string ProvinceId { get; set; }
        /// <summary>
        /// 市Id
        /// </summary>
        [Column("city_id")]
        public string CityId { get; set; }
        /// <summary>
        /// 区Id
        /// </summary>
        [Column("district_id")]
        public string DistrictId { get; set; }
        /// <summary>
        /// 默认地址
        /// </summary>
        [Column("default_address")]
        public bool DefaultAddress { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}