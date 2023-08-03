using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Model
{
    public class TodayReceiveDO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 签收状态
        /// </summary>
        public string SignStatus { get; set; }

        /// <summary>
        /// 签收人
        /// </summary>
        public string SignUser { get; set; }

        /// <summary>
        /// 待签收总数量
        /// </summary>
        public int SumNum { get; set; }

        /// <summary>
        /// 已经签收的数量
        /// </summary>

        public int HaveSignNum { get; set; }

        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime SignTime { get; set; }

        /// <summary>
        /// 包裹信息
        /// </summary>
        public string DeliveryInfo { get; set; }
    }
}
