using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Dal.Model;

namespace Ae.Shop.Api.Imp.Mappers.Stock
{
    public class OrderStockProfile : Profile
    {
        public OrderStockProfile()
        {
            CreateMap<OccupyItemDO, OccupyItemDTO>();

            CreateMap<OccupyItemDTO, OccupyItemDO>();

            CreateMap<OccupyQueueDTO, OccupyQueueDO>();

            CreateMap<OccupyQueueDO, OccupyQueueDTO>();

            CreateMap<PagedEntity<OccupyQueueDO>, ApiPagedResultData<OccupyQueueDTO>>();
            CreateMap<WarehouseTransferPackageDTO, WarehouseTransferPackageDO>();
            CreateMap<WarehouseTransferPackageDO, WarehouseTransferPackageDTO>();

            CreateMap<OccupyStockLogDO, OccupyStockLogDTO>();
        }
    }
}
