using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model.ShopManage
{
    public class ShopCustomerListDto
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
        public ReceiveVehicleDto DefaultVehicle { get; set; }
    }

    public class ReceiveVehicleDto
    {
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 车型拼接
        /// </summary>
        public string CarType { get; set; }
    }
}
