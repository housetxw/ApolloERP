using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.User.Api.Core.Request.Vehicle
{
    /// <summary>
    /// 
    /// </summary>
    public class AddUserCarRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 车昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [Required(ErrorMessage = "品牌不能为空")]
        public string Brand { get; set; }

        /// <summary>
        /// 车系不能为空
        /// </summary>
        [Required(ErrorMessage = "车系不能为空")]
        public string Vehicle { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        [Required(ErrorMessage = "车系Id不能为空")]
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
        /// 购买年份
        /// </summary>
        public string BuyYear { get; set; }

        /// <summary>
        /// 购买月份
        /// </summary>
        public string BuyMonth { get; set; }

        /// <summary>
        /// 保险到期日
        /// </summary>
        public DateTime? InsureExpireDate { get; set; }

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
        public DateTime? LastBaoYangTime { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        public string VinCode { get; set; }

        /// <summary>
        /// 默认车型
        /// </summary>
        public bool DefaultCar { get; set; }

        /// <summary>
        /// 发动机编号
        /// </summary>
        public string EngineNo { get; set; }

        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        public string TireSizeForSingle { get; set; }

        /// <summary>
        /// 保险公司
        /// </summary>
        public string InsuranceCompany { get; set; }

        /// <summary>
        /// 行驶证注册时间
        /// </summary>
        public DateTime? RegistrationTime { get; set; }

        /// <summary>
        /// 车牌所属省份
        /// </summary>
        public string CarNoProvince { get; set; }

        /// <summary>
        /// 车牌所属城市
        /// </summary>
        public string CarNoCity { get; set; }

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
