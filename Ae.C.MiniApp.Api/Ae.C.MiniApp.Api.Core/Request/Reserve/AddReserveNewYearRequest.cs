using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class AddReserveNewYearRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// 车ID
        /// </summary>
        public string CarId { get; set; } = string.Empty;
        /// <summary>
        /// 预约日期
        /// </summary>
        public int ReserveDate { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        [Required(ErrorMessage = "预约时间不能为空")]
        public string ReserveTime { get; set; }
        /// <summary>
        /// 预约项目Code
        /// </summary>
        public string ServiceCode { get; set; } = string.Empty;
        /// <summary>
        /// 服务大类名称
        /// </summary>
        public string ServiceName { get; set; } = string.Empty;
        /// <summary>
        /// 预约图片
        /// </summary>
        public List<ImgDTO> Imgs { get; set; }
        /// <summary>
        /// 预约商品
        /// </summary>
        public List<ReserveProductVO> productItem { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
