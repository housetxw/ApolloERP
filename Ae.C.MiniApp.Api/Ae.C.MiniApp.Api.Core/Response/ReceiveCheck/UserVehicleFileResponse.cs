﻿using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.ReceiveCheck
{
    public class UserVehicleFileResponse
    {
        /// <summary>
        /// 主数据
        /// </summary>
        public VehicleFileMainData MainData { get; set; }

        /// <summary>
        /// 车辆部件概况
        /// </summary>
        public List<CarPartsSituation> CarPartsSituation { get; set; }

        /// <summary>
        /// 维修记录
        /// </summary>
        public Maintenancerecord Maintenancerecord { get; set; }
    }

    /// <summary>
    /// 主数据
    /// </summary>
    public class VehicleFileMainData
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 客户手机号界面显示  可能脱敏
        /// </summary>
        public string UserTelDisplay { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 车品牌url
        /// </summary>
        public string BrandUrl { get; set; }

        /// <summary>
        /// 车型展示
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 最近一次到店时间
        /// </summary>
        public string LastInTime { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// 到店次数
        /// </summary>
        public int ReceiveCount { get; set; }

        /// <summary>
        /// 总消费额
        /// </summary>
        public decimal TotalMoney { get; set; }

        /// <summary>
        /// 保险公司
        /// </summary>
        public string InsuranceCompany { get; set; }

        /// <summary>
        /// 保险到期日
        /// </summary>
        public string InsureExpireDate { get; set; }
    }

    /// <summary>
    /// 车辆部件概况
    /// </summary>
    public class CarPartsSituation
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 部件信息
        /// </summary>
        public List<CarPartsItem> Items { get; set; }
    }

    /// <summary>
    /// 部件
    /// </summary>
    public class CarPartsItem
    {
        /// <summary>
        /// 部件Id
        /// </summary>
        public int PartsId { get; set; }

        /// <summary>
        /// code
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 状态 0正常  1异常
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 维修记录
    /// </summary>
    public class Maintenancerecord
    {
        /// <summary>
        /// 服务大类
        /// </summary>
        public List<ServiceCategory> ServiceCategory { get; set; } = new List<ServiceCategory>();

        /// <summary>
        /// 服务记录
        /// </summary>
        public List<ServiceRecord> ServiceRecord { get; set; } = new List<ServiceRecord>();
    }

    /// <summary>
    /// 服务大类
    /// </summary>
    public class ServiceCategory
    {
        /// <summary>
        /// 服务Code
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// 服务记录
    /// </summary>
    public class ServiceRecord
    {
        /// <summary>
        /// 到店时间
        /// </summary>
        public string ReceiveTime { get; set; }

        /// <summary>
        /// 门店名
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        public int? Mileage { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public List<TagInfo> Tags { get; set; }

        /// <summary>
        /// 服务项目
        /// </summary>
        public List<ServiceProject> Projects { get; set; }
    }

    /// <summary>
    /// 服务项目
    /// </summary>
    public class ServiceProject
    {
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 施工技师
        /// </summary>
        public string TechName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalMoney { get; set; }
    }
}
