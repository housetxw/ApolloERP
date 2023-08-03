using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Ae.OrderComment.Service.Core.Model.OrderCommentForApp;
using Ae.OrderComment.Service.Core.Response.OrderCommentForApp;
using Ae.OrderComment.Service.Dal.Model;

namespace Ae.OrderComment.Service.Imp.Mappers
{
    /// <summary>
    /// 订单评论ForApp
    /// </summary>
    public class OrderCommentForApp:Profile
    {
        public OrderCommentForApp()
        {
            CreateMap<GetCommentListDO, GetCommentListVO>();

            CreateMap<GetCommentListDO, ReplyDetailResponse>();

            
        }
    }
}
