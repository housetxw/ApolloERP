using AutoMapper;
using Ae.Order.Service.Core.Model;
using Ae.Order.Service.Core.Model.Order;
using Ae.Order.Service.Core.Model.Vehicle;
using Ae.Order.Service.Core.Request.Order;
using Ae.Order.Service.Core.Response;
using Ae.Order.Service.Core.Response.Order;
using Ae.Order.Service.Dal.Model;
using Ae.Order.Service.Dal.Model.Order;

namespace Ae.Order.Service.Imp.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDO, OrderDTO>().ReverseMap();
            CreateMap<OrderProductDTO, OrderProductDO>().ReverseMap();
            CreateMap<OrderProductDO, OrderDetailProductDTO>();
            CreateMap<OrderProductDO, UninstalledOrderProductDTO>();
            CreateMap<GetSalesByPidsDO, GetSalesByPidsResponse>();
            CreateMap<GetAccountCheckAmountDO, GetAccountCheckAmountResponse>().ReverseMap();
            CreateMap<GetOrderListForBossDO, GetOrderListForBossResponse>();
            CreateMap<BatchGetOrderCountByShopIdDO, BatchGetOrderCountByShopIdResponse>();
            CreateMap<GetOrdersByUserIdResponse, OrderDO>().ReverseMap();

            CreateMap<OrderAddressDO, OrderAddressDTO>().ReverseMap();


            CreateMap<UserVehicleDTO, OrderCarDO>();
        }
    }
}
