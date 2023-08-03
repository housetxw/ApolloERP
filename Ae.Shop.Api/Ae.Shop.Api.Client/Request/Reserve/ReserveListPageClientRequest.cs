using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request.Reserve
{
    public class ReserveListPageClientRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 车牌 模糊搜索
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 预约类型
        /// </summary>
        public string ReserveType { get; set; }

        /// <summary>
        /// 预约渠道
        /// </summary>
        public int ReserveChannel { get; set; }

        /// <summary>
        /// 预约状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 预约开始日期 yyyy-MM-dd
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 预约结束日期 yyyy-MM-dd
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// 预约技师
        /// </summary>
        public bool ReserveTech { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
