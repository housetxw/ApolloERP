using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
   public class GetShopListRequest:BasePageRequest
    {
        /// <summary>
        /// 门店简单名
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 门店id
        /// </summary>
        public List<long> ShopIds { get; set; }
        /// <summary>
        /// 门店类型 -1仓库 1合作店 2工场店 4上门养车 8认证店 
        /// </summary>
        public List<int> ShopTypes { get; set; }
        /// <summary>
        /// 查询类型 -1仓库 0仓库和门店 2工场店 
        /// </summary>
        public int QueryType { get; set; }
        /// <summary>
        /// 省ID
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 市ID
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 区ID
        /// </summary>
        public string DistrictId { get; set; }


        public long CompanyId { get; set; }
    }
}
