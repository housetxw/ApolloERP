using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 得到派工技师列表请求对象
    /// </summary>
    public class GetDispatchTechniciansRequest : BaseGetRequest
    {
        /// <summary>
        /// 门店Is
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

    }
}
