using Ae.Shop.Api.Core.Model.Fiance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Fiance
{
    /// <summary>
    /// 核对账单对象
    /// </summary>
    public class CreateAccountCheckClientRequest
    {
        public List<CreateAccountCheckVO> Accounts { get; set; } = new List<CreateAccountCheckVO>();
    }
}
