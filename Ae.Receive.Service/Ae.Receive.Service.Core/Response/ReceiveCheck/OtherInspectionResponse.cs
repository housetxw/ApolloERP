using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.ReceiveCheck
{
    /// <summary>
    /// 其他检查Response
    /// </summary>
    public class OtherInspectionResponse
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 附加检查项
        /// </summary>
        public List<OtherProjectResult> OtherProjectResult { get; set; } = new List<OtherProjectResult>();
    }

    /// <summary>
    /// 附加信息
    /// </summary>
    public class OtherProjectResult
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public long ProjectId { get; set; }

        /// <summary>
        /// 检查项名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目Code
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 结果值 0未检查 1有 2无
        /// </summary>
        public decimal ResultValue { get; set; }

        /// <summary>
        /// 检查结果图片图片
        /// </summary>
        public List<string> Image { get; set; }
    }
}
