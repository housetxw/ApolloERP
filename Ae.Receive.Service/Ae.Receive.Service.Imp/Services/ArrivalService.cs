using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Clients;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Core.Interfaces;
using Ae.Receive.Service.Core.Request.Arrival;
using Ae.Receive.Service.Core.Response.Arrival;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Receive.Service.Dal.Repositorys;
using Microsoft.AspNetCore.Mvc;
using Ae.Receive.Service.Common.Constant;
using Ae.Receive.Service.Common.Extension;
using Ae.Receive.Service.Core.Enums;
using System.Linq;
using Ae.Receive.Service.Core.Model.Arrival;
using AutoMapper;
using Ae.Receive.Service.Dal.Model;
using ApolloErp.Common.BrandLogoHelper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ae.Receive.Service.Dal.Model.Arrival;
using Ae.Receive.Service.Client.Request.Order;
using Ae.Receive.Service.Common.Exceptions;
using Ae.Receive.Service.Core.Response;
using Ae.Receive.Service.Client.Enum;
using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Client.Response;
using NLog.Web.LayoutRenderers;
using Ae.Receive.Service.Core.Enums;
using Org.BouncyCastle.Ocsp;

namespace Ae.Receive.Service.Imp.Services
{
    /// <summary>
    /// 到店记录服务实现
    /// </summary>
    public class ArrivalService : IArrivalService
    {
        private readonly ApolloErpLogger<ArrivalService> _logger;
        private readonly IUserClient _userClient;
        private readonly IVehicleClient _vehicleClient;
        private readonly IShopManageClient _shopManageClient;
        private readonly IShopReserveService _shopReserveService;
        private readonly IArrivalRepository _arrivalRepo;
        private readonly IMapper _mapper;
        private readonly IReverseClient _reverseClient;
        private readonly IOrderClient _orderClient;
        private readonly IShopReserveOrderRepository _shopReserveOrderRepository;
        private readonly IConfiguration _configuration;
        private readonly IShopReserveRepository _shopReserveRepository;
        private readonly IReservePictureRepository _reservePictureRepository;
        private readonly IShopArrivalOrderRepository _shopArrivalOrderRepository;
        private readonly IShopArrivalCancelRepository _shopArrivalCancelRepository;
        private readonly IReceiveCheckService _receiveCheckService;
        private readonly IShopArrivalCarReportRepository _shopArrivalCarReportRepository;
        private readonly IShopArrivalVideoRepository _shopArrivalVideoRepository;

        public ArrivalService(ApolloErpLogger<ArrivalService> logger, IUserClient userClient, IVehicleClient vehicleClient,
            IShopManageClient shopManageClient, IShopReserveService shopReserveService,
            IArrivalRepository arrivalRepository, IMapper mapper,
            IReverseClient reverseClient, IOrderClient orderClient, IShopReserveOrderRepository shopReserveOrderRepository,
            IConfiguration configuration, IShopReserveRepository shopReserveRepository, IReservePictureRepository reservePictureRepository,
            IShopArrivalOrderRepository shopArrivalOrderRepository,
            IShopArrivalCancelRepository shopArrivalCancelRepository,
            IReceiveCheckService receiveCheckService, IShopArrivalCarReportRepository shopArrivalCarReportRepository, IShopArrivalVideoRepository shopArrivalVideoRepository)
        {
            _logger = logger;
            _userClient = userClient;
            _vehicleClient = vehicleClient;
            _shopManageClient = shopManageClient;
            _shopReserveService = shopReserveService;
            _arrivalRepo = arrivalRepository;
            _mapper = mapper;
            _reverseClient = reverseClient;
            _orderClient = orderClient;
            _shopReserveOrderRepository = shopReserveOrderRepository;
            _configuration = configuration;
            _shopReserveRepository = shopReserveRepository;
            _reservePictureRepository = reservePictureRepository;
            _shopArrivalOrderRepository = shopArrivalOrderRepository;
            _shopArrivalCancelRepository = shopArrivalCancelRepository;
            _receiveCheckService = receiveCheckService;
            _shopArrivalCarReportRepository = shopArrivalCarReportRepository;
            _shopArrivalVideoRepository = shopArrivalVideoRepository;
        }

        /// <summary>
        /// 快速排队接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<QuickQueueResponse>> QuickQueue(QuickQueueRequest request)
        {
            var userInfo = await _userClient.GetUserInfo(new Client.Request.GetUserInfoClientRequest()
            {
                UserId = request.UserId
            });

            var carInfo = await _vehicleClient.GetUserDefaultVehicleAsync(new Client.Request.Vehicle.UserDefaultVehicleClientRequest()
            {
                UserId = request.UserId
            });

            var servicesType = await _shopManageClient.GetShopServiceTypeAsync(new Client.Request.Shop.GetShopServiceTypeAsyncRequest()
            {
                ShopId = request.ShopId
            });

            var serviceTypeSelected = new List<KeyValuePair<string, string>>();

            servicesType?.ForEach(_ =>
            {
                serviceTypeSelected.Add(new KeyValuePair<string, string>(_.ServiceType, _.DisplayName));
            });
            var userName = !string.IsNullOrEmpty(userInfo?.UserName) ? userInfo?.UserName :
                !string.IsNullOrEmpty(userInfo?.NickName) ? userInfo?.NickName : "System";

           

            await _shopManageClient.AddShopUserRelation(new Client.Request.AddShopUserRelationRequest()
            {
                UserId = request.UserId,
                ShopId = request.ShopId,
                SubmitBy = userName,
                ReferrerUserId = string.Empty,
                ReferrerShopId = request.ShopId,
                ReferrerType = "ShopCode",
                Channel = "None"
            });

            var shopInfo = await _shopManageClient.GetShopSimpleInfo(new GetShopSimpleInfoClientRequest()
            {
                ShopId = request.ShopId
            });

            return new ApiResult<QuickQueueResponse>()
            {
                Code = ResultCode.Success,
                Data = new QuickQueueResponse()
                {
                    Telephone = userInfo?.UserTel,
                    UserName = userName,
                    ShopName = shopInfo?.SimpleName,
                    Vehicle = new Core.Model.Arrival.VehicleVo()
                    {
                        CarId = carInfo?.CarId,
                        CarNo = carInfo?.CarNumber,
                        Brand = carInfo?.Brand,
                        Vehicle = carInfo?.Vehicle,
                        CarBrandIcon = string.Empty
                    },
                    ServiceTypeSelected = serviceTypeSelected
                }
            };
        }

        /// <summary>
        /// 快速排队请求弹层
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumberLayer(ApiRequest<QuickTakeNumberLayerRequest> request)
        {
            long arrivalRecordId = 0;
            var queueData = await _arrivalRepo.GetShopArrivalForQueue(request?.Data?.UserId, request?.Data?.Vechicle?.CarId, request.Data?.ShopId ?? 0);
            if (queueData)
            {
                throw new CustomException("当前排队号已经生成，不允许重复排队拿号！");
            }
            var reserverInfo = await _shopReserveService.GetSameDayReserveSimpleInfo(new Core.Request.BaseUserRequest()
            {
                UserId = request.Data?.UserId,
                ShopId = request.Data?.ShopId ?? 0,
                CarId = request.Data?.Vechicle?.CarId
            });
            if (reserverInfo?.ReserveId > 0)
            {
                if (reserverInfo?.ReserveTime >= DateTime.Now)
                {
                    return new ApiResult<QuickTakeNumberLayerResponse>()
                    {
                        Code = ResultCode.Success,
                        Data = new QuickTakeNumberLayerResponse()
                        {
                            QuickTakeNumberType = Core.Enums.QuickTakeNumberEnum.Appointment,
                            //  Content = $"根据预约号【{reserverInfo?.ReserveId}】领号"
                            Content = $"{reserverInfo?.ReserveId}"
                        }
                    };
                }
            }
            var generatorNumber = await TakeQueueNumber(request?.Data?.UserId, request.Data.ShopId, QueueTypeEnum.ArrivalQueue);
            if (generatorNumber == 1)
            {
                await _arrivalRepo.SaveQuqueNumberGenerator(new Dal.Model.Arrival.QuqueNumberGeneratorDO()
                {
                    ShopId = request?.Data?.ShopId ?? 0,
                    GeneratorType = (sbyte)QueueTypeEnum.ArrivalQueue.ToInt(),
                    GeneratorDate = DateTime.Now,
                    CreateBy = "System",
                    UpdateBy = "System",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CurrentNumber = 1
                });
            }

            var userInfo = await _userClient.GetUserInfo(new Client.Request.GetUserInfoClientRequest()
            {
                UserId = request?.Data?.UserId
            });

            var shopInfo = await _shopManageClient.GetShopSimpleInfo(new Client.Request.GetShopSimpleInfoClientRequest()
            {
                ShopId = request.Data.ShopId
            });
            var userName = !string.IsNullOrEmpty(userInfo?.UserName) ? userInfo?.UserName :
                       !string.IsNullOrEmpty(userInfo?.NickName) ? userInfo?.NickName : "System";
            long reserverId = reserverInfo?.ReserveId ?? 0;
            arrivalRecordId = await _arrivalRepo.SaveArrival(new Dal.Model.ShopArrivalDO()
            {
                ArrivalTime = DateTime.Now,
                Status = (int)ArrivalRecordStatusEnum.Waiting,
                UserId = request.Data.UserId,
                UserName = userName,
                UserTel = userInfo?.UserTel,
                CarId = request.Data.Vechicle?.CarId,
                CarNo = request.Data?.Vechicle?.CarNo,
                Brand = request.Data?.Vechicle?.Brand,
                Vehicle = request.Data?.Vechicle?.Vehicle,
                ServiceType = request.Data?.ServiceType,
                ShopId = request.Data?.ShopId ?? 0,
                ShopName = shopInfo?.SimpleName ?? string.Empty,
                Remark = request.Data?.Remark,
                ReserveNo = (int)reserverId,
                QueueNumber = $"D{request?.Data?.ShopId}-{generatorNumber}",
                QueueType = QueueTypeEnum.ArrivalQueue.GetDescription(),
                CreateBy = userName,
                UpdateBy = userName,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            });

            bool updateLastReceiveTIme = await _shopManageClient.UpdateUserLastReceiveTime(new Client.Request.Shop.UpdateUserLastReceiveTimeRequest()
            {
                ArriveId = arrivalRecordId,
                ArriveTime = DateTime.Now.ToString(),
                ShopId = request?.Data?.ShopId ?? 0,
                UserId = request?.Data?.UserId,
                SubmitBy = userName
            });

            if (reserverInfo?.ReserveId > 0)
            {
                ///更改预约完结状态
                await _shopReserveService.UpdateReserveStatus(new Core.Request.UpdateReserveStatusRequest()
                {
                    ReserveId = reserverInfo?.ReserveId ?? 0,
                    ReserveStatus = ReserveStatusEnum.Completed,
                    UpdateBy = userName
                });

                if (reserverInfo?.ReserveTime < DateTime.Now)
                {
                    var shopReserveOrder = await _shopReserveOrderRepository.GetReserveOrderByReserveId(reserverInfo?.ReserveId ?? 0);
                    if (shopReserveOrder?.Count > 0)
                    {
                        var orderProducts = await _orderClient.GetOrderProduct(new Client.Request.Order.OrderProductRequest()
                        {
                            OrderNos = shopReserveOrder?.Select(x => x.OrderNo)?.ToList()
                        });

                        var orderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
                        {
                            OrderNos = shopReserveOrder?.Select(x => x.OrderNo)?.ToList(),
                            ShopId = (int)request.Data.ShopId
                        });

                        List<ShopArrivalOrderDO> shopArrivals = new List<ShopArrivalOrderDO>();
                        orderProducts?.ForEach(_ =>
                        {
                            var orderType = (OrderTypeEnum)(orderBase?.Where(x => x.OrderNo == _.OrderNo)?.FirstOrDefault()?.OrderType ?? OrderTypeEnum.Other.ToInt());

                            shopArrivals.Add(new ShopArrivalOrderDO()
                            {
                                OrderNo = _.OrderNo,
                                Pid = _.ProductId,
                                ProductName = _.ProductName,
                                Price = _.Price,
                                ArrivalId = arrivalRecordId,
                                Num = (sbyte)_.TotalNumber,
                                OrderType = orderType.GetDescription().ToString(),
                                CreateBy = userName,
                                UpdateBy = userName,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                IsDeleted = 0
                            });
                        });
                        await _arrivalRepo.InsertBatchAsync<ShopArrivalOrderDO>(shopArrivals);

                        return new ApiResult<QuickTakeNumberLayerResponse>()
                        {
                            Code = ResultCode.Success,
                            Data = new QuickTakeNumberLayerResponse()
                            {
                                QuickTakeNumberType = Core.Enums.QuickTakeNumberEnum.AppointmentLater,
                                Content = $"排队成功\r\n 排队号:D{request?.Data?.ShopId}-{generatorNumber} \r\n 您有一条逾期预约记录，请留意"
                            }
                        };
                    }
                }

            }

