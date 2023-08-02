using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    public class HotProductPageListRequest : BasePageRequest
    {
        /// <summary>
        /// 终端类型：ShopApp(B端APP)/CbJApplet(B端小程序)/YcJApplet(C端小程序)/QpJApple(其他小程序)
        /// </summary>
        public TerminalType? TerminalType { get; set; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 一级类目
        /// </summary>
        public int? MainCategoryId { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        public int? SecondCategoryId { get; set; }

        /// <summary>
        /// 三级分类
        /// </summary>
        public int? ChildCategoryId { get; set; }
    }
}
