using Ae.Receive.Service.Core.Model.Arrival;
using Ae.Receive.Service.Core.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.Arrival
{
    /// <summary>
    /// 到店记录详情返回对象
    /// </summary>
    public class GetArrivalDetailResponse
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public string ArrivalId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Telephone { get; set; }


        /// <summary>
        /// 到店时间
        /// </summary>
        public string ShowArrivalDate { get; set; }

        /// <summary>
        /// 显示到店状态
        /// </summary>
        public string ShowArrivalStatus { get; set; }

        /// <summary>
        /// 接车人
        /// </summary>
        public string PickOne { get; set; }

        /// <summary>
        /// 到店备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 车辆信息
        /// </summary>
        public CarVo CarInfo { get; set; }

        /// <summary>
        /// 预约对象
        /// </summary>
        public ReserverVo ReserverInfo { get; set; }

        /// <summary>
        /// 项目信息列表
        /// </summary>
        public List<ProjectItemVo> ProjectItems { get; set; }

        /// <summary>
        /// 历史到店记录记录
        /// </summary>
        public List<HistoryArrivalVo> HistoryArrivals { get; set; }

        /// <summary>
        /// 订单中部操作
        /// </summary>
        public List<UserOperationVo> UserOperations { get; set; }

        /// <summary>
        /// 未消费离店对象
        /// </summary>
        public string LeaveShopReasonInfo { get; set; }

        /// <summary>
        /// 车辆检测信息
        /// </summary>
        public VehicleCheckVo VehicleCheckInfo { get; set; }


        /// <summary>
        /// 历史订单
        /// </summary>
        public List<ArrivalOrderVO> HistoryOrders { get; set; }
        /// <summary>
        /// 本次服务订单
        /// </summary>
        public List<ArrivalOrderVO> ArrivalOrders { get; set; }

        /// <summary>
        /// 车辆报告
        /// </summary>
        public string CarReport { get; set; }


    }

    /// <summary>
    /// 检查报告
    /// </summary>
    public class VehicleCheckVo
    {
        /// <summary>
        /// 检测Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 正常数量
        /// </summary>
        public int NormalCheckNum { get; set; }

        /// <summary>
        /// 异常检测数量
        /// </summary>
        public int ExceptionCheckNum { get; set; }

        /// <summary>
        /// 未检测数量
        /// </summary>
        public int NoCheckNum { get; set; }

        /// <summary>
        /// 检测状态
        /// </summary>
        public int Status { get; set; }
    }
}
