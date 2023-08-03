using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.SharingPromotion
{
    public class GetSharingOrdersResponse
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 列表商品总数（套餐数+单服务数）
        /// </summary>
        public int ListTotalProductNum { get; set; }

        /// <summary>
        /// 会员信息
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public string OrderTime { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// 商品信息集合
        /// </summary>
        public List<OrderDetailProductVO> Products { get; set; }
    }
}
