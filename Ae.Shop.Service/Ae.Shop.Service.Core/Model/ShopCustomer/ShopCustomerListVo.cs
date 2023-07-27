using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model.ShopCustomer
{
    /// <summary>
    /// 门店客户列表
    /// </summary>
    public class ShopCustomerListVo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 客户手机
        /// </summary>
        public string UserTel { get; set; } = string.Empty;

        /// <summary>
        /// 最后到店时间
        /// </summary>
        public string LastInTime { get; set; } = string.Empty;

        /// <summary>
        /// 性别
        /// </summary>
        public string GenderDisplay { get; set; } = string.Empty;

        /// <summary>
        /// 默认车辆
        /// </summary>
        public ReceiveVehicleVo DefaultVehicle { get; set; }
    }
}
