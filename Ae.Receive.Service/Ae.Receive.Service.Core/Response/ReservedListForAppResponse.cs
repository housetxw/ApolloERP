using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    public class ReservedListForAppResponse
    {
        /// <summary>
        /// 已预约列表
        /// </summary>
        public List<ReservedInfoVO> ReservedList { get; set; }
        /// <summary>
        /// 预约时间 + 预约总数量
        /// </summary>
        public string ReserveDate { get; set; }
    }
    public class ReservedInfoVO
    {
        /// <summary>
        /// 时间点
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 预约列表
        /// </summary>
        public List<ReservedVO> ReserveDetaiList { get; set; }
    }
    public class ReservedVO
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }
        /// <summary>
        /// 预约类型
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }
        /// <summary>
        /// 车系  
        /// </summary>
        public string CarVehicle { get; set; }
        /// 车牌号
        /// </summary>
        public string CarNO { get; set; }
        /// <summary>
        /// 到店逾期状态 0逾期 1到店 2 到店逾期
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 渠道 1c端微信小程序 2 s端
        /// </summary>
        public string Channel { get; set; }
    }
}
