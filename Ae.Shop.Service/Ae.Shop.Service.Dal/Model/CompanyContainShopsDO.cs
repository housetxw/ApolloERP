using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class CompanyContainShopsDO
    {
        /// <summary>
        /// 所属公司ID
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 门店数量
        /// </summary>
        public int TotalCount { get; set; }
    }
}
