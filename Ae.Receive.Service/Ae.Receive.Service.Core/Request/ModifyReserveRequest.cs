using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class ModifyReserveRequest : BaseGetRequest
    {
        /// <summary>
        /// 预约ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "预约ID必须大于0")]
        public long ReserveId { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        [Required(ErrorMessage = "年份不能为空")]
        public string Year { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        [Required(ErrorMessage = "预约日期不能为空")]
        public string ReserveDate { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        [Required(ErrorMessage = "预约时间不能为空")]
        public string ReserveTime { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 车ID
        /// </summary>
        public string CarId { get; set; }
        /// <summary>
        /// 预约项目Code
        /// </summary>
        public string ServiceCode { get; set; }
        /// <summary>
        /// 服务大类名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
