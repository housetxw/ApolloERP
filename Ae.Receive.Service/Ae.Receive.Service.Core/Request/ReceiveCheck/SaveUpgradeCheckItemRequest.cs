using Ae.Receive.Service.Core.Response.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveUpgradeCheckItemRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "到店记录编号必须大于0")]
        public long RecId { get; set; }

        /// <summary>
        /// 检查报告Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 项目Code
        /// </summary>
        public string CheckModuleCode { get; set; }

        /// <summary>
        /// 检查结果
        /// </summary>
        public List<UpgradeProjectRequest> CheckResult { get; set; }

        /// <summary>
        /// 异常图片
        /// </summary>
        public List<UpgradeProjectImageRequest> CheckItemImages { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// 升级项检查
    /// </summary>
    public class UpgradeProjectRequest
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public long CheckItemMainId { get; set; }

        /// <summary>
        /// 检查结果Id
        /// </summary>
        public long CheckSubItemId { get; set; }

        /// <summary>
        /// 输入框的值
        /// </summary>
        public string TextValue { get; set; }

        /// <summary>
        /// 结果词
        /// </summary>
        public List<CheckResultWord> ResultWords { get; set; }
    }

    /// <summary>
    /// 图片
    /// </summary>
    public class UpgradeProjectImageRequest
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public long CheckItemMainId { get; set; }

        /// <summary>
        /// 异常图片
        /// </summary>
        public List<string> Images { get; set; }
    }
}
