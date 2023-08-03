using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class CompanyInStockDTO
    {
        /// <summary>
        /// 公司/门店编号
        /// </summary>
        public long LocationId { get; set; }

      
        public string ProductId { get; set; }

        public string CreateBy { get; set; }

    }
}
