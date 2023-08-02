using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
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
    }
}
