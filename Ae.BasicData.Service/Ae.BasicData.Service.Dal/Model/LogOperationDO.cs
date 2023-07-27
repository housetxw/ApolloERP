using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.BasicData.Service.Dal.Model
{
    [Table("log_operation")]
    public class LogOperationDO
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 日志记录的业务主键id
        /// </summary>
        [Column("log_id")]
        public string LogId { get; set; }

        /// <summary>
        /// C：新增；R：查询；U：更新；D：删除
        /// </summary>
        [Column("log_type")]
        public string LogType { get; set; }

        /// <summary>
        /// account: 账号业务日志；authority：权限业务日志；等等...
        /// </summary>
        [Column("biz_type")]
        public string BizType { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        [Column("req_param")]
        public string ReqParam { get; set; }

        /// <summary>
        /// 操作前内容
        /// </summary>
        [Column("operated_before_content")]
        public string OperatedBeforeContent { get; set; }

        /// <summary>
        /// 操作后内容
        /// </summary>
        [Column("operated_after_content")]
        public string OperatedAfterContent { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Column("operator")]
        public string Operator { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column("comment")]
        public string Comment { get; set; } = "";

        /// <summary>
        /// 是否删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = "";

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
