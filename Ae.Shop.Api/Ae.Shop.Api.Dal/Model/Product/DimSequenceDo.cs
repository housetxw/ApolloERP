using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model.Product
{
    [Table("dim_sequence")]
    public class DimSequenceDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("seq_name")]
        public string SeqName { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Column("current_val")]
        public int CurrentVal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("increment_val")]
        public int IncrementVal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }
    }
}
