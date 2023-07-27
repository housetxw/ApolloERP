using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Core.Model
{
    public class AppOperationLogDTO
    {
        /// <summary>
        /// 日志编号
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 日志场景类型，业务模块
        /// </summary>
        public string Scene { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 日志类型（业务成功，失败……）
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime OccurenceTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 发生门店id
        /// </summary>
        public long OrgId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// App渠道
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// 机型信息
        /// </summary>
        public string PhoneInfo { get; set; }

        /// <summary>
        /// Android, IOS，手持设备
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

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
}
