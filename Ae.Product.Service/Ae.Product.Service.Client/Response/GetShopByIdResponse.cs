using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Client.Response
{
    public class GetShopByIdResponse
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 简单名
        /// </summary>
        public string SimpleName { get; set; }

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
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public string CityId { get; set; }
    }
}
