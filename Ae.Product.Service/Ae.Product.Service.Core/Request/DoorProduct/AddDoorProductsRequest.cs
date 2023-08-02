using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.DoorProduct
{
    /// <summary>
    /// AddDoorProductsRequest
    /// </summary>
    public class AddDoorProductsRequest
    {
        /// <summary>
        /// 产品
        /// </summary>
        public List<AddDoorProductsVo> DoorProducts { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; } = string.Empty;
    }

    /// <summary>
    /// 新增
    /// </summary>
    public class AddDoorProductsVo
    {
        /// <summary>
        /// 产品Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 产品类目
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 是否免上门
        /// </summary>
        public sbyte FreeDoorFee { get; set; }
    }
}