            return new ApiResult<QuickTakeNumberLayerResponse>()
            {
                Code = ResultCode.Success,
                Data = new QuickTakeNumberLayerResponse()
                {
                    QuickTakeNumberType = Core.Enums.QuickTakeNumberEnum.Queue,
                    Content = $"排队成功 \r\n排队号:D{request?.Data?.ShopId}-{generatorNumber}"
                }
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="quickTakeNumberRequest"></param>
        /// <returns></returns>
        private async Task<long> TakeQueueNumber(string userId, long shopId, QueueTypeEnum queueTypeEnum)
        {
            var generatorNumber = await _arrivalRepo.GetQuqueNumberGenerator(new GetQuqueNumberGeneratorRequest()
            {
                ShopId = shopId,
                GeneratorType = queueTypeEnum.ToInt()
            });

            return generatorNumber;

        }

        public async Task<List<TodayArrivalShopStatisticsResDTO>> GetTodayArrivalShopStatistics(TodayArrivalShopStatisticsReqDTO req)
            => await _arrivalRepo.GetTodayArrivalShopStatistics(req);

        public async Task<List<MonthArrivalShopStatisticsResDTO>> GetMonthArrivalShopStatistics(MonthArrivalShopStatisticsReqDTO req)
        {
            req.StartTime = req.EndTime.GetSpecificDateTimeFromDay(-CommonConstant.TwentyNine);
            req.EndTime = req.EndTime.GetEndTimeOfDay();
            var resDto = await _arrivalRepo.GetMonthArrivalShopStatistics(req);
            var res = new List<MonthArrivalShopStatisticsResDTO>();

            if (resDto.Count != CommonConstant.Thirty)
            {
                var dtDic = resDto.Distinct().ToDictionary(d => d.Date, d => d);
                var dtList = req.EndTime.GetStartTimeOfDay().GetThirtyDayDateTimeList();

                dtList.ForEach(f => res.Add(dtDic.ContainsKey(f.Date) ? dtDic[f] : new MonthArrivalShopStatisticsResDTO
                {
                    Date = f,
                    Amount = CommonConstant.Zero
                }));
            }
            else
            {
                res.AddRange(resDto);
            }

            return res
                .OrderBy(o => o.Date)
                .ToList();
        }

        public async Task<List<NewCustomerArrivalShopResDTO>> GetNewCustomerArrivalShopStatistics(NewCustomerArrivalShopReqDTO req)
            => await _arrivalRepo.GetNewCustomerArrivalShopStatistics(req);

        public async Task<List<NewCustomerArrivalShopSaleroomResDTO>> GetNewCustomerArrivalShopSaleroomStatistics(NewCustomerArrivalShopSaleroomReqDTO req)
            => await _arrivalRepo.GetNewCustomerArrivalShopSaleroomStatistics(req);

        /// <summary>
        /// 排队仅拿号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> QuickTakeNumber(ApiRequest<QuickTakeNumberRequest> request)
        {
            long arrivalRecordId = 0;

            var generatorNumber = await TakeQueueNumber(request?.Data?.UserId, request.Data.ShopId, QueueTypeEnum.ArrivalQueue);
            if (generatorNumber == 1)
            {
                await _arrivalRepo.SaveQuqueNumberGenerator(new Dal.Model.Arrival.QuqueNumberGeneratorDO()
                {
                    ShopId = request?.Data?.ShopId ?? 0,
                    GeneratorType = (sbyte)QueueTypeEnum.ArrivalQueue.ToInt(),
                    GeneratorDate = DateTime.Now,
                    CreateBy = "System",
                    UpdateBy = "System",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CurrentNumber = 1
                });
            }

            var userInfo = await _userClient.GetUserInfo(new Client.Request.GetUserInfoClientRequest()
            {
                UserId = request?.Data?.UserId
            });
            var shopInfo = await _shopManageClient.GetShopSimpleInfo(new Client.Request.GetShopSimpleInfoClientRequest()
            {
                ShopId = request.Data.ShopId
            });
            var userName = !string.IsNullOrEmpty(userInfo?.UserName) ? userInfo?.UserName :
                     !string.IsNullOrEmpty(userInfo?.NickName) ? userInfo?.NickName : "System";
            arrivalRecordId = await _arrivalRepo.SaveArrival(new Dal.Model.ShopArrivalDO()
            {
                ArrivalTime = DateTime.Now,
                Status = (int)ArrivalRecordStatusEnum.Waiting,
                UserId = request.Data.UserId,
                UserName = userName,
                UserTel = userInfo?.UserTel,
                CarId = request.Data.Vechicle?.CarId,
                CarNo = request.Data?.Vechicle?.CarNo,
                Tid = request.Data?.Vechicle?.Tid,
                VehicleId = request.Data?.Vechicle?.VehicleId,
                Brand = request.Data?.Vechicle?.Brand,
                Vehicle = request.Data?.Vechicle?.Vehicle,
                PaiLiang = request.Data?.Vechicle?.PaiLiang,
                Nian = request.Data?.Vechicle?.Nian,
                SalesName = request.Data?.Vechicle?.SalesName,
                TotalMileage = request.Data?.Vechicle?.TotalMileage ?? default,
                CarProperties = JsonConvert.SerializeObject(request.Data?.Vechicle?.CarProperties),
                ServiceType = request.Data?.ServiceType,
                ShopId = request.Data?.ShopId ?? 0,
                ShopName = shopInfo?.SimpleName ?? string.Empty,
                Remark = request.Data?.Remark ?? string.Empty,
                ReserveNo = 0,
                QueueNumber = $"D{request?.Data?.ShopId}-{generatorNumber}",
                QueueType = QueueTypeEnum.ArrivalQueue.GetDescription(),
                CreateBy = userName,
                UpdateBy = userName,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            });


            await _shopManageClient.AddShopUserRelation(new Client.Request.AddShopUserRelationRequest()
            {
                UserId = request?.Data?.UserId,
                ShopId = request.Data.ShopId,
                SubmitBy = userName,
                ReferrerUserId = string.Empty,
                ReferrerShopId = (int)request.Data.ShopId,
                ReferrerType = "ShopCode",
                Channel = "None"
            });

            bool updateLastReceiveTIme = await _shopManageClient.UpdateUserLastReceiveTime(new Client.Request.Shop.UpdateUserLastReceiveTimeRequest()
            {
                ArriveId = arrivalRecordId,
                ArriveTime = DateTime.Now.ToString(),
                ShopId = request?.Data?.ShopId ?? 0,
                UserId = request?.Data?.UserId,
                SubmitBy = userName
            });

            return new ApiResult<QuickTakeNumberLayerResponse>()
            {
                Code = ResultCode.Success,
                Data = new QuickTakeNumberLayerResponse()
                {
                    QuickTakeNumberType = Core.Enums.QuickTakeNumberEnum.Queue,
                    Content = $"排队成功 \r\n 排队号:D{request?.Data?.ShopId}-{generatorNumber}",
                    ArrivalId = arrivalRecordId
                }
            };

        }

        /// <summary>
        /// 预约排队拿号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<QuickTakeNumberLayerResponse>> ReserveTakeNumber(ApiRequest<ReserveTakeNumberRequest> request)
        {
            long arrivalRecordId = 0;

            var generatorNumber = await TakeQueueNumber(request?.Data?.UserId, request.Data.ShopId, QueueTypeEnum.ReserverQueue);
            if (generatorNumber == 1)
            {
                await _arrivalRepo.SaveQuqueNumberGenerator(new Dal.Model.Arrival.QuqueNumberGeneratorDO()
                {
                    ShopId = request?.Data?.ShopId ?? 0,
                    GeneratorType = (sbyte)QueueTypeEnum.ReserverQueue.ToInt(),
                    GeneratorDate = DateTime.Now,
                    CreateBy = "System",
                    UpdateBy = "System",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CurrentNumber = 1
                });
            }

            var userInfo = await _userClient.GetUserInfo(new Client.Request.GetUserInfoClientRequest()
            {
                UserId = request?.Data?.UserId
            });
            var shopInfo = await _shopManageClient.GetShopSimpleInfo(new Client.Request.GetShopSimpleInfoClientRequest()
            {
                ShopId = request.Data.ShopId
            });
            var userName = !string.IsNullOrEmpty(userInfo?.UserName) ? userInfo?.UserName :
                     !string.IsNullOrEmpty(userInfo?.NickName) ? userInfo?.NickName : "System";
            arrivalRecordId = await _arrivalRepo.SaveArrival(new Dal.Model.ShopArrivalDO()
            {
                ArrivalTime = DateTime.Now,
                Status = (int)ArrivalRecordStatusEnum.Waiting,
                UserId = request.Data.UserId,
                UserName = userName,
                UserTel = userInfo?.UserTel,
                CarId = request.Data.Vechicle?.CarId,
                CarNo = request.Data?.Vechicle?.CarNo,
                Tid = request.Data?.Vechicle?.Tid,
                VehicleId = request.Data?.Vechicle?.VehicleId,
                Brand = request.Data?.Vechicle?.Brand,
                Vehicle = request.Data?.Vechicle?.Vehicle,
                PaiLiang = request.Data?.Vechicle?.PaiLiang,
                Nian = request.Data?.Vechicle?.Nian,
                SalesName = request.Data?.Vechicle?.SalesName,
                TotalMileage = request.Data?.Vechicle?.TotalMileage ?? default,
                CarProperties = JsonConvert.SerializeObject(request.Data?.Vechicle?.CarProperties),
                ServiceType = request.Data?.ServiceType,
                ShopId = request.Data?.ShopId ?? 0,
                ShopName = shopInfo?.SimpleName ?? string.Empty,
                Remark = request.Data?.Remark ?? string.Empty,
                ReserveNo = request?.Data?.ReserverNo ?? 0,
                QueueNumber = $"Y{request?.Data?.ShopId}-{generatorNumber}",
                QueueType = QueueTypeEnum.ReserverQueue.GetDescription(),
                CreateBy = userName,
                UpdateBy = userName,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            });

            var shopReserveOrder = await _shopReserveOrderRepository.GetReserveOrderByReserveId(request.Data.ReserverNo);
            if (shopReserveOrder?.Count > 0)
            {
                var orderProducts = await _orderClient.GetOrderProduct(new Client.Request.Order.OrderProductRequest()
                {
                    OrderNos = shopReserveOrder?.Select(x => x.OrderNo)?.ToList()
                });

                var orderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
                {
                    OrderNos = shopReserveOrder?.Select(x => x.OrderNo)?.ToList(),
                    ShopId = (int)request.Data.ShopId
                });

                List<ShopArrivalOrderDO> shopArrivals = new List<ShopArrivalOrderDO>();
                orderProducts?.ForEach(_ =>
                {
                    var orderType = (OrderTypeEnum)(orderBase?.Where(x => x.OrderNo == _.OrderNo)?.FirstOrDefault()?.OrderType ?? OrderTypeEnum.Other.ToInt());

                    shopArrivals.Add(new ShopArrivalOrderDO()
                    {
                        OrderNo = _.OrderNo,
                        Pid = _.ProductId,
                        ProductName = _.ProductName,
                        Price = _.Price,
                        ArrivalId = arrivalRecordId,
                        Num = (sbyte)_.TotalNumber,
                        OrderType = orderType.GetDescription().ToString(),
                        CreateBy = userName,
                        UpdateBy = userName,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        IsDeleted = 0
                    });
                });
                await _arrivalRepo.InsertBatchAsync<ShopArrivalOrderDO>(shopArrivals);
            }


            await _shopManageClient.AddShopUserRelation(new Client.Request.AddShopUserRelationRequest()
            {
                UserId = request?.Data?.UserId,
                ShopId = request.Data.ShopId,
                SubmitBy = userName,
                ReferrerUserId = string.Empty,
                ReferrerShopId = (int)request.Data.ShopId,
                ReferrerType = "ShopCode",
                Channel = "None"
            });

            bool updateLastReceiveTIme = await _shopManageClient.UpdateUserLastReceiveTime(new Client.Request.Shop.UpdateUserLastReceiveTimeRequest()
            {
                ArriveId = arrivalRecordId,
                ArriveTime = DateTime.Now.ToString(),
                ShopId = request?.Data?.ShopId ?? 0,
                UserId = request?.Data?.UserId,
                SubmitBy = userName
            });

            ///更改预约完结状态

            await _shopReserveService.UpdateReserveStatus(new Core.Request.UpdateReserveStatusRequest()
            {
                ReserveId = request.Data?.ReserverNo ?? 0,
                ReserveStatus = ReserveStatusEnum.Completed,
                UpdateBy = userName
            });

            return new ApiResult<QuickTakeNumberLayerResponse>()
            {
                Code = ResultCode.Success,
                Data = new QuickTakeNumberLayerResponse()
                {
                    QuickTakeNumberType = Core.Enums.QuickTakeNumberEnum.Queue,
                    Content = $"排队成功 \r\n 排队号:Y{request?.Data?.ShopId}-{generatorNumber}",
                    ArrivalId = arrivalRecordId
                }
            };
        }

