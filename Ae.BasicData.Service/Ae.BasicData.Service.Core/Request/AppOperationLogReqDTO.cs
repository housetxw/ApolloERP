using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BasicData.Service.Core.Request
{
    public class AppOperationLogReqDTO { }

    public class CreateAppOperationLogReqDTO
    {
        /// <summary>
        /// 日志场景类型，业务模块
        /// </summary>
        public string Scene { get; set; } = string.Empty;

        /// <summary>
        /// 日志内容
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 日志类型（业务成功，失败……）
        /// </summary>
        public string Type { get; set; } = string.Empty;

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
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// App渠道
        /// </summary>
        public string Channel { get; set; } = string.Empty;

        /// <summary>
        /// 机型信息
        /// </summary>
        public string PhoneInfo { get; set; } = string.Empty;

        /// <summary>
        /// Android, IOS，手持设备
        /// </summary>
        public string Platform { get; set; } = string.Empty;
    }

}
