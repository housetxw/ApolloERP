using AutoMapper;
using ApolloErp.Log;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Dal.Repositorys.Shop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Imp.Services
{
    public class BankInformationService : IBankInformationService
    {
        private readonly IMapper _mapper;
        private readonly IBankInformationRepository _bankInformationRepository;
        private readonly ApolloErpLogger<BankInformationService> _logger;

        private string Domain = string.Empty;//文件管理 外链默认域名
        public BankInformationService(IMapper mapper,
            ApolloErpLogger<BankInformationService> ApolloErpLogger,
            IBankInformationRepository bankInformationRepository
            )
        {
            _mapper = mapper;
            _bankInformationRepository = bankInformationRepository;
            _logger = ApolloErpLogger;
        }
        /// <summary>
        /// 获取可用银行列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<BankInformationDTO>> GetBankListAsync() 
        {
            var conditon = "where is_deleted = 0";
            var list = await _bankInformationRepository.GetListAsync(conditon.ToString());
            return _mapper.Map<List<BankInformationDTO>>(list);
        }
    }
}
