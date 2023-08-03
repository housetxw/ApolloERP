using AutoMapper;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Clients.ReserveServer;
using Ae.Shop.Api.Client.Clients.VehicleServer;
using Ae.Shop.Api.Client.Request.Reserve;
using Ae.Shop.Api.Client.Request.ShopManage;
using Ae.Shop.Api.Client.Request.Vehicle;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Reserve;
using Ae.Shop.Api.Core.Response.Reserve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class ReserveService: IReserveService
    {
        private readonly IReserveClient reserveClient;
        private readonly IShopMangeClient shopMangeClient;
        private readonly IIdentityService identityService;
        private readonly IVehicleClient vehicleClient;
        private readonly IMapper mapper;

        public ReserveService(IReserveClient reserveClient, IIdentityService identityService, IMapper mapper,
            IShopMangeClient shopMangeClient, IVehicleClient vehicleClient)
        {
            this.reserveClient = reserveClient;
            this.identityService = identityService;
            this.mapper = mapper;
            this.shopMangeClient = shopMangeClient;
            this.vehicleClient = vehicleClient;
        }

        /// <summary>
        /// 预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopReserveListVo>> GetReserveListPage(ReserveListPageRequest request)
        {
            ApiPagedResultData<ShopReserveListVo> result = new ApiPagedResultData<ShopReserveListVo>();

            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            var data = await reserveClient.GetReserveListPage(new ReserveListPageClientRequest
            {
                ShopId = shopId,
                UserTel = request.UserTel,
                CarPlate = request.CarPlate,
                OrderNo = request.OrderNo,
                ReserveType = request.ReserveType,
                ReserveChannel = request.ReserveChannel,
                Status = request.Status,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ReserveTech = request.ReserveTech,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            if (data != null)
            {
                result.TotalItems = data.TotalItems;
                if (data.Items != null)
                {
                    result.Items = mapper.Map<List<ShopReserveListVo>>(data.Items);
                }
            }
            return result;
        }

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReserveDetailForWebVo> GetReserveDetailForWeb(ReserveDetailForWebRequest request)
        {
            var result = await reserveClient.GetReserveDetailForWeb(new ReserveDetailForWebClientRequest
            {
                ReserveId = request.ReserveId
            });
            if (result != null)
            {
                return mapper.Map<ReserveDetailForWebVo>(result);
            }
            return null;
        }

        /// <summary>
        /// 获取门店开启的服务类别
        /// </summary>
        /// <returns></returns>
        public async Task<List<StatusEnum>> GetShopServiceType()
        {
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);

            var result = await shopMangeClient.GetShopServiceTypeAsync(new ShopServiceTypeClientRequest
            {
                ShopId = shopId
            });

            return result?.Select(_ => new StatusEnum
            {
                Value = _.ServiceType,
                DisplayName = _.DisplayName
            })?.ToList() ?? new List<StatusEnum>();
        }

        /// <summary>
        /// 预约技师列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReserveTechnicianVo>> GetReserveTechnician()
        {
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            var result = await shopMangeClient.GetShopTechnicianList(shopId, identityService.GetUserId());
            return result?.Select(_ => new ReserveTechnicianVo
            {
                Id = _.Id,
                Name = _.Name
            })?.ToList() ?? new List<ReserveTechnicianVo>();
        }

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        public async Task<ReserveDateForWebResponse> GetReserveDateForWeb()
        {
            ReserveDateForWebResponse data = new ReserveDateForWebResponse();

            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);

            var result = await reserveClient.GetReserveDateForWeb(new ReserveDateClientRequest
            {
                ShopId = shopId
            });

            if (result != null)
            {
                data.ReserveDateInfoList = mapper.Map<List<ReserveDateVo>>(result);
            }

            data.DatePartList = data.ReserveDateInfoList.Select(_ => _.Date).ToList();

            data.TimePartList = data.ReserveDateInfoList.FirstOrDefault()?.Items?.Select(t => t.DatePart)?.ToList() ?? new List<string>();

            return data;
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CancelReserve(CancelReserveRequest request)
        {
            var result = await reserveClient.CancelReserveV2(new CancelReserveClientRequest()
            {
                ReserveId = request.ReserveId,
                CancelReason = request.CancelReason,
                UpdateBy = identityService.GetUserName()
            });
            return result;
        }

        /// <summary>
        /// 编辑预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditReserve(EditReserveRequest request)
        {
            var clientRequest = mapper.Map<EditReserveClientRequest>(request);

            clientRequest.SubmitBy = identityService.GetUserName();

            var result = await reserveClient.EditReserve(clientRequest);

            return result;
        }

        /// <summary>
        /// 根据手机号查询用户有效预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveListVo>> GetValidReserve(ValidReserveRequest request)
        {
            List<ReserveListVo> reserveList = new List<ReserveListVo>();
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            var result = await reserveClient.GetValidReserve(new ValidReserveClientReserve()
            {
                UserTel = request.UserTel,
                ShopId = shopId
            });
            if (result != null)
            {
                reserveList = mapper.Map<List<ReserveListVo>>(result);
            }

            return reserveList;
        }

        /// <summary>
        /// 添加预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> AddReserve(AddReserveRequest request)
        {
            Int32.TryParse(identityService.GetOrganizationId(), out var shopId);
            var clientRequest = mapper.Map<AddReserveClientRequest>(request);
            clientRequest.SubmitBy = identityService.GetUserName();
            clientRequest.ShopId = shopId;
            var result = await reserveClient.AddReserve(clientRequest);
            return result;
        }
    }
}
