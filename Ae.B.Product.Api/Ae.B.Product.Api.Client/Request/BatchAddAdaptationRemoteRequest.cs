using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class BatchAddAdaptationRemoteRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public List<string> TidList { get; set; }

        /// <summary>
        /// 普通配件
        /// </summary>
        public List<NormalPartRemote> NormalPart { get; set; }

        /// <summary>
        /// 特殊配件
        /// </summary>
        public List<SpecialPartRemote> SpecialPart { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// 普通配件
    /// </summary>
    public class NormalPartRemote
    {
        /// <summary>
        /// 配件名
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// OE件号
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        public List<PartCodeModelRemote> PartCodes { get; set; }
    }

    /// <summary>
    /// 特殊配件
    /// </summary>
    public class SpecialPartRemote
    {
        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        public List<SpecialPartCodeRemoteModel> PartCodes { get; set; }
    }
}
