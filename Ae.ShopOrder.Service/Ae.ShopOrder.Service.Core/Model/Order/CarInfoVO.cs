using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    /// <summary>
    /// 车辆的信息
    /// </summary>
    public class CarInfoVO
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

        public List<ImgVo> ImgList { get; set; }
    }
}
