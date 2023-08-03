using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    /// <summary>
    /// 预约详情B端
    /// </summary>
    public class ReserveDetailVo
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
        /// 预约地址
        /// </summary>
        public string ReserveAddress { get; set; }

        /// <summary>
        /// 预约车辆信息
        /// </summary>
        public ReserveVehicleVo Vehicle { get; set; }

        /// <summary>
        /// 预约项目
        /// </summary>
        public List<ReserveProject> Projects { get; set; }

        /// <summary>
        /// 关联订单
        /// </summary>
        public List<RelationOrderVo> Orders { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 备注图片
        /// </summary>
        public List<ReserveImageVo> ImageList { get; set; }

        /// <summary>
        /// 处理记录
        /// </summary>
        public List<ReserveTrackVo> HistoryProcessList { get; set; }

        /// <summary>
        /// 是否可修改技师
        /// </summary>
        public bool ModifyTech { get; set; }

        /// <summary>
        /// 可操作按钮 CanCancel 可取消  CanModify 可修改  CreateReceive 可创建到店记录
        /// </summary>
        public List<string> Operation { get; set; } = new List<string>();
    }

    /// <summary>
    /// 预约图片
    /// </summary>
    public class ReserveImageVo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// 预约车辆信息
    /// </summary>
    public class ReserveVehicleVo
    {
        /// <summary>
        /// 车Id
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 品牌图片Url
        /// </summary>
        public string BrandUrl { get; set; }

        /// <summary>
        /// 车型展示
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 生产年份
        /// </summary>
        public string Nian { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }
    }

    /// <summary>
    /// 预约项目
    /// </summary>
    public class ReserveProject
    {
        /// <summary>
        /// 预约项目Id
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal TotalPrice { get; set; }
    }

    /// <summary>
    /// 处理记录
    /// </summary>
    public class ReserveTrackVo
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public string OptTitle { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public string OptTime { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public List<string> Detail { get; set; } = new List<string>();
    }

    /// <summary>
    /// 关联订单
    /// </summary>
    public class RelationOrderVo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalMoney { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public List<ReserveProject> Products { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalProductNum { get; set; }
    }
}
