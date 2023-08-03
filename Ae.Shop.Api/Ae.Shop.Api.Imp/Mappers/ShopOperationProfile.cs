using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Response;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Imp.Mappers
{
    public class ShopOperationProfile : Profile
    {
        public ShopOperationProfile()
        {
            CreateMap<GetOrderCommentBaseRequest, GetOrderCommentBaseClientRequest>();
            CreateMap<ApiRequest<GetOrderCommentBaseRequest>, ApiRequest<GetOrderCommentBaseClientRequest>>();
            CreateMap<GetOrderCommentBaseDTO, GetOrderCommentBaseVO>();
            CreateMap<GetOrderCommentForShopDTO, GetOrderCommentForShopVO>();

            CreateMap<ReplyCommentRequest, ReplyCommentClientRequest>();
            CreateMap<ReplyDetailClientResponse, ReplyDetailResponse>();
            CreateMap<ImgDTO, ImgVO>();
            CreateMap<CommentListReplyInfoDTO, CommentListReplyInfoVO>();
            CreateMap<ReplyDetailRequest, ReplyDetailClientRequest>();

            CreateMap<GetCommentListRequest, GetCommentListClientRequest>();
            CreateMap<GetCommentListClientResponse, GetCommentListResponse>();
            CreateMap<CommentSelectDTO, CommentSelectVO>();
            CreateMap<GetCommentListDTO, GetCommentListVO>();
            CreateMap<CommentListReplyInfoDTO, CommentListReplyInfoVO>();
            CreateMap<UserOperationDTO, UserOperationVO>();
        }
    }
}