        /// <summary>
        /// 得到服务记录信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetServiceRecordResponse>> GetServiceRecord(GetServiceRecordRequest request)
        {
            var carInfo = await _vehicleClient.GetUserVehicleByCarIdAsync(new Client.Request.UserVehicleByCarIdClientRequest()
            {
                UserId = request.UserId,
                CarId = request.CarId
            });

            var historyConsumer = await _arrivalRepo.GetHistoryArrivalConsumer(request.CarId, request.UserId);

            var historyConsumerAmount = await _arrivalRepo.GetHistoryArrivalConsumerPrice(request.CarId, request.UserId);

            var historyConsumerCount = await _arrivalRepo.GetHistoryArrivalConsumerCount(request.CarId, request.UserId);

            var arrivalRecords = await _arrivalRepo.GetShopArrivals(request.CarId, request.UserId);

            var arrivalIds = arrivalRecords?.Select(x => x.Id)?.ToList();

            var arrivalOrders = await _arrivalRepo.GetShopArrivalOrders(arrivalIds);


            List<MaintenanceRecordVo> maintenanceRecordVos = new List<MaintenanceRecordVo>();

            arrivalRecords?.ForEach(_ =>
            {
                var maintenanceRecord = new MaintenanceRecordVo()
                {
                    TitleName = "到店记录",
                    ServiceName = _.ServiceType,
                    ServiceShop = _.ShopName,
                    ReserveNumber = _.ReserveNo.ToString(),
                    QueueNumber = _.QueueNumber.ToString(),
                    ServiceTechnician = _.TechName.ToString(),
                    Remark = _.Remark,
                    ArriveDate = _.ArrivalTime.ToString("yyyy-MM-dd")
                };
                var maintanceOrders = arrivalOrders?.Where(x => x.ArrivalId == _.Id)?.ToList();
                maintenanceRecord.MaintenanceOrderInfo = new List<MaintenanceRecordVo>();
                maintanceOrders?.ForEach(x =>
                {
                    maintenanceRecord.MaintenanceOrderInfo.Add(new MaintenanceRecordVo()
                    {
                        TitleName = x.OrderType,
                        ServiceName = x.ProductName,
                        ServiceShop = _.ShopName,
                        ReserveNumber = _.ReserveNo.ToString(),
                        QueueNumber = _.QueueNumber.ToString(),
                        ServiceTechnician = _.TechName.ToString(),
                        Remark = _.Remark,
                        ArriveDate = x.CreateTime.ToString("yyyy-MM-dd"),
                        Price = $"￥{x.Price.ToString("F2")}"
                    });

                });

                maintenanceRecordVos.Add(maintenanceRecord);
            });

            return new ApiResult<GetServiceRecordResponse>()
            {
                Code = ResultCode.Success,
                Data = new GetServiceRecordResponse()
                {
                    Vehicle = new VehicleVo()
                    {
                        Brand = carInfo?.Brand,
                        Vehicle = carInfo?.Vehicle,
                        CarNo = carInfo?.CarNumber,
                        CarId = carInfo?.CarId,
                        CarBrandIcon = string.Empty
                    },
                    ConsumptionHistory = new ConsumptionHistoryVo()
                    {
                        ArriveShopSumCount = historyConsumerCount,
                        LastArriveShopTime = historyConsumer?.LastArrivalTime ?? "",
                        TotalConsumption = historyConsumerAmount.HasValue ? historyConsumerAmount.Value.ToString("F2") : "0",
                        LastKilometres = "-"
                    },
                    MaintenanceRecordHistory = maintenanceRecordVos
                }
            };

        }

        /// <summary>
        /// 到店记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedForArrivalResult<GetArrivalListResponse>> GetArrivalList(GetArrivalListRequest request)
        {
            var dalResponse = await _arrivalRepo.GetArrivalList(request);

            var dalArrivalCount = await _arrivalRepo.GetArrivalListCount(request);

            ApiPagedResultData<GetArrivalListResponse> response =
                _mapper.Map<ApiPagedResultData<GetArrivalListResponse>>(dalResponse);

            ApiPagedResultDataResponse<GetArrivalListResponse> apiPagedResultDataResponse = new ApiPagedResultDataResponse<GetArrivalListResponse>()
            {
                ArrivalListCount = dalArrivalCount,
                Items = response?.Items,
                TotalItems = response.TotalItems
            };

            return new ApiPagedForArrivalResult<GetArrivalListResponse>()
            {
                Code = ResultCode.Success,
                Data = apiPagedResultDataResponse
            };

        }

        /// <summary>
        /// 最近一次到店记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetArrivalListResponse>> GetLastArrival(GetLastArrivalRequest request)
        {
            var dalResponse = await _arrivalRepo.GetLastArrival(request);

            return new ApiResult<GetArrivalListResponse>()
            {
                Code = ResultCode.Success,
                Data = dalResponse
            };

        }

        /// <summary>
        /// 到店记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetArrivalDetailResponse>> GetArrivalDetail(GetArrivalDetailRequest request)
        {
            var shopArrival = await _arrivalRepo.GetShopArrival(request.ArrivalId);

            if (shopArrival == null)
            {
                return new ApiResult<GetArrivalDetailResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = "无此到店记录，请核对后重试！"
                };
            }

