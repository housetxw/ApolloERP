using Ae.Shop.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Arrival
{
    /// <summary>
    /// 到店记录列表请求
    /// </summary>
    public class GetArrivalListRequest : BasePageRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        
        public string ShopId { get; set; }
        /// <summary>
        /// 到店开始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 到店结束日期
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ArrivalRecordStatusEnum Status { get; set; } = ArrivalRecordStatusEnum.All;
    }
}
