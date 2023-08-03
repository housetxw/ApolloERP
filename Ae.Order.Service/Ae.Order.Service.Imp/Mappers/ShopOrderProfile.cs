using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Model.ShopOrder;
using Ae.Order.Service.Core.Request.ShopOrder;
using Ae.Order.Service.Dal.Condition.ShopOrder;
using Ae.Order.Service.Dal.Model;

namespace Ae.Order.Service.Imp.Mappers
{
    public class ShopOrderProfile : Profile
    {
        public ShopOrderProfile()
        {
            CreateMap<GetOrdersForSearchRequest, GetMainOrdersForSearchCondition>();

            CreateMap<PagedEntity<OrderDO>, ApiPagedResultData<OrderDTO>>();

            CreateMap<OrderProductDO, OrderProductDTO>();

            CreateMap<ApiPagedResultData<OrderDTO>, ApiPagedResultData<GetOrdersForSearchDTO>>();

            CreateMap<OrderDTO, GetOrdersForSearchDTO>();

            CreateMap<OrderProductDTO, GetOrdersProductDTO>();

            CreateMap<GetOrdersForStatusRequest, GetMainOrdersForStatusCondition>();

            CreateMap<ApiPagedResultData<OrderDTO>, ApiPagedResultData<GetOrdersForStatusDTO>>();

            CreateMap<OrderDTO, GetOrdersForStatusDTO>();
        }
    }
}
