using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Request;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;

namespace Ae.B.Product.Api.Imp.Services
{
    public class SecurityCodeService : ISecurityCodeService
    {
        private readonly IProductClient _productClient;

        public SecurityCodeService(IProductClient productClient)
        {
            _productClient = productClient;
        }

        /// <summary>
        /// 防伪码查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductSecurityCodeVo> GetSecurityCode(SecurityCodeRequest request)
        {
            var result = await _productClient.GetSecurityCode(new SecurityCodeClientRequest()
            {
                SecurityCode = request.SecurityCode
            });

            if (result != null)
            {
                return new ProductSecurityCodeVo()
                {
                    SecurityCode = result.SecurityCode,
                    SearchCount = result.SearchCount,
                    FirstSearchTime = result.FirstSearchTime.ToString("yyyy-MM-dd HH:mm")
                };
            }

            return null;
        }
    }
}
