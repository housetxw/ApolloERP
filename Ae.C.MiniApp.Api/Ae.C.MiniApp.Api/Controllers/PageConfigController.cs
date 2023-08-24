using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model.Product;
using Ae.C.MiniApp.Api.Core.Request.Product;
using Ae.C.MiniApp.Api.Core.Response.PageConfig;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 界面功能显示配置
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(PageConfigController))]
    [ApiController]
    public class PageConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IShopProductService _shopProductService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="shopProductService"></param>
        public PageConfigController(IConfiguration configuration, IShopProductService shopProductService)
        {
            _configuration = configuration;
            _shopProductService = shopProductService;
        }


        /// <summary>
        /// 首页功能配置
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<MainPageConfigResponse>> GetMainPageConfig(
            [FromQuery] MainPageConfigRequest request)
        {
            var data = await _shopProductService.GetMainPageConfig(request);

            return Result.Success(data);

            // MainPageConfigResponse result = new MainPageConfigResponse()
            // {
            //     TopAdvertising = new List<AdvertisingModel>()
            //     {
            //         new AdvertisingModel()
            //         {
            //             AdvertisingCode = "activity1",
            //             ImageUrl = $"{_configuration["ImageDomain"]}/mini-app-main/88banner.png",
            //             RouteUrl = ""
            //         },
            //         new AdvertisingModel()
            //         {
            //             AdvertisingCode = "activity2",
            //             ImageUrl = $"{_configuration["ImageDomain"]}/mini-app-main/redBanner.png",
            //             RouteUrl = ""
            //         }
            //     },
            //     Modules = new List<ModuleBlock>()
            //     {
            //         new ModuleBlock()
            //         {
            //             Category = "BaoYang",
            //             DisplayName = "上门养护",
            //             ImageUrl = $"{_configuration["ImageDomain"]}/mini-app-main/curingIcon.png",
            //             RouteUrl = "/pages/littleMaintain/main",
            //             SubTitle = "更换机油，三滤",
            //             PackageType = new List<string>() {"xby"},
            //             ServiceType = 1
            //         },
            //         new ModuleBlock()
            //         {
            //             Category = "BaoYang",
            //             DisplayName = "到店养护",
            //             ImageUrl = $"{_configuration["ImageDomain"]}/mini-app-main/maintenance_icon.png",
            //             RouteUrl = "/pages/maintain/main",
            //             SubTitle = "多种套餐适配查询"
            //         },
            //         new ModuleBlock()
            //         {
            //             Category = "Tire",
            //             DisplayName = "轮胎服务",
            //             ImageUrl = $"{_configuration["ImageDomain"]}/mini-app-main/wheels_icon.png",
            //             RouteUrl = "/pages/tireService/main",
            //             SubTitle = "买轮胎送保险"
            //         },
            //         new ModuleBlock()
            //         {
            //             Category = "PackageCard",
            //             DisplayName = "套餐卡",
            //             ImageUrl = $"{_configuration["ImageDomain"]}/mini-app-main/card_icon.png",
            //             RouteUrl = "/pages/packageCard/main",
            //             SubTitle = "次卡 月卡 季卡"
            //         },
            //         new ModuleBlock()
            //         {
            //             Category = "Wash",
            //             DisplayName = "美容洗车",
            //             ImageUrl = $"{_configuration["ImageDomain"]}/mini-app-main/washcar_icon.png",
            //             RouteUrl = "/pages/washCar/main",
            //             SubTitle = "25元起洗车"
            //         },
            //         new ModuleBlock()
            //         {
            //             Category = "BaoYang",
            //             DisplayName = "紧急救援",
            //             ImageUrl = $"{_configuration["ImageDomain"]}/mini-app-main/helpIcon.png",
            //             RouteUrl = "/pages/littleMaintain/main",
            //             SubTitle = "更换冷却液、刹车片",
            //             PackageType = new List<string>() {"dby"},
            //             ServiceType = 1
            //         }
            //     },
            //     ProminentActive = new List<AdvertisingModel>()
            //     {
            //         new AdvertisingModel()
            //         {
            //             AdvertisingCode = "ProminentActive1",
            //             ImageUrl = $"{_configuration["ImageDomain"]}/mini-app-main/home_actvie.png",
            //             RouteUrl = "/pages/detailsPages/main",
            //             Key = _configuration["ProductServer:ApolloErpJiYouPackage"]
            //         }
            //     }
            // };
            // return await Task.FromResult(Result.Success(result));
        }

        /// <summary>
        /// 热门商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiPagedResult<HotProductVo>> GetHotProductPageList(
            [FromQuery] HotProductPageListRequest request)
        {
            var result = await _shopProductService.GetHotProductPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 获取套餐卡列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiPagedResult<PackageCardProductVo>> GetPackageCardProduct(
            [FromQuery] PackageCardProductRequest request)
        {
            var result = await _shopProductService.GetPackageCardProduct(request);

            return Result.Success(result.Items, result.TotalItems);
        }
    }
}