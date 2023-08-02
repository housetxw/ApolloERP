using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.DoorProduct
{
    /// <summary>
    /// EditDoorProductRequest
    /// </summary>
    public class EditDoorProductRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 是否免上门费
        /// </summary>
        public sbyte? FreeDoorFee { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public sbyte? IsDeleted { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; } = string.Empty;
    }
}
