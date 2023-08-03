using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_receive_check_result_word")]
    public class ShopReceiveCheckResultWordDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 检查报告Id
        /// </summary>
        [Column("check_id")]
        public long CheckId { get; set; }
        /// <summary>
        /// 结果Id
        /// </summary>
        [Column("check_result_id")]
        public long CheckResultId { get; set; }
        /// <summary>
        /// 分类Id
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }
        /// <summary>
        /// 结果词Id
        /// </summary>
        [Column("result_word_id")]
        public int ResultWordId { get; set; }
        /// <summary>
        /// 提交批次Id
        /// </summary>
        [Column("submit_batch_id")]
        public long SubmitBatchId { get; set; }
        /// <summary>
        /// 删除
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
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
