using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Shop
{
    public class GetMyCarListResponse
    {
        /// <summary>
        /// 我的爱车列表
        /// </summary>
        public List<MyCarInfoVO> MyCarList { get; set; }
    }
}
