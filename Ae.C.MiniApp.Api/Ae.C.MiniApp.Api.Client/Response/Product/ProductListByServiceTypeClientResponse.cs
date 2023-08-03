using Ae.C.MiniApp.Api.Client.Model.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Product
{
    public class ProductListByServiceTypeClientResponse
    {
        /// <summary>
        /// 商品
        /// </summary>
        public List<CategoryProductListDto> Products { get; set; } = new List<CategoryProductListDto>();

        /// <summary>
        /// 默认轮胎尺寸
        /// </summary>
        public string DefaultTireSize { get; set; }

        /// <summary>
        /// 更多轮胎尺寸
        /// </summary>
        public List<string> TireSizes { get; set; }
    }

    public class CategoryProductListDto : BaoYangPackageProductDto
    {
        /// <summary>
        /// 分组Id 用于选商品互斥的情况
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 需要选择的五级属性
        /// </summary>
        public PropertySelectDto Property { get; set; }

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
