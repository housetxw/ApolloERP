using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
 
namespace Ae.Shop.Service.Dal.Model
{
    [Table("join_in")]
    public class JoinInDO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 手机号
        /// </summary>
        [Column("phone")]
        public string Phone { get; set; } = string.Empty;
        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 省id
        /// </summary>
        [Column("province_id")]
        public int ProvinceId { get; set; } 
        /// <summary>
        /// 市id
        /// </summary>
        [Column("city_id")]
        public int CityId { get; set; } 
        /// <summary>
        /// 区id
        /// </summary>
        [Column("district_id")]
        public int DistrictId { get; set; } 
        /// <summary>
        /// 短地址（省市区）
        /// </summary>
        [Column("short_address")]
        public string ShortAddress { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0未删除 1已删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; } 
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}