            ReserverVo reserverVo = null;
            #region 预约信息
            //取预约信息
            if (shopArrival != null && shopArrival.ReserveNo > 0)
            {
                reserverVo = new ReserverVo();
                var getReserverDetail = await _shopReserveRepository.GetReserveDetail(shopArrival?.ReserveNo ?? 0, true);

                reserverVo.ShowReserverTime = getReserverDetail?.ReserveTime.ToString("yyyy-MM-dd HH:mm:ss");
                reserverVo.ShowReserverInfo = getReserverDetail?.Channel == 1 ?
                    $"客户预约-{getReserverDetail?.ServiceName}-{getReserverDetail?.TechName}" :
                    $"技师预约-{getReserverDetail?.ServiceName}-{getReserverDetail?.TechName}";
                reserverVo.ReserverRemark = getReserverDetail?.Remark;

                var getReserverPicture = await _reservePictureRepository.GetReservePictureList(shopArrival?.ReserveNo ?? 0);
                reserverVo.Imgs = new List<ImgVo>();
                getReserverPicture?.ForEach(_ =>
                {
                    reserverVo.Imgs.Add(new ImgVo()
                    {
                        Url = $"{_configuration["QiNiuImageDomain"]}{_.Url}",
                        // Url = _.Url
                    });
                });
            }
            #endregion

            //当前到店的订单
            var currentArrivalOrder = await _shopArrivalOrderRepository.GetReceiveOrderByRecIds(new List<long> { shopArrival.Id } );
          
            //查用户订单信息
            GetOrdersByUserIdRequest orderR = new GetOrdersByUserIdRequest();
            orderR.ShopId = shopArrival?.ShopId ?? 0;
            orderR.UserId = shopArrival?.UserId;
            List<GetOrdersByUserIdResponse> userOrderList = await _orderClient.GetOrdersByUserId(orderR);
            List<ArrivalOrderVO> arrivalOrder = new List<ArrivalOrderVO>();
            List<ArrivalOrderVO> historyOrder = new List<ArrivalOrderVO>();
            if (userOrderList != null && userOrderList.Any())
            {
                var orderNos = userOrderList.Select(x => x.OrderNo).ToList();
                //获取订单的到店记录
                GetShopArrivalOrderRequest re = new GetShopArrivalOrderRequest();
                re.OrderNos = orderNos;
                var arrivals = await GetShopArrivalOrder(re);
                //组合数据
                ArrivalOrderVO order;
                var arrivalrelate = arrivals?.Data;
                foreach (var o in userOrderList)
                {
                    order = new ArrivalOrderVO();
                    order.OrderNO = o.OrderNo;
                    order.Count = o.TotalProductNum;
                    order.TotalPrice = o.ActualAmount;
                    order.DispatchStatus = ((DispatchStatusEnum)o.DispatchStatus).GetDescription();
                    order.LogisticsStatus = ((SignStatusEnum)o.SignStatus).GetDescription();
                    order.OrderStatus = ((OrderStatusEnum)o.OrderStatus).GetDescription();
                    order.Channel = ((ChannelEnum)o.Channel).GetDescription();
                    order.PayStatus = o.PayStatus;
                    order.Vehicle = o.OrderCar?.Vehicle??"";
                    order.ShowOrderTime = o.OrderTime.ToString("yyyy-MM-dd hh:mm");
                    //_.ProductAttribute == 2
                    var orderFwList = o.OrderProductList.Where(_ => _.ParentOrderPackagePid == 0)?.ToList();
                    if (orderFwList != null && orderFwList.Any())
                    {
                        List<string> ser = new List<string>();
                        orderFwList.ForEach(f =>
                        {
                            ser.Add(f.ProductName ?? "");
                        });
                        order.ServiceName = ser;
                    }
                    List<string> tags = new List<string>();
                    //车型是否一致
                    if ((o.OrderCar?.Tid?.Equals(shopArrival.Tid) ?? false) && (o.OrderCar?.VehicleId?.Equals(shopArrival.VehicleId) ?? false)
                        && (o.OrderCar?.Nian?.Equals(shopArrival.Nian) ?? false) && (o.OrderCar?.PaiLiang?.Equals(shopArrival.PaiLiang) ?? false))
                    {
                        order.IsSameCar = true;
                    }
                    else
                    {
                        order.IsSameCar = false;
                        tags.Add("车型不匹配");
                    }
                    //到店记录关联判断
                    if (currentArrivalOrder.Exists(_=>_.OrderNo == o.OrderNo))
                    {
                        order.ArrivalId = shopArrival.Id.ToString();
                        order.IsRelateArrival = true;
                    }
                    else if (arrivalrelate != null && arrivalrelate.Any())
                    {
                        var arrivalr = arrivalrelate.Where(_ => _.OrderNo == o.OrderNo)?.OrderByDescending(_=>_.ArrivalId)?.FirstOrDefault();
                        if (arrivalr != null && arrivalr.ArrivalId > 0)
                        {
                            order.ArrivalId = arrivalr.ArrivalId.ToString();
                            if (arrivalr.ArrivalId != shopArrival.Id)
                            {
                                order.IsRelateOtherArrival = true;
                                tags.Add("已关联其他到店"+ arrivalr.ArrivalId.ToString());
                            }
                            else
                            {
                                order.IsRelateArrival = true;
                                //arrivalOrder.Add(order);
                            }
                        }
                    }
                    order.Tags = tags;
                    if (order.ArrivalId == shopArrival.Id.ToString())
                    {
                        arrivalOrder.Add(order);
                    }
                    else
                    {
                        historyOrder.Add(order);
                    }
                    //筛选是未服务的还是历史的
                    //if (o.InstallStatus == 1 || o.OrderStatus == (int)OrderStatusEnum.Canceled || o.OrderStatus == (int)OrderStatusEnum.Completed)
                    //{
                    //    if (!(historyOrder?.Exists(_ => _.OrderNO == order.OrderNO) ?? false))
                    //    {
                    //        historyOrder.Add(order);
                    //    }
                    //}
                    //else
                    //{
                    //    if (!(arrivalOrder?.Exists(_ => _.OrderNO == order.OrderNO) ?? false))
                    //        arrivalOrder.Add(order);
                    //}
                }
                //排序显示
                if (arrivalOrder != null && arrivalOrder.Any())
                {
                    arrivalOrder = arrivalOrder.OrderByDescending(o => o.OrderNO).ToList();
                }
                if (historyOrder != null && historyOrder.Any())
                {
                    historyOrder = historyOrder.OrderByDescending(o => o.OrderNO).ToList();
                }
            }


            //预约记录信息，待补定
            //var shopArrivalOrders = await _arrivalRepo.GetShopArrivalOrders(new List<long>() { request.ArrivalId });

            //var getHIstoryArrivals = await _arrivalRepo.GetShopArrivals(shopArrival?.CarId, shopArrival?.UserId);

            //var arrivalIds = getHIstoryArrivals?.Select(x => x.Id)?.ToList();

            //var arrivalOrders = await _arrivalRepo.GetShopArrivalOrders(arrivalIds);

            //var projectItems = new List<ProjectItemVo>();

            //shopArrivalOrders?.ForEach(_ =>
            //{
            //    var price = _.Num * _.Price;
            //    projectItems.Add(new ProjectItemVo()
            //    {
            //        OrderId = _.OrderNo,
            //        Name = _.ProductName,
            //        Num = _.Num,
            //        Price = $"￥{price.ToString("F2")}",
            //    });
            //});

            //var historyArrivalVos = new List<HistoryArrivalVo>();

            //getHIstoryArrivals?.ForEach(_ =>
            //{
            //    if (_.Id != shopArrival.Id)
            //    {
            //        var historyArriavl = new HistoryArrivalVo()
            //        {
            //            CarNo = _.CarNo,
            //            ServiceType = _.ServiceType,
            //            ShowArrivalTime = _.ArrivalTime.ToString("yyyy-MM-dd"),

            //        };
            //        historyArriavl.ProjectItems = new List<ProjectItemVo>();
            //        arrivalOrders?.Where(x => x.ArrivalId == _.Id)?.ToList()?.ForEach(x =>
            //        {
            //            historyArriavl.ProjectItems.Add(new ProjectItemVo()
            //            {
            //                Id = string.Empty,
            //                Name = x.ProductName,
            //                Num = x.Num,
            //                OrderId = x.OrderNo,
            //                Price = $"￥{x.Price.ToString("F2")}",
            //            });
            //        });
            //        historyArrivalVos.Add(historyArriavl);
            //    }
            //});

            var userOperations = new List<UserOperationVo>();

            if (shopArrival?.Status == ArrivalRecordStatusEnum.Waiting.ToInt())
            {
                userOperations.Add(new UserOperationVo()
                {
                    Function = "LeaveShop",
                    ShowFunctionName = "未消费离店",
                    Sort = 1,
                    IsImportance = false
                });
                userOperations.Add(new UserOperationVo()
                {
                    Function = "ReceptionConstruction",
                    ShowFunctionName = "接待",
                    Sort = 2,
                    IsImportance = true
                });
            }
            if (shopArrival?.Status == ArrivalRecordStatusEnum.Dispatching.ToInt())
            {
                userOperations.Add(new UserOperationVo()
                {
                    Function = "LeaveShop",
                    ShowFunctionName = "未消费离店",
                    Sort = 1,
                    IsImportance = false
                });
                userOperations.Add(new UserOperationVo()
                {
                    Function = "Finish",
                    ShowFunctionName = "已完结",
                    Sort = 2,
                    IsImportance = true
                });
                userOperations.Add(new UserOperationVo()
                {
                    Function = "CreateOrder",
                    ShowFunctionName = "新建订单",
                    Sort = 3,
                    IsImportance = true
                });
            }
            userOperations.Add(new UserOperationVo()
            {
                Function = "ConstructionVideo",
                ShowFunctionName = "施工视频",
                Sort =4,
                IsImportance = false
            });

            userOperations.Add(new UserOperationVo()
            {
                Function = "MergePay",
                ShowFunctionName = "合并支付",
                Sort = 5,
                IsImportance = false
            });

