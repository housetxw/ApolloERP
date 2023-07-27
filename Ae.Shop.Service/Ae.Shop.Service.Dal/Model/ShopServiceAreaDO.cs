using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("shop_service_area")]
    public class ShopServiceAreaDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// 类型：0三级省市区 1公里数
        /// </summary>
        [Column("type")]
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("province_id")]
        public string ProvinceId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("city_id")]
        public string CityId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("district_id")]
        public string DistrictId { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("province")]
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("city")]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("district")]
        public string District { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("distance")]
        public decimal Distance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
