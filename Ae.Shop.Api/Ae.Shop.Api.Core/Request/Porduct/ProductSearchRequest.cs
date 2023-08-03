using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Porduct
{
    public class ProductSearchRequest : BasePageRequest
    {
        /// <summary>
        /// 采购类型 1 商品内采 2 商品外采
        /// </summary>
        public int PurchaseType { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }


        /// <summary>
        ///  是否上架  0 否 1 是
        /// </summary>
        public int? IsOnSale { get; set; }
        /// <summary>
        ///  是否只查耗材  0 否 1 是
        /// </summary>
        public int? IsHaoCai { get; set; }
    }
}