            var leaveShopReasonInfo = string.Empty;
            var shopArrivalCancelDo = await _shopArrivalCancelRepository.GetArrivalCancel(request.ArrivalId);
            leaveShopReasonInfo = shopArrivalCancelDo != null ? $"{shopArrivalCancelDo.ReasonType}-{shopArrivalCancelDo.Remark}" : null;
            var checkStatic = await _receiveCheckService.GetCheckStatisticsByRecId(recId: request.ArrivalId);
            VehicleCheckVo vehicleCheckVo = null;
            if (checkStatic != null)
            {
                vehicleCheckVo = new VehicleCheckVo
                {
                    CheckId = checkStatic.CheckId,
                    Status = checkStatic.Status,
                    NormalCheckNum = checkStatic.NormalCount,
                    ExceptionCheckNum = checkStatic.AbNormalCount,
                    NoCheckNum = checkStatic.UncheckCount
                };
            }

            var shopArrivalCarReport = await _shopArrivalCarReportRepository.GetShopArrivalCarReport(request.ArrivalId);
            return new ApiResult<GetArrivalDetailResponse>()
            {
                Code = ResultCode.Success,
                Data = new GetArrivalDetailResponse()
                {
                    ArrivalId = request.ArrivalId.ToString(),
                    UserId = shopArrival?.UserId,
                    UserName = shopArrival?.UserName,
                    Telephone = shopArrival?.UserTel,
                    ShowArrivalDate = shopArrival?.ArrivalTime != null ? shopArrival?.ArrivalTime.ToString("yyyy-MM-dd") : "",
                    ShowArrivalStatus = shopArrival?.Status != null ? ((ArrivalRecordStatusEnum)shopArrival?.Status).ToString() : "",
                    PickOne = shopArrival?.TechName,
                    Remark = shopArrival?.Remark,
                    CarInfo = new CarVo()
                    {
                        CarId = shopArrival?.CarId,
                        CarLogo = $"{_configuration["QiNiuImageDomain"]}{ImageHelper.GetLogoUrlByName(shopArrival?.Brand)}",
                        CarNo = shopArrival?.CarNo,
                        Brand = shopArrival?.Brand,
                        Vehicle = shopArrival?.Vehicle,
                        Tid = shopArrival?.Tid,
                        Nian = shopArrival?.Nian,
                        PaiLiang = shopArrival?.PaiLiang,
                        VehicleId = shopArrival?.VehicleId
                    },
                    ReserverInfo = reserverVo,
                    //ProjectItems = projectItems,
                    //HistoryArrivals = historyArrivalVos,
                    UserOperations = userOperations,
                    LeaveShopReasonInfo = leaveShopReasonInfo,
                    VehicleCheckInfo = vehicleCheckVo,
                    HistoryOrders = historyOrder,
                    ArrivalOrders = arrivalOrder,
                    CarReport = shopArrivalCarReport?.CarReportUrl
                }
            };

        }

        /// <summary>
        /// 得到派工技师列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<GetDispatchTechniciansResponse>>> GetDispatchTechnicians([FromQuery] GetDispatchTechniciansRequest request)
        {
            var getTechnicians = await _shopManageClient.GetTechnicianPage(new Client.Request.Shop.GetTechnicianPageRequest()
            {
                EmployeeId = request.UserId,
                ShopId = request.ShopId,
                PageIndex = 1,
                PageSize = 100
            });
            var getDispatchTechnicians = new List<GetDispatchTechniciansResponse>();
            getTechnicians?.ForEach(_ =>
            {
                int.TryParse(_.Level, out int level);
                getDispatchTechnicians.Add(new GetDispatchTechniciansResponse()
                {
                    UserId = _.Id,
                    Name = _.Name,
                    Level = ((TechnicianLevelEnum)level).GetDescription()
                }); ;
            });
            return new ApiResult<List<GetDispatchTechniciansResponse>>()
            {
                Code = ResultCode.Success,
                Data = getDispatchTechnicians
            };
        }

