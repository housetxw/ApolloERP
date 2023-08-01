using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 更新车辆部位检查信息
    /// </summary>
    public class UpdateCarPartCheckResultRequest
    {
        /// <summary>
        /// 车辆Id
        /// </summary>
        [Required(ErrorMessage = "CarId不能为空")]
        public string CarId { get; set; }

        /// <summary>
        /// 车辆异常部位
        /// </summary>
        public List<CarComponentsRequest> CarErrorComponents { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }

        /// <summary>
        /// 检查Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int CategoryId { get; set; }
    }

    /// <summary>
    /// 车辆异常部位
    /// </summary>
    public class CarComponentsRequest
    {
        /// <summary>
        /// 部位Code
        /// </summary>
        public string KeyName { get; set; }

        /// <summary>
        /// 配置项Id
        /// </summary>
        public List<long> PropertyId { get; set; }
    }
}
