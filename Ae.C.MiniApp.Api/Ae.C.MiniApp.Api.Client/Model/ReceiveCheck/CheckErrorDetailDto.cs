using Ae.C.MiniApp.Api.Client.Response.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.ReceiveCheck
{
    public class CheckErrorDetailDto
    {
        /// <summary>
        /// 检查日期
        /// </summary>
        public string CheckDate { get; set; }

        /// <summary>
        /// 门店简单名
        /// </summary>
        public string ShopSimpleName { get; set; }

        /// <summary>
        /// 异常项
        /// </summary>
        public List<CheckPropertyResultDto> ResultItems { get; set; }
    }

    /// <summary>
    /// 单项检查结果
    /// </summary>
    public class CheckPropertyResultDto
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public long PropertyId { get; set; }

        /// <summary>
        /// 检查项显示
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 异常图片
        /// </summary>
        public List<string> Images { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        public List<ErrorDesListDto> ErrorDesList { get; set; }
    }
}
