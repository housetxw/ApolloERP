using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class GetRegionByCityIdRequest : BaseGetRequest
    {
        /// <summary>
        /// 市ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "市ID必须大于0")]
        public long CityId { get; set; }
    }
}
