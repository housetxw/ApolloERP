using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Dal.Model
{
   public class AccountCheckCollectDO
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

        /// <summary>
        /// 门店已对账金额
        /// </summary>
        public decimal ShopAccountMoney { get; set; }

        /// <summary>
        /// 对账异常金额
        /// </summary>
        public decimal AccountExceptionMoney { get; set; }
    }
}
