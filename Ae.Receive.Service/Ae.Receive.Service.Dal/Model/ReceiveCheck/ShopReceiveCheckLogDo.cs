using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_receive_check_log")]
    public class ShopReceiveCheckLogDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 检查Id
        /// </summary>
        [Column("check_id")]
        public long CheckId { get; set; }
        /// <summary>
        /// 检查项Code
        /// </summary>
        [Column("check_module_code")]
        public string CheckModuleCode { get; set; }
        /// <summary>
        /// 检查项
        /// </summary>
        [Column("check_module")]
        public string CheckModule { get; set; }
        /// <summary>
        /// 类型id
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }

        /// <summary>
        /// 异常数量
        /// </summary>
        [Column("error_count")]
        public int ErrorCount { get; set; }

        /// <summary>
        /// 正常数量
        /// </summary>
        [Column("normal_count")]
        public int NormalCount { get; set; }

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
