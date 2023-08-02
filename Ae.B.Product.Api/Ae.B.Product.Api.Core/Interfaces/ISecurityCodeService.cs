using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;

namespace Ae.B.Product.Api.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISecurityCodeService
    {
        /// <summary>
        /// 防伪码查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductSecurityCodeVo> GetSecurityCode(SecurityCodeRequest request);
    }
}
