using Ae.Receive.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class AddReserveV2Request
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 车ID
        /// </summary>
        [Required(ErrorMessage = "CarID不能为空")]
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
        public string ServiceCode { get; set; }
        /// <summary>
        /// 服务大类名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 预约图片
        /// </summary>
        public List<ImgDTO> Imgs { get; set; }
        /// <summary>
        /// 预约商品
        /// </summary>
        public List<ReserveProductDTO> productItem { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
