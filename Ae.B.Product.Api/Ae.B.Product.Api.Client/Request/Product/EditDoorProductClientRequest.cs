using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Product
{
    public class EditDoorProductClientRequest
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
        public string SubmitBy { get; set; } = string.Empty;
    }
}
