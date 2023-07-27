using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request.ShopCustomer
{
    /// <summary>
    /// 客户列表Request
    /// </summary>
    public class CustomerListRequest : BasePageRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 搜索值
        /// </summary>
        public string SearchContent { get; set; }

        /// <summary>
        /// 推荐技师Id
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 100普通会员  200高级会员  300VIP贵宾
        /// </summary>
        public int MemberLevel { get; set; }

        /// <summary>
        /// 到店起始时间：1 一个月前 3 三个月前 6 半年前  12 一年前
        /// </summary>
        public int StartInTime { get; set; }
    }
}
