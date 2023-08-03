using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Shop
{
    public class ShopOnSaleServiceDTO
    {
        /// <summary>
        /// 服务Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 是否上下架
        /// </summary>
        public bool IsOnline { get; set; }
    }
}
