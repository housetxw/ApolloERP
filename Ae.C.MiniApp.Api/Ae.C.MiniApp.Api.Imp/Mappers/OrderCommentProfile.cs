using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.OrderComment;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Common.Util;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Imp.Mappers
{
    public class OrderCommentProfile : Profile
    {
        public OrderCommentProfile()
        {
            CreateMap<OrderCommentRequest, OrderCommentClientRequest>();
            CreateMap<OrderCommentClientResponse, OrderCommentResponse>();
            CreateMap<ApiResult<OrderCommentClientResponse>, ApiResult<OrderCommentResponse>>();
            CreateMap<OrderCommentDetailTechFormDTO, OrderCommentDetailTechFormVO>();
            CreateMap<OrderCommentDetailShopFormDTO, OrderCommentDetailShopFormVO>();
            CreateMap<OrderCommentDetailProductFormDTO, OrderCommentDetailProductFormVO>()
                .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.ProductImageUrl.AddImageDomain()));//图片绝对路径
            CreateMap<GetCommentLabelsRequest, GetCommentLabelsClientRequest>();
            CreateMap<GetCommentLabelsClientResponse, GetCommentLabelsResponse>();
            CreateMap<ApiResult<List<GetCommentLabelsClientResponse>>, ApiResult<List<GetCommentLabelsResponse>>>();
            CreateMap<SubmitOrderCommentRequest, SubmitOrderCommentClientRequest>();
            CreateMap<ApiRequest<SubmitOrderCommentRequest>, ApiRequest<SubmitOrderCommentClientRequest>>();
            CreateMap<OrderCommentDetailTechSubmitVO, OrderCommentDetailTechSubmitDTO>();
            CreateMap<OrderCommentDetailShopSubmitVO, OrderCommentDetailShopSubmitDTO>();
            CreateMap<OrderCommentDetailProductSubmitVO, OrderCommentDetailProductSubmitDTO>();
            CreateMap<BaseOrderCommentSubmitVO, BaseOrderCommentSubmitDTO>();
            CreateMap<BaseOrderOperationCondition, BaseOrderOperationConditionDTO>();
            CreateMap<UserIdRequest, GetMyCommentListClientRequest>();
            CreateMap<GetMyCommentListClientResponse, GetMyCommentListResponse>();
            CreateMap<ApiPagedResultData<GetMyCommentListClientResponse>, ApiPagedResultData<GetMyCommentListResponse>>();
            CreateMap<CommentProductOrPackageSimpleDTO, CommentProductOrPackageSimpleVO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl.AddImageDomain()));//图片绝对路径
            CreateMap<CommentReplyButtonDTO, CommentReplyButtonVO>();
            CreateMap<BaseCommentListClientResponse, BaseCommentListResponse>();
            CreateMap<CommentListReplyInfoDTO, CommentListReplyInfoVO>();
            CreateMap<LikeCommentRequest, LikeCommentClietRequest>();
            CreateMap<BaseCommentNumAndApplauseRateClientResponse, BaseCommentNumAndApplauseRateResponse>();
            CreateMap<BaseCommentLabelListClientResponse, BaseCommentLabelListResponse>();
            CreateMap<ApiPagedResultData<BaseCommentLabelListClientResponse>, ApiPagedResultData<BaseCommentLabelListResponse>>().ReverseMap();
            CreateMap<ApiPagedResult<BaseCommentLabelListClientResponse>, ApiPagedResult<BaseCommentLabelListResponse>>().ReverseMap();
            CreateMap<BaseGetProductCommentRequest, BaseGetProductCommentClientRequest>();
            CreateMap<GetProductCommentListRequest, GetProductCommentListClientRequest>();
            CreateMap<ApiPagedResultData<BaseCommentListClientResponse>, ApiPagedResultData<BaseCommentListResponse>>().ReverseMap();
            CreateMap<ApiPagedResult<BaseCommentListClientResponse>, ApiPagedResult<BaseCommentListResponse>>().ReverseMap();
            CreateMap<BaseGetShopCommentRequest, BaseGetShopCommentClientRequest>();
            CreateMap<GetShopCommentListRequest, GetShopCommentListClientRequest>();
            CreateMap<BaseGetTechCommentRequest, BaseGetTechCommentClientRequest>();
            CreateMap<GetTechCommentListRequest, GetTechCommentListClientRequest>();

            CreateMap<AppendCommentRequest, AppendCommentClientRequest>();
            CreateMap<AppendCommentClientResponse, AppendCommentResponse>();
            CreateMap<ApiResult<AppendCommentClientResponse>, ApiResult<AppendCommentResponse>>();
            CreateMap<SubmitAppendCommentRequest, SubmitAppendCommentClientRequest>();
        }
    }
}
