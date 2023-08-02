using AutoMapper;
using Microsoft.AspNetCore.Http;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model;
using Ae.B.Product.Api.Client.Request;
using Ae.B.Product.Api.Client.Response;
using Ae.B.Product.Api.Common.Exceptions;
using Ae.B.Product.Api.Common.Extension;
using Ae.B.Product.Api.Common.Util;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Client.Request.BaoYang;
using Ae.B.Product.Api.Client.Request.Product;
using Ae.B.Product.Api.Core.Request.Product;
using Ae.B.Product.Api.Core.Model.Product;
using Ae.B.Product.Api.Client.Model.Product;

namespace Ae.B.Product.Api.Imp.Services
{
    /// <summary>
    /// 产品管理
    /// </summary>
    public class ProductManageService : IProductManageService
    {
        private readonly IProductClient _productClient;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly ApolloErpLogger<ProductManageService> _logger;
        private readonly IBaoYangClient _baoYangClient;
        private readonly IPageConfigClient _pageConfigClient;

        public ProductManageService(IProductClient productClient, IMapper mapper, IIdentityService identityService,
            ApolloErpLogger<ProductManageService> logger, IBaoYangClient baoYangClient, IPageConfigClient pageConfigClient)
        {
            _productClient = productClient;
            _mapper = mapper;
            _identityService = identityService;
            _logger = logger;
            _baoYangClient = baoYangClient;
            _pageConfigClient = pageConfigClient;
        }

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        public async Task<List<CategoryAttributeVo>> GetAttributesByCategoryId(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
                return null;
            }
            var datas = await _productClient.GetAttributesByCategoryId(categoryId);
            var result = _mapper.Map<List<CategoryAttributeVo>>(datas);
            return result;
        }
        /// <summary>
        /// 获取商品品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<CatalogBrandVo>> GetBrandList()
        {
            var datas = await _productClient.GetBrandList();
            var result = _mapper.Map<List<CatalogBrandVo>>(datas);
            return result;
        }

