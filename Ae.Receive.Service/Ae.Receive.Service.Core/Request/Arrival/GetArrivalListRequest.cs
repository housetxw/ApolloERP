using Ae.Receive.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 到店记录列表请求
    /// </summary>
    public class GetArrivalListRequest : BasePageRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Required]
        public long ShopId { get; set; }
        /// <summary>
        /// 到店日期
        /// </summary>
        public string ArrivalDate { get; set; }

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
        [Required]
        public ArrivalRecordStatusEnum Status { get; set; }
        /// <summary>
        /// 包含所有未完工
        /// </summary>
        public bool IncloudNoFinish { get; set; } = false;
    }
}
