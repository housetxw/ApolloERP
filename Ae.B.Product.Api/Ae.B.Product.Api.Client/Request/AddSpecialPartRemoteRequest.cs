using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class AddSpecialPartRemoteRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public List<string> TidList { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        public List<SpecialPartCodeRemoteModel> PartCodes { get; set; }

        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SpecialPartCodeRemoteModel
    {
        /// <summary>
        /// 零件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 种类
        /// </summary>
        public string PartType { get; set; }
    }
}
