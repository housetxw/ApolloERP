using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model;
using Ae.B.Product.Api.Client.Request;
using Ae.B.Product.Api.Client.Response;
using Ae.B.Product.Api.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Client.Model.BaoYang;
using Ae.B.Product.Api.Client.Request.BaoYang;
using ApolloErp.Log;
using System.Linq;

namespace Ae.B.Product.Api.Client.Clients
{
    public class BaoYangClient:IBaoYangClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ApolloErpLogger<BaoYangClient> _logger;

        public BaoYangClient(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            ApolloErpLogger<BaoYangClient> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// 保养适配配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPartAdaptationsResponse>> GetBaoYangPartAdaptationsAsync(
            BaoYangPartAdaptationsRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangPartAdaptationsResponse>> result =
                await client
                    .PostAsJsonAsync<BaoYangPartAdaptationsRequest, ApiResult<List<BaoYangPartAdaptationsResponse>>>(
                        _configuration["BaoYangServer:GetBaoYangPartAdaptations"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<BaoYangPartAdaptationsResponse>();
            }
            else
            {
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 获取配件类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetPartNameResponse>> GetPartNameListAsync()
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<GetPartNameResponse>> result =
                await client.GetAsJsonAsync<ApiResult<List<GetPartNameResponse>>>(
                    _configuration["BaoYangServer:GetPartNameList"], null);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<GetPartNameResponse>();
            }
            else
            {
                throw new CustomException(result.Message);
            }

        }

        /// <summary>
        /// 添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddPartCodeAsync(AddPartCodeRemoteRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client
                    .PostAsJsonAsync<AddPartCodeRemoteRequest, ApiResult<bool>>(
                        _configuration["BaoYangServer:AddPartCode"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 添加特殊配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddSpecialPartCodeAsync(AddSpecialPartRemoteRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client
                    .PostAsJsonAsync<AddSpecialPartRemoteRequest, ApiResult<bool>>(
                        _configuration["BaoYangServer:AddSpecialPartCode"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 批量添加配件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> BatchAddAdaptationAsync(BatchAddAdaptationRemoteRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client
                    .PostAsJsonAsync<BatchAddAdaptationRemoteRequest, ApiResult<bool>>(
                        _configuration["BaoYangServer:BatchAddAdaptation"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 删除配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeletePartCodeAsync(DeletePartCodeRemoteRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client
                    .PostAsJsonAsync<DeletePartCodeRemoteRequest, ApiResult<bool>>(
                        _configuration["BaoYangServer:DeletePartCode"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 修改OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOePartCodeAsync(UpdateOePartCodeRemoteRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client
                    .PostAsJsonAsync<UpdateOePartCodeRemoteRequest, ApiResult<bool>>(
                        _configuration["BaoYangServer:UpdateOePartCode"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 修改零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdatePartCodeAsync(UpdatePartCodeRemoteRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client
                    .PostAsJsonAsync<UpdatePartCodeRemoteRequest, ApiResult<bool>>(
                        _configuration["BaoYangServer:UpdatePartCode"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据Tid适配套餐（剔除 已关联的 套餐）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageDto>> GetBaoYangPackageByTid(BaoYangPackageByTidClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangPackageDto>> result =
                await client
                    .GetAsJsonAsync<BaoYangPackageByTidClientRequest, ApiResult<List<BaoYangPackageDto>>>(
                        _configuration["BaoYangServer:GetBaoYangPackageByTid"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<BaoYangPackageDto>();
            }
            else
            {
                _logger.Error($"GetBaoYangPackageByTid_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 车型关联套餐列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageRefDto>> GetBaoYangPackageRef(BaoYangPackageRefClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangPackageRefDto>> result =
                await client
                    .PostAsJsonAsync<BaoYangPackageRefClientRequest, ApiResult<List<BaoYangPackageRefDto>>>(
                        _configuration["BaoYangServer:GetBaoYangPackageRef"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<BaoYangPackageRefDto>();
            }
            else
            {
                _logger.Error($"GetBaoYangPackageRef_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 车型添加套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> RelateVehicleAndPackage(RelateVehicleAndPackageClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<RelateVehicleAndPackageClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:RelateVehicleAndPackage"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"RelateVehicleAndPackage_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 删除保养套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBaoYangPackageRef(DeleteBaoYangPackageRefClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<DeleteBaoYangPackageRefClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:DeleteBaoYangPackageRef"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"DeleteBaoYangPackageRef_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 配件类型查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangPartsDto>> GetBaoYangParts()
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangPartsDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<BaoYangPartsDto>>>(
                    _configuration["BaoYangServer:GetBaoYangParts"], null);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<BaoYangPartsDto>();
            }
            else
            {
                _logger.Error($"GetBaoYangParts_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 保养类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangTypeConfigDto>> GetPackageByType()
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangTypeConfigDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<BaoYangTypeConfigDto>>>(
                    _configuration["BaoYangServer:GetPackageByType"], null);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<BaoYangTypeConfigDto>();
            }
            else
            {
                _logger.Error($"GetPackageByType_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 辅料类型配置
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangAccessoryDto>> GetPartAccessoryConfig()
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangAccessoryDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<BaoYangAccessoryDto>>>(
                    _configuration["BaoYangServer:GetPartAccessoryConfig"], null);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<BaoYangAccessoryDto>();
            }
            else
            {
                _logger.Error($"GetPartAccessoryConfig_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 查询辅料配置数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPartAccessoryDto>> GetBaoYangPartAccessory(
            BaoYangPartAccessoryClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangPartAccessoryDto>> result =
                await client
                    .PostAsJsonAsync<BaoYangPartAccessoryClientRequest, ApiResult<List<BaoYangPartAccessoryDto>>>(
                        _configuration["BaoYangServer:GetBaoYangPartAccessory"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data ?? new List<BaoYangPartAccessoryDto>();
            }
            else
            {
                _logger.Error($"GetBaoYangPartAccessory_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 五级属性适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPropertyAdaptationDto>> GetBaoYangPropertyAdaptation(
            BaoYangPropertyAdaptationClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangPropertyAdaptationDto>> result =
                await client
                    .PostAsJsonAsync<BaoYangPropertyAdaptationClientRequest,
                        ApiResult<List<BaoYangPropertyAdaptationDto>>>(
                        _configuration["BaoYangServer:GetBaoYangPropertyAdaptation"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetBaoYangPropertyAdaptation_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAccessory(UpdateAccessoryClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<UpdateAccessoryClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:UpdateAccessory"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"UpdateAccessory_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 批量编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> BatchEditAccessory(BatchEditAccessoryClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<BatchEditAccessoryClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:BatchEditAccessory"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"BatchEditAccessory_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 删除辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAccessory(DeleteAccessoryClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<DeleteAccessoryClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:DeleteAccessory"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"DeleteAccessory_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// 根据OE号查询零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<PartCodeDto>> GetPartCodeAndBrandByOe(PartCodeAndBrandByOeClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<PartCodeDto>> result =
                await client.GetAsJsonAsync<PartCodeAndBrandByOeClientRequest, ApiResult<List<PartCodeDto>>>(
                    _configuration["BaoYangServer:GetPartCodeAndBrandByOe"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data;
            }
            else
            {
                _logger.Error($"GetPartCodeAndBrandByOe_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }
        /// <summary>
        /// 获取服务项目枚举
        /// </summary>
        /// <returns></returns>
        public async Task<List<ServiceTypeDTO>> GetServiceTypeEnum()
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<ServiceTypeDTO>> result = await client.GetAsJsonAsync<ApiResult<List<ServiceTypeDTO>>>(_configuration["BaoYangServer:GetServiceTypeEnum"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                _logger.Warn($"GetServiceTypeEnum_Error {result?.Message}");
                throw new CustomException(result?.Message);
            }
        }

        /// <summary>
        /// 保养OE映射关系查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<OeCodeMapDto>> GetOeCodeMapList(OeCodeMapClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiPagedResult<OeCodeMapDto> result =
                await client.GetAsJsonAsync<OeCodeMapClientRequest, ApiPagedResult<OeCodeMapDto>>(
                    _configuration["BaoYangServer:GetOeCodeMapList"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetOeCodeMapList_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// OE件号详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<OePartCodeDetailDto> GetOeCodeMapDetailByOeCode(OeCodeMapDetailByOeCodeClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<OePartCodeDetailDto> result =
                await client.GetAsJsonAsync<OeCodeMapDetailByOeCodeClientRequest, ApiResult<OePartCodeDetailDto>>(
                    _configuration["BaoYangServer:GetOeCodeMapDetailByOeCode"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetOeCodeMapDetailByOeCode_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 删除OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteOePartCode(DeleteOePartCodeClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<DeleteOePartCodeClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:DeleteOePartCode"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"DeleteOePartCode_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 编辑OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditOePartCode(EditOePartCodeClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditOePartCodeClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:EditOePartCode"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"EditOePartCode_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 添加OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddOePartCode(AddOePartCodeClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<AddOePartCodeClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:AddOePartCode"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"AddOePartCode_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 获取保养五级属性描述
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangPropertyDescriptionDto>> GetBaoYangPropertyDescription()
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangPropertyDescriptionDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<BaoYangPropertyDescriptionDto>>>(
                    _configuration["BaoYangServer:GetBaoYangPropertyDescription"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<BaoYangPropertyDescriptionDto>();
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetBaoYangPropertyDescription_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 删除五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeletePropertyAdaptation(DeletePropertyAdaptationClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<DeletePropertyAdaptationClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:DeletePropertyAdaptation"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"DeletePropertyAdaptation_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 保存五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SavePropertyAdaptation(SavePropertyAdaptationClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<SavePropertyAdaptationClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:SavePropertyAdaptation"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"SavePropertyAdaptation_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 配件号关联产品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangPartProductDto>> GetBaoYangProductRef(
            BaoYangProductRefClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiPagedResult<BaoYangPartProductDto> result =
                await client.GetAsJsonAsync<BaoYangProductRefClientRequest, ApiPagedResult<BaoYangPartProductDto>>(
                    _configuration["BaoYangServer:GetBaoYangProductRef"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"GetBaoYangProductRef_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 删除 配件号产品关联关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBaoYangProductRef(DeleteBaoYangProductRefClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<DeleteBaoYangProductRefClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:DeleteBaoYangProductRef"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"DeleteBaoYangProductRef_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 配件号关联商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> InsertPartsAssociation(InsertPartsAssociationClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<InsertPartsAssociationClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:InsertPartsAssociation"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"InsertPartsAssociation_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 校验配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> CheckPartCode(CheckPartCodeClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<string>> result =
                await client.GetAsJsonAsync<CheckPartCodeClientRequest, ApiResult<List<string>>>(
                    _configuration["BaoYangServer:CheckPartCode"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"CheckPartCode_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 校验商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckProductClientResponse> CheckProduct(CheckProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<CheckProductClientResponse> result =
                await client.GetAsJsonAsync<CheckProductClientRequest, ApiResult<CheckProductClientResponse>>(
                    _configuration["BaoYangServer:CheckProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"CheckProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 校验配件号 商品是否匹配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CheckPartCodeResultClientResponse> CheckPartCodeAndProduct(
            CheckPartCodeAndProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<CheckPartCodeResultClientResponse> result =
                await client
                    .GetAsJsonAsync<CheckPartCodeAndProductClientRequest, ApiResult<CheckPartCodeResultClientResponse>>(
                        _configuration["BaoYangServer:CheckPartCodeAndProduct"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                var msg = result?.Message ?? "系统异常";
                _logger.Warn($"CheckPartCodeAndProduct_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<BaoYangCategoryDto>>> GetBaoYangPackagesAsync(
            BaoYangPackagesClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangCategoryDto>> result =
                await client.PostAsJsonAsync<BaoYangPackagesClientRequest, ApiResult<List<BaoYangCategoryDto>>>(
                    _configuration["BaoYangServer:GetBaoYangPackagesAsync"], request);
            if (result.Code != ResultCode.Success)
            {
                _logger.Info($"GetBaoYangPackagesAsync_Error {result.Message}");
            }

            return result;
        }

        /// <summary>
        /// 更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageProductDto>> SearchPackageProductsWithConditionAsync(
            SearchProductClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiPagedResult<BaoYangPackageProductDto> result =
                await client.PostAsJsonAsync<SearchProductClientRequest, ApiPagedResult<BaoYangPackageProductDto>>(
                    _configuration["BaoYangServer:SearchPackageProductsWithConditionAsync"], request);
            if (result.Code == ResultCode.Success)
            {
                return result.Data?.Items?.ToList() ?? new List<BaoYangPackageProductDto>();
            }
            else
            {
                _logger.Info($"SearchPackageProductsWithConditionAsync_Error {result.Message}");
                throw new CustomException(result.Message);
            }
        }

        /// <summary>
        /// PackageType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangPackageConfigDto>> GetPackageTypeConfig(
            PackageTypeConfigClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiPagedResult<BaoYangPackageConfigDto> result =
                await client.GetAsJsonAsync<PackageTypeConfigClientRequest, ApiPagedResult<BaoYangPackageConfigDto>>(
                    _configuration["BaoYangServer:GetPackageTypeConfig"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"GetPackageTypeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 编辑PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditPackageTypeConfig(EditPackageTypeConfigClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditPackageTypeConfigClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:EditPackageTypeConfig"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"EditPackageTypeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 新增PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> AddPackageTypeConfig(AddPackageTypeConfigClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<int> result =
                await client.PostAsJsonAsync<AddPackageTypeConfigClientRequest, ApiResult<int>>(
                    _configuration["BaoYangServer:AddPackageTypeConfig"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"AddPackageTypeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// BaoYangType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaoYangTypeConfigDetailDto>> GetBaoYangTypeConfig(
            BaoYangTypeConfigClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiPagedResult<BaoYangTypeConfigDetailDto> result =
                await client.GetAsJsonAsync<BaoYangTypeConfigClientRequest, ApiPagedResult<BaoYangTypeConfigDetailDto>>(
                    _configuration["BaoYangServer:GetBaoYangTypeConfig"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"GetBaoYangTypeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangCategoryConfigDto>> GetBaoYangCategoryConfig()
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangCategoryConfigDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<BaoYangCategoryConfigDto>>>(
                    _configuration["BaoYangServer:GetBaoYangCategoryConfig"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<BaoYangCategoryConfigDto>();
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"GetBaoYangCategoryConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 编辑BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditBaoYangTypeConfig(EditBaoYangTypeConfigClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditBaoYangTypeConfigClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:EditBaoYangTypeConfig"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"EditBaoYangTypeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 新增BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        public async Task<int> AddBaoYangTypeConfig(AddBaoYangTypeConfigClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<int> result =
                await client.PostAsJsonAsync<AddBaoYangTypeConfigClientRequest, ApiResult<int>>(
                    _configuration["BaoYangServer:AddBaoYangTypeConfig"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"AddBaoYangTypeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 获取所有有效的BaoYangType
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaoYangTypeConfigDto>> GetValidBaoYangTypeConfig()
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<List<BaoYangTypeConfigDto>> result =
                await client.GetAsJsonAsync<ApiResult<List<BaoYangTypeConfigDto>>>(
                    _configuration["BaoYangServer:GetValidBaoYangTypeConfig"], null);
            if (result.IsNotNullSuccess())
            {
                return result.Data ?? new List<BaoYangTypeConfigDto>();
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"GetValidBaoYangTypeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 产品新增/更新 自动更新配件号关联Pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AutoInsertPartsAssociation(AutoInsertPartsAssociationClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<AutoInsertPartsAssociationClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:AutoInsertPartsAssociation"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"AutoInsertPartsAssociation_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 安装服务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<InstallAddFeeConfigDto>> GetInstallAddFeeConfig(
            InstallAddFeeConfigClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiPagedResult<InstallAddFeeConfigDto> result =
                await client.GetAsJsonAsync<InstallAddFeeConfigClientRequest, ApiPagedResult<InstallAddFeeConfigDto>>(
                    _configuration["BaoYangServer:GetInstallAddFeeConfig"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"GetInstallAddFeeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        public async Task<List<InstallAddFeeConfigDto>> GetAllInstallAddFeeConfig()
        {
            int pageSize = 100;
            List<InstallAddFeeConfigDto> result = new List<InstallAddFeeConfigDto>();
            var productResultData = await GetInstallAddFeeConfig(new InstallAddFeeConfigClientRequest()
            {
                PageIndex = 1,
                PageSize = pageSize
            });
            if (productResultData != null)
            {
                var product1 = productResultData.Items?.ToList() ?? new List<InstallAddFeeConfigDto>();
                var totalCount = productResultData.TotalItems;
                result.AddRange(product1);

                int totalPage = (int) Math.Ceiling((decimal) totalCount / pageSize);
                if (totalPage > 1)
                {
                    List<int> pageList = new List<int>();
                    for (int i = 1; i < totalPage; i++)
                    {
                        pageList.Add(i + 1);
                    }

                    var allTask = await Task.WhenAll(pageList.Select(_ => GetInstallAddFeeConfig(
                        new InstallAddFeeConfigClientRequest
                        {
                            PageIndex = _,
                            PageSize = pageSize
                        })));
                    foreach (var itemTask in allTask)
                    {
                        if (itemTask.Items != null && itemTask.Items.Any())
                        {
                            result.AddRange(itemTask.Items);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 新增服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddInstallAddFeeConfig(AddInstallAddFeeConfigClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<AddInstallAddFeeConfigClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:AddInstallAddFeeConfig"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"AddInstallAddFeeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }

        /// <summary>
        /// 编辑服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditInstallAddFeeConfig(EditInstallAddFeeConfigClientRequest request)
        {
            var client = _httpClientFactory.CreateClient("BaoYangServer");
            ApiResult<bool> result =
                await client.PostAsJsonAsync<EditInstallAddFeeConfigClientRequest, ApiResult<bool>>(
                    _configuration["BaoYangServer:EditInstallAddFeeConfig"], request);
            if (result.IsNotNullSuccess())
            {
                return result.Data;
            }
            else
            {
                string msg = result?.Message ?? "系统异常";
                _logger.Info($"EditInstallAddFeeConfig_Error {msg}");
                throw new CustomException(msg);
            }
        }
    }
}
