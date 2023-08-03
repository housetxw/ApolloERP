using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request.Vehicle
{
    public class UpdateCarPartCheckResultClientRequest
    {
        /// <summary>
        /// 车辆Id
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 车辆异常部位
        /// </summary>
        public List<CarComponentsClientRequest> CarErrorComponents { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
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
    public class CarComponentsClientRequest
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