        /// <summary>
        /// 派工保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> DispatchTechnicianSave([FromBody] ApiRequest<DispatchTechnicianSaveRequest> request)
        {
            var updateTechnician = await _arrivalRepo.UpdateTechnicians(request?.Data);

            if (updateTechnician > 0)
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "接待成功"
                };
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "接待失败"
            };
        }

        /// <summary>
        /// 未消费离店原因
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<LeaveShopReasonResponse>>> LeaveShopReason(LeaveShopReasonRequest request)
        {
            var getShopReason = await _arrivalRepo.GetShopArrivalOutPay();
            var shopReason = new List<LeaveShopReasonResponse>();
            getShopReason?.ForEach(_ =>
            {
                shopReason.Add(new LeaveShopReasonResponse()
                {
                    Reason = _.Reason
                });
            });
            return new ApiResult<List<LeaveShopReasonResponse>>()
            {
                Code = ResultCode.Success,
                Data = shopReason
            };
        }

        /// <summary>
        /// 离店原因保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> LeaveShopReasonSave(ApiRequest<LeaveShopReasonSaveRequest> request)
        {
            long.TryParse(request.Data.ArrivalId, out long arrivaId);
            var shopArrivalOrders = await _arrivalRepo.GetShopArrivalOrders(new List<long>() { arrivaId });
            var orderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = shopArrivalOrders?.Select(x => x.OrderNo)?.ToList(),
                ShopId = request.Data.ShopId
            });
            //if (orderBase?.Count() > 0)
            //{
            //    var payStatus = orderBase.Exists(_ => _.PayStatus == PayStatusEnum.HavePay.ToInt());
            //    if (payStatus)
            //    {
            //        return new ApiResult()
            //        {
            //            Code = ResultCode.Failed,
            //            Message = "项目列表中存在已支付产品，不允许未消费离店操作！"
            //        };
            //    }
            //}

            var leaveReson = await _arrivalRepo.LeaveShopReasonSave(new Dal.Model.Arrival.ShopArrivalCancelDO()
            {
                ArrivalId = arrivaId,
                ReasonType = request.Data?.ReasonType,
                Remark = request.Data?.Remark,
                CreateBy = request.Data?.CreateBy,
                CreateTime = DateTime.Now,
                UpdateBy = request.Data?.CreateBy,
                UpdateTime = DateTime.Now
            });
            long updateShopArrivalStatus = 0;
            if (leaveReson > 0)
            {
                long.TryParse(request.Data.ArrivalId, out long arrivalId);
                updateShopArrivalStatus = await _arrivalRepo.UpdateShopArrivalStaus(ArrivalRecordStatusEnum.Leave.ToInt(), arrivalId, request.Data.CreateBy);

                if (updateShopArrivalStatus > 0)
                {
                    if (shopArrivalOrders?.Select(x => x.OrderNo)?.Count() > 0)
                    {
                        //await _orderClient.CancelOrderForReserverOrArrival(new ApiRequest<Client.Request.Order.CancelOrderForReserverOrArrivalRequest>()
                        //{
                        //    Data = new Client.Request.Order.CancelOrderForReserverOrArrivalRequest()
                        //    {
                        //        OrderNos = shopArrivalOrders?.Select(x => x.OrderNo)?.ToList(),
                        //        UserId = request.Data.UserId
                        //    }
                        //});
                        //await _reverseClient.CreateReverseOrderCancelForArrivalOrReserve(new Client.Request.Order.CancelOrderRequest()
                        //{
                        //    UserId = request.Data.UserId,
                        //    OrderNos = shopArrivalOrders?.Select(x => x.OrderNo)?.ToList()
                        //});
                        //所有的订单关联删除
                        await _arrivalRepo.DeleteShopArrivalOrder(arrivaId, request.Data?.CreateBy);
                    }
                }
            }
            if (updateShopArrivalStatus > 0)
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "保存成功"
                };
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "保存失败"
            };
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> DeleteProjectItem(ApiRequest<DeleteProjectItemRequest> request)
        {
            var orderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = new List<string>() { request.Data.OrderId },
                ShopId = request.Data.ShopId
            });
            if (orderBase?.Count() > 0)
            {
                var payStatus = orderBase.FirstOrDefault()?.PayStatus ?? 0;
                if (payStatus == PayStatusEnum.HavePay.ToInt())
                {
                    return new ApiResult()
                    {
                        Code = ResultCode.Failed,
                        Message = "此项目产品已经支付，不允许删除！"
                    };
                }
            }
            var createReverseCancel = await _reverseClient.CreateReverseOrderCancelForArrivalOrReserve(new Client.Request.Order.CancelOrderRequest()
            {
                UserId = request.Data.UserId,
                OrderNos = new List<string>() { request.Data.OrderId }
            });

            long deleteArrivalResult = 0;
            long.TryParse(request.Data?.ArrivalId, out long arrivalId);
            if (createReverseCancel)
            {
                deleteArrivalResult = await _arrivalRepo.DeleteShopArrivalOrder(arrivalId, request.Data?.OrderId, request.Data?.UserName);
            }

            if (deleteArrivalResult > 0)
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "删除成功"
                };
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "删除失败"
            };
        }

        /// <summary>
        /// 得到项目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetProjectListResponse>> GetProjectList([FromQuery] GetProjectListRequest request)
        {
            return null;
        }

        /// <summary>
        /// 项目保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> ProjectItemSave(ApiRequest<ProjectItemSaveRequest> request)
        {
            long.TryParse(request?.Data?.ArrivalId, out var arrivalId);
            var items = new List<Client.Model.ReserveProductClientDTO>();
            request?.Data?.Items?.ForEach(_ =>
            {
                decimal.TryParse(_.Price, out var price);
                items.Add(new Client.Model.ReserveProductClientDTO()
                {
                    Pid = _.Id,
                    Num = _.Num,
                    Price = price,
                    OrderType = GetOrderType(_.OrderType)
                });
            });

            var shopArrival = await _arrivalRepo.GetAsync<ShopArrivalDO>(arrivalId);
            var createOrder = await _orderClient.CreateOrderForArrivalOrReserver(new Client.Request.Order.CreateOrderForArrivalOrReserverRequest()
            {
                CarId = request.Data.CarId,
                OrderType = 0,
                ShopId = request.Data.ShopId,
                UserId = shopArrival?.UserId,
                Items = items
            });

            if (createOrder.Count > 0)
            {
                var orderProducts = await _orderClient.GetOrderProduct(new Client.Request.Order.OrderProductRequest()
                {
                    OrderNos = createOrder
                });

                var userInfo = await _userClient.GetUserInfo(new Client.Request.GetUserInfoClientRequest()
                {
                    UserId = request?.Data?.UserId
                });
                List<ShopArrivalOrderDO> shopArrivals = new List<ShopArrivalOrderDO>();
                orderProducts?.ForEach(_ =>
                {
                    var productName = _.ProductName;
                    if (_.ProductId.Trim() == "SW-0-0-100000")
                    {
                        productName = request.Data?.Items?.Where(x => x.Id.Trim() == "SW-0-0-100000")?.FirstOrDefault()?.Name;
                    }
                    var orderType = request.Data.Items?.Where(x => x.Id == _.ProductId)?.FirstOrDefault()?.OrderType;
                    shopArrivals.Add(new ShopArrivalOrderDO()
                    {
                        OrderNo = _.OrderNo,
                        Pid = _.ProductId,
                        ProductName = productName,
                        Price = _.Price,
                        ArrivalId = arrivalId,
                        Num = (sbyte)_.TotalNumber,
                        OrderType = !string.IsNullOrEmpty(orderType) ? orderType : OrderTypeEnum.Other.GetDescription(),
                        CreateBy = userInfo?.UserName ?? "System",
                        UpdateBy = userInfo?.UserName ?? "System",
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        IsDeleted = 0
                    });
                });
                if (shopArrivals?.Count > 0)
                {
                    await _arrivalRepo.InsertBatchAsync<ShopArrivalOrderDO>(shopArrivals);
                }
            }

            return new ApiResult()
            {
                Code = ResultCode.Success,
                Message = "成功"
            };
        }

        private int GetOrderType(string orderType)
        {
            if (orderType.Trim() == OrderTypeEnum.Tire.GetDescription())
            {
                return OrderTypeEnum.Tire.ToInt();
            }
            if (orderType.Trim() == OrderTypeEnum.Beauty.GetDescription())
            {
                return OrderTypeEnum.Beauty.ToInt();
            }
            if (orderType.Trim() == OrderTypeEnum.Maintenance.GetDescription())
            {
                return OrderTypeEnum.Maintenance.ToInt();
            }
            if (orderType.Trim() == OrderTypeEnum.SheetMetal.GetDescription())
            {
                return OrderTypeEnum.SheetMetal.ToInt();
            }
            if (orderType.Trim() == OrderTypeEnum.CarModification.GetDescription())
            {
                return OrderTypeEnum.CarModification.ToInt();
            }
            return OrderTypeEnum.Other.ToInt();
        }
        /// <summary>
        /// 项目编辑
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<ProjectItemEditResponse>> ProjectItemEdit(ProjectItemEditRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 收款
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<PayResponse>> Pay(PayRequest request)
        {

            long.TryParse(request.ArrivalId, out long arrivalId);
            var shopArrivalOrders = await _arrivalRepo.GetShopArrivalOrders(new List<long>() { arrivalId });
            var projectItems = new List<ProjectItemVo>();
            shopArrivalOrders?.ForEach(_ =>
            {
                var price = _.Num * _.Price;
                projectItems.Add(new ProjectItemVo()
                {
                    Id = string.Empty,
                    Name = _.ProductName,
                    Num = _.Num,
                    Price = $"￥{price.ToString("F2")}"
                });
            });
            return new ApiResult<PayResponse>()
            {
                Code = ResultCode.Success,
                Data = new PayResponse()
                {
                    ProjectItems = projectItems,
                    SumPrice = $"￥{shopArrivalOrders?.Sum(_ => _.Price * _.Num).ToString("F2")}"
                }
            };
        }

        /// <summary>
        /// 收款保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> PaySave(ApiRequest<PaySaveRequest> request)
        {
            long.TryParse(request.Data.ArrivalId, out long arrivalId);
            var shopArrivalOrders = await _arrivalRepo.GetShopArrivalOrders(new List<long>()
            {
                arrivalId
            });

            var updateShopArrivalStatus = await _arrivalRepo.UpdateShopArrivalStaus(ArrivalRecordStatusEnum.Finish.ToInt(), arrivalId, request?.Data?.UserName);

            var updateOrderStatus = false;
            if (updateShopArrivalStatus > 0)
            {
                if (shopArrivalOrders?.Count() > 0)
                {
                    updateOrderStatus = await _orderClient.UpdateOrderPayStatus(new ApiRequest<UpdateOrderPayStatusRequest>()
                    {
                        Data = new UpdateOrderPayStatusRequest()
                        {
                            UpdateBy = request.Data.UserName,
                            OrderNo = shopArrivalOrders?.Select(x => x.OrderNo)?.ToList(),
                            PayMethod = request.Data?.PayMethod ?? 0,
                            PayChannel = request.Data?.PayChannel ?? 0
                        }
                    });

                    var updateOrderCompleteStatus = await _orderClient.UpdateOrderCompleteStatus(new ApiRequest<BatchUpdateCompleteStatusRequest>()
                    {
                        Data = new BatchUpdateCompleteStatusRequest()
                        {
                            UpdateBy = request.Data.UserName,
                            OrderNo = shopArrivalOrders?.Select(x => x.OrderNo)?.ToList()
                        }
                    });

                    await _orderClient.BatchUpdateInstallStatus(new BatchUpdateInstallStatusRequest()
                    {
                        UpdateBy = request.Data.UserName,
                        OrderNo = shopArrivalOrders?.Select(x => x.OrderNo)?.ToList()
                    });
                }
            }

            //增加派工记录
            var shopArrival = await _arrivalRepo.GetAsync(arrivalId);
            List<Core.Request.SaveOrderDispatchRequest> saveOrderDispatchRequests = new List<Core.Request.SaveOrderDispatchRequest>();
            shopArrivalOrders?.ForEach(_ =>
            {
                saveOrderDispatchRequests.Add(new Core.Request.SaveOrderDispatchRequest()
                {
                    OrderNo = _.OrderNo,
                    TechId = shopArrival?.TechId ?? string.Empty,
                    TechName = shopArrival?.TechName ?? string.Empty,
                    Level = shopArrival?.Level ?? string.Empty,
                    CreateBy = request.Data.UserName,
                    ShopId = shopArrival?.ShopId ?? 0
                });
            });
            if (saveOrderDispatchRequests?.Count() > 0)
            {
                await _orderClient.SaveOrderDispatch(new ApiRequest<List<Core.Request.SaveOrderDispatchRequest>>
                {
                    Data = saveOrderDispatchRequests,
                    ApiVersion = request.ApiVersion,
                    Channel = request.Channel,
                    DeviceId = request.DeviceId
                });
            }


            if (updateShopArrivalStatus > 0)
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "支付成功"
                };
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = "支付失败"
            };
        }

        /// <summary>
        /// 得到上一次到店记录下的ShopId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<long>> GetLastShopForLastArrival(GetLastShopForLastArrivalRequest request)
        {
            var getLashArrivalShopId = await _arrivalRepo.GetLastShopForLastArrival(request);
            return new ApiResult<long>()
            {
                Code = ResultCode.Success,
                Data = getLashArrivalShopId
            };
        }

        /// <summary>
        /// 得到订单信息为预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ProjectVo>>> GetOrdersForReserver(GetOrdersForReserverRequest request)
        {
            var orderProducts = await _orderClient.GetOrderProduct(new OrderProductRequest()
            {
                OrderNos = request.OrderNos
            });
            // var getShopOrders = await _arrivalRepo.GetOrdersForReserver(request);
            List<ProjectVo> projectVos = new List<ProjectVo>();
            orderProducts?.ForEach(_ =>
            {
                projectVos.Add(new ProjectVo()
                {
                    Id = _.ProductId,
                    Icon = $"{_configuration["QiNiuImageDomain"]}{_.ImageUrl}",
                    Name = _.ProductName,
                    Price = _.Price.ToString("F2"),
                    Num = _.Number,
                });
            });
            return new ApiResult<List<ProjectVo>>()
            {
                Code = ResultCode.Success,
                Data = projectVos
            };
        }

        /// <summary>
        /// 更改到店记录车型信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> UpdateArrivalVehicle(UpdateArrivalVehicleRequest request)
        {
            _logger.Info($"UpdateArrivalVehicle request= {JsonConvert.SerializeObject(request)}");
            var updateValue = await _arrivalRepo.UpdateArrivalVehicle(request);
            if (updateValue > 0)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "更新成功"
                };
            }
            return new ApiResult()
            {
                Code = ResultCode.Success,
                Message = "更新失败"
            };
        }

        /// <summary>
        /// 得到到店记录订单为员工绩效统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetShopArrivalOrderForStaticResponse>> GetShopArrivalOrderForStatic([FromBody] GetShopArrivalForStaticRequest request)
        {
            var response = new GetShopArrivalOrderForStaticResponse();
            var getShopArrival = await _arrivalRepo.GetShopArrivalForStatic(request);
            var shopArrivalList = new List<ShopArrivalDO>();

            var shopArrivalOrders = new List<ShopArrivalOrderDTO>();
            foreach (var item in getShopArrival.GroupBy(r => r.UserId))
            {
                var dateTime = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("D").ToString()).AddSeconds(-1);
                var lastDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                //判断用户之前是否存在消费记录
                if (item.Any(r => r.FinishTime.Subtract(dateTime).TotalSeconds > 0))
                {
                    //这种的不做统计
                    response.UserIds.Add(item.Key);
                }
                else
                {
                    //取当天第一笔到店记录
                    var firstArrival = item.Where(r => r.FinishTime.ToString("yyyy-MM-dd") == lastDate)
                        .OrderBy(r => r.FinishTime)?.FirstOrDefault();
                    if (firstArrival != null)
                    {
                        shopArrivalList.Add(firstArrival);
                    }
                }
            }

            if (shopArrivalList.Any())
            {
                var getShopArrivalIds = shopArrivalList?.Select(_ => _.Id)?.ToList();
                if (getShopArrivalIds?.Count > 0)
                {
                    var getShopArrivalOrders = await _shopArrivalOrderRepository.GetReceiveOrderByRecIds(getShopArrivalIds);
                    shopArrivalOrders = _mapper.Map<List<ShopArrivalOrderDTO>>(getShopArrivalOrders);
                }

                //到店记录订单绑定用户Id
                foreach (var item in shopArrivalOrders.GroupBy(r => r.ArrivalId))
                {
                    var shopArrival = shopArrivalList.FirstOrDefault(r => r.Id == item.Key);
                    if (shopArrival != null)
                    {
                        item.ToList().ForEach(r =>
                        {
                            r.UserId = shopArrival.UserId;
                        });
                    }
                }
                response.ShopArrivalOrders = shopArrivalOrders;
            }
            return new ApiResult<GetShopArrivalOrderForStaticResponse>()
            {
                Code = ResultCode.Success,
                Data = response
            };
        }

        ///  支付列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<PayListResponse>> PayList(PayRequest request)
        {
            long.TryParse(request.ArrivalId, out long arrivalId);
            var shopArrivalOrders = await _arrivalRepo.GetShopArrivalOrders(new List<long>() { arrivalId });
            var getOrderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = shopArrivalOrders?.Select(_ => _.OrderNo)?.ToList(),
                ShopId = request.ShopId
            });

            PayListResponse payListResponse = new PayListResponse();
            payListResponse.Items = new List<PayListVo>();

            if (shopArrivalOrders?.Count > 0)
            {
                shopArrivalOrders?.ForEach(_ =>
                {
                    //var price = _.Num * _.Price;

                    var signleOrder = getOrderBase.Where(x => x.OrderNo == _.OrderNo)?.FirstOrDefault();
                    var showPayStatus = signleOrder.PayStatus == PayStatusEnum.HavePay.ToInt() ?
                    PayStatusEnum.HavePay.GetDescription() : PayStatusEnum.NotPay.GetDescription();
                    payListResponse.Items.Add(new PayListVo()
                    {
                        OrderNo = _.OrderNo,
                        OrderType = _.OrderType,
                        OrderTime = signleOrder.OrderTime,
                        ShowOrderStatus = showPayStatus,
                        PayStatus = signleOrder?.PayStatus == PayStatusEnum.HavePay.ToInt() ? true : false,
                        //Products = new List<PayListProductVo>()
                        //{
                        //    new PayListProductVo()
                        //    {
                        //        Name=_.ProductName,
                        //        Num=_.Num,
                        //        Price=$"￥{price.ToString("F2")}"
                        //    }
                        //},
                        //SumPrice = $"￥{price.ToString("F2")}"
                        ActualAmount = signleOrder.ActualAmount
                    }) ;

                });
            }
            payListResponse.SumPrice = getOrderBase?.Sum(_ => _.ActualAmount).ToString("F2") ?? "0";

            payListResponse.WaitingPayPrice = getOrderBase?.
                Where(_ => _.PayStatus == PayStatusEnum.NotPay.ToInt())?.Sum(_ => _.ActualAmount).ToString("F2") ?? "0";


            return new ApiResult<PayListResponse>()
            {
                Code = ResultCode.Success,
                Data = payListResponse
            };
        }

        /// <summary>
        /// 判断是否需要唤醒收银台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> CheckIsNeedPayControl(PayRequest request)
        {
            long.TryParse(request.ArrivalId, out long arrivalId);
            var shopArrivalOrders = await _arrivalRepo.GetShopArrivalOrders(new List<long>() { arrivalId });
            var getOrderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = shopArrivalOrders?.Select(_ => _.OrderNo)?.ToList(),
                ShopId = request.ShopId
            });
            var checkIsNeedPay = getOrderBase?.Exists(_ => _.PayStatus == PayStatusEnum.NotPay.ToInt() && _.ActualAmount > 0) ?? false;

            return new ApiResult<bool>()
            {
                Code = ResultCode.Success,
                Data = checkIsNeedPay
            };
        }

        /// <summary>
        /// 得到到店记录下面的订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ShopArrivalOrderDTO>>> GetArrivalOrderList(GetArrivalOrderListRequest request)
        {

            var shopArrivalOrders = await _arrivalRepo.GetShopArrivalOrders(request.ArrivalIds);

            return new ApiResult<List<ShopArrivalOrderDTO>>()
            {
                Code = ResultCode.Success,
                Data = _mapper.Map<List<ShopArrivalOrderDTO>>(shopArrivalOrders)
            };

        }

        /// <summary>
        /// 支付列表 ForMini
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<PayListResponse>> PayListForMini(PayListForMiniRequest request)
        {
            var shopReserveOrder = await _shopReserveOrderRepository.GetReserveOrderByReserveId(request.ReserverNo);

            var getOrderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = shopReserveOrder?.Select(_ => _.OrderNo)?.ToList(),
                ShopId = request.ShopId
            });

            var orderProducts = await _orderClient.GetOrderProduct(new Client.Request.Order.OrderProductRequest()
            {
                OrderNos = shopReserveOrder?.Select(x => x.OrderNo)?.ToList()
            });

            PayListResponse payListResponse = new PayListResponse();
            payListResponse.Items = new List<PayListVo>();
            if (shopReserveOrder?.Count > 0)
            {
                shopReserveOrder?.ForEach(_ =>
                {
                    var signleOrder = getOrderBase.Where(x => x.OrderNo == _.OrderNo)?.FirstOrDefault();
                    var showPayStatus = signleOrder?.PayStatus == PayStatusEnum.HavePay.ToInt() ?
                    PayStatusEnum.HavePay.GetDescription() : PayStatusEnum.NotPay.GetDescription();

                    var orderProduct = orderProducts.Where(x => x.OrderNo == _.OrderNo)?.FirstOrDefault();
                    var price = orderProduct?.TotalAmount ?? 0;
                    payListResponse.Items.Add(new PayListVo()
                    {
                        OrderNo = _.OrderNo,
                        OrderType = signleOrder?.OrderType.ToString() ?? string.Empty,
                        ShowOrderStatus = showPayStatus,
                        PayStatus = signleOrder?.PayStatus == PayStatusEnum.HavePay.ToInt() ? true : false,
                        Products = new List<PayListProductVo>()
                        {
                            new PayListProductVo()
                            {
                                Name=orderProduct?.ProductName??string.Empty,
                                Num=orderProduct.TotalNumber,
                                Price=$"￥{price.ToString("F2")}"
                            }
                        },
                        SumPrice = $"￥{price.ToString("F2")}"
                    });

                });
            }
            payListResponse.SumPrice = getOrderBase?.Sum(_ => _.ActualAmount).ToString("F2") ?? "0";

            payListResponse.WaitingPayPrice = getOrderBase?.
                Where(_ => _.PayStatus == PayStatusEnum.NotPay.ToInt())?.Sum(_ => _.ActualAmount).ToString("F2") ?? "0";

            return new ApiResult<PayListResponse>()
            {
                Code = ResultCode.Success,
                Data = payListResponse
            };
        }

        /// <summary>
        /// 判断是否需要唤醒收银台ForMini
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> CheckIsNeedPayControlForMini(PayListForMiniRequest request)
        {
            var orders = new List<string>();

            if (request.ReserverNo > 0)
            {
                var shopReserveOrder = await _shopReserveOrderRepository.GetReserveOrderByReserveId(request.ReserverNo);

                orders = shopReserveOrder?.Select(_ => _.OrderNo)?.ToList();
            }
            else if (!string.IsNullOrWhiteSpace(request.OrderNo))
            {
                orders = new List<string>() { request.OrderNo };
            }

            var getOrderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = orders,
                ShopId = request.ShopId
            });
            var checkIsNeedPay = getOrderBase?.Exists(_ => _.PayStatus == PayStatusEnum.NotPay.ToInt() && _.ActualAmount > 0) ?? false;


            return new ApiResult<bool>()
            {
                Code = ResultCode.Success,
                Data = checkIsNeedPay
            };
        }

        /// <summary>
        /// 得到车辆最后入店时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> GetLastArriveTimeByCarId(GetArrialMaintenanceRequest request)
        {
            var arrivalTime = await _arrivalRepo.GetLastArriveTimeByCarId(request);

            return new ApiResult<string>()
            {
                Code = ResultCode.Success,
                Data = arrivalTime
            };
        }
        /// <summary>
        /// 得到到店的次数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<int>> GetArriveCountByCarId(GetArrialMaintenanceRequest request)
        {
            var arrivalCount = await _arrivalRepo.GetArriveCountByCarId(request);

            return new ApiResult<int>()
            {
                Code = ResultCode.Success,
                Data = arrivalCount
            };
        }

        /// <summary>
        /// 得到到店的消费金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<double>> GetArriveConsumptionAmountByCarId(GetArrialMaintenanceRequest request)
        {
            var amount = await _arrivalRepo.GetArriveConsumptionAmountByCarId(request);

            return new ApiResult<double>()
            {
                Code = ResultCode.Success,
                Data = amount
            };
        }

        /// <summary>
        /// 得到历史到店维修记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetArrivalMaintenanceListByCarIdResponse>> GetArrivalMaintenanceListByCarId([FromQuery] GetArrivalMaintenanceListByCarIdRequest request)
        {
            var getArrivalMaintenacneHead = await _arrivalRepo.GetArrivalMaintenanceHeader(request);
            getArrivalMaintenacneHead.Insert(0, new GetArrivalMaintenanceListByCarIdVo
            {
                ServiceType = "None",
                ServiceTypeText = "全部",
                Num = getArrivalMaintenacneHead.Sum(x => x.Num)
            });

            var getArrivalMaintenanceItems = await _arrivalRepo.GetArrivalMaintenanceItem(request);

            var data = new GetArrivalMaintenanceListByCarIdResponse();
            data.ManitenanceHead = getArrivalMaintenacneHead;

            if (getArrivalMaintenanceItems != null && getArrivalMaintenanceItems.Any())

            {
                data.Items
                    = new List<ArrivalMaintenanceProjectVo>();

                if (request.ServiceType == OrderTypeEnum.None.ToString())
                {
                    foreach (OrderTypeEnum item in Enum.GetValues(typeof(OrderTypeEnum)))
                    {
                        var projectItems = GetMaintenanceItems(getArrivalMaintenanceItems, item.GetDescription());
                        if (projectItems?.Any() ?? false)
                        {
                            data.Items.AddRange(projectItems);
                        }
                    }
                }

                foreach (OrderTypeEnum item in Enum.GetValues(typeof(OrderTypeEnum)))
                {
                    if (request.ServiceType == item.ToString())
                    {
                        var projectItems = GetMaintenanceItems(getArrivalMaintenanceItems, item.GetDescription());
                        if (projectItems?.Any() ?? false)
                        {
                            data.Items.AddRange(projectItems);
                        }
                    }
                }
            }

            return new ApiResult<GetArrivalMaintenanceListByCarIdResponse>()
            {
                Code = ResultCode.Success,
                Data = data
            };
        }

        /// <summary>
        /// 返回项目内容
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        private List<ArrivalMaintenanceProjectVo> GetMaintenanceItems(List<GetArrivalMaintenanceItemResponse> maintenanceData, string serviceType)
        {
            List<ArrivalMaintenanceProjectVo> result = new List<ArrivalMaintenanceProjectVo>();

            var serviceTypeProjects = maintenanceData?.Where(_ => _.OrderType == serviceType)?.ToList()?.ToLookup(_ => _.Id);

            if (serviceTypeProjects != null)
                foreach (var serviceTypeItem in serviceTypeProjects)
                {
                    var project = new ArrivalMaintenanceProjectVo();
                    project.Items = new List<ArrivalMaintenanceProjectItem>();

                    foreach (var item in serviceTypeItem)
                    {
                        project.Id = item.Id;
                        project.Kilometre = 0;
                        project.ShopName = item.ShopName;
                        project.ServiceTypeText = item.OrderType;
                        project.ArrivalDate = item.ArrivalTime.ToString("yyyy-MM-dd");
                        project.Items.Add(new ArrivalMaintenanceProjectItem()
                        {
                            Num = item.Num,
                            Price = item.Price,
                            ProductName = item.ProductName,
                            TechName = item.TechName
                        });
                    }
                    result.Add(project);
                }
            return result;
        }

        /// <summary>
        /// 得到到店记录订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<ShopArrivalOrderDTO>>> GetShopArrivalOrder([FromQuery] GetShopArrivalOrderRequest request)
        {
            var shopArrivalOrders = await _shopArrivalOrderRepository.GetShopArrivalOrder(request);
            List<ShopArrivalOrderDTO> data = _mapper.Map<List<ShopArrivalOrderDTO>>(shopArrivalOrders);
            return new ApiResult<List<ShopArrivalOrderDTO>>()
            {
                Code = ResultCode.Success,
                Data = data
            };
        }

        /// <summary>
        /// 订单关联到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> ArrivalRelateOrder(ArrivalRelateOrderRequest request)
        {
            var shopArrival = await _arrivalRepo.GetShopArrival(request.ArrivalId);
            if (shopArrival != null)
            {
                if (shopArrival.Status == (int)ShopArrivalStatus.Finished || shopArrival.Status == (int)ShopArrivalStatus.NoPendingLeftShop)
                {
                    throw new CustomException("该到店已完结，不能再关联订单");
                }
            }
            else
            {
                throw new CustomException("该到店记录不存在");
            }

            //GetShopArrivalOrderRequest re = new GetShopArrivalOrderRequest();
            //re.OrderNos = new List<string>() { request.OrderNo };
            //var shopArrivalOrders = await _shopArrivalOrderRepository.GetShopArrivalOrder(re);
            //if (shopArrivalOrders != null && shopArrivalOrders.Count > 0)
            //{
            //    throw new CustomException("已关联到店，不能再次关联");
            //}

            //先解绑其他到店记录
            await _arrivalRepo.DeleteShopArrivalOrder(request.OrderNo, request.CreateBy);

            ShopArrivalOrderDO arrivalOrder = new ShopArrivalOrderDO();
            arrivalOrder.OrderNo = request.OrderNo;
            arrivalOrder.ArrivalId = request.ArrivalId;
            arrivalOrder.CreateBy = request.CreateBy ?? "System";
            arrivalOrder.UpdateBy =  "";
            arrivalOrder.CreateTime = DateTime.Now;
            arrivalOrder.UpdateTime = DateTime.Now;
            arrivalOrder.IsDeleted = 0;
            arrivalOrder.OrderType = string.Empty;
            arrivalOrder.Pid = string.Empty;
            arrivalOrder.ProductName = string.Empty;
            arrivalOrder.Price = 0;
            arrivalOrder.Num = 0;
            bool success = await _arrivalRepo.InsertAsync<ShopArrivalOrderDO>(arrivalOrder) > 0;
            return Result.Success(success);
        }


        /// <summary>
        /// 未消费离店前置判断是否关联订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<int>> BeforeShopReasonSave(BeforeShopReasonSaveRequest request)
        {
            var shopArrivalOrders = await _arrivalRepo.GetShopArrivalOrders(new List<long>() { request.ArrivalId });
            int count = shopArrivalOrders == null ? 0 : shopArrivalOrders.Count;
            return Result.Success(count);
        }


        /// <summary>
        /// 订单取消关联到店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> CancelArrivalRelateOrder([FromBody] ArrivalRelateOrderRequest request)
        {
            bool success = await _arrivalRepo.DeleteShopArrivalOrder(request.ArrivalId, request.OrderNo, request.CreateBy) > 0;
            return Result.Success(success);
        }


        /// <summary>
        /// 到店完结
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> ArrivalFinish(ApiRequest<ArrivalFinishRequest> request)
        {
            long.TryParse(request.Data.ArrivalId, out long arrivaId);
            var shopArrivalOrders = await _arrivalRepo.GetShopArrivalOrders(new List<long>() { arrivaId });
            var orderBase = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = shopArrivalOrders?.Select(x => x.OrderNo)?.ToList(),
            });
            if (orderBase?.Count() > 0)
            {
                var payStatus = orderBase.Exists(_ => (_.OrderStatus != (int)OrderStatusEnum.Canceled && _.OrderStatus != (int)OrderStatusEnum.Completed) && (_.PayStatus == PayStatusEnum.NotPay.ToInt() || _.InstallStatus == InstallStatusEnum.NotInstall.ToInt()));
                if (payStatus)
                {
                    return new ApiResult()
                    {
                        Code = ResultCode.Failed,
                        Message = "到店存在未安装未付款的订单！"
                    };
                }
            }
            //修改到店状态已完结
            var updateShopArrivalStatus = await _arrivalRepo.UpdateShopArrivalStaus(ArrivalRecordStatusEnum.Finish.ToInt(), arrivaId, request?.Data?.UserName) > 0;
            if (!updateShopArrivalStatus)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "操作失败"
                };
            }
            return Result.Success();
        }

        /// <summary>
        /// 保存车辆记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SaveCarReport(SaveCarReportRequest request)
        {
            var result = await _shopArrivalCarReportRepository.InsertAsync(new ShopArrivalCarReportDO()
            {
                ArrivalId = request.ArrivalId,
                CarReportUrl = request.Url,
                CreateBy = request.CreateUser ?? string.Empty,
                CreateTime = DateTime.Now,
                IsDeleted = 0,

            });
            if (result > 0)
                return Result.Success("保存成功");
            return Result.Failed("保存失败");
        }

        public async Task<ApiResult<List<ShopArrivalVideoResponse>>> GetShopArrivalVideo(ShopArrivalVideoRequest request)
        {
            var shopArrivalVideos = await _shopArrivalVideoRepository.GetShopArrivalVideos(request);

            List<ShopArrivalVideoResponse> shopArrivalVideoResponses =
                _mapper.Map<List<ShopArrivalVideoResponse>>(shopArrivalVideos);

            return Result.Success<List<ShopArrivalVideoResponse>>(shopArrivalVideoResponses);

        }

        public async Task<ApiResult> SaveShopArrivalVideo(ApiRequest<SaveShopArrivalVideoRequest> request)
        {
            var insertId = await _shopArrivalVideoRepository.InsertAsync(new ShopArrivalVideoDO()
            {
                ArrivalId = request.Data.ArrivalId,
                FileSize = request.Data.FileSize,
                VideoPath = request.Data.VideoPath,
                VideoName = request.Data.VideoName,
                CreateBy = request.Data.CreateBy,
                Duration = request.Data.Duration,
                CreateTime = DateTime.Now
            });

            if (insertId > 0)
                return Result.Success<int>(insertId, "操作成功");
            return Result.Failed("操作失败");
        }

        public async Task<ApiResult> DeleteShopArrivalVideo(ApiRequest<DeleteShopArrivalVideoRequest> request)
        {
            var updateResult = await _shopArrivalVideoRepository.UpdateShopArrivalVideo(request.Data);
            if (updateResult)
                return Result.Success("操作成功");
            return Result.Failed("操作失败");

        }

        public async Task<ApiResult<List<ShopArrivalVideoResponse>>> GetShopArrivalVideoForOrder(GetShopArrivalVideoForOrderRequest request)
        {
            var getShopArrivalOrders = await _shopArrivalOrderRepository.GetShopArrivalOrder(new GetShopArrivalOrderRequest()
            {
                OrderNos = new List<string>(1) { request.OrderNo }
            });
            var shopArrivalId = getShopArrivalOrders?.Select(_ => _.ArrivalId)?.ToList()?.FirstOrDefault();

            var shopArrivalVideos = await _shopArrivalVideoRepository.GetShopArrivalVideos(new ShopArrivalVideoRequest()
            {
                ArrivalId = shopArrivalId ?? 0
            });
            List<ShopArrivalVideoResponse> shopArrivalVideoResponses =
                _mapper.Map<List<ShopArrivalVideoResponse>>(shopArrivalVideos);

            return Result.Success<List<ShopArrivalVideoResponse>>(shopArrivalVideoResponses);


        }

        public async Task<ApiResult<List<GetDispatchTechniciansResponse>>> GetShopEmployeeByJobIdPage(GetDispatchTechniciansRequest request)
        {
            var getTechnicians = await _shopManageClient.GetShopEmployeeByJobIdPage(new GetShopEmployeeByJobIdPageRequest()
            {
                EmployeeId = request.UserId,
                ShopId = request.ShopId,
                JobId=new List<int> {2,3,4,5 },
                PageIndex = 1,
                PageSize = 100
            });
            var getDispatchTechnicians = new List<GetDispatchTechniciansResponse>();
            getTechnicians?.ForEach(_ =>
            {
                int.TryParse(_.Level, out int level);
                if (_.JobName.Contains("技师"))
                {
                    getDispatchTechnicians.Add(new GetDispatchTechniciansResponse()
                    {
                        UserId = _.Id,
                        Name = _.Name,
                        Level = ((TechnicianLevelEnum)level).GetDescription()
                    }); ;
                }
                else
                {
                    getDispatchTechnicians.Add(new GetDispatchTechniciansResponse()
                    {
                        UserId = _.Id,
                        Name = _.Name,
                        Level =_.JobName
                    }); 
                }
               
            });
            return new ApiResult<List<GetDispatchTechniciansResponse>>()
            {
                Code = ResultCode.Success,
                Data = getDispatchTechnicians
            };
        }
    }
}
