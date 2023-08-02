using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    public class RelateVehicleAndPackageClientRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 保养类型
        /// </summary>
        public string ByType { get; set; }

        /// <summary>
        /// 套餐pid
        /// </summary>
        public List<string> PackageId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
