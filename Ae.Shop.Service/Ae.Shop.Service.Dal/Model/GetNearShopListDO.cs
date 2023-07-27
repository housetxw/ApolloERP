using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class GetNearShopListDO
    {
        /// <summary>
        /// 列表
        /// </summary>
        public List<NearShopInfoDO> List { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalItems { get; set; }
    }
}
