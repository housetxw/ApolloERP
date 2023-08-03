using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_check_result_image")]
    public class ShopCheckResultImageDo
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
        /// 分类Id
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }
        /// <summary>
        /// 图片连接
        /// </summary>
        [Column("url")]
        public string Url { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        [Column("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 排序
        /// </summary>
        [Column("rank")]
        public int Rank { get; set; }
        /// <summary>
        /// 检查项目Id
        /// </summary>
        [Column("property_id")]
        public long PropertyId { get; set; }
        /// <summary>
        /// 提交批次Id
        /// </summary>
        [Column("submit_batch_id")]
        public long SubmitBatchId { get; set; }

        /// <summary>
        /// 图片类型0正常 1异常
        /// </summary>
        [Column("image_type")]
        public int ImageType { get; set; }

        /// <summary>
        /// 删除
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
