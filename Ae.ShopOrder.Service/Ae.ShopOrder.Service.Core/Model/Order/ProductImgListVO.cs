using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    /// <summary>
    /// 产品图片VO
    /// </summary>
    public class ProductImgListVO
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 实例图片Url
        /// </summary>
        public string ExampleImgUrl { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        public List<ImgVo> ImgList { get; set; }
    }
}
