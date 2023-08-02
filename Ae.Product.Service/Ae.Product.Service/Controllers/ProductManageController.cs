using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Core.Model.DoorProduct;
using Ae.Product.Service.Core.Model.InstallService;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Core.Request.Config;
using Ae.Product.Service.Core.Request.DoorProduct;
using Ae.Product.Service.Core.Request.InstallService;
using Ae.Product.Service.Core.Response;
using Ae.Product.Service.Filters;

namespace Ae.Product.Service.Controllers
{
    /// <summary>
    /// 商品管理相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(ProductManageController))]
    public class ProductManageController : ControllerBase
    {
        private readonly IProductManageService _productManageService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="productManageService"></param>
        public ProductManageController(IProductManageService productManageService)
        {
            _productManageService = productManageService;
        }


        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddBrand([FromBody]AddBrandRequest request)
        {
            var result = await _productManageService.AddBrandAsync(request);
            if (result)
                return Result.Success();
            else
            {
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = "品牌添加失败"
                };
            }
        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> EditBrand([FromBody]AddBrandRequest request)
        {
            var result = await _productManageService.EditBrandAsync(request);
            if (result)
                return Result.Success();
            else
            {
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = "品牌编辑失败"
                };
            }
        }

        /// <summary>
        /// 批量生成产品
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> BatchGenerationProducts([FromBody] BatchGenerationProductsRequest request)
        {
            _productManageService.BatchGenerationProducts(request);
            var msg = "成功!";
            return Result.Success<string>(msg);
        }

        /// <summary>
        ///  编辑商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> SaveProduct([FromBody]ProductEditRequest request)
        {
            var result = _productManageService.SaveProduct(request);
            return Result.Success<bool>(result);
        }
        /// <summary>
        ///  批量导入商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> ImportProduct([FromBody]ImportProductRequest request)
        {
            var result = _productManageService.ImportProduct(request);
            return Result.Success<bool>(result);
        }

