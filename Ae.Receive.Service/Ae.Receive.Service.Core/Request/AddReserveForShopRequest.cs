using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    /// <summary>
    /// 添加预约
    /// </summary>
    public class AddReserveForShopRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [Required(ErrorMessage = "客户姓名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "客户手机号不能为空")]
        public string UserTel { get; set; }

        /// <summary>
        /// 预约类型
        /// </summary>
        public string ReserveType { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserveTime { get; set; }

        /// <summary>
        /// 技师Id
        /// </summary>
        public string TechId { get; set; } = string.Empty;

        /// <summary>
        /// 技师姓名
        /// </summary>
        public string TechName { get; set; } = string.Empty;

        /// <summary>
        /// 预约备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 预约车辆信息
        /// </summary>
        public ReserveVehicleRequest Vehicle { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
