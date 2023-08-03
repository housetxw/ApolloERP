using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Product
{
    public class CategoryProductVo: ProductBaseInfoVo
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
