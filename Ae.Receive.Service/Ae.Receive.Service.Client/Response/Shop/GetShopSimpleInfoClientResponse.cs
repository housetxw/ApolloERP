using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Response
{
    public class GetShopSimpleInfoClientResponse
    {
        /// <summary>
        /// 简单名称
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 店全称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 营业类型 1 快修店
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// 门店类型 1：自营
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 门店状态 1待审核 2已通过审核 3未通过审核
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string DistrictId { get; set; }
        /// <summary>
        /// 短地址（省市区）
        /// </summary>
        public string ShortAddress { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 对外电话
        /// </summary>
        public string ExternalPhone { get; set; }
        /// <summary>
        /// 门店老板姓名
        /// </summary>
        public string OwnerName { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板电话
        /// </summary>
        public string OwnerPhone { get; set; } = string.Empty;
    }
}
