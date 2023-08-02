using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Core.Interfaces
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

        /// <summary>
        /// 批量生成防伪码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> BatchGenerateSecurityCode(BatchGenerateSecurityCodeRequest request);
    }
}
