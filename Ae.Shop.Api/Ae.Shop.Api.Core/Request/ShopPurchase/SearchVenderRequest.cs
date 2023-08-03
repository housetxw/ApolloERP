using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopPurchase
{
    public class SearchVenderRequest : BasePageRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VenderName { get; set; }
        /// <summary>
        /// 联系人名称或电话
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 状态（0全部，1正常 2停用 3已删除）
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 结算方式（1现结 2账期）
        /// </summary>
        public sbyte SettlementMethod { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();
    }
}
