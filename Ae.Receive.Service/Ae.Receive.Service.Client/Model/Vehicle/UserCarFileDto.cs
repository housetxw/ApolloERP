using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Model.Vehicle
{
    public class UserCarFileDto
    {
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 品牌图片地址
        /// </summary>
        public string BrandUrl { get; set; }

        /// <summary>
        /// 车系不能为空
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
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 保险到期日
        /// </summary>
        public DateTime? InsureExpireDate { get; set; }

        /// <summary>
        /// 公里数
        /// </summary>
        public int TotalMileage { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        public string VinCode { get; set; }

        /// <summary>
        /// 保险公司
        /// </summary>
        public string InsuranceCompany { get; set; }

        /// <summary>
        /// 车辆部件概况
        /// </summary>
        public List<CarPartsSituationDto> CarPartsSituation { get; set; }
    }

    /// <summary>
    /// 车辆部件概况
    /// </summary>
    public class CarPartsSituationDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 部件信息
        /// </summary>
        public List<CarPartsItemDto> Items { get; set; }
    }

    /// <summary>
    /// 部件
    /// </summary>
    public class CarPartsItemDto
    {
        /// <summary>
        /// 部件Id
        /// </summary>
        public long PartsId { get; set; }

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
}
