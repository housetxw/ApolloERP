using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Product
{
    public class AddDoorProductsClientRequest
    {
        /// <summary>
        /// 产品
        /// </summary>
        public List<AddDoorProductsDto> DoorProducts { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; } = string.Empty;
    }

    /// <summary>
    /// 新增
    /// </summary>
    public class AddDoorProductsDto
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
