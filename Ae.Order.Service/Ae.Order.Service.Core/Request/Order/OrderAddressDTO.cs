using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class OrderAddressDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 地址类型（0未设置 1门店地址 2客户地址）
        /// </summary>
        public sbyte AddressType { get; set; }
        /// <summary>
        /// 门店Id（当地址类型为门店时存储）
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 地址Id（当地址类型为客户时存储）
        /// </summary>
        public long AddressId { get; set; }
        /// <summary>
        /// 地址标签（家/公司/学校等）
        /// </summary>
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// 是否默认（1是）
        /// </summary>
        public sbyte IsDefault { get; set; }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiverName { get; set; } = string.Empty;
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceiverPhone { get; set; } = string.Empty;
        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市Id
        /// </summary>
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 区县Id
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 区县
        /// </summary>
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddress { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
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
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
