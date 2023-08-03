using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 得到最近一次到店记录请求
    /// </summary>
    public class GetLastArrivalRequest: BaseGetRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Required]
        public long ShopId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNo { get; set; }

        /// <summary>
        /// 只查未完工？
        /// </summary>
        public bool IsNoFinish { get; set; } = false;
    }
}
