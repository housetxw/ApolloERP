using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetShopCarRecordPageListRequest : BaseOnlyPageRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNumber { get; set; } = string.Empty;
        /// <summary>
        /// 技师名称或手机号
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 用车开始时间
        /// </summary>
        public DateTime StartTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 用车结束时间
        /// </summary>
        public DateTime EndTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
