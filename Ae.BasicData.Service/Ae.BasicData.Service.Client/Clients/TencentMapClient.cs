using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.BasicData.Service.Client.Inteface;
using Ae.BasicData.Service.Client.Request;
using Ae.BasicData.Service.Client.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ae.BasicData.Service.Common.Exceptions;
using Newtonsoft.Json;

namespace Ae.BasicData.Service.Client.Clients
{
    public class TencentMapClient : ITencentMapClient
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IHttpClientFactory clientFactory;
        private readonly ApolloErpLogger<TencentMapClient> _logger;
        private IConfiguration configuration { get; }
        
        public TencentMapClient(IHttpClientFactory clientFactory, IConfiguration configuration,
            ApolloErpLogger<TencentMapClient> logger
            )
        {
            this.clientFactory = clientFactory;
            this.configuration = configuration;
            _logger = logger;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 根据经纬度获取当前位置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<AddressComponent>> GetPosition(GetPositionClientRequest request) 
        {
            string TencentKey = configuration["TencentKey"];
            request.key = TencentKey;
            var client = clientFactory.CreateClient("TencentMapServer");
            GetPositionClientResponse result = new GetPositionClientResponse();
            try
            {
                 result = await client.GetAsJsonAsync<GetPositionClientRequest, GetPositionClientResponse>(configuration["TencentMapServer:GetPosition"], request);
                if (result != null && result.Status == 0 && result.Result != null && result.Result.Address_Component != null)
                {
                    _logger.Info($"API: GetPosition 返回值：{JsonConvert.SerializeObject(result)}");
                    var address = new AddressComponent()
                    {
                        Province = result.Result.Address_Component.Province,
                        City = result.Result.Address_Component.City,
                        District = result.Result.Address_Component.District,
                        Street_Number = result.Result.Address_Component.Street_Number,
                    };
                    if (result.Result.ad_info != null)
                    {
                        address.AdCode = result.Result.ad_info.AdCode;
                    }
                    return new ApiResult<AddressComponent>()
                    {
                        Data = address,
                        Code = ResultCode.Success,
                        Message = "定位成功"
                    };
                }
                else 
                {
                    return new ApiResult<AddressComponent>()
                    {
                        Code = ResultCode.Failed,
                        Message = result?.Message
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"GetPosition {ex.Message}");
                throw new CustomException(ex.Message);
            }

            finally
            {
                _logger.Info($"API: GetPosition 请求参数：{JsonConvert.SerializeObject(request)}");
                _logger.Info($"API: GetPosition 返回值：{JsonConvert.SerializeObject(result)}");
            }
        }

        /// <summary>
        /// 根据地址获取坐标
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetCoordinateClientResponse> GetCoordinate(GetCoordinateClientRequest request)
        {
            string TencentKey = configuration["TencentKey"];
            request.key = TencentKey;
            var client = clientFactory.CreateClient("TencentMapServer");
            var result = await client.GetAsJsonAsync<GetCoordinateClientRequest, GetCoordinateClientResponse>(configuration["TencentMapServer:GetPosition"], request);
            return result;
        }

    }
}
