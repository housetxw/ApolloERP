using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class GetMyCarListClientResponse
    {
        /// <summary>
        /// 我的爱车列表
        /// </summary>
        public List<MyCarInfoDTO> MyCarList { get; set; }
    }
}
