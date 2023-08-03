using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Api.Core.Model.Arrival;

namespace Ae.Shop.Api.Core.Response.Arrival
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
        /// 项目信息列表
        /// </summary>
        public List<ProjectItemVo> ProjectItems { get; set; }

        /// <summary>
        /// 施工视频
        /// </summary>
        public List<ArrivalVideo> ArrivalVideos { get; set; } = new List<ArrivalVideo>();

        /// <summary>
        /// 车辆检查报告
        /// </summary>
        public string CarReportUrl { get; set; } = string.Empty;

        /// <summary>
        /// 未消费离店对象
        /// </summary>
        public string LeaveShopReasonInfo { get; set; }



    }

    /// <summary>
    /// 施工视频
    /// </summary>
    public class ArrivalVideo
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 视频路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
