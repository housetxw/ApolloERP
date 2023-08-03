using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Fiance
{
    public class CreateAccountCheckVO
    {

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 门店编号
        /// </summary>
        public long LocationId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string LocationName { get; set; } = string.Empty;

        /// <summary>
        /// 门店核对结果
        /// </summary>
        public string ShopCheckResult { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
    }
}
