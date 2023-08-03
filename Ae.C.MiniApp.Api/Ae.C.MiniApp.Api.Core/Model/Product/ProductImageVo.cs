using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Product
{
    public class ProductImageVo
    {  
        /// <summary>
        /// 图片路径
        /// </summary>
        public string Path { get; set; } = string.Empty;
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }
     
    }
}
