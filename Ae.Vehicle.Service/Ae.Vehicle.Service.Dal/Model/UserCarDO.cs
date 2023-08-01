using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("user_car")]
    public class UserCarDO
    {
        /// <summary>
        /// 车Id
        /// </summary>
        [Key]
        [Column("id")]
        public Guid CarId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Column("nick_name")]
        public string NickName { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        [Column("car_number")]
        public string CarNumber { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        [Column("vehicle")]
        public string Vehicle { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        [Column("vehicle_id")]
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        [Column("pai_liang")]
        public string PaiLiang { get; set; }

        /// <summary>
        /// 年
        /// </summary>
        [Column("nian")]
        public string Nian { get; set; }

        /// <summary>
        /// tid
        /// </summary>
        [Column("tid")]
        public string Tid { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        [Column("sales_name")]
        public string SalesName { get; set; }

        /// <summary>
        /// 购买年份
        /// </summary>
        [Column("buy_year")]
        public string BuyYear { get; set; }

        /// <summary>
        /// 购买月份
        /// </summary>
        [Column("buy_month")]
        public string BuyMonth { get; set; }

        /// <summary>
        /// 保险到期日
        /// </summary>
        [Column("insure_expire_date")]
        public DateTime? InsureExpireDate { get; set; }

        /// <summary>
        /// 公里数
        /// </summary>
        [Column("total_mileage")]
        public int TotalMileage { get; set; }

        /// <summary>
        /// 最后一次保养公里数
        /// </summary>
        [Column("last_bao_yang_km")]
        public int LastBaoYangKm { get; set; }

        /// <summary>
        /// 最后一次保养时间
        /// </summary>
        [Column("last_bao_yang_time")]
        public DateTime? LastBaoYangTime { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        [Column("vin_code")]
        public string VinCode { get; set; }

        /// <summary>
        /// 默认车型
        /// </summary>
        [Column("default_car")]
        public bool DefaultCar { get; set; }

        /// <summary>
        /// 发动机编号
        /// </summary>
        [Column("engine_no")]
        public string EngineNo { get; set; }

        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        [Column("tire_size_for_single")]
        public string TireSizeForSingle { get; set; }

        /// <summary>
        /// 保险公司
        /// </summary>
        [Column("insurance_company")]
        public string InsuranceCompany { get; set; }

        /// <summary>
        /// 行驶证注册时间
        /// </summary>
        [Column("registration_time")]
        public DateTime? RegistrationTime { get; set; }

        /// <summary>
        /// 车牌所属省份
        /// </summary>
        [Column("car_no_province")]
        public string CarNoProvince { get; set; }

        /// <summary>
        /// 车牌所属城市
        /// </summary>
        [Column("car_no_city")]
        public string CarNoCity { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        [Column("data_source")]
        public string DataSource { get; set; }

        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
