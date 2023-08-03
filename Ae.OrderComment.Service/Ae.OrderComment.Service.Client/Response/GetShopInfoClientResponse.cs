using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Client.Response
{
    public class GetShopInfoClientResponse
    {
        /// <summary>
        /// 门店id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 简单名称
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 门店类型
        /// </summary>
        public string ShopType { get; set; }
        /// <summary>
        /// 总评分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 总订单数量
        /// </summary>
        public int TotalOrder { get; set; }
        /// <summary>
        /// 总评论数量
        /// </summary>
        public int TotalComment { get; set; }
        /// <summary>
        /// 门头图片
        /// </summary>
        public string ShopImageUrl { get; set; }
    }
}
