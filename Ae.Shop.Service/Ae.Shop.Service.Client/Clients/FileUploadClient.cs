using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Request;
using Ae.Shop.Service.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Client.Clients
{
    public class FileUploadClient : IFileUploadClient
    {

        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<FileUploadClient> _logger;
        private IConfiguration configuration { get; }


        public FileUploadClient(IHttpClientFactory clientFactory, IConfiguration configuration, ApolloErpLogger<FileUploadClient> logger)
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        public async Task<ApiResult<string>> UploadBytes(UploadByteRequest request)
        {
            var client = clientFactory.CreateClient("FileUploadServer");
            ApiResult<string> result =
                await client.PostAsJsonAsync<UploadByteRequest, ApiResult<string>>(configuration["FileUploadServer:UploadBytes"], request);
            if (result.Code != ResultCode.Success)
            {
                _logger.Error($"UploadBytes_Error {result.Message}");
                throw new CustomException(result.Message);
            }
            return result;

        }
    }
}
