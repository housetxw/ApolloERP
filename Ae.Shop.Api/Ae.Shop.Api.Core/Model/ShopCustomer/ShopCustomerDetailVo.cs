using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.ShopCustomer
{
    /// <summary>
    /// 门店客户详情
    /// </summary>
    public class ShopCustomerDetailVo
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 联系姓名
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 性别（0未设置 1男 2女）
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string BirthDay { get; set; }

        /// <summary>
        /// 用户手机号[脱敏]
        /// </summary>
        public string UserTelDes { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 会员等级显示
        /// </summary>
        public string MemberLevel { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        public string PersonalSign { get; set; }

        /// <summary>
        /// 最近到店时间
        /// </summary>
        public string LastArriveTime { get; set; }

        /// <summary>
        /// 用户车型信息
        /// </summary>
        public List<UserVehicleVo> Vehicles { get; set; }

        /// <summary>
        /// 驾驶证到期日
        /// </summary>
        public string DriverLicenseExpiry { get; set; }
    }

    public class UserVehicleVo
    {
        /// <summary>
        /// 车Id
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
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
        public string Brand { get; set; }

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
        public List<VehiclePropertyVo> CarProperties { get; set; }

        /// <summary>
        /// 车型拼接
        /// </summary>
        public string CarType { get; set; }
    }

    /// <summary>
    /// 五级属性
    /// </summary>
    public class VehiclePropertyVo
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
