using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.Product.Service.Dal.Repository;
using Ae.Product.Service.Dal.Model;
using Microsoft.Extensions.Configuration;

namespace Ae.Product.Service.Imp.Services
{
    public class SecurityCodeService : ISecurityCodeService
    {
        private readonly IProductSecurityCodeRepository _productSecurityCodeRepository;
        private readonly IConfiguration _configuration;

        public SecurityCodeService(IProductSecurityCodeRepository productSecurityCodeRepository,
            IConfiguration configuration)
        {
            _productSecurityCodeRepository = productSecurityCodeRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// 防伪码查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductSecurityCodeVo> GetSecurityCode(SecurityCodeRequest request)
        {
            var result = await _productSecurityCodeRepository.GetProductSecurityCode(request.SecurityCode);
            if (result != null)
            {
                await _productSecurityCodeRepository.UpdateSearchCount(result.Id);
                return new ProductSecurityCodeVo()
                {
                    SecurityCode = result.SecurityCode,
                    SearchCount = result.SearchCount + 1,
                    FirstSearchTime = result.FirstSearchTime ?? DateTime.Now
                };
            }

            return null;
        }

        /// <summary>
        /// 批量生成防伪码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> BatchGenerateSecurityCode(BatchGenerateSecurityCodeRequest request)
        {
            List<ProductSecurityCodeDO> securityCodes = new List<ProductSecurityCodeDO>();
            for (int i = 0; i < request.Count; i++)
            {
                string securityCode = GuidToLongId().ToString();
                securityCodes.Add(new ProductSecurityCodeDO()
                {
                    SecurityCode = securityCode,
                    RouteUrl = $"{_configuration["SecurityCodeH5Url"]}?securityCode={securityCode}",
                    CreateBy = "System",
                    CreateTime = DateTime.Now
                });
            }

            if (securityCodes.Any())
            {
                await _productSecurityCodeRepository.InsertBatchAsync(securityCodes);
            }

            return true;
        }


        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        private static long GuidToLongId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();

            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
