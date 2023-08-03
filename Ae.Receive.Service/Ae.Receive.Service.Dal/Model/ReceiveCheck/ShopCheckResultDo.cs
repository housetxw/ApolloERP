using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_check_result")]
    public class ShopCheckResultDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 检查报告Id
        /// </summary>
        [Column("check_id")]
        public long CheckId { get; set; }
        /// <summary>
        /// 检查项目Id
        /// </summary>
        [Column("property_id")]
        public long PropertyId { get; set; }

        /// <summary>
        /// 属性值类型：0检查项值  1检查结果值
        /// </summary>
        [Column("property_type")]
        public int PropertyType { get; set; }

        /// <summary>
        /// 文本值
        /// </summary>
        [Column("text_value")]
        public string TextValue { get; set; } = string.Empty;
        /// <summary>
        /// 数值值
        /// </summary>
        [Column("numeric_value")]
        public decimal NumericValue { get; set; }
        /// <summary>
        /// 分类Id
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }
        /// <summary>
        /// 提交批次Id
        /// </summary>
        [Column("submit_batch_id")]
        public long SubmitBatchId { get; set; }
        /// <summary>
        /// 结果词Json串
        /// </summary>
        [Column("result_words_json")]
        public string ResultWordsJson { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除
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
