using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class ShopLocationVO
    {
        /// <summary>
        /// 区/县ID
        /// </summary>
        public long DistrictId { get; set; }
        /// <summary>
        /// 区/县名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 门店数量
        /// </summary>
        public int TotalCount { get; set; }
    }
}
