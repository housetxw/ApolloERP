using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model
{
    [Table("reserve_picture")]
    public class ReservePictureDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("reserve_id")]
        public long ReserveId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("url")]
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("created_by")]
        public string CreatedBy { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Column("created_time")]
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("updated_by")]
        public string UpdatedBy { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Column("updated_time")]
        public DateTime UpdatedTime { get; set; }
    }
}
