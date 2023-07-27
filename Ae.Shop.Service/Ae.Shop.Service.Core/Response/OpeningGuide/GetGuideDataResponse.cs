using Ae.Shop.Service.Core.Model.OpeningGuide;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response.OpeningGuide
{
    /// <summary>
    /// 开店引导首页返回对象
    /// </summary>
    public class GetGuideDataResponse<T>
    {
        public T Data { get; set; }
        ///// <summary>
        ///// 第一个步骤
        ///// </summary>
        //public ShopBaseInfoVO FirstStep { get; set; }

        ///// <summary>
        ///// 第二步骤
        ///// </summary>
        //public ShopAddressInfoVO SecondStep { get; set; }
    }
}
