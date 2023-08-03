﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Model.Vehicle
{
    public class UserVehicleDTO
    {
        /// <summary>
        /// 车Id
        /// </summary>
        public string CarId { get; set; } = string.Empty;

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 车昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNumber { get; set; } = string.Empty;

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// 车系不能为空
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;

        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; } = string.Empty;

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; } = string.Empty;

        /// <summary>
        /// 生产年份
        /// </summary>
        public string Nian { get; set; } = string.Empty;

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; } = string.Empty;

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; } = string.Empty;

        /// <summary>
        /// 购买年份
        /// </summary>
        public string BuyYear { get; set; } = string.Empty;

        /// <summary>
        /// 购买月份
        /// </summary>
        public string BuyMonth { get; set; } = string.Empty;

        /// <summary>
        /// 保险到期日
        /// </summary>
        public DateTime? InsureExpireDate { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 公里数
        /// </summary>
        public int TotalMileage { get; set; }

        /// <summary>
        /// 最后一次保养公里数
        /// </summary>
        public int LastBaoYangKm { get; set; }

        /// <summary>
        /// 最后一次保养时间
        /// </summary>
        public DateTime? LastBaoYangTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 车架号
        /// </summary>
        public string VinCode { get; set; } = string.Empty;

        /// <summary>
        /// 默认车型
        /// </summary>
        public bool DefaultCar { get; set; }

        /// <summary>
        /// 发动机编号
        /// </summary>
        public string EngineNo { get; set; } = string.Empty;

        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        public string TireSizeForSingle { get; set; } = string.Empty;

        /// <summary>
        /// 保险公司
        /// </summary>
        public string InsuranceCompany { get; set; } = string.Empty;

        /// <summary>
        /// 行驶证注册时间
        /// </summary>
        public DateTime? RegistrationTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 车牌所属省份
        /// </summary>
        public string CarNoProvince { get; set; } = string.Empty;

        /// <summary>
        /// 车牌所属城市
        /// </summary>
        public string CarNoCity { get; set; } = string.Empty;

        /// <summary>
        /// 五级属性集合
        /// </summary>
        public List<VehiclePropertyRequest> CarProperties { get; set; }
    }

    /// <summary>
    /// 五级属性
    /// </summary>
    public class VehiclePropertyRequest
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyKey { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string PropertyValue { get; set; }
    }
}
