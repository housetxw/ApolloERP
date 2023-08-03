using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.OrderComment.Service.Core.Model;
using Ae.OrderComment.Service.Dal.Model;
using Ae.OrderComment.Service.Core.Request;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Core.Response;

namespace Ae.OrderComment.Service.Imp.Mappers
{
   public class OrderCommentProfile:Profile
    {
        public OrderCommentProfile()
        {
            CreateMap<GetOrderCommentForProductDO, GetOrderCommentForProductDTO>();

            CreateMap<PagedEntity<GetOrderCommentForProductDO>, ApiPagedResultData<GetOrderCommentForProductDTO>>();

            CreateMap<GetOrderCommentForShopDO, GetOrderCommentForShopDTO>();

            CreateMap<PagedEntity<GetOrderCommentForShopDO>, ApiPagedResultData<GetOrderCommentForShopDTO>>();

            CreateMap<GetOrderCommentForTechDO, GetOrderCommentForTechDTO>();

            CreateMap<PagedEntity<GetOrderCommentForTechDO>, ApiPagedResultData<GetOrderCommentForTechDTO>>();

            CreateMap<CommentImageDO, CommentImageDTO>();

            CreateMap<GetOrderCommentForReplyDO, GetOrderCommentForReplyDTO>();

            CreateMap<PagedEntity<GetOrderCommentForReplyDO>, ApiPagedResultData<GetOrderCommentForReplyDTO>>();
            
            CreateMap<ProductCommentTotalDO, ProductCommentTotalDTO>();

            CreateMap<CommentLabelConfigDO, GetCommentLabelsResponse>();

            CreateMap<CommentDO, GetMyCommentListResponse>().ForMember(dest => dest.CommentId, opt => opt.MapFrom(src => src.Id));
            CreateMap<PagedEntity<CommentDO>, ApiPagedResultData<GetMyCommentListResponse>>();
            CreateMap<CommentLabelTotalDO, BaseCommentLabelListResponse>();
            CreateMap<PagedEntity<CommentLabelTotalDO>, ApiPagedResultData<BaseCommentLabelListResponse>>();
            CreateMap<BaseCommentListDO, BaseCommentListResponse>();
            CreateMap<PagedEntity<BaseCommentListDO>, ApiPagedResultData<BaseCommentListResponse>>();
        }
    }
}
