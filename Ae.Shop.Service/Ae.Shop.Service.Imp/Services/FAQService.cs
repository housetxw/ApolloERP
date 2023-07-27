using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Imp.Services
{
    public class FAQService : IFAQService
    {
        private readonly IFAQRepository _iFAQRepository;
        private readonly IMapper _mapper;

        public FAQService(
            IFAQRepository iFAQRepository,
            IMapper mapper
            ) 
        {
            _iFAQRepository = iFAQRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 问答列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<FaqDTO>> GetFAQListAsync(GetFAQListRequest request) 
        {
            var result = await _iFAQRepository.GetFAQListAsync(request);
            return _mapper.Map<ApiPagedResultData<FaqDTO>>(result);
        }
        /// <summary>
        /// 查询FAQ详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FaqDTO> GetFAQInfoAsync(GetFAQInfoRequest request) 
        {
            var faqInfo = await _iFAQRepository.GetAsync(request.FAQId);
            return _mapper.Map<FaqDTO>(faqInfo);
        }
        /// <summary>
        /// 获取FAQ渠道列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<FaqChannelDTO>> GetFaqChannelListAsync() 
        {
            var list = await _iFAQRepository.GetFaqChannelListAsync();
            return _mapper.Map<List<FaqChannelDTO>>(list);
        }
        /// <summary>
        /// 查询FAQ分类列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<FaqCategoryDTO>> GetFaqCategoryListAsync(GetFaqCategoryListByIdRequest request) 
        {
            var list = await _iFAQRepository.GetFaqCategoryListAsync(request.CategoryId);
            return _mapper.Map<List<FaqCategoryDTO>>(list);
        }

        
        /// <summary>
        /// 新增/修改FAQ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> ModifyFAQAsync(ModifyFAQRequest request)
        {
            var id = 0;
            if (request.Id > 0)
            {
                var faqInfo = await _iFAQRepository.GetAsync(request.Id);
                if (faqInfo != null)
                {
                    faqInfo.ChannelId = request.ChannelId;
                    faqInfo.CategoryOneId = request.CategoryOneId;
                    faqInfo.CategoryTwoId = request.CategoryTwoId;
                    faqInfo.CategoryThreeId = request.CategoryThreeId;
                    faqInfo.Question = request.Question;
                    faqInfo.Answer = request.Answer;
                   // faqInfo.UpdateBy = "";//todo
                    faqInfo.UpdateTime = DateTime.Now;
                    id = await _iFAQRepository.UpdateAsync(faqInfo, new[] { "ChannelId", "CategoryOneId", "CategoryTwoId", "CategoryThreeId", "Question", "Answer", "UpdateBy", "UpdateTime" });
                }
            }
            else 
            {
                var faqInfo = _mapper.Map<FaqDO>(request);
                faqInfo.CreateTime = DateTime.Now;
                faqInfo.UpdateTime = DateTime.Now;
                //faqInfo.CreateBy = "SYSTEM";//todo
                id = await _iFAQRepository.InsertAsync(faqInfo);
            }
            if (id > 0)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess
                };
            }
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.FriendlyMistake
            };
        }
        /// <summary>
        /// 删除FAQ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteFAQByIdAsync(GetFAQInfoRequest request)
        {
            var faqInfo = await _iFAQRepository.GetAsync(request.FAQId);
            if (faqInfo != null) 
            {
                faqInfo.IsDeleted = 1;
                var num = await _iFAQRepository.UpdateAsync(faqInfo,new[] { "IsDeleted" });
                if (num > 0)
                {
                    return new ApiResult()
                    {
                        Code = ResultCode.Success,
                        Message = CommonConstant.DeleteSuccess
                    };
                }
                else 
                {
                    return new ApiResult()
                    {
                        Code = ResultCode.Failed,
                        Message = CommonConstant.DeleteFailed
                    };
                }
            }
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.FriendlyMistake
            };
        }
    }
}