        /// <summary>
        ///   查询类目息 by 类目 和 类目 level 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<List<CategoryInfoVo>> GetCategorysById(int? categoryId, int? level)
        {

            var datas = await _productClient.GetCategorysById(categoryId, level);
            var result = _mapper.Map<List<CategoryInfoVo>>(datas);
            return result;
        }

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<UnitVo>> GetUnits()
        {
            var datas = await _productClient.GetUnits();
            var result = _mapper.Map<List<UnitVo>>(datas);
            return result;
        }

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductSearchResponse> SearchProduct(ProductSearchRequest request)
        {
            if (request == null)
            {
                return null;
            }
            var para = _mapper.Map<ProductSearchClientRequest>(request);
            if (!string.IsNullOrEmpty(request.ProductAttribute))
            {
                para.ProductAttributes = new List<string>() { request.ProductAttribute };
            }
            var data = await _productClient.SearchProduct(para);
            var result = _mapper.Map<ProductSearchResponse>(data);
            return result;
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductDetailVo> GetProductByProductCode(string productCode)
        {
            if (string.IsNullOrEmpty(productCode))
            {
                return null;
            }
            var para = new ProductDetailSearchClientRequest() { ProductCodes = new List<string> { productCode } };
            var datas = await _productClient.GetProductsByProductCodes(para);
            var result = new ProductDetailVo();
            if (datas.Count > 0)
            {
                result = _mapper.Map<ProductDetailVo>(datas.FirstOrDefault());
                var product = result.Product;
                //判断商品性质
                if (product?.ProductAttribute != 1)
                {
                    var categoryAttributes = await _productClient.GetAttributesByCategoryId(product?.ChildCategoryId.ToString());
                    var attributeList = _mapper.Map<List<ProductAttributevalueVo>>(categoryAttributes);
                    var productAttributevalues = result.Attributevalues;
                    if (productAttributevalues != null && productAttributevalues.Count > 0)
                    {
                        foreach (var item in attributeList)
                        {
                            var productAttributevalue = productAttributevalues.Where(t => t.AttributeNameId == item.AttributeNameId).FirstOrDefault();
                            if (productAttributevalue != null)
                            {
                                item.Id = productAttributevalue.Id;
                                item.AttributeValue = productAttributevalue.AttributeValue;
                            }
                        }
                    }
                    result.Attributevalues = attributeList;
                }
                else
                {
                    var productPackages = await _productClient.GetPackageProductsByCodes(para);
                    if (productPackages.Count() > 0)
                    {
                        var productPackage = productPackages.FirstOrDefault();
                        result.PackageDetails = _mapper.Map<List<ProductPackageDetailVo>>(productPackage?.Details);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveProduct(ApiRequest<ProductEditRequest> request)
        {
            if (request == null || request.Data.Product == null)
            {
                throw new CustomException("商品信息必填！");
            }

            var para = _mapper.Map<ProductEditClientRequest>(request.Data);
            para.UserId = _identityService.GetUserName(); //获取用户Id
            var result = await _productClient.SaveProduct(para);

            return result;
        }

        /// <summary>
        /// 导入商品
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<List<ProductImportVo>> ImportProduct(IFormFile file)
        {
            var result = new List<ProductImportVo>();
            var importRows = ExcelHelper.Import<ProductImportVo>(file.OpenReadStream(), file.FileName);
            if (importRows.IsNullOrEmpty())
            {
                throw new CustomException("导入数据不能为空");
            }
            var products = new List<ProductImportDto>();
            var userId = _identityService.GetUserId(); //获取用户Id
            foreach (ProductImportVo item in importRows)
            {
                if (string.IsNullOrEmpty(item.CategoryId) && string.IsNullOrEmpty(item.Name) && string.IsNullOrEmpty(item.DisplayName) && string.IsNullOrEmpty(item.Unit))
                {
                    continue;
                }
                var product = new ProductImportDto();
                if (string.IsNullOrEmpty(item.CategoryId))
                {
                    item.ErrorMessage = "商品类目必填！";
                    result.Add(item);
                    continue;
                }
                product.CategoryId = Convert.ToInt32(item.CategoryId);

                if (string.IsNullOrEmpty(item.Name))
                {
                    item.ErrorMessage = "商品名称必填！";
                    result.Add(item);
                    continue;
                }
                product.Name = item.Name;

                if (string.IsNullOrEmpty(item.DisplayName))
                {
                    item.ErrorMessage = "商品标题必填！";
                    result.Add(item);
                    continue;
                }
                product.DisplayName = item.DisplayName;

                if (string.IsNullOrEmpty(item.ProductAttribute))
                {
                    item.ErrorMessage = "产品性质必填！";
                    result.Add(item);
                    continue;
                }
                product.ProductAttribute = Convert.ToInt32(item.ProductAttribute);

                if (string.IsNullOrEmpty(item.Unit))
                {
                    item.ErrorMessage = "单位必填！";
                    result.Add(item);
                    continue;
                }
                product.Unit = item.Unit;
                product.Brand = item.Brand ?? "";
                //税率
                decimal taxRate = 0;
                if (!decimal.TryParse(item.TaxRate, out taxRate))
                {
                    item.ErrorMessage = "税率格式有误";
                    result.Add(item);
                    continue;
                }
                product.TaxRate = taxRate;
                //市场价
                decimal standardPrice = 0;
                if (!decimal.TryParse(item.StandardPrice, out standardPrice))
                {
                    item.ErrorMessage = "市场价格式有误";
                    result.Add(item);
                    continue;
                }
                product.StandardPrice = standardPrice;
                //销售价
                decimal salesPrice = 0;
                if (!decimal.TryParse(item.SalesPrice, out salesPrice))
                {
                    item.ErrorMessage = "销售价格式有误";
                    result.Add(item);
                    continue;
                }
                product.SalesPrice = salesPrice;

                //长
                decimal length = 0;
                if (!decimal.TryParse(item.Length, out length))
                {
                    item.ErrorMessage = "长格式有误";
                    result.Add(item);
                    continue;
                }
                product.Length = length;

                //宽
                decimal width = 0;
                if (!decimal.TryParse(item.Width, out width))
                {
                    item.ErrorMessage = "宽格式有误";
                    result.Add(item);
                    continue;
                }
                product.Width = width;

                //宽
                decimal height = 0;
                if (!decimal.TryParse(item.Height, out height))
                {
                    item.ErrorMessage = "宽格式有误";
                    result.Add(item);
                    continue;
                }
                product.Height = height;
                product.PartCode = item.PartCode ?? "";
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
                var request = new ImportProductClientRequest() { UserId = userId };
                for (int i = 0; i < totalPages; i++)
                {
                    var skipNum = i * pageSize;
                    request.Products = products.Skip(skipNum).Take(pageSize).ToList();
                    await _productClient.ImportProduct(request);
                }
            }
            return result;
        }

        /// <summary>
        /// 导入商品属性
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<List<ProductAttributeImportVo>> ImportProductAttribute(IFormFile file)
        {
            var importRows = ExcelHelper.Import<ProductAttributeImportVo>(file.OpenReadStream(), file.FileName);
            if (importRows.IsNullOrEmpty())
            {
                throw new CustomException("导入数据不能为空");
            }
            var result = new List<ProductAttributeImportVo>();
            var userId = _identityService.GetUserId(); //获取用户Id
            var attributeNames = await _productClient.SelectAttributeNames(new AttributeNameSearchClientRequest()); //所有的属性名称
            //var emptyRows = importRows.Where(t => string.IsNullOrEmpty(t.PartCode))?.ToList();
            //if (!emptyRows.IsNullOrEmpty())
            //{
            //    throw new CustomException("存在配件编码为空的数据，请核对！");
            //}
            var partNos = importRows.Select(t => t.PartCode).Distinct().ToList();
            var pageSize = 100;
            var totalCount = partNos.Count;
            var totalPages = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
            var productInfoList = new List<ProductBaseInfoDto>();
            for (int i = 0; i < totalPages; i++)
            {
                var skipNum = i * pageSize;
                var requestPartNos = partNos.Skip(skipNum).Take(pageSize).ToList();
                var productInfos = await _productClient.SelectProductsByPartNos(new ProductPartClientRequest() { PartNos = requestPartNos });
                if (!productInfos.IsNullOrEmpty())
                {
                    productInfoList.AddRange(productInfos);
                }
            }
            var productAttributes = new List<ProductAttributeImportDto>();
            foreach (var item in importRows)
            {
                if (string.IsNullOrEmpty(item.PartCode) && string.IsNullOrEmpty(item.AttributeName) && string.IsNullOrEmpty(item.AttributeValue))
                {
                    continue;
                }
                var productAttribute = new ProductAttributeImportDto();
                var product = productInfoList.Where(a => a.PartCode == item.PartCode).FirstOrDefault();
                if (product == null)
                {
                    item.ErrorMessage = "PartCode不存在！";
                    result.Add(item);
                    continue;
                }
                productAttribute.ProductId = product.Id.ToString();
                if (string.IsNullOrEmpty(item.AttributeName))
                {
                    item.ErrorMessage = "属性名称不能为空！";
                    result.Add(item);
                    continue;
                }
                productAttribute.AttributeName = item.AttributeName;
                var attributeNameDto = attributeNames.Where(t => t.DisplayName == item.AttributeName).FirstOrDefault();
                if (attributeNameDto == null)
                {
                    item.ErrorMessage = "属性名称不存在！";
                    result.Add(item);
                    continue;
                }
                productAttribute.AttributeNameId = attributeNameDto.Id;
                if (string.IsNullOrEmpty(item.AttributeValue))
                {
                    item.ErrorMessage = "属性值不能为空！";
                    result.Add(item);
                    continue;
                }
                productAttribute.AttributeValue = item.AttributeValue;
                productAttributes.Add(productAttribute);
            }
            if (!result.IsNullOrEmpty())
            {
                return result.OrderBy(t => t.PartCode).ToList();
            }
            if (!productAttributes.IsNullOrEmpty())
            {
                pageSize = 50;
                totalCount = importRows.Count;
                totalPages = totalCount % pageSize == 0 ? totalCount / pageSize : totalCount / pageSize + 1;
                var request = new ImportProductAttributeClientRequest() { UserId = userId };
                for (int i = 0; i < totalPages; i++)
                {
                    var skipNum = i * pageSize;
                    request.ProductAttributes = productAttributes.Skip(skipNum).Take(pageSize).ToList();
                    await _productClient.ImportProductAttribute(request);
                }
            }
            return result;
        }

        /// <summary>
        /// 分页查询商品品牌信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetProductBrandVo>> GetProductBrandList(GetProductBrandListRequest request)
        {
            return await _productClient.GetProductBrandList(request);
        }

        /// <summary>
        /// 分页查询商品单位信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetProductUnitListVo>> GetProductUnitList(GetProductUnitListRequest request)
        {
            return await _productClient.GetProductUnitList(request);
        }
        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddBrand(AddBrandRequest request)
        {
            request.UserId = _identityService.GetUserId() ?? "System";
            return await _productClient.AddBrand(request);
        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> EditBrand(AddBrandRequest request)
        {
            request.UserId = _identityService.GetUserId() ?? "System";
            return await _productClient.EditBrand(request);
        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddUnit(AddUnitRequest request)
        {
            request.UserId = _identityService.GetUserId() ?? "System";
            return await _productClient.AddUnit(request);
        }

        /// <summary>
        /// 编辑单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> EditUnit(AddUnitRequest request)
        {
            request.UserId = _identityService.GetUserId() ?? "System";
            return await _productClient.EditUnit(request);
        }

        public async Task<ApiResult<List<ListCategoryVo>>> ListCategory(GetCategoryListRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.ShopId = shopId;

            var result = Result.Failed<List<ListCategoryVo>>("加载异常");
            try
            {
                var clientResult = await _productClient.ListCategory(request);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = _mapper.Map<List<ListCategoryVo>>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else
                {
                    result = Result.Failed<List<ListCategoryVo>>(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("ListCategoryEx", ex);
            }
            return result;
        }

        public async Task<ApiResult<DimCategoryVo>> GetCategory(int id)
        {
            var result = Result.Failed<DimCategoryVo>("加载异常");
            try
            {
                var clientResult = await _productClient.GetCategory(id);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = _mapper.Map<DimCategoryVo>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else
                {
                    result = Result.Failed<DimCategoryVo>(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("GetCategoryEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 分页查询商品属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AttributeSearchResponse> SearchAttribute(AttributeSearchRequest request)
        {
            var result = new AttributeSearchResponse();
            var para = _mapper.Map<AttributeSearchClientRequest>(request);
            if (para == null)
            {
                return result;
            }
            var pagedResult = await _productClient.SearchAttribute(para);
            result.Items = _mapper.Map<List<AttributeNameVo>>(pagedResult.Data.Items);
            result.TotalItems = pagedResult.Data.TotalItems;
            return result;
        }

        public async Task<ApiResult<List<CategoryTreeSelectVo>>> CategoryTreeSelect(GetCategoryListRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.ShopId = shopId;

            var result = Result.Failed<List<CategoryTreeSelectVo>>("查询失败");
            try
            {
                var clientResult = await _productClient.CategoryTreeSelect(request);
                if (clientResult.IsNotNullSuccess())
                {
                    var response = _mapper.Map<List<CategoryTreeSelectVo>>(clientResult.Data);
                    result = Result.Success(response, clientResult.Message);
                }
                else
                {
                    result = Result.Failed<List<CategoryTreeSelectVo>>(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("CategoryTreeSelectEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> UpdateCategory(DimCategoryVo vo)
        {
            var result = Result.Failed("修改失败");
            try
            {
                vo.UpdateBy = _identityService.GetUserName();
                var clientResult = await _productClient.UpdateCategory(vo);
                if (clientResult.IsNotNullSuccess())
                {
                    result = Result.Success(clientResult.Message);
                }
                else
                {
                    result = Result.Failed(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("UpdateCategoryEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> AddCategory(DimCategoryVo vo)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            vo.ShopId = shopId;

            var result = Result.Failed("新增失败");
            try
            {
                vo.CreateBy = _identityService.GetUserName();
                vo.CategoryType = 1;
                var clientResult = await _productClient.AddCategory(vo);
                if (clientResult.IsNotNullSuccess())
                {
                    result = Result.Success(clientResult.Message);
                }
                else
                {
                    result = Result.Failed(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("AddCategoryEx", ex);
            }
            return result;
        }

        public async Task<ApiResult> DeleteCategory(int id)
        {
            var result = Result.Failed("删除失败");
            try
            {
                var vo = new DimCategoryVo() { Id = id, UpdateBy = _identityService.GetUserName() };
                var clientResult = await _productClient.DeleteCategory(vo);
                if (clientResult.IsNotNullSuccess())
                {
                    result = Result.Success(clientResult.Message);
                }
                else
                {
                    result = Result.Failed(clientResult.Code, clientResult.Message);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error("DeleteCategoryEx", ex);
            }
            return result;
        }
        /// <summary>
        /// 根据属性Id 获取属性名称
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AttributeResponse> GetAttributeNameById(int attributeNameId)
        {
            var result = new AttributeResponse();
            var attribute = await _productClient.GetAttributeById(attributeNameId);
            result = _mapper.Map<AttributeResponse>(attribute);
            return result;
        }

        /// 新增属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveAttribute(ApiRequest<AttributeEditRequest> request)
        {
            if (request == null || request.Data.AttributeName == null)
            {
                throw new CustomException("属性信息必填！");
            }
            var para = _mapper.Map<AttributeEditClientRequest>(request.Data);
            para.UserId = _identityService.GetUserId(); //获取用户Id
            var result = await _productClient.SaveAttribute(para);
            return result;
        }

        /// <summary>
        ///  获取所有的属性名信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<AttributeNameVo>> GetAttributeNames()
        {
            var result = new List<AttributeNameVo>();
            var attributes = await _productClient.SelectAttributeNames(new AttributeNameSearchClientRequest());
            result = _mapper.Map<List<AttributeNameVo>>(attributes);
            return result;
        }

        /// <summary>
        ///获取类目属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<CategoryAttributeVo>> SelectCategoryAttribute(CategoryAttributeRequest request)
        {
            var result = new List<CategoryAttributeVo>();
            var para = new CategoryAttributeClientRequest()
            {
                CategoryId = request.ChildCategoryId,
                IsDeleted = request.IsDeleted,
                Key = request.Key
            };
            var attributes = await _productClient.SelectCategoryAttribute(para);
            result = _mapper.Map<List<CategoryAttributeVo>>(attributes);
            return result;
        }

        /// 新增类目属性信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SaveCategoryAttribute(CategoryAttributeEditRequest request)
        {
            if (request == null || request.CategoryAttributes.IsNullOrEmpty())
            {
                throw new CustomException("类目属性信息必填！");
            }
            var para = _mapper.Map<CategoryAttributeEditClientRequest>(request);
            para.UserId = _identityService.GetUserId(); //获取用户Id
            var result = await _productClient.SaveCategoryAttribute(para);
            return result;
        }

        /// <summary>
        /// 根据父级类目获取子类目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<DimCategoryVo>> GetCategoryByParentId(CategoryByParentIdRequest request)
        {
            var result = await _productClient.GetCategoryByParentId(new CategoryByParentIdClientRequest()
            {
                ParentId = request.ParentId
            });

            return result;
        }

        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<GetProductBrandVo>> GetAllProductBrandList()
        {
            var result = await _productClient.GetAllProductBrandList();

            return result;
        }

        /// <summary>
        /// 获取上门产品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<DoorProductVo>> GetDoorProductPageList(
            DoorProductPageListRequest request)
        {
            ApiPagedResultData<DoorProductVo> response = new ApiPagedResultData<DoorProductVo>()
            {
                Items = new List<DoorProductVo>()
            };
            var result = await _productClient.GetDoorProductPageList(new DoorProductPageListClientRequest()
            {
                Key = request.Key,
                Brand = request.Brand,
                MainCategoryId = request.MainCategoryId.GetIntValue(),
                SecondCategoryId = request.SecondCategoryId.GetIntValue(),
                ChildCategoryId = request.ChildCategoryId.GetIntValue(),
                IsDoorProduct = request.IsDoorProduct,
                FreeDoorFee = request.FreeDoorFee,
                OnSale = request.OnSale,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    response.Items = _mapper.Map<List<DoorProductVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 编辑上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditDoorProduct(EditDoorProductRequest request)
        {
            var result = await _productClient.EditDoorProduct(new EditDoorProductClientRequest()
            {
                Id = request.Id,
                FreeDoorFee = request.FreeDoorFee,
                IsDeleted = request.IsDeleted,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 新增上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddDoorProducts(AddDoorProductsRequest request)
        {
            var result = await _productClient.AddDoorProducts(new AddDoorProductsClientRequest()
            {
                DoorProducts = _mapper.Map<List<AddDoorProductsDto>>(request.DoorProducts),
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 查询安装服务列表
        /// </summary>
        public async Task<ApiPagedResultData<ProductInstallServiceVo>>
            GetProductInstallService(ProductInstallServiceRequest request)
        {
            ApiPagedResultData<ProductInstallServiceVo> response = new ApiPagedResultData<ProductInstallServiceVo>()
            {
                Items = new List<ProductInstallServiceVo>()
            };
            var result = await _productClient.GetProductInstallService(new ProductInstallServiceClientRequest()
            {
                Key = request.Key,
                Brand = request.Brand,
                MainCategoryId = request.MainCategoryId.GetIntValue(),
                SecondCategoryId = request.SecondCategoryId.GetIntValue(),
                ChildCategoryId = request.ChildCategoryId.GetIntValue(),
                HasInstallService = request.HasInstallService,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    response.Items = _mapper.Map<List<ProductInstallServiceVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 查询安装服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ProductInstallServiceVo> GetProductInstallServiceDetail(
            ProductInstallServiceDetailRequest request)
        {
            var result = await _productClient.GetProductInstallServiceDetail(
                new ProductInstallServiceDetailClientRequest()
                {
                    Pid = request.Pid
                });
            if (result != null)
            {
                return _mapper.Map<ProductInstallServiceVo>(result);
            }

            return null;
        }

        /// <summary>
        /// 搜索安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductBaseInfoVo>> SearchInstallService(SearchInstallServiceRequest request)
        {
            var result = await _productClient.SearchInstallService(request.Content);

            if (result != null && result.Any())
            {
                return _mapper.Map<List<ProductBaseInfoVo>>(result);
            }

            return new List<ProductBaseInfoVo>();
        }

        /// <summary>
        /// 编辑安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditInstallService(EditInstallServiceRequest request)
        {
            var result = await _productClient.EditInstallService(new EditInstallServiceClientRequest()
            {
                Products = request.Products?.Select(_ => new ProductForInstallDto
                {
                    ProductId = _.ProductId,
                    Pid = _.Pid

                })?.ToList(),
                InstallService = request.InstallService?.Select(_ => new InstallServiceDto
                {
                    Id = _.Id,
                    ServiceId = _.ServiceId,
                    ServiceName = _.ServiceName,
                    Price = _.Price,
                    FreeInstall = _.FreeInstall,
                    ChangeNum = _.ChangeNum
                })?.ToList(),
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ConfigHotProductVo>> GetHotProductPageList(
            HotProductPageListRequest request)
        {
            ApiPagedResultData<ConfigHotProductVo> response = new ApiPagedResultData<ConfigHotProductVo>()
            {
                Items = new List<ConfigHotProductVo>()
            };
            var result = await _productClient.GetHotProductPageList(new HotProductPageListClientRequest()
            {
                TerminalType = request.TerminalType,
                Key = request.Key,
                Brand = request.Brand,
                MainCategoryId = request.MainCategoryId.GetIntValue(),
                SecondCategoryId = request.SecondCategoryId.GetIntValue(),
                ChildCategoryId = request.ChildCategoryId.GetIntValue(),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    response.Items = _mapper.Map<List<ConfigHotProductVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 新增热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddHotProduct(AddHotProductRequest request)
        {
            var result = await _productClient.AddHotProduct(new AddHotProductClientRequest()
            {
                TerminalType = request.TerminalType,
                Products = request.Products.Select(_ => new HotProductClientReq
                {
                    Pid = _.Pid,
                    Rank = _.Rank
                }).ToList(),
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 编辑热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditHotProduct(EditHotProductRequest request)
        {
            var result = await _productClient.EditHotProduct(new EditHotProductClientRequest()
            {
                Id = request.Id,
                Rank = request.Rank,
                IsDeleted = request.IsDeleted,
                SubmitBy = _identityService.GetUserName()
            });
            return result;
        }

        /// <summary>
        /// 搜索商品For添加热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ConfigHotProductVo>> SearchProductForHot(
            SearchProductForHotRequest request)
        {
            ApiPagedResultData<ConfigHotProductVo> response = new ApiPagedResultData<ConfigHotProductVo>()
            {
                Items = new List<ConfigHotProductVo>()
            };
            var result = await _productClient.SearchProductForHot(new SearchProductForHotClientRequest()
            {
                TerminalType = request.TerminalType,
                ProductName = request.ProductName,
                Brand = request.Brand,
                MainCategoryId = request.MainCategoryId.GetIntValue(),
                SecondCategoryId = request.SecondCategoryId.GetIntValue(),
                ChildCategoryId = request.ChildCategoryId.GetIntValue(),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    response.Items = _mapper.Map<List<ConfigHotProductVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 获取终端类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<TerminalTypeVo>> GetTerminalTypeEnum()
        {
            List<TerminalTypeVo> result = new List<TerminalTypeVo>();
            Type t = typeof(Core.Enums.TerminalType);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.TerminalType couponType = (Core.Enums.TerminalType) arrays.GetValue(i);
                result.Add(new TerminalTypeVo()
                {
                    Id = (int) couponType,
                    DisplayName = EnumExtension.GetEnumDescription(couponType)
                });
            }

            return await Task.FromResult(result);
        }

        /// <summary>
        /// 套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PackageCardProductVo>> GetPackageCardProductPageList(
            PackageCardProductPageListRequest request)
        {
            ApiPagedResultData<PackageCardProductVo> response = new ApiPagedResultData<PackageCardProductVo>()
            {
                Items = new List<PackageCardProductVo>()
            };
            var result = await _productClient.GetPackageCardProductPageList(
                new PackageCardProductPageListClientRequest()
                {
                    Type = request.Type,
                    OnSale = request.OnSale,
                    Key = request.Key,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize
                });
            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    response.Items = _mapper.Map<List<PackageCardProductVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 搜索商品For添加套餐卡商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PackageCardProductVo>> SearchProductForPackageCard(
            SearchProductForPackageCardRequest request)
        {
            ApiPagedResultData<PackageCardProductVo> response = new ApiPagedResultData<PackageCardProductVo>()
            {
                Items = new List<PackageCardProductVo>()
            };
            var result = await _productClient.SearchProductForPackageCard(new SearchProductForPackageCardClientRequest()
            {
                ProductName = request.ProductName,
                Brand = request.Brand,
                MainCategoryId = request.MainCategoryId.GetIntValue(),
                SecondCategoryId = request.SecondCategoryId.GetIntValue(),
                ChildCategoryId = request.ChildCategoryId.GetIntValue(),
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (result != null)
            {
                response.TotalItems = result.TotalItems;
                if (result.Items != null && result.Items.Any())
                {
                    response.Items = _mapper.Map<List<PackageCardProductVo>>(result.Items);
                }
            }

            return response;
        }

        /// <summary>
        /// 添加套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddPackageCardProduct(AddPackageCardProductRequest request)
        {
            var result = await _productClient.AddPackageCardProduct(new AddPackageCardProductClientRequest()
            {
                Products = request.Products?.Select(_ => new PackageCardProductClientReq
                {
                    Pid = _.Pid,
                    Rank = _.Rank,
                    Type = _.Type
                })?.ToList(),
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 编辑套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditPackageCardProduct(EditPackageCardProductRequest request)
        {
            var result = await _productClient.EditPackageCardProduct(new EditPackageCardProductClientRequest()
            {
                Id = request.Id,
                Rank = request.Rank,
                Type = request.Type,
                IsDeleted = request.IsDeleted,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 获取套餐卡类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<PackageCardTypeVo>> GetPackageCardTypeEnum()
        {
            List<PackageCardTypeVo> result = new List<PackageCardTypeVo>();
            Type t = typeof(Core.Enums.TerminalType);
            Array arrays = Enum.GetValues(t);
            for (int i = 0; i < arrays.LongLength; i++)
            {
                Core.Enums.PackageCardEnum couponType = (Core.Enums.PackageCardEnum) arrays.GetValue(i);
                result.Add(new PackageCardTypeVo()
                {
                    Id = (int) couponType,
                    DisplayName = EnumExtension.GetEnumDescription(couponType)
                });
            }

            return await Task.FromResult(result);
        }

        public async Task<ApiPagedResult<GetShopPackageCardProductPageListVo>> GetShopPackageCardProductPageList(ApiRequest<GetShopPackageCardProductPageListRequest> request)
        {
           return await _pageConfigClient.GetShopPackageCardProductPageList(request);
        }

        public async Task<ApiResult> SaveShopPackageCard(ApiRequest<ConfigShopPackageCardDTO> request)
        {
            request.Data.CreateBy = _identityService.GetUserName();
            request.Data.CreateTime = DateTime.Now;
            request.Data.UpdateTime = DateTime.Now;
            request.Data.UpdateBy = _identityService.GetUserName();
            return await _pageConfigClient.SaveShopPackageCard(request);
        }

        public async Task<ApiResult<GetShopPackageCardProductPageListVo>> GetShopCardDetail(ApiRequest<GetShopCardDetailRequest> request)
        {
            return await _pageConfigClient.GetShopCardDetail(request);
        }
    }
}
