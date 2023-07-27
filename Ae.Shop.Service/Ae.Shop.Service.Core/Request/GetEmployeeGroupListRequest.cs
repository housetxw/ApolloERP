using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetEmployeeGroupListRequest
    {
        /// <summary>
        /// 姓名（登录token中已用，不可修改）
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string GroupName { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string GroupLeader { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
    }
}
