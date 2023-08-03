using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model
{
   public class ShopOccupyMappingDTO
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 关联前置仓编号
        /// </summary>
        public long RelationShopId { get; set; }
        /// <summary>
        /// 关联前置仓名称
        /// </summary>
        public string RelationShopName { get; set; } = string.Empty;

    }
}
