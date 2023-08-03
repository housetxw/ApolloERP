using Ae.Shop.Api.Core.Model.ShopReport;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response.ShopReport
{
    public class ShopSalesMonthResponse
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string InstallDate { get; set; }
        /// <summary>
        /// 数据内容
        /// </summary>
        public List<ShopSaleMonthVO> ShopSaleMonthList { get; set; }
    }
}
