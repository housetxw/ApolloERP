using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 得到排队生成器号
    /// </summary>
    public class GetQuqueNumberGeneratorRequest
    {
        /// <summary>
        /// ShopId
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 生成类型
        /// </summary>
        public int GeneratorType { get; set; }


    }
}