        /// <summary>
        /// 初始化轮胎数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> IniTireProduct(IFormFile request)
        {
            string fileName = request.FileName.Split(new[] { '.' })[0];
            var stream = request.OpenReadStream();
            StreamReader sr = new StreamReader(stream);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                var product = line.Split(new string[] { "\t" }, StringSplitOptions.None);
                var Specifications = product[0].Trim();//33 规格 155/65R13
                var CP_Tire_Pattern = product[2].Trim();//27 花纹
                var price = product[4].Trim();
                var zaiZhong= product[5].Trim();//26
                var suDu= product[6].Trim();//30
                var remark= product[7].Trim();//30
                var result = await _productManageService.IniTireProduct(Specifications, CP_Tire_Pattern, price,
                    zaiZhong, suDu, remark);
            }
            sr.Close();
            return Result.Success(true);
        }

        /// <summary>
        ///  导入商品属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> ImportProductAttribute([FromBody]ImportProductAttributeRequest request)
        {
            var result = _productManageService.ImportProductAttribute(request);
            return Result.Success<bool>(result);
        }

        /// <summary>
        /// 批量更新商品接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> BatchUpdateProduct([FromBody]BatchUpdateProductRequest request)
        {
            var result = _productManageService.BatchUpdateProduct(request);
            return Result.Success<bool>(result);
        }

        /// <summary>
        /// 批量更新商品图片接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> BatchUpdateProductImage()
        {
            var result = _productManageService.BatchUpdateProductImage();
            return Result.Success<bool>(result);
        }

        /// 添加单位
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddUnit([FromBody]AddUnitRequest request)
        {
            var result = await _productManageService.AddUnitAsync(request);
            if (result)
                return Result.Success();
            else
            {
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = "单位添加失败"
                };
            }
        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> EditUnit([FromBody]AddUnitRequest request)
        {
            var result = await _productManageService.EditUnitAsync(request);
            if (result)
                return Result.Success();
            else
            {
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = "单位编辑失败"
                };
            }
        }

        /// <summary>
        /// 列出类目树
        /// </summary>
        /// <param name="displayName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ListCategoryVo>>> ListCategory([FromQuery] GetCategoryListRequest request)
        {
            return await _productManageService.ListCategory(request);
        }

        /// <summary>
        /// 获取类目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<DimCategoryVo>> GetCategory([FromQuery]int id)
        {
            return await _productManageService.GetCategory(id);
        }

        /// <summary>
        /// 父级类目查子集类目集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<DimCategoryVo>>> GetCategoryByParentId([FromQuery]CategoryByParentIdRequest request)
        {
            return await _productManageService.GetCategoryByParentId(request);
        }

        /// <summary>
        /// 获取所有品牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetProductBrandVo>>> GetAllProductBrandList()
        {
            var result = await _productManageService.GetAllProductBrandList();

            return Result.Success(result);
        }

        /// <summary>
        /// 类目树选项查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CategoryTreeSelectVo>>> CategoryTreeSelect([FromQuery] GetCategoryListRequest request)
        {
            return await _productManageService.CategoryTreeSelect(request);
        }

        /// <summary>
        /// 修改类目
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpdateCategory([FromBody]DimCategoryVo vo)
        {
            return await _productManageService.UpdateCategory(vo);
        }

        /// <summary>
        /// 新增类目
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddCategory([FromBody]DimCategoryVo vo)
        {
            return await _productManageService.AddCategory(vo);
        }

        /// <summary>
        /// 删除类目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> DeleteCategory([FromBody]DimCategoryVo vo)
        {
            return await _productManageService.DeleteCategory(vo);
        }
        /// <summary>
        ///  编辑属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> SaveAttribute([FromBody]AttributeEditRequest request)
        {
            var result = _productManageService.SaveAttribute(request);
            return Result.Success<bool>(result);
        }

        /// <summary>
        ///  编辑类目属性
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> SaveCategoryAttribute([FromBody]CategoryAttributeEditRequest request)
        {
            var result = _productManageService.SaveCategoryAttribute(request);
            return Result.Success<bool>(result);
        }

        #region 安装服务

        /// <summary>
        /// 查询安装服务列表
        /// </summary>
        [HttpGet]
        public async Task<ApiPagedResult<ProductInstallServiceVo>> GetProductInstallService(
            [FromQuery] ProductInstallServiceRequest request)
        {
            var result = await _productManageService.GetProductInstallService(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 查询安装服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductInstallServiceVo>> GetProductInstallServiceDetail(
            [FromQuery] ProductInstallServiceDetailRequest request)
        {
            var result = await _productManageService.GetProductInstallServiceDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 添加安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddInstallService([FromBody] AddInstallServiceRequest request)
        {
            var result = await _productManageService.AddInstallService(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditInstallService([FromBody] EditInstallServiceRequest request)
        {
            var result = await _productManageService.EditInstallService(request);

            return Result.Success(result);
        }

        #endregion

        #region 安装服务 对外api

        /// <summary>
        /// 获取产品对应安装服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<InstallServiceByProductResponse>> GetInstallServiceByProduct(
            [FromBody] InstallServiceByProductRequest request)
        {
            var result = await _productManageService.GetInstallServiceByProduct(request);

            return Result.Success(result);
        }

        #endregion

        #region 上门产品

        /// <summary>
        /// 获取上门产品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<DoorProductVo>> GetDoorProductPageList(
            [FromQuery] DoorProductPageListRequest request)
        {
            var result = await _productManageService.GetDoorProductPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        //public async Task<ApiPagedResult<DoorProductVo>> SearchProductForDoor()

        /// <summary>
        /// 编辑上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditDoorProduct([FromBody] EditDoorProductRequest request)
        {
            var result = await _productManageService.EditDoorProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增上门产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddDoorProducts([FromBody] AddDoorProductsRequest request)
        {
            var result = await _productManageService.AddDoorProducts(request);

            return Result.Success(result);
        }

        #endregion

        #region 前台分类

        /// <summary>
        /// 获取前台类目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<FrontCategoryVo>> GetFrontCategoryPageList(
            [FromQuery] FrontCategoryPageListRequest request)
        {
            var result = await _productManageService.GetFrontCategoryPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 前台分类详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<FrontCategoryDetailVo>> GetFrontCategoryDetail(
            [FromQuery] FrontCategoryDetailRequest request)
        {
            var result = await _productManageService.GetFrontCategoryDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增前台分类配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddFrontCategory([FromBody] AddFrontCategoryRequest request)
        {
            var result = await _productManageService.AddFrontCategory(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑前台分类配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditFrontCategory([FromBody] EditFrontCategoryRequest request)
        {
            var result = await _productManageService.EditFrontCategory(request);

            return Result.Success(result);
        }

        #endregion
    }
}