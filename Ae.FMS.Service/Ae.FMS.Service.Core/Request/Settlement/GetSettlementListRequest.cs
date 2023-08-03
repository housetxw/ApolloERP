using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request.Settlement
{
    /// <summary>
    /// 结算请求对象
    /// </summary>
    public class GetSettlementListRequest:BasePageRequest
    {
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        public long ShopId { get; set; }

        public string ProvinceId { get; set; }

        public string CityId { get; set; }

        public List<string> SettlementTimes { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string DistrictId { get; set; }

        /// <summary>
        /// 开始结算日期
        /// </summary>
        public string StartSettlementTime { get; set; }

        /// <summary>
        /// 结束结算日期
        /// </summary>
        public string EndSettlementTime { get; set; }

        /// <summary>
        /// 对账人
        /// </summary>
        public string CheckUser { get; set; }

        /// <summary>
        /// 付款状态
        /// </summary>
        public string PayStatus { get; set; }
    }
}





