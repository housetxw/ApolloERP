using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.ReceiveCheck
{
    /// <summary>
    /// 仪表盘Response
    /// </summary>
    public class CarDashboardResponse
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// Vin码
        /// </summary>
        public string VinCode { get; set; } = string.Empty;

        /// <summary>
        /// 里程数
        /// </summary>
        public int Mileage { get; set; }

        /// <summary>
        /// 仪表盘
        /// </summary>
        public string DashboardImg { get; set; } = string.Empty;

        /// <summary>
        /// 故障灯检查结果
        /// </summary>
        public List<NormalProjectResult> LightCheckResult { get; set; } = new List<NormalProjectResult>();
    }

    /// <summary>
    /// 常规检查项结果
    /// </summary>
    public class NormalProjectResult
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
        /// 结果值 0正常  1异常
        /// </summary>
        public decimal ResultValue { get; set; }

        /// <summary>
        /// 异常项Id
        /// </summary>
        public long AbnormalId { get; set; }

        /// <summary>
        /// 检查结果图片图片
        /// </summary>
        public List<ErrorImageVo> Image { get; set; }

        /// <summary>
        /// 示例图片
        /// </summary>
        public List<ExampleImage> ExampleImage { get; set; }
    }

    /// <summary>
    /// 异常图片
    /// </summary>
    public class ErrorImageVo
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// 检查项图片
    /// </summary>
    public class ExampleImage
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 0正常  1异常
        /// </summary>
        public int Type { get; set; }
    }
}
