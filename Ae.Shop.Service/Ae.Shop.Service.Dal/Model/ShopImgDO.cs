using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using Ae.Shop.Service.Common.Helper;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("shop_img")]
    public class ShopImgDO
    {
        /// <summary>
        /// 门店图片主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 门店id
        /// </summary>
        [Column("shop_id")]
        [CompareDiffAttribute(Name = " 门店id")]
        public long ShopId { get; set; } 
        /// <summary>
        /// 图片路径
        /// </summary>
        [Column("img_url")]
        [CompareDiffAttribute(Name = "图片路径")]
        public string ImgUrl { get; set; } = string.Empty;
        /// <summary>
        /// 图片类型 1门头图片 2门店照片 3资质证明 4正面照 5营业执照  7经营许可证
        /// </summary>
        [Column("type")]
        [CompareDiffAttribute(Name = "图片类型")]
        public sbyte Type { get; set; } 
        /// <summary>
        /// 删除标识 0未删除 1已删除
        /// </summary>
        [Column("is_deleted")]
        [CompareDiffAttribute(Name = "删除标识")]
        public sbyte IsDeleted { get; set; } 
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        [CompareDiffAttribute(Name = "创建人")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        [CompareDiffAttribute(Name = "创建时间")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        [CompareDiffAttribute(Name = "修改人")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        [CompareDiffAttribute(Name = "修改时间")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}