using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class UpdatePartCodeRemoteRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
