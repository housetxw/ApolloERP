using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.BasicData.Service.Dal.Model
{
    [Table("app_operation_log")]
    public class AppOperationLogDO
    {
        /// <summary>
        /// 日志编号
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 日志场景类型，业务模块
        /// </summary>
        [Column("scene")]
        public string Scene { get; set; } = string.Empty;

        /// <summary>
        /// 日志内容
        /// </summary>
        [Column("content")]
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 日志类型（业务成功，失败……）
        /// </summary>
        [Column("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 发生时间
        /// </summary>
        [Column("occurence_time")]
        public DateTime OccurenceTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 发生门店id
        /// </summary>
        [Column("org_id")]
        public long OrgId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// App渠道
        /// </summary>
        [Column("channel")]
        public string Channel { get; set; } = string.Empty;

        /// <summary>
        /// 机型信息
        /// </summary>
        [Column("phone_info")]
        public string PhoneInfo { get; set; } = string.Empty;

        /// <summary>
        /// Android, IOS，手持设备
        /// </summary>
        [Column("platform")]
        public string Platform { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
