using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Response
{
   public class AccountCheckCollectResponse
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 已对账应结算
        /// </summary>
        public decimal AccountedSettlement { get; set; }

        /// <summary>
        /// 未对账应结算
        /// </summary>
        public decimal UnAccountedSettlement { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public decimal Subtotal { get; set; }

        /// <summary>
        /// 门店未已对账数量
        /// </summary>
        public int ShopUnAccountNum { get; set; }

        /// <summary>
        /// 门店已对账数量
        /// </summary>
        public int ShopAccountNum { get; set; }

        /// <summary>
        /// 总部已核对数量
        /// </summary>
        public int RgAccountNum { get; set; }

        /// <summary>
        /// 对账异常订单
        /// </summary>
        public int AccountErrorNum { get; set; }
    }
}
