using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Model
{

    [Table("order_address")]
    public class OrderAddressDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        [Column("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// 地址类型（0未设置 1门店地址 2客户地址）
        /// </summary>
        [Column("address_type")]
        public sbyte AddressType { get; set; }
        /// <summary>
        /// 门店Id（当地址类型为门店时存储）
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 地址Id（当地址类型为客户时存储）
        /// </summary>
        [Column("address_id")]
        public long AddressId { get; set; }
        /// <summary>
        /// 地址标签（家/公司/学校等）
        /// </summary>
        [Column("label")]
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// 是否默认（1是）
        /// </summary>
        [Column("is_default")]
        public sbyte IsDefault { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        [Column("receiver_name")]
        public string ReceiverName { get; set; } = string.Empty;
        /// <summary>
        /// 收货人电话
        /// </summary>
        [Column("receiver_phone")]
        public string ReceiverPhone { get; set; } = string.Empty;
        /// <summary>
        /// 省Id
        /// </summary>
        [Column("province_id")]
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 省
        /// </summary>
        [Column("province")]
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市Id
        /// </summary>
        [Column("city_id")]
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 市
        /// </summary>
        [Column("city")]
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 区县Id
        /// </summary>
        [Column("district_id")]
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 区县
        /// </summary>
        [Column("district")]
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 详细地址
        /// </summary>
        [Column("detail_address")]
        public string DetailAddress { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
