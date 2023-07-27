using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Ae.BasicData.Service.Core.Enums;

namespace Ae.BasicData.Service.Core.Model
{
    public class LogOperationDTO
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 日志记录的业务主键id
        /// </summary>
        public string LogId { get; set; } = "";

        /// <summary>
        /// C：新增；R：查询；U：更新；D：删除
        /// </summary>
        public string LogType { get; set; } = "";

        /// <summary>
        /// DB 表名称
        /// </summary>
        public string BizType { get; set; } = "";

        /// <summary>
        /// 请求参数
        /// </summary>
        public string ReqParam { get; set; } = "";

        /// <summary>
        /// 操作前内容
        /// </summary>
        public string OperatedBeforeContent { get; set; } = "";

        /// <summary>
        /// 操作后内容
        /// </summary>
        public string OperatedAfterContent { get; set; } = "";

        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; } = "";

        /// <summary>
        /// 描述
        /// </summary>
        public string Comment { get; set; } = "";

        /// <summary>
        /// 是否删除
        /// </summary>
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = "";

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    public enum LogType
    {
        /// <summary>
        /// Create
        /// </summary>
        [Description("C")]
        C = 0,

        /// <summary>
        /// Retrieve/Query
        /// </summary>
        [Description("R")]
        R = 1,

        /// <summary>
        /// Update
        /// </summary>
        [Description("U")]
        U = 2,

        /// <summary>
        /// Logically deleted
        /// </summary>
        [Description("D")]
        D = 3
    }
}
