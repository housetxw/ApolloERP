using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Porduct
{
    /// <summary>
    /// SearchProductPageListRequest
    /// </summary>
    public class SearchProductPageListRequest : BasePageRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }


        /// <summary>
        /// 一级类目Id
        /// </summary>
        public int? MainCategoryId { get; set; }


        /// <summary>
        /// 二级类目Id
        /// </summary>
        public int? SecondCategoryId { get; set; }

        /// <summary>
        /// 三级类目Id
        /// </summary>
        public int? ChildCategoryId { get; set; }

        /// <summary>
        /// 是否禁用 0 否 1 是
        /// </summary>
        public int? IsDeleted { get; set; }

        /// <summary>
        ///  是否上架  0 否 1 是
        /// </summary>
        public int? IsOnSale { get; set; }

        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品 默认
        /// </summary>
        public string ProductAttribute { get; set; }

        /// <summary>
        /// 是否零售
        /// </summary>
        public int? IsRetail { get; set; }
    }
}
