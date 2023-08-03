using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.Receive.Service.Dal.Model.Arrival
{
    [Table("shop_arrival_video")]
    public class ShopArrivalVideoDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 到店id
        /// </summary>
        [Column("arrival_id")]
        public long ArrivalId { get; set; }
        /// <summary>
        /// 视频路径
        /// </summary>
        [Column("video_path")]
        public string VideoPath { get; set; } = string.Empty;
        /// <summary>
        /// 名称
        /// </summary>
        [Column("video_name")]
        public string VideoName { get; set; } = string.Empty;
        /// <summary>
        /// 分钟
        /// </summary>
        [Column("duration")]
        public long Duration { get; set; }
        /// <summary>
        /// 存储空间
        /// </summary>
        [Column("file_size")]
        public long FileSize { get; set; }
        /// <summary>
        /// 删除标识
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
