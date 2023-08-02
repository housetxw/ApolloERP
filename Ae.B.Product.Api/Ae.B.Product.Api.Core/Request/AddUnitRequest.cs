using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ae.B.Product.Api.Core.Request
{
    public class AddUnitRequest
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public Boolean IsForbid { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
    }
}
