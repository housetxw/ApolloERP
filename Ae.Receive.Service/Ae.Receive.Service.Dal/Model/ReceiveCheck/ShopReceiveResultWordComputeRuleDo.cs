using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_receive_result_word_compute_rule")]
    public class ShopReceiveResultWordComputeRuleDo
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
        [Column("rule_id")]
        public long RuleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("result_word_id")]
        public int ResultWordId { get; set; }
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
        public string UpdateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
