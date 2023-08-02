using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    /// <summary>
    /// 商品搜索 
    /// </summary>
    public class ProductSearchRequest : BasePageRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }


        /// <summary>
        /// 搜索排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 开始价格
        /// </summary>
        public decimal? StartPrice { get; set; }

        /// <summary>
        /// 结束价格
        /// </summary>
        public decimal? EndPrice { get; set; }

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
        /// 商品类型 2 子产品 4 父产品 默认查询子产品
        /// </summary>
        public int ClassType { get; set; } = 2;

        /// <summary>
        /// 父产品Id
        /// </summary>
        public int? ParentProductId { get; set; }

        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品 默认
        /// </summary>
        public string ProductAttribute { get; set; }

        /// <summary>
        /// 配件编码
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 配件编号
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// 是否零售
        /// </summary>
        public int? IsRetail { get; set; }
    }
}
