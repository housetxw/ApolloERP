using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_receive_result_word")]
    public class ShopReceiveResultWordDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// code
        /// </summary>
        [Column("code")]
        public string Code { get; set; }
        /// <summary>
        /// name
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [Column("value")]
        public int Value { get; set; }
        /// <summary>
        /// 分组
        /// </summary>
        [Column("work_group")]
        public string WorkGroup { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Column("type")]
        public string Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
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
