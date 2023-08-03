using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Request
{
    public class AddShopUserRelationRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }

        public int ReferrerShopId { get; set; }

        public string ReferrerUserId { get; set; }

        public string Channel { get; set; }

        public string ReferrerType { get; set; }
    }
}
