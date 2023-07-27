using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetAllUnitResponse
    {
        /// <summary>
        /// 单位列表
        /// </summary>
        public List<UnitDTO> List { get; set; }
    }
}
