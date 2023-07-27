using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.Shop.Service.Dal.Model
{
    [Table("advertisement")]
    public class AdvertisementDO
    {
        /// <summary>
        /// 广告主键ID
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 标题
        /// </summary>
        [Column("title")]
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 链接地址
        /// </summary>
        [Column("url")]
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// 图片地址
        /// </summary>
        [Column("img_url")]
        public string ImgUrl { get; set; } = string.Empty;
        /// <summary>
        /// 广告类型 
        /// </summary>
        [Column("type")]
        public sbyte Type { get; set; } 
        /// <summary>
        /// 有效开始时间
        /// </summary>
        [Column("start_date")]
        public DateTime StartDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 有效结束时间
        /// </summary>
        [Column("end_date")]
        public DateTime EndDate { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 是否展示
        /// </summary>
        [Column("is_display")]
        public sbyte IsDisplay { get; set; } 
        /// <summary>
        /// 是否删除 0否 1是
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