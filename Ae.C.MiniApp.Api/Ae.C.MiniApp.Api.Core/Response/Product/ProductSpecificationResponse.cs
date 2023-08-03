using Ae.C.MiniApp.Api.Core.Model.Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Product
{
    /// <summary>
    /// 商品规格选择 返回模型
    /// </summary>
    public class ProductSpecificationResponse
    {
        //数据格式参考
        //https://blog.csdn.net/qq_36763293/article/details/101055843

        /// <summary>
        /// 规格列表
        /// </summary>
        public List<SpecificationVo> Specifications { get; set; }

        /// <summary>
        ///  组合商品list
        /// </summary>
        public List<ProductCombinationVo> Products { get; set; }
    }

    public class SpecificationVo {
        public string Name { get; set; }
        public List<SpecificationItemVo> Items { get; set; }
    }
    public class SpecificationItemVo
    {
        public string Name { get; set; }
    }

}
