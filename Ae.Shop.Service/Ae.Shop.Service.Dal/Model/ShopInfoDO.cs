using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    /// <summary>
    /// 门店信息内部使用
    /// </summary>
    public class ShopInfoDO
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
        /// 市
        /// </summary>
        public long CityId { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public long DistrictId { get; set; }
    }
}
