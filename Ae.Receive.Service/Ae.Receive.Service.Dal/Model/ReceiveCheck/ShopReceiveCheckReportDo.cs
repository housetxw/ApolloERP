using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_receive_check_report")]
    public class ShopReceiveCheckReportDo
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
        [Column("check_id")]
        public long CheckId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("receive_id")]
        public long ReceiveId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("normal_count")]
        public int NormalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("abnormal_count")]
        public int AbnormalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("uncheck_count")]
        public int UncheckCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("version_num")]
        public int VersionNum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("mobile_summary")]
        public string MobileSummary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
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
