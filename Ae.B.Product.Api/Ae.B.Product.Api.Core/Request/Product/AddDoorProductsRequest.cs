using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    /// <summary>
    /// AddDoorProductsRequest
    /// </summary>
    public class AddDoorProductsRequest
    {
        /// <summary>
        /// 产品
        /// </summary>
        [Required(ErrorMessage = "请选择产品")]
        [MinLength(1, ErrorMessage = "请选择产品")]
        public List<AddDoorProductsVo> DoorProducts { get; set; }
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
