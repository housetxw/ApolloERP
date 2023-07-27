using Ae.Account.Api.Client.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Client.Response
{
    public class GetAllUnitResponse
    {
        /// <summary>
        /// 单位列表
        /// </summary>
        public List<UnitDTO> List { get; set; }
    }
}
