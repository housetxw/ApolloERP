using Ae.Receive.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    public class GetReserveDetailForAppResponse
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }
        /// <summary>
        /// 车型信息 
        /// </summary>
        public string CarVehicle { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNO { get; set; }
        /// <summary>
        /// 预约类型
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserveTime { get; set; }
        /// <summary>
        /// 车标url
        /// </summary>
        public string CarLogo { get; set; }
        /// <summary>
        /// 技师名称
        /// </summary>
        public string TechName { get; set; }
        /// <summary>
        /// 已预约的订单
        /// </summary>
        public List<ReservedOrderVO> ReservedOrders { get; set; }
        /// <summary>
        /// 预约备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 处理记录
        /// </summary>
        public List<ReserveTrackDTO> TrackList { get; set; }
    }
    /// <summary>
    /// 已预约订单信息
    /// </summary>
    public class ReservedOrderVO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 车型信息 
        /// </summary>
        public string CarVehicle { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<string> Tags { get; set; }
        /// <summary>
        /// 预约产品列表
        /// </summary>
        public List<ReservedProductVO> ReservedProducts { get; set; }
    }
    /// <summary>
    /// 产品信息
    /// </summary>
    public class ReservedProductVO
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }
    }
}
