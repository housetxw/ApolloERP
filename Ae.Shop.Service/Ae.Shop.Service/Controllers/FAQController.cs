using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class FAQController : Controller
    {
        private readonly ApolloErpLogger<FAQController> logger;
        private readonly IFAQService iFAQService;
        private readonly IIdentityService _identityService;


        public FAQController(ApolloErpLogger<FAQController> logger, 
            IFAQService iFAQService,IIdentityService identityService
            )
        {
            this.logger = logger;
            this.iFAQService = iFAQService;
            _identityService = identityService;
        }

        /// <summary>
        /// 问答列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<FaqDTO>> GetFAQListAsync([FromQuery] GetFAQListRequest request) 
        {
            var result = await iFAQService.GetFAQListAsync(request);
            ApiPagedResult<FaqDTO> response = new ApiPagedResult<FaqDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 查询FAQ详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<FaqDTO>> GetFAQInfoAsync([FromQuery] GetFAQInfoRequest request) 
        {
            var result = await iFAQService.GetFAQInfoAsync(request);
            return Result.Success(result);
                 
        }

        /// <summary>
        /// 获取FAQ渠道列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<FaqChannelDTO>>> GetFaqChannelListAsync() 
        {
            var result = await iFAQService.GetFaqChannelListAsync();
            return Result.Success(result);
        }

        /// <summary>
        /// 查询FAQ分类列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<FaqCategoryDTO>>> GetFaqCategoryListAsync(GetFaqCategoryListByIdRequest request) 
        {
            var result = await iFAQService.GetFaqCategoryListAsync(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 新增/修改FAQ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> ModifyFAQAsync([FromBody] ApiRequest<ModifyFAQRequest> request)
        {
            //logger.Info($"修改FAQ data: {JsonConvert.SerializeObject(request)}");

            request.Data.CreateBy = _identityService.GetUserName();
            request.Data.UpdateBy = _identityService.GetUserName();
          
            return await iFAQService.ModifyFAQAsync(request.Data);
        }
        /// <summary>
        /// 删除FAQ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> DeleteFAQByIdAsync([FromBody] ApiRequest<GetFAQInfoRequest> request) 
        {
            //logger.Info($"删除FAQ data: {JsonConvert.SerializeObject(request)}");
            return await iFAQService.DeleteFAQByIdAsync(request.Data);
        }
    }
}
