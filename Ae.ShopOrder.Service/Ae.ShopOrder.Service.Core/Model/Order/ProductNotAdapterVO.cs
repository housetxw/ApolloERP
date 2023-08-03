using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    /// <summary>
    /// 产品不适配信息
    /// </summary>
    public class ProductNotAdapterVO
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Pid { get; set; }
        /// <summary>
        /// 显示的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品的图片
        /// </summary>
        public List<ProductImgListVO> ProductImgList { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        public string Remark { get; set; }


        /// <summary>
        /// 操作类型
        /// </summary>
        public List<HandleModeListVO> HandleModeList { get; set; }

    }

}
