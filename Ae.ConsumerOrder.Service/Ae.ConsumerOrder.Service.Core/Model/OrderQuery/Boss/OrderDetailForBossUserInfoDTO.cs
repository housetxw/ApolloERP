using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    public class OrderDetailForBossUserInfoDTO
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; } = string.Empty;
        /// <summary>
        /// 会员等级显示
        /// </summary>
        public string MemberLevel { get; set; } = string.Empty;

        /// <summary>
        /// 显示联系地址
        /// </summary>
        public string DisplayContactAddress { get; set; }

        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; } = string.Empty;
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; } = string.Empty;
        /// <summary>
        /// 显示车型名称信息（格式：品牌Brand 车系Vehicle 年份Nian 排量PaiLiang 款型SalesName）
        /// </summary>
        public string DisplayCarName { get; set; }
        /// <summary>
        /// 总公里数
        /// </summary>
        public int TotalMileage { get; set; }
    }
}
