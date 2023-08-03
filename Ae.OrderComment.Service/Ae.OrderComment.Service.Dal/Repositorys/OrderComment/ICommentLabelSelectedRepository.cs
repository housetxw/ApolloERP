using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public interface ICommentLabelSelectedRepository : IRepository<CommentLabelSelectedDO>
    {
        Task<PagedEntity<CommentLabelTotalDO>> GetProductCommentLabelList(GetProductCommentListRequest request);
        Task<PagedEntity<CommentLabelTotalDO>> GetShopCommentLabelList(GetShopCommentListRequest request);

        Task<List<ShopLabelStatisticsDo>> GetShopLabel(long shopId);
    }
}
