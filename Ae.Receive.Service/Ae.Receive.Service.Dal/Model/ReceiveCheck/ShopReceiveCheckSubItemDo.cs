using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    [Table("shop_receive_check_sub_item")]
    public class ShopReceiveCheckSubItemDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 配置Id
        /// </summary>
        [Column("config_id")]
        public long ConfigId { get; set; }
        /// <summary>
        /// 前缀
        /// </summary>
        [Column("prefix")]
        public string Prefix { get; set; }
        /// <summary>
        /// 后缀
        /// </summary>
        [Column("suffix")]
        public string Suffix { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("rank")]
        public decimal Rank { get; set; }
        /// <summary>
        /// 0单选  1多选
        /// </summary>
        [Column("check_type")]
        public int CheckType { get; set; }
        /// <summary>
        /// input 数量
        /// </summary>
        [Column("check_count")]
        public int CheckCount { get; set; }
        /// <summary>
        /// 是否需要计算
        /// </summary>
        [Column("is_compute")]
        public bool IsCompute { get; set; }
        /// <summary>
        /// 是否需要拍照
        /// </summary>
        [Column("need_photo")]
        public sbyte NeedPhoto { get; set; }
        /// <summary>
        /// 操作类型 input-num,input-txt,checkbox,checkbox-input-num,checkbox-input-txt,checkbox-scancode-battery,image,radio
        /// </summary>
        [Column("opt_type")]
        public string OptType { get; set; }
        /// <summary>
        /// 操作建议
        /// </summary>
        [Column("suggestion")]
        public string Suggestion { get; set; }
        /// <summary>
        /// 分组
        /// </summary>
        [Column("group_name")]
        public string GroupName { get; set; }
        /// <summary>
        /// 检查项Id
        /// </summary>
        [Column("check_item_main_id")]
        public long CheckItemMainId { get; set; }
        /// <summary>
        /// 数量显示
        /// </summary>
        [Column("number_limit")]
        public string NumberLimit { get; set; }
        /// <summary>
        /// 标记删除
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
