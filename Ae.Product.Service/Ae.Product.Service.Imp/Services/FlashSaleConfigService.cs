using AutoMapper;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Dal.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Ae.Product.Service.Core.Enums;
using Microsoft.Extensions.Configuration;

namespace Ae.Product.Service.Imp.Services
{

    public class FlashSaleConfigService : IFlashSaleConfigService
    {
        private readonly IMapper _mapper;
        private readonly IFlashSaleConfigRepository _flashSaleConfigRepository;
        private readonly ApolloErpLogger<FlashSaleConfigService> _logger;
        private readonly IProductRepository _productRepository;
        public IConfiguration _Configuration { get; }


        public FlashSaleConfigService(IMapper _mapper, IFlashSaleConfigRepository flashSaleConfigRepository,
            ApolloErpLogger<FlashSaleConfigService> logger, IProductRepository productRepository,
             IConfiguration Configuration)
        {
            this._mapper = _mapper;
            this._flashSaleConfigRepository = flashSaleConfigRepository;
            this._logger = logger;
            this._productRepository = productRepository;
            this._Configuration = Configuration;
        }

        public async Task<ApiResult<string>> CreatFlashSaleConfig(FlashSaleConfigDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var flashConfigs = await _flashSaleConfigRepository.GetListAsync("WHERE is_deleted = 0  AND `status` = '已生效'  and product_id=@product_id", new { product_id = request.ProductId });
                if (flashConfigs.Any())
                {
                    return new ApiResult<string>
                    {
                        Code = ResultCode.Exception,
                        Message = " 产品已添加!"
                    };
                }
                request.CreateTime = DateTime.Now;
                var vo = _mapper.Map<FlashSaleConfigDO>(request);
                vo.Status = FlashSaleConfigEnum.已生效.ToString();
                await _flashSaleConfigRepository.InsertAsync(vo);
                res.Code = ResultCode.Success;
                res.Message = "创建成功!";
            }
            catch (Exception ex)
            {
                _logger.Error($"CreatFlashSaleConfig_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<List<FlashSaleConfigDTO>>> GetActiveFlashSaleConfigs()
        {
            var res = new ApiResult<List<FlashSaleConfigDTO>>();
            try
            {
                var result = await _flashSaleConfigRepository.GetListAsync(@"WHERE
                                                active_start_time <= @currnet_date AND active_end_time >= @currnet_date
                                                AND is_deleted = 0
                                                AND `status` = '已生效' ", new { currnet_date = DateTime.Now });

                var productCodes = result.Select(r => r.ProductId).ToList();
                //商品信息
                var productInfos = _productRepository.GetProductsByProductCode(productCodes);
                var vo = _mapper.Map<List<FlashSaleConfigDTO>>(result);

                foreach (var item in vo)
                {
                    item.Num -= item.SaleNum;

                    var productInfo = productInfos.FirstOrDefault(r => r.ProductCode == item.ProductId);
                    item.Image1 = this._Configuration["ImageDomain"] + (productInfo?.Image1 ?? string.Empty);
                    item.SalesPrice = productInfo?.SalesPrice ?? 0;
                    item.Description = productInfo?.DisplayName;
                }
                res.Data = vo;
                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"GetActiveFlashSaleConfigs_Error", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiPagedResult<FlashSaleConfigDTO>> GetFlashSaleConfigs(GetFlashSaleConfigRequest request)
        {
            var res = new ApiPagedResult<FlashSaleConfigDTO>();
            try
            {
                var conditions = new StringBuilder(" where is_deleted=0 ");
                var param = new DynamicParameters();
                if (!string.IsNullOrWhiteSpace(request.ProductName))
                {
                    var productId = request.ProductName;
                    var productNames = request.ProductName.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                    if (productNames.Any())
                    {
                        string productName = string.Join("%", productNames);
                        conditions.Append(" AND (product_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR product_id=@productId)");
                        param.Add("@productId", productId);
                    }
                }
                if (!string.IsNullOrWhiteSpace(request.Status))
                {
                    conditions.Append(" and status =@status ");
                    param.Add("@status", request.Status);
                }
                if (request.StartTime.HasValue)
                {
                    conditions.Append(" AND (@startTime <= active_start_time OR @startTime <= active_end_time)");
                    param.Add("@startTime", request.StartTime.Value);
                }
                if (request.EndTime.HasValue)
                {
                    conditions.Append(" AND (@endTime >= active_start_time OR @endTime >= active_end_time)");
                    param.Add("@endTime", request.EndTime.Value);
                }
                var result = await _flashSaleConfigRepository.GetListPagedAsync(request.PageIndex, request.PageSize, conditions.ToString(), "id desc", param);


                var vo = _mapper.Map<ApiPagedResultData<FlashSaleConfigDTO>>(result);

                res.Data = vo;

                res.Code = ResultCode.Success;
                res.Message = "查询成功!";
            }
            catch (Exception ex)
            {
                _logger.Error($"AuditAllotTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<FlashSaleConfigDTO>> GetFlashSaleProduct(FlashSaleConfigDTO request)
        {
            var res = new ApiResult<FlashSaleConfigDTO>();
            try
            {
                var result = await _flashSaleConfigRepository.GetListAsync(@"WHERE is_deleted = 0  AND `status` = '已生效'  and product_id=@product_id ", new { product_id = request.ProductId });
                if (result.Any())
                {
                    //需要判断当前数据是否已经失效了
                    if (DateTime.Now.Subtract(result.FirstOrDefault().ActiveEndTime).TotalSeconds > 0)
                    {
                        var vo = new FlashSaleConfigDO
                        {
                            UpdateBy = "System",
                            UpdateTime = DateTime.Now,
                            Status = FlashSaleConfigEnum.已结束.ToString(),
                            Id = result.FirstOrDefault().Id
                        };

                        await _flashSaleConfigRepository.UpdateAsync(vo, new List<string> { "Status", "UpdateBy", "UpdateTime" });
                        res.Code = ResultCode.Success;
                        res.Data = null;
                    }
                    else
                    {
                        var vo = _mapper.Map<FlashSaleConfigDTO>(result?.FirstOrDefault());
                        res.Data = vo;
                        res.Code = ResultCode.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"AuditAllotTask_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        public async Task<ApiResult<string>> UpdateFlashSaleConfig(FlashSaleConfigDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                request.UpdateTime = DateTime.Now;
                var vo = _mapper.Map<FlashSaleConfigDO>(request);
                await _flashSaleConfigRepository.UpdateAsync(vo, new List<string> { "Status", "UpdateBy", "UpdateTime" });
                res.Code = ResultCode.Success;
                res.Message = "删除成功!";
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateFlashSaleConfig_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 更新已售数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> UpdateFlashSaleConfigNum(FlashSaleConfigDTO request)
        {
            var res = new ApiResult<string>();
            try
            {
                var flashConfig = await _flashSaleConfigRepository.GetAsync(request.Id);
                if (flashConfig != null)
                {
                    request.SaleNum += flashConfig.SaleNum;
                    if (request.SaleNum > flashConfig.Num)
                    {
                        return new ApiResult<string>
                        {
                            Code = ResultCode.Exception,
                            Message = "已售数量不能大于活动数量!"
                        };
                    }
                    request.UpdateTime = DateTime.Now;
                    var vo = _mapper.Map<FlashSaleConfigDO>(request);

                    if (request.SaleNum == flashConfig.Num)
                    {
                        vo.Status = FlashSaleConfigEnum.已结束.ToString();
                        //要将活动更新为“已结束”
                        await _flashSaleConfigRepository.UpdateAsync(vo, new List<string> { "Status", "SaleNum", "UpdateBy", "UpdateTime" });
                    }
                    else
                    {
                        await _flashSaleConfigRepository.UpdateAsync(vo, new List<string> { "SaleNum", "UpdateBy", "UpdateTime" });
                    }
                    res.Code = ResultCode.Success;
                    res.Message = "更新成功!";
                }
                else
                {
                    return new ApiResult<string>
                    {
                        Code = ResultCode.Exception,
                        Message = "数据不存在!"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"UpdateFlashSaleConfigNum_Error Data:{JsonConvert.SerializeObject(request)}", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }

        /// <summary>
        /// 自动下架过期的活动
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<string>> AutoOffFlashSaleConfig()
        {
            var res = new ApiResult<string>();
            try
            {
                await _flashSaleConfigRepository.AutoOffFlashSaleConfig();

                res.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                _logger.Error($"AutoOffFlashSaleConfig_Error", ex);
                res.Code = ResultCode.Exception;
            }
            return res;
        }
    }
}
