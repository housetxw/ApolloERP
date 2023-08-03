using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.UserAddress
{
   public class UserAddressTagDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 标签编号(1,2,3,4,5开始)
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string TagName { get; set; } = string.Empty;
    }
}
