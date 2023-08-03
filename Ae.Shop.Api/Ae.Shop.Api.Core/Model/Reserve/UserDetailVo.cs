using Ae.Shop.Api.Core.Model.ShopCustomer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Reserve
{
    public class UserDetailVo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 车辆
        /// </summary>
        public List<UserVehicleVo> Cars { get; set; } = new List<UserVehicleVo>();
    }
}
