using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model.ShopCustomer
{
    /// <summary>
    /// 客户列表
    /// </summary>
    public class ShopCustomerVo
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
        /// 客户手机
        /// </summary>
        public string UserTel { get; set; } = string.Empty;

        /// <summary>
        /// 最后到店时间
        /// </summary>
        public string LastInTime { get; set; } = string.Empty;

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarPlate { get; set; } = string.Empty;

        /// <summary>
        /// 常用车辆
        /// </summary>
        public List<ReceiveVehicleVo> CommonVehicle { get; set; } = new List<ReceiveVehicleVo>();

        /// <summary>
        /// 会员标签
        /// </summary>
        public int MemberTag { get; set; }

        /// <summary>
        /// 会员标签值
        /// </summary>
        public string MemberTagDisplay { get; set; }
    }

    /// <summary>
    /// 车型
    /// </summary>
    public class ReceiveVehicleVo
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
