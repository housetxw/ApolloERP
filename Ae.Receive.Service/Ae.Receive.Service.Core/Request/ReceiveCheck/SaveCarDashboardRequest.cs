using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 
    /// </summary>
    public class SaveCarDashboardRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "到店记录编号必须大于0")]
        public long RecId { get; set; }

        /// <summary>
        /// 检查项Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// Vin码
        /// </summary>
        [Required(ErrorMessage = "VIN码不能为空")]
        public string VinCode { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "里程数必须大于0")]
        public int Mileage { get; set; }

        /// <summary>
        /// 仪表盘
        /// </summary>
        [Required(ErrorMessage = "仪表盘不能为空")]
        public string DashboardImg { get; set; }

        /// <summary>
        /// 故障灯检查结果
        /// </summary>
        public List<NormalProjectRequest> LightCheckResult { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// 常规项目Request
    /// </summary>
    public class NormalProjectRequest
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 结果值 0正常  1异常
        /// </summary>
        public decimal ResultValue { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public List<ErrorImageModel> Image { get; set; }
    }

    /// <summary>
    /// 异常图片
    /// </summary>
    public class ErrorImageModel
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图片上文案提示
        /// </summary>
        public List<string> Tips { get; set; }
    }
}
