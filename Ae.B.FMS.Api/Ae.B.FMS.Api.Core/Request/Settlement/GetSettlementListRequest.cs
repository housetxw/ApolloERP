using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Request
{
  public  class GetSettlementListRequest
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
        public List<string> PayStatuss { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
