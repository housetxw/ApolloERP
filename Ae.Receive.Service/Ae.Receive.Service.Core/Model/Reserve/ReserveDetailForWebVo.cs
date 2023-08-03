using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Reserve
{
    /// <summary>
    /// Shop站点预约详情
    /// </summary>
    public class ReserveDetailForWebVo
    {
        /// <summary>
        /// 预约Id
        /// </summary>
        public long ReserveId { get; set; }

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
        /// 预约类型
        /// </summary>
        public string ReserveType { get; set; }

        /// <summary>
        /// 预约类型 显示
        /// </summary>
        public string ReserveTypeName { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserveTime { get; set; }

        /// <summary>
        /// 预约技师
        /// </summary>
        public string Technician { get; set; }

        /// <summary>
        /// 技师Id
        /// </summary>
        public string TechId { get; set; }

        /// <summary>
        /// 预约车辆信息
        /// </summary>
        public ReserveVehicleVo Vehicle { get; set; }

        /// <summary>
        /// 预约项目
        /// </summary>
        public List<ReserveProject> Projects { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 备注图片
        /// </summary>
        public List<string> ImageList { get; set; }

        /// <summary>
        /// 处理记录
        /// </summary>
        public List<ReserveTrackVo> HistoryProcessList { get; set; }

        /// <summary>
        /// 可操作按钮 CanCancel 可取消  CanModify 可修改
        /// </summary>
        public List<string> Operation { get; set; } = new List<string>();

        /// <summary>
        /// 预约渠道
        /// </summary>
        public int ReserveChannel { get; set; }

        /// <summary>
        /// 渠道显示
        /// </summary>
        public string ChannelDisplay { get; set; }

        /// <summary>
        /// 历史到店
        /// </summary>
        public List<HistoryReceiveVo> HistoryReceive { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public string CreatedTime { get; set; }
    }

    /// <summary>
    /// 历史到店记录
    /// </summary>
    public class HistoryReceiveVo
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public long RecId { get; set; }

        /// <summary>
        /// 到店时间
        /// </summary>
        public string ReceiveTime { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 接待人
        /// </summary>
        public string ReceiveBy { get; set; }
    }
}
