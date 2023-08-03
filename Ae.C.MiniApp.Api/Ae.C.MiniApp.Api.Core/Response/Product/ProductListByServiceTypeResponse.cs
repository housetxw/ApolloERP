using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Product
{
    /// <summary>
    /// 服务大类 商品列表
    /// </summary>
    public class ProductListByServiceTypeResponse
    {
        /// <summary>
        /// 商品
        /// </summary>
        public List<CategoryProductListVo> Products { get; set; } = new List<CategoryProductListVo>();

        /// <summary>
        /// 默认轮胎尺寸
        /// </summary>
        public string DefaultTireSize { get; set; } = string.Empty;

        /// <summary>
        /// 更多轮胎尺寸
        /// </summary>
        public List<string> TireSizes { get; set; } = new List<string>();
    }

    /// <summary>
    /// 商品
    /// </summary>
    public class CategoryProductListVo : BaoYangPackageProductModel
    {
        /// <summary>
        /// 分组Id 用于选商品互斥的情况
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 需要选择的五级属性
        /// </summary>
        public PropertySelectModel Property { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; } = string.Empty;

        /// <summary>
        /// 产品类型 0：自营产品   1：门店服务项目
        /// </summary>
        public int ProductType { get; set; }
    }
}
