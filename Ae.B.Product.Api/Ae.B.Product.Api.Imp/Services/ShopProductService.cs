using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Profile;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model.ShopProduc;
using Ae.B.Product.Api.Client.Request.ShopProduct;
using Ae.B.Product.Api.Common.Exceptions;
using Ae.B.Product.Api.Common.Extension;
using Ae.B.Product.Api.Common.Util;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model.ShopProduct;
using Ae.B.Product.Api.Core.Request.ShopProduct;
using Ae.B.Product.Api.Core.Response.ShopProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Imp.Services
{
    public class ShopProductService : IShopProductService
    {
        private readonly IProductClient _productClient;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IShopManageClient _shopManageClient;
        private readonly ApolloErpLogger<ShopProductService> _logger;
        private readonly IBaoYangClient _baoYangClient;

        public ShopProductService(IProductClient productClient, IMapper mapper,
            IIdentityService identityService, IShopManageClient shopManageClient,
            ApolloErpLogger<ShopProductService> logger, IBaoYangClient baoYangClient)
        {
            this._productClient = productClient;
            this._mapper = mapper;
            this._identityService = identityService;
            this._shopManageClient = shopManageClient;
            this._logger = logger;
            this._baoYangClient = baoYangClient;
        }
        /// <summary>
        /// 查询单个商品信息
        /// </summary>
        /// <param name="shopProductCode"></param>
        /// <returns></returns>
        public async Task<ShopProductVo> GetShopProductByCode(string shopProductCode)
        {
            var result = new ShopProductVo();
            var shopProductDto = await _productClient.GetShopProductByCode(shopProductCode);
            result = _mapper.Map<ShopProductVo>(shopProductDto);
            if (result.ShopId > 0)
            {
                //获取门店信息
                var shopInfos = await _shopManageClient.GetShopListByIdsAsync(new List<long>() { result.ShopId });
                var shopInfo = shopInfos.Where(a => a.Id == result.ShopId).FirstOrDefault();
                if (shopInfo != null)
                {
                    result.ShopName = shopInfo.SimpleName;
                }
            }

            return result;
        }

        /// <summary>
        ///  保存门店商品信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveShopProduct(ShopProductEditRequest request)
        {
            if (request == null || request.ShopProduct == null)
            {
                throw new CustomException("门店商品必填！");
            }
            var para = _mapper.Map<ShopProductEditClientRequest>(request);
            para.Images = request.ShopProduct.Images;
            para.UserId = _identityService.GetUserId(); //获取用户Id
            //如果是上架 默认审核通过
            if (para.ShopProduct.OnSale == 1)
            {
                para.ShopProduct.AuditStatus = 1;//默认审核通过
                para.ShopProduct.AuditUser = _identityService.GetUserName();
                para.ShopProduct.AuditOpinion = "Boss 后台审核";
                para.ShopProduct.AuditTime = DateTime.Now;
            }
            var result = await _productClient.SaveShopProduct(para);
            return result;
        }

        /// <summary>
        ///  分页查询门店商品信息
        /// </summary>
        /// <returns></returns>
        public async Task<ShopProductSearchResponse> SearchShopProduct(ShopProductSearchRequest request)
        {
            var result = new ShopProductSearchResponse();
            var para = _mapper.Map<ShopProductSearchClientRequest>(request);
            para.IsStoreoutside = 0;//只查门店项目
            var pageShopProduct = await _productClient.SearchShopProduct(para);
            result.Items = _mapper.Map<List<ShopProductVo>>(pageShopProduct.Data.Items);
            result.TotalItems = pageShopProduct.Data.TotalItems;
            if (!result.Items.IsNullOrEmpty())
            {
                //获取门店信息
                var shopIds = result.Items.Select(t => t.ShopId).ToList();
                var shopInfos = await _shopManageClient.GetShopListByIdsAsync(shopIds);
                var serviceTypeDtos = await _baoYangClient.GetServiceTypeEnum();
                result.Items.ForEach((t) =>
                {
                    var shopInfo = shopInfos?.Where(a => a.Id == t.ShopId)?.FirstOrDefault();
                    if (shopInfo != null)
                    {
                        t.ShopName = shopInfo.SimpleName;
                    }

                    var serviceType = serviceTypeDtos?.Where(c => c.Id == t.CategoryId)?.FirstOrDefault();
                    if (serviceType != null)
                    {
                        t.CategoryName = serviceType.DisplayName;
                    }
                });
            }

            return result;
        }

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<ShopSimpleInfoVo>> GetShopList(ShopListRequest request)
        {
            var res = await _shopManageClient.GetShopListAsync(new ShopListClientRequest
            {
                PageIndex = 1,
                PageSize = 20,
                SimpleName = request.SimpleName
            });
            var vo = _mapper.Map<ApiPagedResult<ShopSimpleInfoVo>>(res);
            return vo;
        }

        /// <summary>
        /// 获取门店开启的服务项目
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceTypeVo>> GetShopServiceType(long shopId)
        {
            var serviceTypeDtos = await _shopManageClient.GetShopServiceTypeAsync(shopId);
            var result = _mapper.Map<List<ShopServiceTypeVo>>(serviceTypeDtos);
            return result;
        }

        /// <summary>
        /// 导入门店商品
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<List<ShopProductImportVo>> ImportShopProduct(IFormFile file)
        {
            var result = new List<ShopProductImportVo>();
            var importRows = ExcelHelper.Import<ShopProductImportVo>(file.OpenReadStream(), file.FileName);
            if (importRows.IsNullOrEmpty())
            {
                throw new CustomException("导入数据不能为空");
            }
            var products = new List<ShopProductImportDto>();
            var userId = _identityService.GetUserId(); //获取用户Id
            foreach (ShopProductImportVo item in importRows)
            {
                if (string.IsNullOrEmpty(item.ShopId) && string.IsNullOrEmpty(item.CategoryId)
                    && string.IsNullOrEmpty(item.ProductName) && string.IsNullOrEmpty(item.DisplayName)
                    && string.IsNullOrEmpty(item.SalesPrice))
                {
                    continue;
                }
                var product = new ShopProductImportDto();
                if (string.IsNullOrEmpty(item.ShopId))
                {
                    item.ErrorMessage = "门店Id必填！";
                    result.Add(item);
                    continue;
                }
                product.ShopId = Convert.ToInt32(item.ShopId);
                if (string.IsNullOrEmpty(item.CategoryId))
                {
                    item.ErrorMessage = "类目Id必填！";
                    result.Add(item);
                    continue;
                }
                product.CategoryId = Convert.ToInt32(item.CategoryId);

                if (string.IsNullOrEmpty(item.ProductName))
                {
                    item.ErrorMessage = "商品名称必填！";
                    result.Add(item);
                    continue;
                }
                product.ProductName = item.ProductName;

                if (string.IsNullOrEmpty(item.DisplayName))
                {
                    item.ErrorMessage = "小程序显示名称必填！";
                    result.Add(item);
                    continue;
                }
                product.DisplayName = item.DisplayName;
                product.Description = item.Description ?? "";
                product.Icon = item.Icon ?? "";
                //销售价
                decimal salesPrice = 0;
                if (!decimal.TryParse(item.SalesPrice, out salesPrice))
                {
                    item.ErrorMessage = "销售价格式有误";
                    result.Add(item);
                    continue;
                }
                product.SalesPrice = salesPrice;
                products.Add(product);
            }
            if (!result.IsNullOrEmpty())
            {
                return result;
            }
            if (!products.IsNullOrEmpty())
            {
                var pageSize = 50;
                var totalCount = products.Count;
                var totalPages = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
                var request = new ImportShopProductClientRequest() { UserId = userId };
                for (int i = 0; i < totalPages; i++)
                {
                    var skipNum = i * pageSize;
                    request.Products = products.Skip(skipNum).Take(pageSize).ToList();
                    await _productClient.ImportShopProduct(request);
                }
            }
            return result;
        }

        /// <summary>
        ///  审核门店项目上架
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AuditShopProduct(ShopProductAuditRequest request)
        {
            if (request == null)
            {
                throw new CustomException("审核信息必填！");
            }
            var para = _mapper.Map<ShopProductAuditClientRequest>(request);
            para.UserId = _identityService.GetUserId(); //获取用户Id
            para.AuditUser = _identityService.GetUserName(); //获取用户名称
            var result = await _productClient.AuditShopProduct(para);
            if (result)
            {
                //获取门店信息
                var shopProductDto = await _productClient.GetShopProductByCode(request.ShopProductCode);
                var shopInfo = await _shopManageClient.GetShopSimpleInfoAsync(new ShopBaseInfoRequest() { ShopId = shopProductDto.ShopId });
                if (shopInfo != null)
                {
                    //验证门店老板电话
                    if (string.IsNullOrEmpty(shopInfo.OwnerPhone))
                    {
                        throw new CustomException("门店老板联系方式未填写，审核短信发送失败！");
                    }
                    //发送审核信息
                    SendAuditSms(shopInfo, request.AuditStatus, shopProductDto.ProductName, request.AuditOpinion);
                }
            }
            return result;
        }

        /// <summary>
        /// 恭喜您，您提交的*替换项目名称*项目已通过审核，请到APP或小程序查看
        /// 很遗憾，由于*此处替换为审核意见*原因，您提交的*替换项目名称*项目未通过，若有疑问请联系门店负责人：*替换为门店负责人姓名-电话*
        /// 发送审核信息短信
        /// </summary>
        /// <param name="shopInfo">门店信息</param>
        /// <param name="projectName">项目名称</param>
        /// <param name="reason">原因</param>
        private void SendAuditSms(ShopBaseInfoDto shopInfo, int auditStatus, string projectName,string auditOpinion)
        {
            var project_name = projectName;
            var phoneNumber = shopInfo.OwnerPhone;
            IClientProfile profile = DefaultProfile.GetProfile("", "", "");
            DefaultAcsClient client = new DefaultAcsClient(profile);
            CommonRequest request = new CommonRequest();
            request.Method = MethodType.POST;
            request.Domain = "dysmsapi.aliyuncs.com";
            request.Version = "2017-05-25";
            request.Action = "SendSms";
            request.AddQueryParameters("PhoneNumbers", phoneNumber);
            request.AddQueryParameters("SignName", "总部");
            if (auditStatus == 1)
            {
                request.AddQueryParameters("TemplateCode", "SMS_185");
                request.AddQueryParameters("TemplateParam", "{\"project_name\":\"" + project_name + "\"}");
            }
            else
            {
                var reason = auditOpinion;
                var shop_controller = shopInfo.Head;
                var shop_controller_phone = shopInfo.HeadPhone;
                request.AddQueryParameters("TemplateCode", "SMS_1852"); 
                request.AddQueryParameters("TemplateParam", "{\"project_name\":\""+ project_name + "\",\"reason\":\""+ reason + "\",\"shop_controller\":\""+ shop_controller + "\",\"phone\":\""+ shop_controller_phone + "\"}");
            }
            try
            {
                _logger.Info($"发送短信开始:phone:{phoneNumber},signName:总部,发送门店项目审核短信");
                CommonResponse response = client.GetCommonResponse(request);
                _logger.Info(
                    $"发送短信结束:phone:{phoneNumber},signName:总部,发送门店项目审核短信,result:{System.Text.Encoding.Default.GetString(response.HttpResponse.Content)}");
            }
            catch (ServerException e)
            {
                _logger.Error($"发送门店项目审核短信:phone:{phoneNumber},异常", e);
            }
            catch (ClientException e)
            {
                _logger.Error($"发送门店项目审核短信:phone:{phoneNumber},异常", e);
            }
        }

        
    }
}
