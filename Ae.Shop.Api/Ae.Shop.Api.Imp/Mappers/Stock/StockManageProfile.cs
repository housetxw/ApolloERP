using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Response;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Dal.Model.WMS;

namespace Ae.Shop.Api.Imp.Mappers.Stock
{
    public class StockManageProfile : Profile
    {
        public StockManageProfile()
        {
            CreateMap<InventoryDO, InventoryDTO>();

            CreateMap<PagedEntity<InventoryDO>, ApiPagedResultData<InventoryDTO>>();

            CreateMap<InventoryBatchFlowDO, InventoryBatchFlowDTO>();

            CreateMap<PagedEntity<InventoryBatchFlowDO>, ApiPagedResultData<InventoryBatchFlowDTO>>();

            CreateMap<StockInOutTempDO, StockInOutDTO>();

            CreateMap<PagedEntity<StockInOutTempDO>, ApiPagedResultData<StockInOutDTO>>();

            CreateMap<FctShopProductDO, FctShopProductDTO>();
                
            CreateMap<InventoryBatchDO, InventoryBatchDTO>();

            CreateMap<StockInoutItemDO, StockInoutItemDTO>();

            CreateMap<StockInoutDO, StockInOutDTO>();

            CreateMap<PagedEntity<StoragePlanDO>, ApiPagedResultData<StoragePlanDTO>>();

            CreateMap<StoragePlanDO, StoragePlanDTO>();

            CreateMap<StoragePlanProductDO, StoragePlanProductDTO>();

            CreateMap<PagedEntity<StoragePlanProductDO>, ApiPagedResultData<StoragePlanProductDTO>>();

            CreateMap<StockDiffDO, StockDiffDTO>();

            CreateMap<PagedEntity<StockDiffDO>, ApiPagedResultData<StockDiffDTO>>();

            CreateMap<StockDiffDO, StockDiffDTO>();

            CreateMap<InventoryExceptionFileDO, InventoryExceptionFileDTO>();

            CreateMap<InventoryExceptionFileDTO, InventoryExceptionFileDO>();

            CreateMap<InventoryExceptionRecordDO, InventoryExceptionRecordDTO>();

            CreateMap<InventoryExceptionRecordDTO, InventoryExceptionRecordDO>();

            CreateMap<SignDetailDO, GetTodayReceivePackageDTO>();

            CreateMap<PagedEntity<SignDetailDO>, TodaySignPackageApiPagedResult<GetTodayReceivePackageDTO>>();
            CreateMap<InventoryFlowItemDO, InventoryFlowItemDTO>();
            CreateMap<ShopWmsLogDO, ShopWmsLogDTO>();

            CreateMap<AllotTaskDO, AllotTaskDTO>();
            CreateMap<PagedEntity<AllotTaskDO>, ApiPagedResultData<AllotTaskDTO>>();

            CreateMap<AllotProductDO, AllotProductDTO>();


            CreateMap<ProductStockDTO, ProductStockResponse>()
                .ForMember(dest => dest.AvailableNum, opt => opt.MapFrom(o => o.ActualNum))
                .ForMember(dest => dest.StockUnit, opt => opt.MapFrom(o => o.UomText));

            CreateMap<List<ProductStockDTO>, List<ProductStockResponse>>();
            CreateMap< ApiResult<List<ProductStockDTO>>, ApiResult<List<ProductStockResponse>>>();
        }
    }
}
