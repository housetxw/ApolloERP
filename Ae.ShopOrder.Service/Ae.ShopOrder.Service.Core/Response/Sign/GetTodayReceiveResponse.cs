using System;
using System.Collections.Generic;
using System.Text;
using Ae.ShopOrder.Service.Core.Model;

namespace Ae.ShopOrder.Service.Core.Response.Sign
{
    /// <summary>
    /// 收货进度返回对象
    /// </summary>
    public class GetTodayReceiveResponse
    {
        /// <summary>
        /// 收货进度
        /// </summary>
        public int TitleNum { get; set; }
        /// <summary>
        /// 明细
        /// </summary>
        public List<GetTodayReceiveVO> Items;
    }
}
