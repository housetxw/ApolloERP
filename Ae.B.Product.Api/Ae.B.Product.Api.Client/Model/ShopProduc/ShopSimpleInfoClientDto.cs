using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.ShopProduc
{
    public class ShopSimpleInfoClientDto
    {
        /// <summary>
        /// 门店id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 简单名称
        /// </summary>
        public string SimpleName { get; set; }
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
        /// 省名称
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市名称
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区名称
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
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
        /// 门店类型
        /// </summary>
        public string Type { get; set; }
    }
}
