using System;
using AutoMapper;
using Ae.Receive.Service.Common.Exceptions;
using Ae.Receive.Service.Core.Interfaces;
using Ae.Receive.Service.Core.Model;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Core.Response;
using Ae.Receive.Service.Dal.Model;
using Ae.Receive.Service.Dal.Repositorys;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ae.Receive.Service.Client.Inteface;
using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Core.Enums;
using Ae.Receive.Service.Common.Helper;
using Ae.Receive.Service.Client.Clients;
using ApolloErp.Web.WebApi;
using System.Linq;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using Ae.Receive.Service.Common.Constant;
using Dapper;
using ApolloErp.Common.BrandLogoHelper;
using Ae.Receive.Service.Client.Enum;
using Ae.Receive.Service.Client.Model.BaoYang;
using Ae.Receive.Service.Client.Request.Order;
using Ae.Receive.Service.Dal.Model.Extend;
using Ae.Receive.Service.Client.Model.Order;
using Ae.Receive.Service.Client.Request.User;
using Ae.Receive.Service.Client.Request.Vehicle;
using Ae.Receive.Service.Client.Model;
using Ae.Receive.Service.Core.Request.Arrival;
using Newtonsoft.Json;
using Ae.Receive.Service.Core.Request.Reserve;
using Ae.Receive.Service.Core.Model.Reserve;
using Ae.Receive.Service.Common.Extension;
using Ae.Receive.Service.Client.Response;

namespace Ae.Receive.Service.Imp.Services
{
    public class ShopReserveService : IShopReserveService
    {
        private readonly IShopReserveRepository _shopReserveRepository;
        private readonly IMapper _mapper;
        private readonly IShopReserveOrderRepository _shopReserveOrderRepository;
        private readonly IVehicleClient _vehicleClient;
        private readonly IOrderClient _orderClient;
        private readonly IShopManageClient _shopManageClient;
        private readonly IUserClient _userClient;
        private readonly IShopReserveTimeConfigRepository _shopReserveTimeConfigRepository;
        private readonly ApolloErpLogger<ShopReserveService> logger;
        private readonly IReserveTrackRepository _reserveTrackRepository;
        private readonly IBaoYangClient _baoYangClient;
        private readonly IShopReceiveRepository _shopReceiveRepository;
        private readonly IReservePictureRepository _reservePictureRepository;
        private readonly IConfiguration _configuration;
        private readonly IReserveTrackDetailRepository _reserveTrackDetailRepository;
        private readonly int reserveDelayMinute = 0;//预约预留时间分钟
        private readonly IReverseClient _reverseClient;
        private readonly IArrivalRepository _arrivalRepo;
        private readonly IProductClient _productClient;
        private readonly IShopReceiveService _shopReceiveService;

        public ShopReserveService(IShopReserveRepository shopReserveRepository,
            IShopReserveOrderRepository shopReserveOrderRepository,
            IVehicleClient vehicleClient,
            IOrderClient orderClient,
            IShopManageClient shopManageClient,
            IUserClient userClient,
            IShopReserveTimeConfigRepository shopReserveTimeConfigRepository,
            ApolloErpLogger<ShopReserveService> logger,
            IReserveTrackRepository reserveTrackRepository,
            IMapper mapper, IBaoYangClient baoYangClient, IShopReceiveRepository shopReceiveRepository,
            IReservePictureRepository reservePictureRepository, IConfiguration configuration,
            IReserveTrackDetailRepository reserveTrackDetailRepository, IReverseClient reverseClient,
            IArrivalRepository arrivalRepository,
            IProductClient productClient, IShopReceiveService shopReceiveService
            )
        {
            _shopReserveRepository = shopReserveRepository;
            _mapper = mapper;
            _shopReserveOrderRepository = shopReserveOrderRepository;
            _vehicleClient = vehicleClient;
            _orderClient = orderClient;
            _shopManageClient = shopManageClient;
            _userClient = userClient;
            _shopReserveTimeConfigRepository = shopReserveTimeConfigRepository;
            this.logger = logger;
            _reserveTrackRepository = reserveTrackRepository;
            _baoYangClient = baoYangClient;
            _shopReceiveRepository = shopReceiveRepository;
            _reservePictureRepository = reservePictureRepository;
            _configuration = configuration;
            _reserveTrackDetailRepository = reserveTrackDetailRepository;
            _reverseClient = reverseClient;
            _arrivalRepo = arrivalRepository;
            _productClient = productClient;
            _shopReceiveService = shopReceiveService;
        }

        /// <summary>
        /// 判断我的预约跳转类型
        /// 1：有预约记录 2：无订单预约 3：一条订单预约 4：多条订单预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<JudgeMyReserveResponse> JudgeMyReserve(JudgeMyReserveRequest request)
        {
            var response = new JudgeMyReserveResponse();
            var count = await _shopReserveRepository.GetReservedCount(request.UserId);
            if (count > 0)
            {
                response.Type = 1;
            }
            else
            {
                //获取未安装订单列表
                var paras = new GetUninstalledOrderListClientRequest()
                {
                    UserId = request.UserId
                };
                var getUninstalledOrderListClientResponse = await _orderClient.GetUninstalledOrderList(paras);
                if (getUninstalledOrderListClientResponse.IsNotNullSuccess())
                {
                    if (getUninstalledOrderListClientResponse.Data.TotalItems == 1)
                    {
                        response.Type = 3;
                    }
                    else
                    {
                        response.Type = 4;
                    }
                }
                else
                {
                    response.Type = 2;
                }
            }

            return response;
        }


        /// <summary>
        /// 获取可预约订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<CanReserveOrderDTO>> CanReserveOrderList(
            CanReserveOrderListRequest request)
        {
            //获取未安装订单列表
            var paras = new GetUninstalledOrderListClientRequest()
            {
                UserId = request.UserId,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            var getUninstalledOrderListClientResponse = await _orderClient.GetUninstalledOrderList(paras);
            if (getUninstalledOrderListClientResponse.IsNotNullSuccess())
            {
                List<CanReserveOrderDTO> reserveOrderList = new List<CanReserveOrderDTO>();
                foreach (var item in getUninstalledOrderListClientResponse.Data.Items)
                {
                    CanReserveOrderDTO reserveOrder = new CanReserveOrderDTO();
                    reserveOrder.OrderNo = item.OrderNo;
                    reserveOrder.OrderStatus = item.OrderStatusDisplayName;
                    reserveOrder.OrderTotalAmount = item.ActualAmount;
                    var productList = item.UninstalledOrderProducts.Where(o => o.ParentOrderPackagePid == 0).ToList();
                    foreach (var itemPro in productList)
                    {
                        reserveOrder.Number += itemPro.TotalNumber;
                        reserveOrder.Unit = itemPro.Unit;
                    }

                    reserveOrder.ReserveProductVOs = _mapper.Map<List<Core.Model.ReserveProductDTO>>(productList);

                    reserveOrderList.Add(reserveOrder);
                }

                ApiPagedResultData<CanReserveOrderDTO> result = new ApiPagedResultData<CanReserveOrderDTO>();
                result.Items = reserveOrderList;
                result.TotalItems = getUninstalledOrderListClientResponse.Data.TotalItems;
                return result;
            }

            return new ApiPagedResultData<CanReserveOrderDTO>();
        }

        /// <summary>
        /// 新增预约初始化
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReserveInitialResponse> ReserveInitial(ReserveInitialRequest request)
        {
            var carInfo = new MyCarInfoDTO();
            var reserveOrder = new CanReserveOrderDTO();
            ReserveShopInfoDTO ShopInfo = new ReserveShopInfoDTO();
            //门店默认服务
            var shopServiceItems = new List<ShopServiceDTO>()
            {
                new ShopServiceDTO()
                {
                    Code = "MaintenanceService", Name = "保养养护"
                },
                new ShopServiceDTO()
                {
                    Code = "TireService", Name = "轮胎服务"
                },
                new ShopServiceDTO()
                {
                    Code = "CarBeauty", Name = "美容洗车"
                },
            };
            //有订单
            if (!string.IsNullOrEmpty(request.OrderNo))
            {
                //根据订单号查询订单信息 车型信息
                var clientResult = _orderClient.GetCarByOrderNo(new GetCarByOrderNoClientRequest()
                { OrderNo = request.OrderNo });
                var orderResult = _orderClient.GetOrderDetail(new GetOrderDetailClientRequest()
                { OrderNo = request.OrderNo, UserId = request.UserId });
                await Task.WhenAll(clientResult, orderResult);
                if (clientResult.Result.IsNotNullSuccess() && clientResult.Result.Data != null)
                {
                    var car = clientResult.Result.Data;
                    carInfo.Brand = car.Brand;
                    carInfo.CarId = car.CarId;
                    carInfo.CarNO = car.CarNumber;
                    carInfo.Nian = car.Nian;
                    carInfo.PaiLiang = car.PaiLiang;
                    carInfo.SalesName = car.SalesName;
                    carInfo.Vehicle = car.Vehicle;
                    carInfo.VinNO = car.VinCode;
                    carInfo.CarLogo = CommonHelper.GetLogoUrlByName(car.Brand);
                }

                if (orderResult.Result.IsNotNullSuccess() && orderResult.Result.Data != null)
                {
                    var order = orderResult.Result.Data;
                    //门店信息
                    var shopSimpleInfo = _shopManageClient.GetShopSimpleInfo(new GetShopSimpleInfoClientRequest()
                    {
                        ShopId = order.ShopId
                    });
                    //门店服务大类
                    var shopServiceCategory =
                        _shopManageClient.GetShopServiceCategory(new GetShopSimpleInfoClientRequest()
                        { ShopId = order.ShopId });
                    await Task.WhenAll(shopSimpleInfo, shopServiceCategory);

                    if (shopSimpleInfo != null && shopSimpleInfo.Result != null)
                    {
                        ShopInfo.Id = order.ShopId;
                        ShopInfo.Address = shopSimpleInfo.Result.ShortAddress + shopSimpleInfo.Result.Address;
                        ShopInfo.SimpleName = shopSimpleInfo.Result.SimpleName;
                        ShopInfo.Telephone = shopSimpleInfo.Result.Telephone;

                    }

                    if (shopServiceCategory != null && shopServiceCategory.Result != null)
                    {
                        shopServiceItems.Clear();
                        shopServiceCategory.Result.ForEach(o => shopServiceItems.Add(new ShopServiceDTO()
                        {
                            Code = o.Code,
                            Name = o.Name
                        }));
                    }

                    //获取商品数量
                    int number = 0;
                    string unit = string.Empty;
                    List<Core.Model.ReserveProductDTO> ReserveProductVOs = new List<Core.Model.ReserveProductDTO>();
                    foreach (var item in order.Products)
                    {
                        if (item.PackageOrProduct != null)
                        {
                            //商品数量
                            number += item.PackageOrProduct.Number;
                            unit = item.PackageOrProduct.Unit;
                            Core.Model.ReserveProductDTO reserveProductDTO = new Core.Model.ReserveProductDTO()
                            {
                                ImageUrl = item.PackageOrProduct.ImageUrl,
                                ProductName = item.PackageOrProduct.ProductName
                            };
                            ReserveProductVOs.Add(reserveProductDTO);
                        }
                    }

                    reserveOrder.OrderNo = order.OrderNo;
                    reserveOrder.OrderStatus = order.DisplayOrderStatus;
                    reserveOrder.OrderTotalAmount = order.ActualAmount;
                    reserveOrder.Number = number;
                    reserveOrder.Unit = unit;
                    reserveOrder.ReserveProductVOs = ReserveProductVOs;
                }
            }
            else
            {

                carInfo = null;
                reserveOrder = null;
                if (request.ShopId > 0)
                {
                    var shopSimpleInfo = await _shopManageClient.GetShopSimpleInfo(new GetShopSimpleInfoClientRequest()
                    {
                        ShopId = request.ShopId
                    });
                    if (shopSimpleInfo != null)
                    {
                        ShopInfo.Id = request.ShopId;
                        ShopInfo.Address = shopSimpleInfo.ShortAddress + shopSimpleInfo.Address;
                        ShopInfo.SimpleName = shopSimpleInfo.SimpleName;
                        ShopInfo.Telephone = shopSimpleInfo.Telephone;
                    }
                    else
                    {
                        ShopInfo = null;
                    }
                }
            }

            //可预约日期
            List<ReserveDateDTO> ReserveDate = new List<ReserveDateDTO>();
            var dt = DateTime.Now.AddDays(1);
            for (int i = 0; i < 7; i++)
            {
                var currentDate = dt.AddDays(i);
                ReserveDateDTO date = new ReserveDateDTO()
                {
                    Year = currentDate.Year.ToString(),
                    ReserveDate = currentDate.ToString("MM-dd"),
                    ReserveWeekDay = CommonHelper.GetWeekDay(currentDate.DayOfWeek.ToString()),
                    OffsetValue = i + 1
                };
                ReserveDate.Add(date);
            }

            //节点时间
            int start = 9;
            int end = 18;
            List<ReserveTimeDTO> reserveTimes = new List<ReserveTimeDTO>();
            for (int i = 0; i < end - start; i++)
            {
                ReserveTimeDTO reserveTimeDTO = new ReserveTimeDTO()
                {
                    ReserveTime = start + i + ":00"
                };
                reserveTimes.Add(reserveTimeDTO);
            }


            //返回信息
            var response = new ReserveInitialResponse()
            {
                CanReserveDate = ReserveDate,
                CarInfo = carInfo,
                ReserveOrder = reserveOrder,
                ReserveTimeList = reserveTimes,
                ShopInfo = ShopInfo,
                ShopServiceItems = shopServiceItems,
                ReserveExplain = "预约成功后请提前10分组到店，若迟到10分钟以上需要重新排队或预约。" +
                                 "当预约时间内有其他车辆占用工位，您将是其后的第一顺位客户，技师会优先为您服务。" +
                                 "预约提交后，门店会第一时间和客户确认预约信息，确认通过后预约正式生效。车辆、门店、到店时间是必填项，请完整填写。"
            };
            return response;
        }
        /// <summary>
        /// 获取门店预约日期
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CanReserveDateResponse> CanReserveDate(BaseShopRequest request)
        {
            //可预约日期
            List<ReserveDateDTO> reserveDate = new List<ReserveDateDTO>();
            var dt = DateTime.Now.AddDays(1);
            for (int i = 0; i < 7; i++)
            {
                var currentDate = dt.AddDays(i);
                ReserveDateDTO date = new ReserveDateDTO()
                {
                    Year = currentDate.Year.ToString(),
                    ReserveDate = currentDate.ToString("MM-dd"),
                    ReserveWeekDay = CommonHelper.GetWeekDay(currentDate.DayOfWeek.ToString()),
                    OffsetValue = i + 1
                };
                reserveDate.Add(date);
            }

            //节点时间
            int start = 9;
            int end = 18;
            List<ReserveTimeDTO> reserveTimes = new List<ReserveTimeDTO>();
            for (int i = 0; i < end - start; i++)
            {
                ReserveTimeDTO reserveTimeDTO = new ReserveTimeDTO()
                {
                    ReserveTime = start + i + ":00"
                };
                reserveTimes.Add(reserveTimeDTO);
            }
            var ownerPhone = string.Empty;
            var shopInfo = await _shopManageClient.GetShopSimpleInfo(new GetShopSimpleInfoClientRequest { ShopId = request.ShopId });
            if (shopInfo != null)
            {
                ownerPhone = shopInfo.OwnerPhone;
            }
            return new CanReserveDateResponse()
            {
                ReserveDate = reserveDate,
                ReserveTime = reserveTimes,
                Phone = ownerPhone
            };
        }


        /// <summary>
        /// 获取上门养车门店预约日期+时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveDateDTO>> GetShopReserveDateTime(BaseShopRequest request)
        {

            var dt = DateTime.Today.AddDays(1);
            //可预约日期
            List<ReserveDateDTO> reserveDate = new List<ReserveDateDTO>();

            //节点时间
            var timeList = await CanReserveTime(new CanReserveTimeRequest { ShopId = request.ShopId, OffsetValue = 1 });
            foreach (var item in timeList)
            {
                ReserveDateDTO date = new ReserveDateDTO()
                {
                    Year = dt.Year.ToString(),
                    ReserveDate = dt.ToString("MM/dd"),
                    ReserveWeekDay = CommonHelper.GetWeekDay(dt.DayOfWeek.ToString()),
                    ReserveTime = item.ReserveTime,
                    //IsHighLight = item.IsHighLight
                };
                reserveDate.Add(date);
            }
            return reserveDate;
        }


        /// <summary>
        /// 新增预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddShopReserveAsync(AddReserveRequest request)
        {
            var result = Result.Failed();

            //新增数据
            var id = 0;
            try
            {
                var dt = DateTime.Now;
                //根据userId 查用户信息
                var userInfo = _userClient.GetUserInfo(new GetUserInfoClientRequest() { UserId = request.UserId }).Result;

                DateTime reserveTime = dt;
                if (!string.IsNullOrEmpty(request.ReserveTime))
                {
                    if (request.ReserveTime.Equals("IsWait"))
                    {
                        request.IsWait = 1;
                        reserveTime = DateTime.Parse(request.Year + "-" + request.ReserveDate);
                    }
                    else
                    {
                        request.IsWait = 0;
                        reserveTime =
                            DateTime.Parse(request.Year + "-" + request.ReserveDate + " " + request.ReserveTime);
                    }
                }

                //重新赋值
                ShopReserveDO shopReserveDO = new ShopReserveDO()
                {
                    ReserveTime = reserveTime,
                    ReserveNo = 1,
                    IsWait = request.IsWait,
                    IsAnyOrder = request.IsAnyOrder,
                    Status = 1,
                    Channel = 1,
                    UserId = request.UserId,
                    ShopId = request.ShopId,
                    ServiceCode = request.ServiceCode,
                    ServiceName = request.ServiceName,
                    CarId = request.CarId,
                    CreateTime = dt
                };

                if (userInfo != null)
                {
                    shopReserveDO.UserName = userInfo.UserName;
                    shopReserveDO.UserSex = CommonHelper.GetDescriptionByEnum((SexEnum)userInfo.Gender);
                    shopReserveDO.UserTel = userInfo.UserTel;
                    shopReserveDO.CreateBy = userInfo.UserName;
                }
                else
                {
                    return new ApiResult()
                    {
                        Code = ResultCode.Failed,
                        Message = CommonConstant.NoUser
                    };
                }

                //根据CarId 查车型信息
                var carInfo = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdClientRequest
                {
                    UserId = request.UserId,
                    CarId = request.CarId
                });
                if (carInfo != null)
                {
                    shopReserveDO.CarNo = carInfo.CarNumber;
                    shopReserveDO.Brand = carInfo.Brand;
                    shopReserveDO.Vehicle = carInfo.Vehicle;
                    shopReserveDO.PaiLiang = carInfo.PaiLiang;
                    shopReserveDO.Nian = carInfo.Nian;
                    shopReserveDO.SalesName = carInfo.SalesName;
                    shopReserveDO.VinNo = carInfo.VinCode;
                    shopReserveDO.CarLogo = CommonHelper.GetLogoUrlByName(carInfo.Brand);
                }


                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    id = await _shopReserveRepository.InsertAsync(shopReserveDO);
                    if (request.IsAnyOrder == 1 && !string.IsNullOrEmpty(request.OrderNo) && id > 0)
                    {
                        //有订单预约，添加订单
                        var shopReserveOrderDO = new ShopReserveOrderDO()
                        {
                            ReserveId = id,
                            OrderNo = request.OrderNo,
                            CreateBy = shopReserveDO.CreateBy,
                            CreateTime = dt
                        };
                        var exId = await _shopReserveOrderRepository.InsertAsync(shopReserveOrderDO);
                        //修改订单预约状态
                        //await _orderClient.UpdateOrderReserveStatus(new UpdateOrderReserveStatusClientRequest()
                        //{ OrderNo = request.OrderNo, ReserveStatus = 1, ReserveTime = dt });
                    }

                    var reserveTrackDO = new ReserveTrackDO()
                    {
                        ReserveId = id,
                        OptType = ReserveOptEnum.Create.ToString(),
                        Content = userInfo.UserName + "创建了预约记录",
                        CreateBy = userInfo.UserName,
                        CreateTime = dt
                    };
                    await AddReserveTrank(reserveTrackDO);

                    ts.Complete();
                }

                result = Result.Success("新增预约成功");
            }
            catch (Exception ex)
            {
                logger.Error($"AddShopReserveAsync", ex);
                result = Result.Exception("新增预约失败");
            }

            return result;
        }

        /// <summary>
        /// 新增预约V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> AddShopReserveV3Async(AddReserveV3Request request)
        {
            //新增数据
            long id = 0;
            var dt = DateTime.Now;
            DateTime reserveTime = dt;
            DateTime.TryParse(request.Year + "-" + request.Month + "-" + request.Day + " " + request.ReserveTime, out reserveTime);

            var orderNos = new List<string>();
            orderNos.Add(request.OrderNo);

            ///补录订单车型
            if (!string.IsNullOrEmpty(request.CarId)&&!string.IsNullOrEmpty(request.UserId))
                await _orderClient.UpdateOrderCar(new ApiRequest<UpdateOrderCarRequest>()
                {
                    Data = new UpdateOrderCarRequest()
                    {
                        OrderNo = request.OrderNo,
                        CarId = request.CarId,
                        UserId = request.UserId
                    }
                });


            //查用户信息
            var userInfo = _userClient.GetUserInfo(new GetUserInfoClientRequest() { UserId = request.UserId });

            //订单信息
            var orderBaseInfoResponse = _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest() { OrderNos = orderNos });


            //var orderBaseInfo = _orderClient.GetOrderDetailForMiniApp(new GetOrderDetailClientRequest()
            //{ OrderNo = request.OrderNo, UserId = request.UserId });

            //根据订单号查询车型信息
            var userCarInfo = _orderClient.GetOrderCarsInfo(new GetOrderCarsRequest { OrderNos = orderNos });
            await Task.WhenAll(userInfo, orderBaseInfoResponse, userCarInfo);
            var userCarInfoResult = userCarInfo.Result;
            var userResult = userInfo.Result;
            var orderBaseInfoResult = orderBaseInfoResponse.Result;


            //重新赋值
            ShopReserveDO shopReserveDO = new ShopReserveDO()
            {
                ReserveTime = reserveTime,
                Status = 1,
                Channel = 1,
                UserId = request.UserId,
                CreateTime = dt
            };

            if (userResult != null)
            {
                shopReserveDO.UserName = string.IsNullOrEmpty(userResult.UserName) ? userResult.NickName : userResult.UserName;
                shopReserveDO.UserSex = CommonHelper.GetDescriptionByEnum((SexEnum)userResult.Gender);
                shopReserveDO.UserTel = userResult.UserTel;
                shopReserveDO.CreateBy = userResult.UserName;
            }
            else
            {
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.NoUser
                };
            }
            //判断订单信息
            if (orderBaseInfoResult != null && orderBaseInfoResult.Any())
            {
                var orderBaseInfo = orderBaseInfoResult.FirstOrDefault();
                shopReserveDO.ShopId = orderBaseInfo.ShopId;
                var reserveType = TransServiceType(orderBaseInfo.OrderType);
                shopReserveDO.ServiceType = reserveType.ToString();
                shopReserveDO.ServiceName = CommonHelper.GetDescriptionByEnum(reserveType);
                shopReserveDO.Type = (sbyte)(orderBaseInfo.ProduceType == 5 ? 1 : 0);
                var orderAddress = (await _orderClient.GetOrderAddressList(new OrderAddressListClientRequest()
                { OrderIds = new List<long>() { orderBaseInfo.Id } })).FirstOrDefault();
                if (orderAddress != null)
                {
                    shopReserveDO.Address =
                        $"{orderAddress.Province}{orderAddress.City}{orderAddress.District}{orderAddress.DetailAddress}";
                }
            }
            else
            {
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.ErrorOrderInfo
                };
            }

            //判断车辆信息
            if (userCarInfoResult == null || userCarInfoResult.Data == null || !userCarInfoResult.Data.Any())
            {
                return new ApiResult()
                {
                    Code = ResultCode.Failed,
                    Message = CommonConstant.UserCarError
                };
            }
            var userCar = userCarInfoResult.Data.FirstOrDefault();

            //判断一人一车是否已有预约
            var reserveInfo = await _shopReserveRepository.GetTheDayReserveeInfo(request.UserId, reserveTime.DayOfYear - dt.DayOfYear, userCar.CarId, shopReserveDO.ShopId);
            if (reserveInfo != null)
            {
                id = reserveInfo.Id;
            }

            try
            {
                await _shopManageClient.AddShopUserRelation(new AddShopUserRelationRequest()
                {
                    ShopId = shopReserveDO.ShopId,
                    UserId = request.UserId,
                    SubmitBy = request.UserId,
                    Channel = "Consumer",
                    ReferrerType = "None"
                });
            }
            catch (Exception e)
            {
                logger.Info($"AddShopUserRelation_Error_AddReserveC ShopId={shopReserveDO.ShopId},UserId={request.UserId} ，Error={JsonConvert.SerializeObject(e)}");
            }

            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                shopReserveDO.CarNo = userCar.CarNumber;
                shopReserveDO.CarId = userCar.CarId;
                shopReserveDO.Brand = userCar.Brand;
                shopReserveDO.Nian = userCar.Nian;
                shopReserveDO.PaiLiang = userCar.PaiLiang;
                shopReserveDO.Vehicle = userCar.Vehicle;
                shopReserveDO.SalesName = userCar.SalesName;
                shopReserveDO.CarLogo = CommonHelper.GetLogoUrlByName(userCar.Brand);

                if (id <= 0)
                {
                    //新增预约记录
                    id = await AddReserve(shopReserveDO);
                    var reserveTrackDO = new ReserveTrackDO()
                    {
                        ReserveId = id,
                        OptType = ReserveOptEnum.Create.ToString(),
                        Content = userResult.UserName + "创建了预约记录",
                        CreateBy = userResult.UserName,
                        CreateTime = dt
                    };
                    await AddReserveTrank(reserveTrackDO);
                }

                //新增预约关联订单
                var shopReserveOrderDO = new ShopReserveOrderDO()
                {
                    ReserveId = id,
                    OrderNo = request.OrderNo,
                    CreateBy = shopReserveDO.CreateBy,
                    CreateTime = dt
                };
                var reserveOrderId = await _shopReserveOrderRepository.InsertAsync(shopReserveOrderDO);

                //修改订单预约状态
                await _orderClient.UpdateOrderReserveStatus(new UpdateOrderReserveStatusClientRequest()
                { OrderNo = request.OrderNo, ReserveStatus = 1, ReserveTime = dt });

              
                ts.Complete();
            }
            return Result.Success("新增预约成功");
        }

        private ReserveTypeEnum TransServiceType(int orderType)
        {
            switch (orderType)
            {
                case 1:
                    return ReserveTypeEnum.Tire;
                case 2:
                    return ReserveTypeEnum.BaoYang;
                case 3:
                    return ReserveTypeEnum.Wash;
                case 4:
                    return ReserveTypeEnum.SheetMetal;
                case 5:
                    return ReserveTypeEnum.CarModification;
                default:
                    return ReserveTypeEnum.Other;

            }
        }

        /// <summary>
        /// 添加预约处理记录
        /// </summary>
        /// <param name="reserveTrackDO"></param>
        /// <returns></returns>
        public async Task<int> AddReserveTrank(ReserveTrackDO reserveTrackDO)
        {
            return await _reserveTrackRepository.InsertAsync(reserveTrackDO);
        }

        /// <summary>
        /// 根据预约ID查询预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetReserveInfoResponse> GetReserveInfo(GetReserveInfoRequest request)
        {
            //查询预约表信息
            var reserveInfo = await _shopReserveRepository.GetReserveInfo(request.ReserveId, 0);
            if (reserveInfo == null)
            {
                return null;
            }

            //预约信息
            var reserveShopInfoDTO = new ReserveShopInfoDTO()
            {
                ReserveDate = reserveInfo.ReserveTime.ToString("MM-dd"),
                ReserveTime = reserveInfo.ReserveTime.ToString("HH:mm"),
                IsAnyOrder = reserveInfo.IsAnyOrder,
                IsWait = reserveInfo.IsWait,
                Code = reserveInfo.ServiceCode,
                Name = reserveInfo.ServiceName
            };
            //车型信息 
            var carInfo = new MyCarInfoDTO()
            {
                CarId = reserveInfo.CarId,
                Brand = reserveInfo.Brand,
                CarLogo = reserveInfo.CarLogo,
                CarNO = reserveInfo.CarNo,
                Nian = reserveInfo.Nian,
                PaiLiang = reserveInfo.PaiLiang,
                Vehicle = reserveInfo.Vehicle,
                SalesName = reserveInfo.SalesName
            };

            //查询门店信息
            var clientRequest = new GetShopSimpleInfoClientRequest()
            {
                ShopId = reserveInfo.ShopId
            };
            var shopSimpleInfo = _shopManageClient.GetShopSimpleInfo(clientRequest);
            //门店服务大类
            var shopServiceCategory = _shopManageClient.GetShopServiceCategory(clientRequest);
            await Task.WhenAll(shopSimpleInfo, shopServiceCategory);
            if (shopSimpleInfo != null && shopSimpleInfo.Result != null)
            {
                reserveShopInfoDTO.Id = reserveInfo.ShopId;
                reserveShopInfoDTO.Address = shopSimpleInfo.Result.ShortAddress + shopSimpleInfo.Result.Address;
                reserveShopInfoDTO.SimpleName = shopSimpleInfo.Result.SimpleName;
                reserveShopInfoDTO.Telephone = shopSimpleInfo.Result.Telephone;
            }

            //查询门店服务项目 是否选中
            var shopServiceItems = new List<ShopServiceDTO>();
            List<string> serviceCode = reserveInfo.ServiceCode.Split(";").ToList();
            foreach (var item in shopServiceCategory.Result)
            {
                var code = new ShopServiceDTO()
                {
                    Name = item.Name,
                    Code = item.Code
                };
                foreach (var itemCode in serviceCode)
                {
                    if (itemCode == item.Code)
                    {
                        code.IsCheck = true;
                    }
                }

                shopServiceItems.Add(code);
            }

            //根据订单号查询订单信息 单条订单
            CanReserveOrderDTO reserveOrder = new CanReserveOrderDTO();
            if (reserveInfo.IsAnyOrder == 1)
            {
                var orderResult = await _orderClient.GetOrderDetail(new GetOrderDetailClientRequest()
                { OrderNo = reserveInfo.OrderNO, UserId = reserveInfo.UserId });
                if (orderResult.IsNotNullSuccess() && orderResult.Data != null)
                {
                    //获取商品数量
                    int number = 0;
                    string unit = string.Empty;
                    List<Core.Model.ReserveProductDTO> ReserveProductVOs = new List<Core.Model.ReserveProductDTO>();
                    foreach (var item in orderResult.Data.Products)
                    {
                        if (item.PackageOrProduct != null)
                        {
                            //商品数量
                            number += item.PackageOrProduct.Number;
                            unit = item.PackageOrProduct.Unit;
                            Core.Model.ReserveProductDTO reserveProductDTO = new Core.Model.ReserveProductDTO()
                            {
                                ImageUrl = item.PackageOrProduct.ImageUrl,
                                ProductName = item.PackageOrProduct.ProductName
                            };
                            ReserveProductVOs.Add(reserveProductDTO);
                        }
                    }

                    reserveOrder.OrderNo = orderResult.Data.OrderNo;
                    reserveOrder.OrderStatus = orderResult.Data.DisplayOrderStatus;
                    reserveOrder.OrderTotalAmount = orderResult.Data.ActualAmount;
                    reserveOrder.Number = number;
                    reserveOrder.Unit = unit;
                    reserveOrder.ReserveProductVOs = ReserveProductVOs;
                }
            }

            //预约日期
            List<ReserveDateDTO> ReserveDate = new List<ReserveDateDTO>();
            var dt = DateTime.Now.AddDays(1);
            for (int i = 0; i < 7; i++)
            {
                var currentDate = dt.AddDays(i);
                ReserveDateDTO date = new ReserveDateDTO()
                {
                    Year = currentDate.Year.ToString(),
                    ReserveDate = currentDate.ToString("MM-dd"),
                    ReserveWeekDay = CommonHelper.GetWeekDay(currentDate.DayOfWeek.ToString()),
                    OffsetValue = i + 1
                };
                if (reserveInfo.ReserveTime.ToString("MM-dd") == date.ReserveDate)
                {
                    date.IsHighLight = true;
                }

                ReserveDate.Add(date);
            }

            int day = reserveInfo.ReserveTime.DayOfYear - DateTime.Now.DayOfYear;
            //时间节点
            List<ReserveTimeDTO> reserveTimeList = await CanReserveTime(new CanReserveTimeRequest()
            {
                ShopId = reserveInfo.ShopId,
                ReserveId = reserveInfo.Id,
                OffsetValue = day,
                Year = reserveInfo.ReserveTime.Year.ToString(),
                ReserveDate = reserveInfo.ReserveTime.ToString("MM-dd"),
            });


            //返回数据
            var response = new GetReserveInfoResponse();
            response.ShopInfo = reserveShopInfoDTO;
            response.CanReserveDate = ReserveDate;
            response.CarInfo = carInfo;
            response.ReserveTimeList = reserveTimeList;
            response.ReserveOrders = reserveOrder;
            response.ShopServiceItems = shopServiceItems;
            response.ReserveExplain = "预约成功后请提前10分组到店，若迟到10分钟以上需要重新排队或预约。" +
                                      "当预约时间内有其他车辆占用工位，您将是其后的第一顺位客户，技师会优先为您服务。" +
                                      "预约提交后，门店会第一时间和客户确认预约信息，确认通过后预约正式生效。车辆、门店、到店时间是必填项，请完整填写。";
            return response;
        }


        /// <summary>
        /// 根据预约ID查询预约详情V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetReserveInfoV3Response>> GetReserveInfoV3(GetReserveInfoV3Request request)
        {
            if (request.ReserveId == 0 && string.IsNullOrWhiteSpace(request.OrderNo))
            {
                throw new CustomException("参数错误");
            }
            //查询预约信息
            var reserveInfo = await _shopReserveRepository.GetReserveInfo(request.ReserveId, 0, request.OrderNo);
            if (reserveInfo == null)
            {
                if (request.ReserveId > 0)
                {
                    throw new CustomException(CommonConstant.NoReserveInfo);
                }
            }

            DateTime nowDay = DateTime.Today;
            long reserveId = 0;//预约ID
            long shopId = 0;//门店ID
            var simpleName = string.Empty;//门店名称
            //预约日期
            int year = 0;
            int month = 0;
            int day = 0;
            var reserveTime = string.Empty;//预约时间点
            var orderNos = new List<string>();//订单号

            var carInfo = new MyCarInfoDTO();

            if (reserveInfo != null)
            {
                reserveId = reserveInfo.Id;
                shopId = reserveInfo.ShopId;
                //车型信息 
                carInfo.CarId = reserveInfo.CarId;
                carInfo.Brand = reserveInfo.Brand;
                carInfo.CarLogo = reserveInfo.CarLogo;
                carInfo.CarNO = reserveInfo.CarNo;
                carInfo.Nian = reserveInfo.Nian;
                carInfo.PaiLiang = reserveInfo.PaiLiang;
                carInfo.Vehicle = reserveInfo.Vehicle;
                carInfo.SalesName = reserveInfo.SalesName;
                year = reserveInfo.ReserveTime.Year;
                month = reserveInfo.ReserveTime.Month;
                day = reserveInfo.ReserveTime.Day;
                reserveTime = reserveInfo.ReserveTime.ToString("HH:mm");
                if (request.ReserveId > 0)
                {
                    //预约关联订单
                    var reserveOrderList = await _shopReserveOrderRepository.GetReserveOrderByReserveId(request.ReserveId);
                    if (reserveOrderList != null && reserveOrderList.Any())
                    {
                        orderNos = reserveOrderList.Select(_ => _.OrderNo).ToList();
                    }
                    else
                    {
                        throw new CustomException("该预约暂无关联订单");
                    }
                }
                else
                {
                    orderNos.Add(request.OrderNo);
                    //根据订单号查询车型信息
                    var userCarInfo = await _orderClient.GetOrderCarsInfo(new GetOrderCarsRequest { OrderNos = orderNos });
                    if (userCarInfo != null && userCarInfo.Data != null && userCarInfo.Data.Any())
                    {
                        var userCar = userCarInfo.Data.FirstOrDefault();
                        carInfo.CarId = userCar.CarId;
                        carInfo.Brand = userCar.Brand;
                        carInfo.Nian = userCar.Nian;
                        carInfo.PaiLiang = userCar.PaiLiang;
                        carInfo.Vehicle = userCar.Vehicle;
                        carInfo.SalesName = userCar.SalesName;
                    }
                }
            }
            else
            {
                orderNos.Add(request.OrderNo);
                //根据订单号查询车型信息
                var userCarInfo = await _orderClient.GetOrderCarsInfo(new GetOrderCarsRequest { OrderNos = orderNos });
                if (userCarInfo != null && userCarInfo.Data != null && userCarInfo.Data.Any())
                {
                    var userCar = userCarInfo.Data.FirstOrDefault();
                    carInfo.CarId = userCar.CarId;
                    carInfo.Brand = userCar.Brand;
                    carInfo.Nian = userCar.Nian;
                    carInfo.PaiLiang = userCar.PaiLiang;
                    carInfo.Vehicle = userCar.Vehicle;
                    carInfo.SalesName = userCar.SalesName;
                }
            }

            #region  订单号查询订单商品信息

            List<GetOrderListForMiniAppResponse> orderList = new List<GetOrderListForMiniAppResponse>();
            var orders = await _orderClient.BatchGetOrderListForMiniApp(new BatchGetOrderListForMiniAppRequest()
            {
                OrderNos = orderNos,
                MiniAppType = 1
            });
            if (orders.IsNotNullSuccess() && orders.Data.Any())
            {
                orderList = orders.Data;
            }
            else
            {
                throw new CustomException("订单信息异常");
            }
            //订单信息
            List<CanReserveOrderDTO> reserveOrderItems = new List<CanReserveOrderDTO>();
            if (reserveInfo == null)
            {
                shopId = orderList.FirstOrDefault().ShopId;
            }

            foreach (var item in orderList)
            {
                CanReserveOrderDTO reserveOrder = new CanReserveOrderDTO();
                //获取商品数量
                string unit = string.Empty;
                List<ReserveProductDTO> ReserveProductVOs = new List<ReserveProductDTO>();
                foreach (var itemProduct in item.Products)
                {
                    unit = itemProduct.Unit;
                    ReserveProductDTO reserveProductDTO = new ReserveProductDTO()
                    {
                        ImageUrl = itemProduct.ImageUrl,
                        ProductName = itemProduct.DisplayName,
                        Number = itemProduct.Number,
                        Price = itemProduct.Price,
                        Unit = itemProduct.Unit,
                        TotalAmount = itemProduct.Price * itemProduct.Number
                    };
                    ReserveProductVOs.Add(reserveProductDTO);
                }
                reserveOrder.OrderNo = item.OrderNo;
                reserveOrder.OrderStatus = item.DisplayOrderStatus;
                reserveOrder.OrderTotalAmount = item.ActualAmount;
                reserveOrder.Number = item.ListTotalProductNum;
                reserveOrder.Unit = unit;
                reserveOrder.ReserveProductVOs = ReserveProductVOs;

                reserveOrderItems.Add(reserveOrder);
            }
            #endregion

            #region 日历信息
            DateTime tomorrow = nowDay.AddDays(1);//获取明天
            DateTime firstDay = new DateTime(tomorrow.Year, tomorrow.Month, 1);//第一天
            DateTime lastDay = firstDay.AddMonths(2); //两个月后第一天
            var reserveCountList = _shopReserveRepository.GetReservedCountDay(shopId, firstDay, lastDay);
            var configList = _shopReserveTimeConfigRepository.GetReserveTimeConfigByShopId(shopId, firstDay.ToString("yyyy-MM-dd"), lastDay.ToString("yyyy-MM-dd"));
            //门店信息
            var shopInfo = _shopManageClient.GetShopSimpleInfo(new GetShopSimpleInfoClientRequest()
            {
                ShopId = shopId
            });

            await Task.WhenAll(reserveCountList, configList, shopInfo);
            var reserveListResult = reserveCountList.Result;
            var configListResult = configList.Result;
            var shopInfoResult = shopInfo.Result;
            //两个月日历
            var reserveSurplus = new List<ReserveSurplusContentDTO>();
            //置灰
            List<string> surplusDisabled = new List<string>();
            int[] reserveBegin = new int[3] { tomorrow.Year, tomorrow.Month, tomorrow.Day };
            int[] reserveEnd = new int[3];
            for (int i = 0; i < 2; i++)
            {
                DateTime monthDate = firstDay.AddMonths(i);
                int lastday = firstDay.AddMonths(i + 1).AddDays(-1).Day; //获取当月的最后一天
                reserveEnd[0] = monthDate.Year;
                reserveEnd[1] = monthDate.Month;
                reserveEnd[2] = lastday;
                for (int n = 1; n <= lastday; n++)
                {

                    DateTime currentDay = new DateTime(monthDate.Year, monthDate.Month, n);
                    if (currentDay > nowDay)
                    {
                        //当前配置
                        var currentConfigList = configListResult.Where(o => o.YearDay == currentDay.ToString("yyyy-MM-dd")).ToList();
                        bool haveValue = false;
                        int configCount = 0;
                        if (currentConfigList != null && currentConfigList.Any())
                        {
                            haveValue = true;
                            configCount = currentConfigList.FirstOrDefault().ReserveCount;
                        }
                        //已预约
                        var currentReserveList = reserveListResult.Where(o => o.ReserveDate == currentDay.ToString("yyyy-MM-dd")).ToList();
                        int reserveCount = 0;
                        if (currentReserveList != null && currentReserveList.Any())
                        {
                            reserveCount = currentReserveList.FirstOrDefault().ReserveCount;
                        }

                        if (haveValue)
                        {
                            if (configCount - reserveCount > 0)
                            {
                                ReserveSurplusContentDTO reserveSurplusDTO = new ReserveSurplusContentDTO();
                                reserveSurplusDTO.Date = monthDate.Year + "-" + monthDate.Month + "-" + n;
                                reserveSurplusDTO.Content = "剩余" + (configCount - reserveCount);
                                reserveSurplus.Add(reserveSurplusDTO);
                            }
                            else
                            {
                                surplusDisabled.Add(currentDay.ToString("yyyy-MM-dd"));
                            }
                        }
                        else
                        {
                            int weekDay = CommonHelper.GetWeekDayNum(currentDay.DayOfWeek.ToString());
                            currentConfigList = configListResult.Where(o => o.WeekDay == weekDay).ToList();
                            if (currentConfigList != null && currentConfigList.Any())
                            {
                                configCount = currentConfigList.FirstOrDefault().ReserveCount;
                                if (configCount - reserveCount > 0)
                                {
                                    ReserveSurplusContentDTO reserveSurplusDTO = new ReserveSurplusContentDTO();
                                    reserveSurplusDTO.Date = monthDate.Year + "-" + monthDate.Month + "-" + n;
                                    reserveSurplusDTO.Content = "剩余" + (configCount - reserveCount);
                                    reserveSurplus.Add(reserveSurplusDTO);
                                }
                                else
                                {
                                    surplusDisabled.Add(currentDay.ToString("yyyy-MM-dd"));
                                }
                            }
                            else
                            {
                                surplusDisabled.Add(currentDay.ToString("yyyy-MM-dd"));
                            }
                        }
                    }
                }
            }

            #endregion
            //门店信息
            if (shopInfoResult != null)
            {
                simpleName = shopInfoResult.SimpleName;
            }
            else
            {
                throw new CustomException("门店信息异常");
            }

            #region 时间节点初始化

            var canReserveTime = new CanReserveTimeV3Request()
            {
                OffsetValue = 1,
                ShopId = shopId,
                Type = 0
            };
            List<ReserveTimeDTO> reserveTimeList = await CanReserveTimeV3(new CanReserveTimeV3Request
            {
                OffsetValue = 1,
                ShopId = shopId,
                Type = 0
            });

            #endregion

            //返回数据
            var response = new GetReserveInfoV3Response();
            response.ShopId = shopId;
            response.SimpleName = simpleName;
            response.CarInfo = carInfo;
            response.ReserveId = reserveId;
            response.Year = year;
            response.Month = month;
            response.Day = day;
            response.ReserveTime = reserveTime;
            response.ReserveTimeList = reserveTimeList;
            response.ReserveOrders = reserveOrderItems;
            response.Items = reserveSurplus;
            response.Disabled = surplusDisabled.ToArray();
            response.SurplusBegin = reserveBegin;
            response.SurplusEnd = reserveEnd;
            response.ReserveExplain = "1 预约成功后请提前10分钟到店，若迟到10分钟以上需要重新排队或预约。" +
                                      "2 当预定时间内有其他车辆占用工位，您将是其后的第一顺位客户，技师会优先为您服务。";
            return new ApiResult<GetReserveInfoV3Response>()
            {
                Data = response,
                Code = ResultCode.Success,
                Message = CommonConstant.OperateSuccess
            };
        }

        /// <summary>
        /// 预约日历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetReserveSurplusResponse> GetShopReserveSurplus(BaseShopRequest request)
        {
            DateTime nowDay = DateTime.Today;
            DateTime tomorrow = nowDay.AddDays(1);//获取明天
            DateTime firstDay = new DateTime(tomorrow.Year, tomorrow.Month, 1);//第一天
            DateTime lastDay = firstDay.AddMonths(2); //两个月后第一天
            var reserveCountList = _shopReserveRepository.GetReservedCountDay(request.ShopId, firstDay, lastDay);
            var configList = _shopReserveTimeConfigRepository.GetReserveTimeConfigByShopId(request.ShopId, firstDay.ToString("yyyy-MM-dd"), lastDay.ToString("yyyy-MM-dd"));

            await Task.WhenAll(reserveCountList, configList);
            var reserveListResult = reserveCountList.Result;
            var configListResult = configList.Result;

            #region
            //两个月日历
            var reserveSurplus = new List<ReserveSurplusContentDTO>();
            //置灰
            List<string> surplusDisabled = new List<string>();
            int[] reserveBegin = new int[3] { tomorrow.Year, tomorrow.Month, tomorrow.Day };
            int[] reserveEnd = new int[3];
            for (int i = 0; i < 2; i++)
            {
                DateTime monthDate = firstDay.AddMonths(i);
                int lastday = firstDay.AddMonths(i + 1).AddDays(-1).Day; //获取当月的最后一天
                reserveEnd[0] = monthDate.Year;
                reserveEnd[1] = monthDate.Month;
                reserveEnd[2] = lastday;
                for (int n = 1; n <= lastday; n++)
                {

                    DateTime currentDay = new DateTime(monthDate.Year, monthDate.Month, n);
                    if (currentDay > nowDay)
                    {
                        //当前配置
                        var currentConfigList = configListResult.Where(o => o.YearDay == currentDay.ToString("yyyy-MM-dd")).ToList();
                        bool haveValue = false;
                        int configCount = 0;
                        if (currentConfigList != null && currentConfigList.Any())
                        {
                            haveValue = true;
                            configCount = currentConfigList.FirstOrDefault().ReserveCount;
                        }
                        //已预约
                        var currentReserveList = reserveListResult.Where(o => o.ReserveDate == currentDay.ToString("yyyy-MM-dd")).ToList();
                        int reserveCount = 0;
                        if (currentReserveList != null && currentReserveList.Any())
                        {
                            reserveCount = currentReserveList.FirstOrDefault().ReserveCount;
                        }

                        if (haveValue)
                        {
                            if (configCount - reserveCount > 0)
                            {
                                ReserveSurplusContentDTO reserveSurplusDTO = new ReserveSurplusContentDTO();
                                reserveSurplusDTO.Date = monthDate.Year + "-" + monthDate.Month + "-" + n;
                                reserveSurplusDTO.Content = "剩余" + (configCount - reserveCount);
                                reserveSurplus.Add(reserveSurplusDTO);
                            }
                            else
                            {
                                surplusDisabled.Add(currentDay.ToString("yyyy-MM-dd"));
                            }
                        }
                        else
                        {
                            int weekDay = CommonHelper.GetWeekDayNum(currentDay.DayOfWeek.ToString());
                            currentConfigList = configListResult.Where(o => o.WeekDay == weekDay).ToList();
                            if (currentConfigList != null && currentConfigList.Any())
                            {
                                configCount = currentConfigList.FirstOrDefault().ReserveCount;
                                if (configCount - reserveCount > 0)
                                {
                                    ReserveSurplusContentDTO reserveSurplusDTO = new ReserveSurplusContentDTO();
                                    reserveSurplusDTO.Date = monthDate.Year + "-" + monthDate.Month + "-" + n;
                                    reserveSurplusDTO.Content = "剩余" + (configCount - reserveCount);
                                    reserveSurplus.Add(reserveSurplusDTO);
                                }
                                else
                                {
                                    surplusDisabled.Add(currentDay.ToString("yyyy-MM-dd"));
                                }
                            }
                            else
                            {
                                surplusDisabled.Add(currentDay.ToString("yyyy-MM-dd"));
                            }
                        }
                    }
                }
            }

            #endregion
            #region 时间节点初始化

            var canReserveTime = new CanReserveTimeV3Request()
            {
                OffsetValue = 1,
                ShopId = request.ShopId,
                Type = 0
            };
            List<ReserveTimeDTO> reserveTimeList = await CanReserveTimeV3(new CanReserveTimeV3Request
            {
                OffsetValue = 1,
                ShopId = request.ShopId,
                Type = 0
            });

            #endregion

            return new GetReserveSurplusResponse()
            {
                ReserveSurplus = reserveSurplus,
                ReserveTimeList = reserveTimeList,
                SurplusBegin = reserveBegin.ToArray(),
                SurplusEnd = reserveEnd.ToArray(),
                Disabled = surplusDisabled.ToArray(),
                ReserveExplain = "预约成功后请提前10分组到店，若迟到10分钟以上需要重新排队或预约。" +
                                 "当预约时间内有其他车辆占用工位，您将是其后的第一顺位客户，技师会优先为您服务。" +
                                 "预约提交后，门店会第一时间和客户确认预约信息，确认通过后预约正式生效。车辆、门店、到店时间是必填项，请完整填写。"
            };
        }

        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> ModifyReserve(ModifyReserveRequest request)
        {
            //查询预约表信息
            var reserveInfo = await _shopReserveRepository.GetReserveInfo(request.ReserveId, 0);
            if (reserveInfo == null)
            {
                return false;
            }

            var updateReserve = _mapper.Map<UpdateReserveDTO>(request);
            updateReserve.UpdateTime = DateTime.Now;
            //拼接预约时间
            if (request.ReserveTime.Equals("IsWait"))
            {
                updateReserve.IsWait = 1;
                updateReserve.ReserveDateTime = DateTime.Parse(request.Year + "-" + request.ReserveDate);
            }
            else
            {
                updateReserve.ReserveDateTime =
                    DateTime.Parse(request.Year + "-" + request.ReserveDate + " " + request.ReserveTime);
            }

            //根据CarId 查车型信息
            if (!string.IsNullOrEmpty(request.CarId))
            {
                var carInfo = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdClientRequest
                {
                    UserId = request.UserId,
                    CarId = request.CarId
                });
                if (carInfo != null)
                {
                    var logo = CommonHelper.GetLogoUrlByName(carInfo.Brand);
                    updateReserve.CarNo = carInfo.CarNumber;
                    updateReserve.Brand = carInfo.Brand;
                    updateReserve.Vehicle = carInfo.Vehicle;
                    updateReserve.PaiLiang = carInfo.PaiLiang;
                    updateReserve.Nian = carInfo.Nian;
                    updateReserve.SalesName = carInfo.SalesName;
                    updateReserve.CarLogo = logo;
                }
            }

            bool isSuccess = false;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                isSuccess = await _shopReserveRepository.UpdateReserve(updateReserve);
                if (isSuccess && !string.IsNullOrEmpty(reserveInfo.OrderNO))
                {
                    //修改订单预约状态
                    await _orderClient.UpdateOrderReserveStatus(new UpdateOrderReserveStatusClientRequest()
                    { OrderNo = reserveInfo.OrderNO, ReserveStatus = 1, ReserveTime = updateReserve.UpdateTime });
                }

                var reserveTrackDO = new ReserveTrackDO()
                {
                    ReserveId = request.ReserveId,
                    OptType = ReserveOptEnum.Modify.ToString(),
                    Content = updateReserve.UpdateBy + "修改了预约记录",
                    CreateBy = updateReserve.UpdateBy,
                    CreateTime = DateTime.Now
                };
                await AddReserveTrank(reserveTrackDO);
                ts.Complete();
            }

            return isSuccess;
        }


        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> ModifyReserveTime(ModifyReserveTimeRequest request)
        {
            var result = Result.Failed();
            //查询预约表信息
            var reserveInfo = await _shopReserveRepository.GetReserveInfo(request.ReserveId, 0);
            if (reserveInfo == null)
            {
                return result;
            }
            DateTime.TryParse(request.Year + "-" + request.Month + "-" + request.Day + " " + request.ReserveTime, out var reserveTime);

            var updateReserve = new UpdateReserveDTO();
            updateReserve.ReserveId = request.ReserveId;
            updateReserve.UpdateBy = request.UpdateBy;
            updateReserve.UpdateTime = DateTime.Now;
            updateReserve.ReserveDateTime = reserveTime;

            bool isSuccess = false;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                isSuccess = await _shopReserveRepository.UpdateReserve(updateReserve);
                var reserveTrackDO = new ReserveTrackDO()
                {
                    ReserveId = request.ReserveId,
                    OptType = ReserveOptEnum.Modify.ToString(),
                    Content = updateReserve.UpdateBy + "修改了预约记录",
                    CreateBy = updateReserve.UpdateBy,
                    CreateTime = DateTime.Now
                };
                await AddReserveTrank(reserveTrackDO);
                ts.Complete();
            }
            if (isSuccess)
            {
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = CommonConstant.OperateSuccess
                };
            }
            return result;
        }

        /// <summary>
        /// 修改预约状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> UpdateReserveStatus(UpdateReserveStatusRequest request)
        {
            //预约状态 枚举类型
            var status = (int)request.ReserveStatus;
            var result = await _shopReserveRepository.UpdateReserveStatus(request.ReserveId, status, request.UpdateBy);
            return result;
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> CancelReserve(CancelReserveRequest request)
        {
            bool isSuccess = false;
            //查询预约表信息
            var reserveInfo = await _shopReserveRepository.GetReserveInfo(request.ReserveId, 0);
            if (reserveInfo != null)
            {
                isSuccess = await _shopReserveRepository.CancelReserve(request);
                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (isSuccess && !string.IsNullOrEmpty(reserveInfo.OrderNO))
                    {
                        //修改订单预约状态
                        await _orderClient.UpdateOrderReserveStatus(new UpdateOrderReserveStatusClientRequest()
                        { OrderNo = reserveInfo.OrderNO, ReserveStatus = 0, ReserveTime = reserveInfo.ReserveTime });
                    }

                    ts.Complete();
                }
            }

            return isSuccess;
        }

        /// <summary>
        /// 获取可预约时间节点
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeDTO>> CanReserveTime(CanReserveTimeRequest request)
        {
            var reserveInfo = new ReserveInfoDO();
            if (request.ReserveId > 0)
            {
                //查询预约表信息
                reserveInfo = await _shopReserveRepository.GetReserveInfo(request.ReserveId, 0);
                if (reserveInfo == null)
                {
                    throw new CustomException("该预约不存在");
                }
            }

            //营业时间
            int start = 9;
            int end = 21;
            List<ReserveTimeDTO> reserveTimeList = new List<ReserveTimeDTO>();
            for (int i = 0; i < end - start; i++)
            {
                ReserveTimeDTO reserveTimeDTO = new ReserveTimeDTO()
                {
                    ReserveTime = start + i + ":00"
                };
                reserveTimeList.Add(reserveTimeDTO);
            }

            DateTime reserveStart = DateTime.Today;
            if (request.OffsetValue > 0)
            {
                reserveStart = reserveStart.AddDays(request.OffsetValue);
            }
            else
            {
                reserveStart = DateTime.Parse(request.Year + "-" + request.ReserveDate);
            }

            string reserveDate = reserveStart.ToString("yyyy-MM-dd");
            int weekDay = CommonHelper.GetWeekDayNum(reserveStart.DayOfWeek.ToString());
            //查询时间点配置信息
            var shopReserveTimeConfigList =
                await _shopReserveTimeConfigRepository.GetReserveTimeConfigByDateAsync(request.ShopId, weekDay,
                    reserveDate);
            if (shopReserveTimeConfigList != null && shopReserveTimeConfigList.Any())
            {
                //查询门店某一天各个时间点已预约的数量
                var reserveCountList =
                    await _shopReserveRepository.GetReservedCountByShopId(request.ShopId, reserveStart);
                foreach (var item in reserveTimeList)
                {
                    foreach (var configItem in shopReserveTimeConfigList)
                    {
                        //选中，高亮
                        item.IsHighLight = true;
                        string reserveTime = reserveInfo.ReserveTime.ToString("HH:mm");
                        if (request.ReserveId > 0 && reserveInfo.ReserveTime.DayOfYear == reserveStart.DayOfYear)
                        {
                            if (configItem.StartTime.Equals(reserveTime) && item.ReserveTime.Equals(reserveTime))
                            {
                                item.IsChecked = true;
                            }

                            if (reserveInfo.IsWait == 1 && item.ReserveTime.Equals("到店等待"))
                            {
                                item.IsChecked = true;
                            }
                        }

                        //超出预约数量时置灰
                        foreach (var countItem in reserveCountList)
                        {
                            if (countItem.ReserveCount >= configItem.ReserveCount)
                            {
                                item.IsHighLight = false;
                            }
                        }
                    }
                }
            }

            return reserveTimeList;
        }

        /// <summary>
        /// 获取可预约时间节点V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeDTO>> CanReserveTimeV2(CanReserveTimeRequest request)
        {
            //营业时间
            int start = 9;
            int end = 18;
            List<ReserveTimeDTO> reserveTimeList = new List<ReserveTimeDTO>();
            for (int i = 0; i < end - start; i++)
            {
                var startTime = string.Empty;
                var time = start + i;
                if (time < 10)
                {
                    startTime = "0" + time + ":00";
                }
                else
                {
                    startTime = time + ":00";
                }
                ReserveTimeDTO reserveTimeDTO = new ReserveTimeDTO()
                {
                    ReserveTime = startTime
                };
                reserveTimeList.Add(reserveTimeDTO);
            }

            DateTime reserveStart = DateTime.Today;
            if (request.OffsetValue > 0)
            {
                reserveStart = reserveStart.AddDays(request.OffsetValue);
            }

            string reserveDate = reserveStart.ToString("yyyy-MM-dd");
            int weekDay = CommonHelper.GetWeekDayNum(reserveStart.DayOfWeek.ToString());
            //查询时间点配置信息
            var shopReserveTimeConfigList =
                await _shopReserveTimeConfigRepository.GetReserveTimeConfigByDateAsync(request.ShopId, weekDay,
                    reserveDate);
            if (shopReserveTimeConfigList != null && shopReserveTimeConfigList.Any())
            {
                //查询门店某一天各个时间点已预约的数量
                var reserveCountList =
                    await _shopReserveRepository.GetReservedCountByShopId(request.ShopId, reserveStart);
                foreach (var item in reserveTimeList)
                {
                    foreach (var configItem in shopReserveTimeConfigList)
                    {
                        if (item.ReserveTime.Equals(configItem.StartTime))
                        {
                            //选中，高亮
                            if (configItem.ReserveCount > 0)
                            {
                                item.IsHighLight = true;
                            }
                            string reserveTime = string.Empty;
                            //超出预约数量时置灰
                            foreach (var countItem in reserveCountList)
                            {
                                reserveTime = countItem.ReserveTime.ToString("HH:mm");
                                if (item.ReserveTime.Equals(reserveTime) && countItem.ReserveCount >= configItem.ReserveCount)
                                {
                                    item.IsHighLight = false;
                                }
                            }
                        }

                    }
                }
            }

            return reserveTimeList;
        }

        /// <summary>
        /// 获取可预约时间节点V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeDTO>> CanReserveTimeV3(CanReserveTimeV3Request request)
        {
            //营业时间
            int start = 9;
            int end = 21;
            List<ReserveTimeDTO> reserveTimeList = new List<ReserveTimeDTO>();
            for (int i = 0; i <= end - start; i++)
            {
                var startTime = string.Empty;
                var time = start + i;
                if (time < 10)
                {
                    startTime = "0" + time + ":00";
                }
                else
                {
                    startTime = time + ":00";
                }
                ReserveTimeDTO reserveTimeDTO = new ReserveTimeDTO()
                {
                    ReserveTime = startTime
                };
                reserveTimeList.Add(reserveTimeDTO);
            }
            DateTime reserveStart = DateTime.Today;
            if (request.Type > 0)
            {
                DateTime reserveTime = new DateTime(request.Year, request.Month, request.Day);
                request.OffsetValue = reserveTime.DayOfYear - reserveStart.DayOfYear;
                if (request.OffsetValue <= 0)
                {
                    return reserveTimeList;
                }
            }
            else
            {
                return reserveTimeList;
            }
            reserveStart = reserveStart.AddDays(request.OffsetValue);

            string reserveDate = reserveStart.ToString("yyyy-MM-dd");
            int weekDay = CommonHelper.GetWeekDayNum(reserveStart.DayOfWeek.ToString());
            //查询时间点配置信息
            var shopReserveTimeConfigList =
                await _shopReserveTimeConfigRepository.GetReserveTimeConfigByDateAsync(request.ShopId, weekDay,
                    reserveDate);
            if (shopReserveTimeConfigList != null && shopReserveTimeConfigList.Any())
            {
                //查询门店某一天各个时间点已预约的数量
                var reserveCountList =
                    await _shopReserveRepository.GetReservedCountByShopId(request.ShopId, reserveStart);
                foreach (var item in reserveTimeList)
                {
                    foreach (var configItem in shopReserveTimeConfigList)
                    {
                        if (item.ReserveTime.Equals(configItem.StartTime))
                        {
                            //选中，高亮
                            if (configItem.ReserveCount > 0)
                            {
                                item.IsHighLight = true;
                            }
                            string reserveTime = string.Empty;
                            //超出预约数量时置灰
                            foreach (var countItem in reserveCountList)
                            {
                                reserveTime = countItem.ReserveTime.ToString("HH:mm");
                                if (item.ReserveTime.Equals(reserveTime) && countItem.ReserveCount >= configItem.ReserveCount)
                                {
                                    item.IsHighLight = false;
                                }
                            }
                        }

                    }
                }
            }

            return reserveTimeList;
        }

        /// <summary>
        /// 已预约列表---微信小程序
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ReservedInfoDTO>> ReservedList(ReservedListRequest request)
        {
            ApiPagedResultData<ReservedInfoDTO> response = new ApiPagedResultData<ReservedInfoDTO>();
            //查询已预约列表
            var result =
                await _shopReserveRepository.GetReserveListAsync(request.UserId, request.PageIndex, request.PageSize);
            if (result != null && result.List != null)
            {
                List<long> shopIdList = new List<long>();
                result.List.ForEach(o => shopIdList.Add(o.ShopId));
                List<long> shopIds = shopIdList.Distinct().ToList();
                List<long> reserveIds = result.List.Select(o => o.Id).ToList();
                //查询门店信息
                var clientRequest = new GetShopListClientRequest()
                {
                    ShopIds = shopIds,
                    PageIndex = 1,
                    PageSize = request.PageSize
                };
                var shopList = await _shopManageClient.GetShopSimpleList(clientRequest);
                var reserveOrderNums = await _shopReserveOrderRepository.GetReserveInfoByReserveIdOrOrderNum(new Core.Request.GetReserveInfoByReserveIdOrOrderNum { ReserveIds = reserveIds });

                List<ReservedInfoDTO> reserveList = new List<ReservedInfoDTO>();
                foreach (var item in result.List)
                {
                    var reserveInfo = new ReservedInfoDTO();
                    reserveInfo.ReserveId = item.Id;
                    reserveInfo.ReserveNO = (int)item.Id;
                    reserveInfo.ReserveTime = item.ReserveTime.ToString("yyyy-MM-dd HH:mm");
                    reserveInfo.ShopId = item.ShopId;
                    reserveInfo.ServiceName = item.ServiceName;
                    reserveInfo.OrderNO = string.Join("、", reserveOrderNums.Where(_ => _.ReserveId == item.Id).Select(o => o.OrderNo).ToArray());
                    reserveInfo.CarId = item.CarId;
                    reserveInfo.Brand = item.Brand;
                    reserveInfo.Vehicle = item.Vehicle;
                    reserveInfo.PaiLiang = item.PaiLiang;
                    reserveInfo.Nian = item.Nian;
                    reserveInfo.SalesName = item.SalesName;
                    reserveInfo.CarNO = item.CarNo;
                    reserveInfo.VinNO = item.VinNo;
                    reserveInfo.CarLogo = item.CarLogo;
                    reserveInfo.ReserveStatus = Enum.GetName(typeof(ReserveStatusEnum), item.Status);
                    reserveInfo.ShopSimpleName = shopList.Find(o => o.Id.Equals(item.ShopId)).SimpleName;
                    reserveInfo.Type = item.Type;
                    if (!string.IsNullOrEmpty(item.OrderNO))
                    {
                        reserveInfo.IsDisplay = true;
                    }

                    reserveList.Add(reserveInfo);
                }

                response.Items = reserveList;
                response.TotalItems = result.TotalItems;
            }

            return response;
        }
        /// <summary>
        /// 预约列表V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ReservedListV2DTO>> ReserveListV2(ReserveListV2Request request)
        {
            ApiPagedResultData<ReservedListV2DTO> response = new ApiPagedResultData<ReservedListV2DTO>();
            //查询已预约列表
            var result =
                await _shopReserveRepository.GetReserveListV2Async(request.UserId, request.PageIndex, request.PageSize, request.Type);
            if (result != null && result.List != null)
            {
                List<long> list = new List<long>();
                result.List.ForEach(o => list.Add(o.ShopId));
                List<long> shopIds = list.Distinct().ToList();
                //查询门店信息
                var clientRequest = new GetShopListClientRequest()
                {
                    ShopIds = shopIds,
                    PageIndex = 1,
                    PageSize = request.PageSize
                };
                var shopList = await _shopManageClient.GetShopSimpleList(clientRequest);
                //支付按钮判断，已完成，已取消的不处理
                var reservePayList = new List<ReserveListPayDTO>();
                if (request.Type == 1)
                {
                    var ids = result.List.Select(o => o.Id).ToList();
                    var reserveOrders = await _shopReserveOrderRepository.GetReserveInfoByReserveIdOrOrderNum(new GetReserveInfoByReserveIdOrOrderNum
                    {
                        ReserveIds = ids
                    });

                    var orderBaseInfos = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
                    {
                        OrderNos = reserveOrders.Select(o => o.OrderNo).ToList()
                    });

                    if (orderBaseInfos == null || !orderBaseInfos.Any())
                    {
                        throw new CustomException("订单信息异常");
                    }


                    IEnumerable<IGrouping<long, ShopReserveOrderDO>> orderList = reserveOrders.GroupBy(t => t.ReserveId, t => t);
                    foreach (IGrouping<long, ShopReserveOrderDO> info in orderList)
                    {
                        var reservePay = new ReserveListPayDTO()
                        {
                            ReserveId = info.Key
                        };
                        decimal amount = decimal.Zero;//支付金额
                        foreach (var item in info.ToList())
                        {
                            foreach (var orderBaseInfo in orderBaseInfos)
                            {
                                if (item.OrderNo.Equals(orderBaseInfo.OrderNo) && orderBaseInfo.PayStatus == 0 && orderBaseInfo.OrderStatus == 20)
                                {
                                    reservePay.DisplayPayButton = true;
                                    amount += orderBaseInfo.ActualAmount;
                                }
                                if (item.OrderNo.Equals(orderBaseInfo.OrderNo))
                                {
                                    reservePay.PayStatus = orderBaseInfo.PayStatus;
                                }
                            }
                        }
                        reservePay.Amount = amount;
                        reservePayList.Add(reservePay);
                    }
                }

                List<ReservedListV2DTO> reserveList = new List<ReservedListV2DTO>();
                foreach (var item in result.List)
                {
                    var simpleName = shopList.Find(o => o.Id.Equals(item.ShopId))?.SimpleName;
                    if (string.IsNullOrEmpty(simpleName))
                    {
                        simpleName = "";
                    }
                    var reserveInfo = new ReservedListV2DTO();
                    reserveInfo.ReserveId = item.Id;
                    reserveInfo.ReserveNO = (int)item.Id;
                    reserveInfo.ReserveTime = item.ReserveTime.ToString("yyyy-MM-dd HH:mm");
                    reserveInfo.ShopId = item.ShopId;
                    reserveInfo.ServiceName = item.ServiceName;
                    reserveInfo.VehicleName = item.Vehicle;
                    reserveInfo.ShopSimpleName = simpleName;
                    reserveInfo.ReserveStatus = Enum.GetName(typeof(ReserveStatusEnum), item.Status);

                    foreach (var itemPay in reservePayList)
                    {
                        if (itemPay.ReserveId == item.Id)
                        {
                            //reserveInfo.Amount = itemPay.Amount;
                            //reserveInfo.DisplayPayButton = itemPay.DisplayPayButton;
                            if (itemPay.PayStatus == 0)
                            {
                                reserveInfo.IsCancelButton = true;
                                reserveInfo.IsReserveButton = true;
                            }
                        }
                    }
                    reserveList.Add(reserveInfo);
                }

                response.Items = reserveList;
                response.TotalItems = result.TotalItems;
            }

            return response;
        }

        /// <summary>
        /// 查询预约详情V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetRebookReserveInfoResponse> GetReserveInfoV2(RebookReserveRequest request)
        {
            //查询预约表信息
            var reserveInfo = await _shopReserveRepository.GetReserveInfo(request.ReserveId, 0);
            if (reserveInfo == null)
            {
                throw new CustomException("该预约不存在");
            }
            var cancelResult = CancelReserveV2(new CancelReserveRequest
            {
                CancelReason = "用户取消",
                ReserveId = request.ReserveId,
                UpdateBy = reserveInfo.UserName
            });
            //订单商品信息
            List<ShopProductDTO> products = new List<ShopProductDTO>();
            var orders = _shopReserveOrderRepository.GetReserveOrderByReserveId(request.ReserveId);
            await Task.WhenAll(cancelResult, orders);
            if (cancelResult.Result != null && !cancelResult.Result.Data)
            {
                throw new CustomException(cancelResult.Result.Message);
            }
            if (cancelResult.Result.Data && orders.Result != null && orders.Result.Any())
            {
                var orderNos = orders.Result.Select(o => o.OrderNo).ToList();
                var orderProducts = _orderClient.GetOrderProduct(new OrderProductRequest()
                {
                    OrderNos = orderNos
                });
                //查询订单商品活动
                var productList = _productClient.GetProductActivityByOrderNos(new ProductActivityByOrderNosClientRequest { OrderNos = orderNos });
                await Task.WhenAll(orderProducts, productList);
                orderProducts.Result?.ForEach(_ =>
                {
                    products.Add(new ShopProductDTO()
                    {
                        ProductCode = _.ProductId,
                        Icon = _.ImageUrl,
                        DisplayName = _.ProductName,
                        SalesPrice = _.Price,
                        Amount = _.TotalNumber,
                        Description = _.Description
                    });
                });
                if (productList.Result != null && productList.Result.Any())
                {
                    foreach (var item in products)
                    {
                        foreach (var itemProduct in productList.Result)
                        {
                            if (item.ProductCode.Equals(itemProduct.ProductCode))
                            {
                                item.ActivityId = itemProduct.ActivityId;
                            }
                        }
                    }
                }
            }
            //车型信息
            MyCarInfoDTO carInfo = new MyCarInfoDTO()
            {
                CarId = reserveInfo.CarId,
                Brand = reserveInfo.Brand,
                Vehicle = reserveInfo.Vehicle,
                PaiLiang = reserveInfo.PaiLiang,
                Nian = reserveInfo.Nian,
                CarNO = reserveInfo.CarNo,
                CarLogo = reserveInfo.CarLogo
            };

            return new GetRebookReserveInfoResponse()
            {
                Products = products,
                CarInfo = carInfo,
                ShopId = reserveInfo.ShopId,
                ServiceCode = reserveInfo.ServiceCode,
                ServiceName = reserveInfo.ServiceName
            };
        }

        /// <summary>
        /// 预约看板
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReservedListForAppResponse> GetReserveListForAppAsync(ReservedListForAppRequest request)
        {
            DateTime startTime = DateTime.Now.AddDays(request.OffsetValue);
            string reserveDate = string.Empty;
            if (request.OffsetValue == 0)
            {
                reserveDate = "今天 ";
            }
            else if (request.OffsetValue == 1)
            {
                reserveDate = "明天 ";
            }
            else if (request.OffsetValue == 2)
            {
                reserveDate = "后天 ";
            }

            reserveDate = reserveDate + startTime.ToString("MM-dd ") +
                          CommonHelper.GetWeekDay(startTime.DayOfWeek.ToString());

            List<ReservedInfoVO> reservedInfos = new List<ReservedInfoVO>();
            //查询已预约列表
            var reservedList = await _shopReserveRepository.GetReserveListForAppAsync(request);
            if (reservedList != null)
            {
                reserveDate = reserveDate + " 约：" + reservedList.Count + "辆车";
                //按时间分组
                var groupList = reservedList.GroupBy(t => t.ReserveTime).ToList();
                foreach (IGrouping<DateTime, ReserveInfoDO> info in groupList)
                {
                    var reservedInfo = new ReservedInfoVO();
                    reservedInfo.Time = info.Key.Hour + ":00";
                    reservedInfo.Count = info.Count();
                    List<ReservedVO> reserveDetaiList = new List<ReservedVO>();
                    info.ToList().ForEach(o => reserveDetaiList.Add(new ReservedVO()
                    {
                        ReserveId = o.Id,
                        ServiceName = o.ServiceName,
                        UserName = o.UserName,
                        UserTel = o.UserTel,
                        CarNO = o.CarNo,
                        Channel = CommonHelper.GetDescriptionByEnum((ReserveChannelEnum)o.Channel),
                        Status = JudgeOverdue(o.ReserveTime, o.ArrivalTime),
                        CarVehicle = CommonHelper.GetCarVehicle(o.Vehicle, o.PaiLiang, o.Nian, o.SalesName)
                    }));
                    reservedInfo.ReserveDetaiList = reserveDetaiList;
                    reservedInfos.Add(reservedInfo);
                }
            }
            else
            {
                reserveDate = reserveDate + " 约：0辆车";
            }

            ReservedListForAppResponse response = new ReservedListForAppResponse()
            {
                ReserveDate = reserveDate,
                ReservedList = reservedInfos
            };

            return response;
        }

        /// <summary>
        /// 判断是否逾期
        /// </summary>
        /// <returns></returns>
        private string JudgeOverdue(DateTime reserveTime, DateTime receiveTime)
        {
            DateTime dt = DateTime.Now;
            string status = string.Empty;
            if (reserveTime < dt)
            {
                status = ArrivedStatusEnum.Overdue.ToString();
            }
            else
            {
                if (receiveTime != DateTime.MinValue)
                {
                    if (reserveTime > receiveTime)
                    {
                        status = ArrivedStatusEnum.ArrivedShop.ToString();
                    }
                    else
                    {
                        status = ArrivedStatusEnum.ArrivedShopOverdue.ToString();
                    }
                }
            }

            return status;
        }

        /// <summary>
        /// 获取预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetReserveDetailForAppResponse> GetReserveDetailForApp(GetReserveDetailForAppRequest request)
        {
            //查询预约表信息
            var reserveInfo = await _shopReserveRepository.GetReserveInfo(request.ReserveId, request.ShopId);
            if (reserveInfo == null)
            {
                throw new CustomException("暂无数据");
            }

            //根据订单号查询订单信息
            List<ReservedOrderVO> reservedOrders = new List<ReservedOrderVO>();
            if (reserveInfo.IsAnyOrder == 1)
            {
                var orderResult = await _orderClient.GetOrderDetail(new GetOrderDetailClientRequest()
                { OrderNo = reserveInfo.OrderNO, UserId = reserveInfo.UserId });
                if (orderResult.IsNotNullSuccess() && orderResult.Data != null)
                {
                    List<ReservedProductVO> ReserveProductVOs = new List<ReservedProductVO>();
                    foreach (var item in orderResult.Data.Products)
                    {
                        if (item.PackageOrProduct != null && item.PackageOrProduct.ProductAttribute == 1)
                        {
                            foreach (var itemPackage in item.PackageProducts)
                            {
                                ReservedProductVO reserveProductVO = new ReservedProductVO()
                                {
                                    ProductName = itemPackage.ProductName,
                                    Number = itemPackage.Number
                                };
                                ReserveProductVOs.Add(reserveProductVO);
                            }
                        }
                        else
                        {
                            ReservedProductVO reserveProductVO = new ReservedProductVO()
                            {
                                ProductName = item.PackageOrProduct.ProductName,
                                Number = item.PackageOrProduct.Number
                            };
                            ReserveProductVOs.Add(reserveProductVO);
                        }
                    }

                    foreach (var item in orderResult.Data.Services)
                    {
                        ReservedProductVO reserveProductVO = new ReservedProductVO()
                        {
                            ProductName = item.DisplayName,
                            Number = item.Number
                        };
                        ReserveProductVOs.Add(reserveProductVO);
                    }

                    var reservedOrder = new ReservedOrderVO()
                    {
                        OrderNo = orderResult.Data.OrderNo,
                        CarVehicle = CommonHelper.GetCarVehicle(reserveInfo.Vehicle, reserveInfo.PaiLiang,
                            reserveInfo.Nian, reserveInfo.SalesName),
                        ReservedProducts = ReserveProductVOs
                    };
                    reservedOrders.Add(reservedOrder);
                }
            }

            var trackList = await GetReserveTracks(request.ReserveId);
            var response = new GetReserveDetailForAppResponse()
            {
                CarNO = reserveInfo.CarNo,
                CarLogo = reserveInfo.CarLogo,
                CarVehicle = CommonHelper.GetCarVehicle(reserveInfo.Vehicle, reserveInfo.PaiLiang, reserveInfo.Nian,
                    reserveInfo.SalesName),
                ReservedOrders = reservedOrders,
                ReserveTime = reserveInfo.ReserveTime.ToString("yyyy-MM-dd HH:mm"),
                UserName = reserveInfo.UserName,
                UserTel = reserveInfo.UserTel,
                ServiceName = reserveInfo.ServiceName,
                Remark = reserveInfo.Remark,
                //TechName = reserveInfo.TechName,//todo
                TechName = "-",
                TrackList = trackList
            };

            return response;
        }

        /// <summary>
        /// 获取预约处理记录
        /// </summary>
        /// <param name="ReserveId"></param>
        /// <returns></returns>
        public async Task<List<ReserveTrackDTO>> GetReserveTracks(long ReserveId)
        {
            var para = new DynamicParameters();
            para.Add("@ReserveId", ReserveId);
            string sql = @"where reserve_id = @ReserveId ";
            var list = await _reserveTrackRepository.GetListAsync(sql, para);
            return _mapper.Map<List<ReserveTrackDTO>>(list);
        }

        /// <summary>
        /// 新增门店预约时间工位配置
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddReserveTimeConfig(AddReserveTimeConfigRequest request)
        {
            if (request != null && request.Items != null && request.Items.Any())
            {
                DateTime dt = DateTime.Now;
                var configItems = new List<ShopReserveTimeConfigDO>();
                request.Items.ForEach(o => configItems.Add(new ShopReserveTimeConfigDO
                {
                    ShopId = request.ShopId,
                    WeekDay = request.WeekDay,
                    YearDay = request.YearDay,
                    StartTime = o.StartTime,
                    ReserveCount = o.ReserveCount,
                    ConfigType = request.ConfigType,
                    CreateBy = request.CreateBy,
                    CreateTime = dt
                }));

                await _shopReserveTimeConfigRepository.DeleteReserveTimeConfigAsync(request.ShopId,
                request.WeekDay, request.YearDay, request.ConfigType);

                _shopReserveTimeConfigRepository.InsertBatch(configItems);

                //using (TransactionScope ts = new TransactionScope())
                //{
                //    var configItem = await _shopReserveTimeConfigRepository.GetReserveTimeConfigByDateTypeAsync(request.ShopId,
                //        request.WeekDay, request.YearDay, request.ConfigType);
                //    if (configItem != null && configItem.Any())
                //    {
                //        await _shopReserveTimeConfigRepository.DeleteReserveTimeConfigAsync(request.ShopId,
                //        request.WeekDay, request.YearDay, request.ConfigType);
                //    }
                //    configItems.ForEach(o => _shopReserveTimeConfigRepository.InsertAsync(o));
                //    ts.Complete();
                //}
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 获取预约时间配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveTimeConfigDTO>> GetReserveTimeConfig(GetReserveTimeConfigRequest request)
        {
            //营业时间
            int start = 6;
            int end = 22;
            List<ReserveTimeConfigDTO> reserveTimeList = new List<ReserveTimeConfigDTO>();
            for (int i = 0; i < end - start; i++)
            {
                var startTime = string.Empty;
                var time = start + i;
                if (time < 10)
                {
                    startTime = "0" + time + ":00";
                }
                else
                {
                    startTime = time + ":00";
                }
                var reserveTimeDTO = new ReserveTimeConfigDTO()
                {
                    StartTime = startTime
                };
                reserveTimeList.Add(reserveTimeDTO);
            }
            var result = await _shopReserveTimeConfigRepository.GetReserveTimeConfigByDateTypeAsync(request.ShopId,
                request.WeekDay, request.YearDay, request.ConfigType);
            if (result != null && result.Any())
            {
                foreach (var item in reserveTimeList)
                {
                    foreach (var itemConfig in result)
                    {
                        if (item.StartTime.Equals(itemConfig.StartTime))
                        {
                            item.ReserveCount = itemConfig.ReserveCount;
                        }
                    }
                }
            }
            return reserveTimeList;
        }

        /// <summary>
        /// 获取门店预约总数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetShopTotalReserveResponse> GetShopTotalReserve(BaseShopRequest request)
        {
            int total = await _shopReserveRepository.GetShopTotalReserve(request.ShopId);
            return new GetShopTotalReserveResponse() { TotalReserve = total };
        }

        /// <summary>
        /// 新增预约V2（C端小程序）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<long>> AddReserveV2Async(AddReserveV2Request request)
        {
            var reserveInfo = await _shopReserveRepository.GetTheDayReserveeInfo(request.UserId, request.ReserveDate, request.CarId, request.ShopId);
            if (reserveInfo != null)
            {
                return new ApiResult<long>()
                {
                    Data = 0,
                    Code = ResultCode.Failed,
                    Message = CommonConstant.ReserveExist
                };
            }
            var result = Result.Failed();
            var dt = DateTime.Now;
            var reserveDatetime = DateTime.Today.AddDays(request.ReserveDate);
            request.ReserveTime = reserveDatetime.ToString("yyyy-MM-dd ") + request.ReserveTime;

            DateTime.TryParse(request.ReserveTime, out var reserveTime);
            reserveTime = new DateTime(reserveTime.Year, reserveTime.Month, reserveTime.Day, reserveTime.Hour, 0, 0);

            //重新赋值
            ShopReserveDO shopReserveDO = new ShopReserveDO()
            {
                ReserveTime = reserveTime,
                ReserveNo = 0,
                Status = 1,
                Channel = 1,
                UserId = request.UserId,
                ShopId = request.ShopId,
                ServiceName = CommonHelper.GetDescriptionByEnum((OrderTypeEnum)Enum.Parse(typeof(OrderTypeEnum), request.ServiceCode)),
                ServiceType = request.ServiceCode,
                CarId = request.CarId,
                CreateTime = dt,
                Remark = request.Remark
            };

            //根据userId 查用户信息
            var userInfo = _userClient.GetUserInfo(new GetUserInfoClientRequest() { UserId = request.UserId }).Result;
            if (userInfo != null)
            {
                shopReserveDO.UserName = userInfo.UserName;
                shopReserveDO.UserSex = CommonHelper.GetDescriptionByEnum((SexEnum)userInfo.Gender);
                shopReserveDO.UserTel = userInfo.UserTel;
                shopReserveDO.CreateBy = userInfo.UserName;
            }
            else
            {
                return new ApiResult<long>()
                {
                    Data = 0,
                    Code = ResultCode.Failed,
                    Message = CommonConstant.NoUser
                };
            }

            //根据CarId 查车型信息
            var carInfo = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdClientRequest
            {
                UserId = request.UserId,
                CarId = request.CarId
            });
            if (carInfo != null)
            {
                shopReserveDO.CarNo = carInfo.CarNumber;
                shopReserveDO.Brand = carInfo.Brand;
                shopReserveDO.Vehicle = carInfo.Vehicle;
                shopReserveDO.PaiLiang = carInfo.PaiLiang;
                shopReserveDO.Nian = carInfo.Nian;
                shopReserveDO.SalesName = carInfo.SalesName;
                shopReserveDO.VinNo = carInfo.VinCode;
                shopReserveDO.CarLogo = CommonHelper.GetLogoUrlByName(carInfo.Brand);
            }
            var id = 0;
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var reserveTypeEnum = await _baoYangClient.GetServiceTypeEnum() ?? new List<ServiceTypeEnumDto>();
                id = await _shopReserveRepository.InsertAsync(shopReserveDO);
                if (id > 0)
                {
                    //添加图片
                    if (request.Imgs != null && request.Imgs.Any())
                    {
                        var imgs = new List<ReservePictureDO>();
                        request.Imgs.ForEach(o => imgs.Add(
                            new ReservePictureDO { ReserveId = id, Url = o.Url, CreatedBy = shopReserveDO.UserName, CreatedTime = dt }
                            ));
                        imgs.ForEach(o => _reservePictureRepository.InsertAsync(o));
                    }
                    //创建订单
                    var products = new List<ReserveProductClientDTO>();
                    request.productItem.ForEach(o => products.Add(new ReserveProductClientDTO
                    {
                        Pid = o.ProductCode,
                        Price = o.TotalAmount,
                        Num = o.Number,
                        ActivityId = o.ActivityId
                    }));
                    var clientRequest = new CreateOrderForArrivalOrReserverRequest()
                    {
                        ShopId = shopReserveDO.ShopId,
                        UserId = shopReserveDO.UserId,
                        CarId = shopReserveDO.CarId,
                        OrderType = CommonHelper.GetOrderType(request.ServiceCode),
                        PromotionChannel = "MiniApp",
                        Items = products
                    };

                    var clientResponse = await _orderClient.CreateOrderForArrivalOrReserver(clientRequest);
                    if (clientResponse != null && clientResponse.Any())
                    {
                        var shopReserveOrderDO = new List<ShopReserveOrderDO>();
                        clientResponse.ForEach(o => shopReserveOrderDO.Add(
                            new ShopReserveOrderDO
                            {
                                OrderNo = o,
                                ReserveId = id,
                                CreateBy = shopReserveDO.UserName,
                                CreateTime = dt
                            }));
                        shopReserveOrderDO.ForEach(o => _shopReserveOrderRepository.InsertAsync(o));
                    }
                }

                var submitBy = !string.IsNullOrEmpty(shopReserveDO.UserName) ? shopReserveDO.UserName : request.UserId;
                await _shopManageClient.AddShopUserRelation(new AddShopUserRelationRequest()
                {
                    ShopId = request.ShopId,
                    UserId = request.UserId,
                    SubmitBy = submitBy,
                    Channel = "Consumer",
                    ReferrerType = "None"
                });

                List<ReserveTrackDetail> trackDetail = new List<ReserveTrackDetail>();
                trackDetail.Add(
                        new ReserveTrackDetail()
                        {
                            Field = ReserveEditFieldEnum.ReserveType.ToString(),
                            FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.ReserveType),

                            AfterValue = reserveTypeEnum.FirstOrDefault(x => x.ServiceType.Equals(request.ServiceCode))
                                             ?.DisplayName ?? string.Empty
                        });

                trackDetail.Add(
                        new ReserveTrackDetail()
                        {
                            Field = ReserveEditFieldEnum.ReserveTime.ToString(),
                            FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.ReserveTime),
                            AfterValue = request.ReserveTime
                        });

                trackDetail.Add(
                        new ReserveTrackDetail()
                        {
                            Field = ReserveEditFieldEnum.TechName.ToString(),
                            FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.TechName),
                            AfterValue = "暂无"
                        });

                await AddReserveTrackLog(id, shopReserveDO.CreateBy, ReserveOptEnum.Create, trackDetail);

                ts.Complete();
            }
            return new ApiResult<long>()
            {
                Data = id,
                Code = ResultCode.Success,
                Message = CommonConstant.AddReserveSuccess
            };
        }

        #region B端预约

        /// <summary>
        /// 预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReserveListResponse> GetReserveList(ReserveListRequest request)
        {
            DateTime.TryParse(request.ReserveDate, out var startTime);
            DateTime now = DateTime.Now;
            ReserveListResponse result = new ReserveListResponse()
            {
                DayInfo = new DayInfo()
                {
                    MonthTitle = startTime.ToString("MM-dd"),
                    TodayString = GetTodayString(startTime, now),
                    WeekTitle = GetDayOfWeek(startTime)
                }
            };
            ReceiveListWithCondition dalRequest = new ReceiveListWithCondition()
            {
                ShopId = request.ShopId,
                StarTime = startTime,
                EndTime = startTime.AddDays(1),
                TechId = request.TechId
            };
            var reserveTypeEnum = await _baoYangClient.GetServiceTypeEnum() ?? new List<ServiceTypeEnumDto>();
            var reserveData = await _shopReserveRepository.GetReserveListWithCondition(dalRequest);

            if (reserveData != null && reserveData.Any())
            {
                int totalCount = reserveData.Count;
                if (request.HideCanceled)
                {
                    reserveData = reserveData.Where(_ => _.Status != 3).ToList();//隐藏已取消
                }

                if (!string.IsNullOrEmpty(request.ReserveType))
                {
                    reserveData = reserveData.Where(_ => _.ServiceType == request.ReserveType).ToList();
                }

                var arriveData = reserveData.Where(_ => _.Status == 2).Select(_ => _.Id).ToList();//已完结的预约
                List<ShopReceiveDO> receiveData = new List<ShopReceiveDO>();
                if (arriveData.Any())
                {
                    receiveData =
                        await _shopReceiveRepository.GetShopReceiveByReserveIds(arriveData);
                }
                List<ReserveTimeVo> reserveTime = new List<ReserveTimeVo>();
                var groupList = reserveData.GroupBy(t => t.ReserveTime)
                    .ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
                foreach (var reserveItem in groupList.OrderBy(_ => _.Key))
                {
                    List<ReserveListVo> reserveList = new List<ReserveListVo>();
                    reserveItem.Value.ForEach(_ =>
                    {
                        DateTime? receiveTime = receiveData.FirstOrDefault(t => t.ReserveNo == _.Id)?.ArrivalTime;
                        var reserveStatus = GetReserveStatus(_.Status, _.ReserveTime, now, receiveTime);
                        ReserveListVo itemReserve = new ReserveListVo()
                        {
                            ReserveId = _.Id,
                            UserName = _.UserName,
                            UserTel = _.UserTel,
                            ReserveChannel = GetReserveChannel(_.Channel),
                            ReserveTypeName = reserveTypeEnum.FirstOrDefault(x => x.ServiceType.Equals(_.ServiceType))
                                ?.DisplayName,
                            Technician = _.TechName,
                            CarPlate = _.CarNo,
                            CarType = _.Vehicle,
                            Status = reserveStatus.Item1,
                            StatusDisplay = reserveStatus.Item2,
                            ReserveTime = _.ReserveTime.ToString("yyyy-MM-dd HH:mm"),
                            ReserveAddress = _.Address
                        };
                        reserveList.Add(itemReserve);
                    });
                    ReserveTimeVo reserveTimeList = new ReserveTimeVo()
                    {
                        Hour = reserveItem.Key.ToString("HH:mm"),
                        Count = reserveItem.Value.Count,
                        ReserveList = reserveList
                    };
                    reserveTime.Add(reserveTimeList);
                }

                result.ReserveTimeList = reserveTime;
                result.TotalReserve = totalCount;
            }

            return result;
        }

        /// <summary>
        /// 预约搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReserveSearchResponse> ReserveSearch(ReserveSearchRequest request)
        {
            ReserveSearchResponse result = new ReserveSearchResponse();
            ReceiveListWithCondition dalRequest = new ReceiveListWithCondition()
            {
                ShopId = request.ShopId,
                UserTel = request.SearchContent,
                CarPlate = request.SearchContent
            };
            DateTime now = DateTime.Now;
            var reserveTypeEnum = await _baoYangClient.GetServiceTypeEnum() ?? new List<ServiceTypeEnumDto>();
            var reserveData = await _shopReserveRepository.GetReserveListWithCondition(dalRequest);
            if (reserveData != null && reserveData.Any())
            {
                var arriveData = reserveData.Where(_ => _.Status == 2).Select(_ => _.Id).ToList();//已完结的预约
                List<ShopReceiveDO> receiveData = new List<ShopReceiveDO>();
                if (arriveData.Any())
                {
                    receiveData =
                        await _shopReceiveRepository.GetShopReceiveByReserveIds(arriveData);
                }
                List<ReserveListVo> reserveList = new List<ReserveListVo>();
                reserveData.ForEach(_ =>
                {
                    DateTime? receiveTime = receiveData.FirstOrDefault(t => t.ReserveNo == _.Id)?.ArrivalTime;
                    var reserveStatus = GetReserveStatus(_.Status, _.ReserveTime, now, receiveTime);
                    ReserveListVo item = new ReserveListVo()
                    {
                        ReserveId = _.Id,
                        UserName = _.UserName,
                        UserTel = _.UserTel,
                        ReserveChannel = GetReserveChannel(_.Channel),
                        ReserveTypeName = reserveTypeEnum.FirstOrDefault(x => x.ServiceType.Equals(_.ServiceType))
                            ?.DisplayName,
                        Technician = _.TechName,
                        CarPlate = _.CarNo,
                        CarType = _.Vehicle,
                        Status = reserveStatus.Item1,
                        StatusDisplay = reserveStatus.Item2,
                        ReserveTime = _.ReserveTime.ToString("yyyy-MM-dd HH:mm"),
                        ReserveAddress = _.Address
                    };
                    reserveList.Add(item);
                });
                result.ReserveList = reserveList;
                result.ReserveCount = reserveData.Count;
            }

            return result;
        }

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReserveDetailVo> ReserveDetail(ReserveDetailRequest request)
        {
            long reserveId = request.ReserveId;
            var reserveTask = _shopReserveRepository.GetReserveDetail(reserveId); //预约详情
            var reserveOrderTask = _shopReserveOrderRepository.GetReserveOrderByReserveId(reserveId); //关联订单
            var reserveTrackTask = GetReserveTrackLog(reserveId); //处理记录
            var reservePictureTask = _reservePictureRepository.GetReservePictureList(reserveId); //图片
            var reserveTypeEnumTask = _baoYangClient.GetServiceTypeEnum();
            var reserveOrder = await reserveOrderTask;
            List<OrderProductDto> orderProduct = new List<OrderProductDto>();
            List<OrderDTO> orderMain = new List<OrderDTO>();
            if (reserveOrder != null && reserveOrder.Any())
            {
                var orderNos = reserveOrder.Select(_ => _.OrderNo).ToList();
                var orderProductTask = _orderClient.GetOrderProduct(new OrderProductRequest()
                {
                    OrderNos = orderNos
                });
                var orderMainTask = _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
                {
                    OrderNos = orderNos
                });
                await Task.WhenAll(orderProductTask, orderMainTask);
                orderProduct = orderProductTask.Result ?? new List<OrderProductDto>();
                orderMain = orderMainTask.Result ?? new List<OrderDTO>();
            }

            await Task.WhenAll(reserveTask, reserveTrackTask, reservePictureTask, reserveTypeEnumTask);

            var reserveTypeEnum = reserveTypeEnumTask.Result ?? new List<ServiceTypeEnumDto>();
            var reserve = reserveTask.Result;
            if (reserve != null)
            {
                var reserveImage = reservePictureTask.Result ?? new List<ReservePictureDO>();
                string carType = string.Empty;
                if (!string.IsNullOrEmpty(reserve.Vehicle))
                {
                    carType += "|" + reserve.Vehicle;
                }

                if (!string.IsNullOrEmpty(reserve.PaiLiang))
                {
                    carType += "|" + reserve.PaiLiang;
                }

                if (!string.IsNullOrEmpty(reserve.Nian))
                {
                    carType += "|" + reserve.Nian;
                }

                if (!string.IsNullOrEmpty(reserve.SalesName))
                {
                    carType += "|" + reserve.SalesName;
                }

                if (!string.IsNullOrEmpty(carType))
                {
                    carType = carType.Substring(1);
                }
                else
                {
                    carType = "无";
                }

                ReserveDetailVo result = new ReserveDetailVo()
                {
                    ReserveId = reserve.Id,
                    UserId = reserve.UserId,
                    UserName = reserve.UserName,
                    UserTel = reserve.UserTel,
                    ReserveType = reserve.ServiceType,
                    ReserveTypeName = reserveTypeEnum.FirstOrDefault(x => x.ServiceType.Equals(reserve.ServiceType))
                        ?.DisplayName,
                    ReserveTime = reserve.ReserveTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    Technician = reserve.TechName,
                    TechId = reserve.TechId,
                    Vehicle = new ReserveVehicleVo()
                    {
                        CarId = reserve.CarId,
                        CarPlate = reserve.CarNo,
                        BrandUrl = string.IsNullOrEmpty(reserve.Brand)
                            ? ""
                            : _configuration["QiNiuImageDomain"] + ImageHelper.GetLogoUrlByName(reserve.Brand),
                        CarType = carType,
                        Brand = reserve.Brand,
                        Vehicle = reserve.Vehicle,
                        VehicleId = reserve.VehicleId,
                        PaiLiang = reserve.PaiLiang,
                        Nian = reserve.Nian,
                        SalesName = reserve.SalesName,
                        Tid = reserve.Tid
                    },
                    ReserveAddress = reserve.Address,
                    Projects = orderProduct.Select(x => new ReserveProject
                    {
                        Pid = x.ProductId,
                        DisplayName = x.ProductName,
                        Count = x.TotalNumber,
                        TotalPrice = x.TotalAmount
                    }).ToList(),
                    Remark = reserve.Remark,
                    ImageList = reserveImage.Select(x => new ReserveImageVo
                    {
                        Id = x.Id,
                        Url = GetImageUrl(x.Url)
                    }).ToList(),
                    HistoryProcessList = reserveTrackTask.Result ?? new List<ReserveTrackVo>(),
                    Orders = orderMain.Where(_ => _.OrderStatus != 10 && _.OrderStatus != 40).Select(_ =>
                        new RelationOrderVo
                        {
                            OrderNo = _.OrderNo,
                            TotalMoney = _.ActualAmount,
                            Status = _.InstallStatus == 1 ? "已安装" : "待安装",
                            TotalProductNum = _.TotalProductNum,
                            Products = orderProduct.Where(t => t.OrderNo == _.OrderNo).Select(t => new ReserveProject
                            {
                                Pid = t.ProductId,
                                DisplayName = t.ProductName,
                                Count = t.TotalNumber,
                                TotalPrice = t.TotalAmount
                            }).ToList()
                        }).ToList()
                };
                if (reserve.Status == 1 &&
                    DateTime.Now.AddMinutes(-reserveDelayMinute).CompareTo(reserve.ReserveTime) <= 0)
                {
                    result.Operation.Add("CanCancel");
                    result.Operation.Add("CanModify");
                    result.Operation.Add("CreateReceive");
                }

                return result;
            }

            return null;
        }

        private string GetImageUrl(string imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                if (imageUrl.StartsWith("https://"))
                {
                    return imageUrl;
                }
                else
                {
                    return _configuration["QiNiuImageDomain"] + imageUrl;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReserveDateVo>> GetReserveDateForWeb(ReserveDateRequest request)
        {
            DateTime now = DateTime.Now;
            DateTime todayDateTime = new DateTime(now.Year, now.Month, now.Day);
            var defaultConfigTask =
                _shopReserveTimeConfigRepository.GetDefaultReserveTimeConfigByShopId(request.ShopId);
            var definedConfigTask = _shopReserveTimeConfigRepository.GetDefinedReserveTimeConfigByShopId(request.ShopId,
                now.ToString("yyyy-MM-dd"), now.AddDays(10).ToString("yyyy-MM-dd"));
            var reserveListTask = _shopReserveRepository.GetReserveListWithCondition(new ReceiveListWithCondition()
            {
                ShopId = request.ShopId,
                StarTime = todayDateTime,
                EndTime = todayDateTime.AddDays(10),
                HideCanceled = true
            });
            await Task.WhenAll(defaultConfigTask, definedConfigTask, reserveListTask);
            var defaultConfig = defaultConfigTask.Result ?? new List<ShopReserveTimeConfigDO>();
            var definedConfig = definedConfigTask.Result ?? new List<ShopReserveTimeConfigDO>();
            var reserveList = reserveListTask.Result ?? new List<ShopReserveDO>();
            List<ReserveDateVo> result = new List<ReserveDateVo>();
            for (int i = 0; i < 10; i++)
            {
                List<ReserveTimeRecord> records = new List<ReserveTimeRecord>();
                var date = now.AddDays(i);
                var dayOfWeek = GetDayOfWeekInt(date);
                var dayOfWeekDisplay = GetDayOfWeek(date);
                if (date.ToString("yyyyMMdd").Equals(now.ToString("yyyyMMdd")))
                {
                    dayOfWeekDisplay = "今天";
                }

                if (date.ToString("yyyyMMdd").Equals(now.AddDays(1).ToString("yyyyMMdd")))
                {
                    dayOfWeekDisplay = "明天";
                }

                for (int j = 6; j < 23; j++)
                {
                    DateTime reserveTime = new DateTime(date.Year, date.Month, date.Day, j, 0, 0);
                    string hour = j < 10 ? $"0{j}:00" : $"{j}:00";
                    ReserveTimeRecord timeRecord = new ReserveTimeRecord()
                    {
                        DatePart = hour,
                        DateTimePart = reserveTime.ToString("yyyy-MM-dd HH:mm"),
                        ReservedNum = reserveList.Where(_ => _.ReserveTime == reserveTime).ToList().Count
                    };

                    var definedItem = definedConfig.FirstOrDefault(_ =>
                        _.YearDay == date.ToString("yyyy-MM-dd") && _.StartTime == hour);
                    if (definedItem == null)
                    {
                        definedItem = defaultConfig.FirstOrDefault(_ => _.WeekDay == dayOfWeek && _.StartTime == hour);
                    }

                    if (definedItem != null)
                    {
                        if (definedItem.ReserveCount > 0)
                        {
                            timeRecord.EnableNum = definedItem.ReserveCount;
                        }
                    }

                    if (now > reserveTime)
                    {
                        timeRecord.Enable = false; //逾期
                        timeRecord.Overdue = true;
                    }
                    else
                    {
                        if (timeRecord.EnableNum != null && timeRecord.EnableNum > 0)
                        {
                            if (timeRecord.EnableNum > timeRecord.ReservedNum)
                            {
                                timeRecord.Enable = true;
                            }
                            else
                            {
                                timeRecord.Enable = false; //已约满
                                timeRecord.Remark = "满";
                            }
                        }
                        else
                        {
                            timeRecord.Enable = false; //繁忙
                            timeRecord.Remark = "满";
                        }
                    }

                    records.Add(timeRecord);
                }

                ReserveDateVo reserveDate = new ReserveDateVo()
                {
                    Date = date.ToString("MM/dd"),
                    Week = dayOfWeekDisplay,
                    Items = records,
                    FullReserve = records.All(x => !x.Enable)
                };

                result.Add(reserveDate);
            }

            return result;
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> CancelReserveV2(CancelReserveRequest request)
        {
            var result = new ApiResult<bool>()
            {
                Data = false,
                Code = ResultCode.Failed,
                Message = CommonConstant.ReserveCancelFailed
            };
            //取消验证是否已支付，已支付订单无法取消
            var reserveOrder = await _shopReserveOrderRepository.GetReserveOrderByReserveId(request.ReserveId);
            if (reserveOrder != null && reserveOrder.Any())
            {
                var orderBaseInfos = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
                {
                    OrderNos = reserveOrder.Select(o => o.OrderNo).ToList()
                });
                foreach (var orderBaseInfo in orderBaseInfos)
                {
                    if (orderBaseInfo.PayStatus == 1)
                    {
                        return new ApiResult<bool>()
                        {
                            Data = false,
                            Code = ResultCode.Failed,
                            Message = CommonConstant.OrderAlreadyPaid
                        };
                    }
                }
            }

            var cancelResult = await _shopReserveRepository.CancelReserve(request);
            if (cancelResult)
            {
                List<ReserveTrackDetail> trackDetail = new List<ReserveTrackDetail>()
                {
                    new ReserveTrackDetail()
                    {
                        Field = ReserveEditFieldEnum.CancelReason.ToString(),
                        FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.CancelReason),
                        AfterValue = request.CancelReason
                    }
                };
                await AddReserveTrackLog(request.ReserveId, request.UpdateBy, ReserveOptEnum.Cancel, trackDetail);
                //if (reserveOrder != null && reserveOrder.Any())
                //{
                //    await _reverseClient.CreateReverseOrderCancelForArrivalOrReserve(new CancelOrderRequest()
                //    {
                //        UserId = request.UpdateBy,
                //        OrderNos = reserveOrder.Select(_ => _.OrderNo).ToList()
                //    });
                //}
            }
            result = new ApiResult<bool>()
            {
                Data = true,
                Code = ResultCode.Success,
                Message = CommonConstant.ReserveCancelSuccess
            };
            return result;
        }

        /// <summary>
        /// 编辑预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> EditReserve(EditReserveRequest request)
        {
            var reserveTypeEnum = await _baoYangClient.GetServiceTypeEnum() ?? new List<ServiceTypeEnumDto>();
            var serviceName = reserveTypeEnum.FirstOrDefault(x => x.ServiceType.Equals(request.ReserveType))?.DisplayName ?? string.Empty;
            var oldReserve = await _shopReserveRepository.GetReserveDetail(request.ReserveId, false);
            if (oldReserve == null)
            {
                throw new CustomException($"预约Id：{request.ReserveId} 不存在");
            }

            var vehicle = request.Vehicle;
            vehicle = await ReserveVehicleHandle(vehicle, oldReserve.UserId, false, request.SubmitBy);

            ShopReserveDO shopReserveDo = new ShopReserveDO()
            {
                Id = request.ReserveId,
                UserName = request.UserName,
                ServiceType = request.ReserveType,
                ServiceName = serviceName,
                ReserveTime = DateTime.Parse(request.ReserveTime),
                TechId = request.TechId,
                TechName = request.TechName,
                Remark = request.Remark,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now,
                CarId = vehicle.CarId,
                CarNo = vehicle.CarPlate,
                Brand = vehicle.Brand,
                Vehicle = vehicle.Vehicle,
                VehicleId = vehicle.VehicleId,
                PaiLiang = vehicle.PaiLiang,
                Nian = vehicle.Nian,
                SalesName = vehicle.SalesName,
                Tid = vehicle.Tid
            };

            var result = await _shopReserveRepository.UpdateAsync(shopReserveDo,
                new List<string>()
                {
                    "ServiceType","ServiceName", "ReserveTime", "TechId", "TechName", "Remark", "CarId", "CarNo", "Brand", "Vehicle",
                    "VehicleId", "PaiLiang", "Nian", "SalesName", "Tid", "UpdateBy", "UpdateTime","UserName"
                });
            if (result > 0)
            {
                List<ReserveTrackDetail> trackDetail = new List<ReserveTrackDetail>();
                if (request.UserName != oldReserve.UserName)
                {
                    trackDetail.Add(
                        new ReserveTrackDetail()
                        {
                            Field = ReserveEditFieldEnum.UserName.ToString(),
                            FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.UserName),
                            BeforeValue = oldReserve.UserName ?? string.Empty,
                            AfterValue = request.UserName ?? string.Empty
                        });
                }

                if (request.ReserveType != oldReserve.ServiceType)
                {
                    trackDetail.Add(
                        new ReserveTrackDetail()
                        {
                            Field = ReserveEditFieldEnum.ReserveType.ToString(),
                            FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.ReserveType),
                            BeforeValue = reserveTypeEnum
                                              .FirstOrDefault(x => x.ServiceType.Equals(oldReserve.ServiceType))
                                              ?.DisplayName ?? string.Empty,
                            AfterValue = reserveTypeEnum.FirstOrDefault(x => x.ServiceType.Equals(request.ReserveType))
                                             ?.DisplayName ?? string.Empty
                        });
                }

                if (DateTime.Parse(request.ReserveTime) != oldReserve.ReserveTime)
                {
                    trackDetail.Add(
                        new ReserveTrackDetail()
                        {
                            Field = ReserveEditFieldEnum.ReserveTime.ToString(),
                            FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.ReserveTime),
                            BeforeValue = oldReserve.ReserveTime.ToString("yyyy-MM-dd HH:mm"),
                            AfterValue = DateTime.Parse(request.ReserveTime).ToString("yyyy-MM-dd HH:mm")
                        });
                }

                if (request.TechName != oldReserve.TechName)
                {
                    trackDetail.Add(
                        new ReserveTrackDetail()
                        {
                            Field = ReserveEditFieldEnum.TechName.ToString(),
                            FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.TechName),
                            BeforeValue = oldReserve.TechName,
                            AfterValue = request.TechName
                        });
                }

                if (request.Remark != oldReserve.Remark)
                {
                    trackDetail.Add(
                        new ReserveTrackDetail()
                        {
                            Field = ReserveEditFieldEnum.Remark.ToString(),
                            FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.Remark),
                            BeforeValue = oldReserve.Remark,
                            AfterValue = request.Remark
                        });
                }

                if (vehicle.CarPlate != oldReserve.CarNo)
                {
                    trackDetail.Add(
                        new ReserveTrackDetail()
                        {
                            Field = ReserveEditFieldEnum.CarPlate.ToString(),
                            FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.CarPlate),
                            BeforeValue = oldReserve.CarNo,
                            AfterValue = vehicle.CarPlate
                        });
                }

                string oldVehicle = string.Empty;
                if (!string.IsNullOrEmpty(oldReserve.Brand))
                {
                    oldVehicle += "|" + oldReserve.Brand;
                }
                if (!string.IsNullOrEmpty(oldReserve.Vehicle))
                {
                    oldVehicle += "|" + oldReserve.Vehicle;
                }
                if (!string.IsNullOrEmpty(oldReserve.PaiLiang))
                {
                    oldVehicle += "|" + oldReserve.PaiLiang;
                }
                if (!string.IsNullOrEmpty(oldReserve.Nian))
                {
                    oldVehicle += "|" + oldReserve.Nian;
                }
                if (!string.IsNullOrEmpty(oldReserve.SalesName))
                {
                    oldVehicle += "|" + oldReserve.SalesName;
                }
                if (!string.IsNullOrEmpty(oldVehicle))
                {
                    oldVehicle = oldVehicle.Substring(1);
                }
                else
                {
                    oldVehicle = "无";
                }

                string newVehicle = string.Empty;
                if (!string.IsNullOrEmpty(vehicle.Brand))
                {
                    newVehicle += "|" + vehicle.Brand;
                }
                if (!string.IsNullOrEmpty(vehicle.Vehicle))
                {
                    newVehicle += "|" + vehicle.Vehicle;
                }
                if (!string.IsNullOrEmpty(vehicle.PaiLiang))
                {
                    newVehicle += "|" + vehicle.PaiLiang;
                }
                if (!string.IsNullOrEmpty(vehicle.Nian))
                {
                    newVehicle += "|" + vehicle.Nian;
                }
                if (!string.IsNullOrEmpty(vehicle.SalesName))
                {
                    newVehicle += "|" + vehicle.SalesName;
                }
                if (!string.IsNullOrEmpty(newVehicle))
                {
                    newVehicle = newVehicle.Substring(1);
                }
                else
                {
                    newVehicle = "无";
                }

                if (oldVehicle != newVehicle)
                {
                    trackDetail.Add(
                        new ReserveTrackDetail()
                        {
                            Field = ReserveEditFieldEnum.CarType.ToString(),
                            FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.CarType),
                            BeforeValue = oldVehicle,
                            AfterValue = newVehicle
                        });
                }

                await AddReserveTrackLog(request.ReserveId, request.SubmitBy, ReserveOptEnum.Modify, trackDetail);
            }

            return result > 0;
        }

        /// <summary>
        /// 根据手机号查询用户有效预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ReserveListVo>> GetValidReserve(ValidReserveRequest request)
        {
            var reserveTypeEnum = await _baoYangClient.GetServiceTypeEnum() ?? new List<ServiceTypeEnumDto>();
            var reserveList = await _shopReserveRepository.GetValidReserveList(request.UserTel, request.ShopId);

            return reserveList?.Select(_ => new ReserveListVo
            {
                ReserveId = _.Id,
                ReserveTime = _.ReserveTime.ToString("yyyy-MM-dd HH:mm:ss"),
                UserName = _.UserName,
                UserTel = _.UserTel,
                CarPlate = _.CarNo,
                ReserveChannel = GetReserveChannel(_.Channel),
                ReserveTypeName = reserveTypeEnum.FirstOrDefault(x => x.ServiceType.Equals(_.ServiceType))?.DisplayName,
                Technician = _.TechName
            })?.ToList() ?? new List<ReserveListVo>();
        }

        /// <summary>
        /// 添加预约（B端）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> AddReserveForShop(AddReserveForShopRequest request)
        {
            var vehicle = request.Vehicle;
            string userSex = string.Empty;
            DateTime.TryParse(request.ReserveTime, out var reserveTime);
            reserveTime = new DateTime(reserveTime.Year, reserveTime.Month, reserveTime.Day, reserveTime.Hour, 0, 0);
            DateTime now = DateTime.Now;
            if (string.IsNullOrEmpty(request.UserId))
            {
                request.UserId = await _userClient.CreateUser(new CreateUserClientRequest()
                {
                    SubmitBy = request.SubmitBy,
                    UserName = request.UserName,
                    UserTel = request.UserTel,
                    Channel = ChannelType.Employee,
                    ReferrerType = ReferrerType.SelfSearch
                });
            }

            try
            {
                await _shopManageClient.AddShopUserRelation(new AddShopUserRelationRequest()
                {
                    ShopId = request.ShopId,
                    UserId = request.UserId,
                    SubmitBy = request.SubmitBy,
                    Channel = "Employee",
                    ReferrerType = "SelfSearch"
                });
            }
            catch (Exception e)
            {
                logger.Info($"AddShopUserRelation_Error_AddReserveB ShopId={request.ShopId},UserId={request.UserId} ，Error={JsonConvert.SerializeObject(e)}");
            }


            vehicle = await ReserveVehicleHandle(vehicle, request.UserId, true, request.SubmitBy);

            await EnableReserveCheck(request.ShopId, request.UserTel, vehicle?.CarPlate ?? string.Empty, reserveTime, now);

            var reserveTypeEnum = await _baoYangClient.GetServiceTypeEnum() ?? new List<ServiceTypeEnumDto>();

            var serviceName = reserveTypeEnum.FirstOrDefault(x => x.ServiceType.Equals(request.ReserveType))?.DisplayName ?? string.Empty;

            ShopReserveDO shopReserveDo = new ShopReserveDO()
            {
                ReserveTime = reserveTime,
                Channel = 2,
                Status = 1,
                UserId = request.UserId,
                UserName = request.UserName,
                UserTel = request.UserTel,
                UserSex = userSex,
                CarId = vehicle.CarId,
                VinNo = vehicle.VinCode,
                CarNo = vehicle.CarPlate,
                Brand = vehicle.Brand,
                Vehicle = vehicle.Vehicle,
                VehicleId = vehicle.VehicleId,
                PaiLiang = vehicle.PaiLiang,
                Nian = vehicle.Nian,
                SalesName = vehicle.SalesName,
                Tid = vehicle.Tid,
                CarLogo = ImageHelper.GetLogoUrlByName(vehicle.Brand) ?? string.Empty,
                ShopId = request.ShopId,
                TechId = request.TechId,
                TechName = request.TechName,
                Remark = request.Remark,
                CreateBy = request.SubmitBy,
                CreateTime = now,
                ServiceType = request.ReserveType,
                ServiceName = serviceName
            };
            var result = await _shopReserveRepository.InsertAsync(shopReserveDo);
            if (result > 0)
            {
                List<ReserveTrackDetail> trackDetail = new List<ReserveTrackDetail>()
                {
                    new ReserveTrackDetail()
                    {
                        Field = ReserveEditFieldEnum.ReserveTime.ToString(),
                        FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.ReserveTime),
                        AfterValue = reserveTime.ToString("yyyy-MM-dd HH:mm")
                    },
                    new ReserveTrackDetail()
                    {
                        Field = ReserveEditFieldEnum.ReserveType.ToString(),
                        FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.ReserveType),
                        AfterValue = serviceName
                    },
                    new ReserveTrackDetail()
                    {
                        Field = ReserveEditFieldEnum.TechName.ToString(),
                        FieldName = EnumHelper.GetEnumDescription(ReserveEditFieldEnum.TechName),
                        AfterValue = request.TechName
                    }
                };
                await AddReserveTrackLog(result, request.SubmitBy, ReserveOptEnum.Create, trackDetail);
            }

            return result;
        }

        /// <summary>
        /// 根据预约id或订单号查询预约关联订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopReserveOrderDTO>> GetReserveInfoByReserveIdOrOrderNum(GetReserveInfoByReserveIdOrOrderNum request)
        {
            var reserveOrders = await _shopReserveOrderRepository.GetReserveInfoByReserveIdOrOrderNum(request);
            return _mapper.Map<List<ShopReserveOrderDTO>>(reserveOrders);
        }

        /// <summary>
        /// 用户车型处理
        /// </summary>
        /// <param name="vehicle"></param>
        /// <param name="userId"></param>
        /// <param name="fromUser">是否需要读C端车型</param>
        /// <param name="createBy"></param>
        /// <returns></returns>
        private async Task<ReserveVehicleRequest> ReserveVehicleHandle(ReserveVehicleRequest vehicle, string userId,
            bool fromUser, string createBy)
        {
            if (vehicle == null)
            {
                vehicle = new ReserveVehicleRequest();
                vehicle.CarId = Guid.Empty.ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(vehicle.CarId))
                {
                    if (fromUser)
                    {
                        //根据CarId 查车型信息
                        var carInfo = await _vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdClientRequest
                        {
                            UserId = userId,
                            CarId = vehicle.CarId
                        });
                        if (carInfo != null)
                        {
                            vehicle.CarPlate = carInfo.CarNumber;
                            vehicle.Brand = carInfo.Brand;
                            vehicle.Vehicle = carInfo.Vehicle;
                            vehicle.VehicleId = carInfo.VehicleId;
                            vehicle.PaiLiang = carInfo.PaiLiang;
                            vehicle.Nian = carInfo.Nian;
                            vehicle.SalesName = carInfo.SalesName;
                            vehicle.Tid = carInfo.Tid;
                            vehicle.VinCode = carInfo.VinCode;
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(vehicle.VehicleId))
                    {
                        vehicle.CarId = await _vehicleClient.AddUserCarAsync(new AddUserCarClientRequest()
                        {
                            UserId = userId,
                            Brand = vehicle.Brand,
                            Vehicle = vehicle.Vehicle,
                            VehicleId = vehicle.VehicleId,
                            PaiLiang = vehicle.PaiLiang,
                            Nian = vehicle.Nian,
                            SalesName = vehicle.SalesName,
                            Tid = vehicle.Tid,
                            CarNumber = vehicle.CarPlate,
                            DataSource = "Shop",
                            CreateBy = createBy
                        });
                    }
                    else
                    {
                        vehicle.CarId = Guid.Empty.ToString();
                    }
                }
            }

            return vehicle;
        }

        /// <summary>
        /// 是否可预约校验
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="userTel"></param>
        /// <param name="reserveTime"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        private async Task EnableReserveCheck(int shopId, string userTel, string carPlate, DateTime reserveTime, DateTime now)
        {
            if (now.CompareTo(reserveTime) >= 0)
            {
                throw new CustomException("预约时间必须大于当前时间！");
            }

            var defaultConfigTask =
                _shopReserveTimeConfigRepository.GetDefaultReserveTimeConfigByShopId(shopId);
            var definedConfigTask = _shopReserveTimeConfigRepository.GetDefinedReserveTimeConfigByShopId(shopId,
                reserveTime.ToString("yyyy-MM-dd"), reserveTime.AddDays(1).ToString("yyyy-MM-dd"));
            var reserveListTask = _shopReserveRepository.GetReserveListWithCondition(new ReceiveListWithCondition()
            {
                ShopId = shopId,
                StarTime = new DateTime(reserveTime.Year, reserveTime.Month, reserveTime.Day),
                EndTime = new DateTime(reserveTime.Year, reserveTime.Month, reserveTime.Day).AddDays(1),
                HideCanceled = true
            });
            await Task.WhenAll(defaultConfigTask, definedConfigTask, reserveListTask);
            var defaultConfig = defaultConfigTask.Result ?? new List<ShopReserveTimeConfigDO>();
            var definedConfig = definedConfigTask.Result ?? new List<ShopReserveTimeConfigDO>();
            var reserveList = reserveListTask.Result ?? new List<ShopReserveDO>();
            var hour = reserveTime.ToString("HH:mm");
            var dayOfWeek = GetDayOfWeekInt(reserveTime);
            var definedItem = definedConfig.FirstOrDefault(_ => _.StartTime == hour);
            if (definedItem == null)
            {
                definedItem = defaultConfig.FirstOrDefault(_ => _.WeekDay == dayOfWeek && _.StartTime == hour);
            }

            if (definedItem == null || definedItem.ReserveCount == 0)
            {
                throw new CustomException("所选时间不可约，请修改预约时间");
            }

            if (definedItem.ReserveCount <= reserveList.Where(_ => _.ReserveTime == reserveTime).ToList().Count)
            {
                throw new CustomException("所选时间已约满，请修改预约时间");
            }

            var currentUserReserve = reserveList.Where(_ => _.UserTel == userTel && _.CarNo == carPlate && _.ReserveTime.CompareTo(now) >= 0);
            if (currentUserReserve.Any())
            {
                throw new CustomException("此手机此车牌当天已有预约，请修改预约时间");
            }
        }

        /// <summary>
        /// 查询用户当天预约信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetReserveSimpleResponse> GetSameDayReserveSimpleInfo(BaseUserRequest request)
        {
            var response = new GetReserveSimpleResponse();
            var reserveInfo = await _shopReserveRepository.GetSameDayReserveSimpleInfo(request.UserId, request.ShopId, request.CarId);
            if (reserveInfo != null)
            {
                response.ReserveId = reserveInfo.Id;
                response.ReserveTime = reserveInfo.ReserveTime;
            }
            return response;
        }



        /// <summary>
        /// 获取星期
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string GetDayOfWeek(DateTime time)
        {
            switch (time.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "周一";
                case DayOfWeek.Tuesday:
                    return "周二";
                case DayOfWeek.Wednesday:
                    return "周三";
                case DayOfWeek.Thursday:
                    return "周四";
                case DayOfWeek.Friday:
                    return "周五";
                case DayOfWeek.Saturday:
                    return "周六";
                case DayOfWeek.Sunday:
                    return "周日";
            }

            return "";
        }

        /// <summary>
        /// 获取星期
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int GetDayOfWeekInt(DateTime time)
        {
            switch (time.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                case DayOfWeek.Sunday:
                    return 7;
            }

            return 0;
        }

        private string GetTodayString(DateTime time, DateTime now)
        {
            DateTime today = now;
            if (time.ToString("yyyyMMdd").Equals(today.ToString("yyyyMMdd")))
            {
                return "今天";
            }

            if (time.ToString("yyyyMMdd").Equals(today.AddDays(1).ToString("yyyyMMdd")))
            {
                return "明天";
            }

            if (time.ToString("yyyyMMdd").Equals(today.AddDays(2).ToString("yyyyMMdd")))
            {
                return "后天";
            }

            if (time.ToString("yyyyMMdd").Equals(today.AddDays(-1).ToString("yyyyMMdd")))
            {
                return "昨天";
            }

            return "";
        }

        private string GetReserveChannel(int reserveChannel)
        {
            string channel = "";
            switch (reserveChannel)
            {
                case 1:
                    channel = "客户预约";
                    break;
                case 2:
                    channel = "门店预约";
                    break;
            }

            return channel;
        }

        private Tuple<string, string> GetReserveStatus(int reserveStatus, DateTime reserveTime, DateTime now, DateTime? receiveTime)
        {
            ReserveStatusForViewEnum reserveEnum = ReserveStatusForViewEnum.Unconfirmed;
            if (reserveStatus == 1)
            {
                if (now.AddMinutes(-reserveDelayMinute).CompareTo(reserveTime) <= 0)
                {
                    reserveEnum = ReserveStatusForViewEnum.Valid;
                }
                else
                {
                    reserveEnum = ReserveStatusForViewEnum.OverdueNotArrive;//逾期 未到店
                    //if (arrived)
                    //{
                    //    reserveEnum = ReserveStatusForViewEnum.OverdueAndArrive;
                    //}
                    //else
                    //{
                    //    reserveEnum = ReserveStatusForViewEnum.OverdueNotArrive;
                    //}
                }
            }
            else if (reserveStatus == 2)//完结
            {
                if (receiveTime != null && receiveTime.Value.CompareTo(reserveTime) > 0)//逾期到店
                {
                    reserveEnum = ReserveStatusForViewEnum.OverdueAndArrive;
                }
                else
                {
                    reserveEnum = ReserveStatusForViewEnum.Completed;
                }
            }
            else if (reserveStatus == 3)
            {
                reserveEnum = ReserveStatusForViewEnum.Canceled;
            }

            return new Tuple<string, string>(reserveEnum.ToString(), EnumHelper.GetEnumDescription(reserveEnum));
        }

        private Tuple<string, string> GetReserveStatusForShop(int reserveStatus, DateTime reserveTime, DateTime now, DateTime? receiveTime)
        {
            ReserveStatusForViewEnum reserveEnum = ReserveStatusForViewEnum.Unconfirmed;
            if (reserveStatus == 1)
            {
                if (now.AddMinutes(-reserveDelayMinute).CompareTo(reserveTime) <= 0)
                {
                    reserveEnum = ReserveStatusForViewEnum.Valid;
                    return new Tuple<string, string>(reserveEnum.ToString(), "待到店");
                }
                else
                {
                    reserveEnum = ReserveStatusForViewEnum.OverdueNotArrive;//逾期 未到店
                    return new Tuple<string, string>(reserveEnum.ToString(), "逾期未到店");
                }
            }
            else if (reserveStatus == 2)//完结
            {
                if (receiveTime != null && receiveTime.Value.CompareTo(reserveTime) > 0)//逾期到店
                {
                    reserveEnum = ReserveStatusForViewEnum.OverdueAndArrive;
                    return new Tuple<string, string>(reserveEnum.ToString(), "逾期已到店");
                }
                else
                {
                    reserveEnum = ReserveStatusForViewEnum.Completed;
                    return new Tuple<string, string>(reserveEnum.ToString(), "已正常到店");
                }
            }
            else if (reserveStatus == 3)
            {
                reserveEnum = ReserveStatusForViewEnum.Canceled;
                return new Tuple<string, string>(reserveEnum.ToString(), "已取消");
            }
            return new Tuple<string, string>(reserveEnum.ToString(), "待确认");
        }

        /// <summary>
        /// 预约列表查询-分页的
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<ShopReserveListVo>> GetReserveListPage(ReserveListPageRequest request)
        {
            ApiPagedResultData<ShopReserveListVo> result = new ApiPagedResultData<ShopReserveListVo>
            {
                Items = new List<ShopReserveListVo>()
            };
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(request.StartDate))
            {
                startDate = Convert.ToDateTime(request.StartDate);
            }
            if (!string.IsNullOrEmpty(request.EndDate))
            {
                endDate = Convert.ToDateTime(request.EndDate).AddDays(1);
            }
            var reserveTypeEnum = (await _baoYangClient.GetServiceTypeEnum()) ?? new List<ServiceTypeEnumDto>();

            List<long> reserveIdList = new List<long>();
            if (!string.IsNullOrEmpty(request.OrderNo))
            {
                var reserveOrder = await _shopReserveOrderRepository.GetReserveOrderByOrderNo(request.OrderNo);
                if (reserveOrder != null && reserveOrder.Any())
                {
                    reserveIdList = reserveOrder.Select(_ => _.ReserveId).ToList();
                }
            }

            var response = await _shopReserveRepository.GetCommonReserveListPage(new CommonReserveListPageCondition
            {
                ShopId = request.ShopId,
                UserTel = request.UserTel,
                CarPlate = request.CarPlate,
                ReserveIds = reserveIdList,
                ReserveType = request.ReserveType,
                ReserveChannel = request.ReserveChannel,
                Status = request.Status,
                StarTime = startDate,
                EndTime = endDate,
                ReserveTech = request.ReserveTech,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });
            if (response != null)
            {
                result.TotalItems = response.Item2;
                if (response.Item1 != null)
                {
                    response.Item1.ForEach(_ =>
                    {
                        var reserveStatus = GetReserveStatusForShop(_.Status, _.ReserveTime, DateTime.Now, _.ArriveTime);
                        ShopReserveListVo itemReserve = new ShopReserveListVo
                        {
                            ReserveId = _.Id,
                            UserName = _.UserName,
                            UserTel = _.UserTel,
                            Channel = _.Channel,
                            ChannelDisplay = GetReserveChannel(_.Channel),
                            ReserveType = _.ServiceType,
                            ReserveTypeDisplay = reserveTypeEnum.FirstOrDefault(x => x.ServiceType.Equals(_.ServiceType))
                                ?.DisplayName ?? string.Empty,
                            ReserveTime = _.ReserveTime.ToString("yyyy-MM-dd HH:mm:ss"),
                            TechId = _.TechId,
                            TechName = _.TechName,
                            CarPlate = _.CarNo,
                            CarType = CommonHelper.GetCarVehicle(_.Vehicle, _.PaiLiang, _.Nian, _.SalesName),
                            Status = reserveStatus.Item1,
                            StatusDisplay = reserveStatus.Item2
                        };
                        result.Items.Add(itemReserve);
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// 预约详情 - shop站点的
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReserveDetailForWebVo> GetReserveDetailForWeb(ReserveDetailRequest request)
        {
            long reserveId = request.ReserveId;
            var reserveTask = _shopReserveRepository.GetReserveDetail(reserveId); //预约详情
            var reserveOrderTask = _shopReserveOrderRepository.GetReserveOrderByReserveId(reserveId); //关联订单
            var reserveTrackTask = GetReserveTrackLog(reserveId); //处理记录
            var reservePictureTask = _reservePictureRepository.GetReservePictureList(reserveId); //图片
            var reserveTypeEnumTask = _baoYangClient.GetServiceTypeEnum();
            var reserveOrder = await reserveOrderTask;
            List<OrderProductDto> orderProduct = new List<OrderProductDto>();
            if (reserveOrder != null && reserveOrder.Any())
            {
                orderProduct = (await _orderClient.GetOrderProduct(new OrderProductRequest()
                {
                    OrderNos = reserveOrder.Select(_ => _.OrderNo).ToList()
                })) ?? new List<OrderProductDto>();
            }

            await Task.WhenAll(reserveTask, reserveTrackTask, reservePictureTask, reserveTypeEnumTask);

            var reserveTypeEnum = reserveTypeEnumTask.Result ?? new List<ServiceTypeEnumDto>();
            var reserve = reserveTask.Result;
            if (reserve != null)
            {
                var historyReceive = await _shopReceiveService.GetHistoryReceiveNoProject(reserve.UserId);
                var reserveImage = reservePictureTask.Result ?? new List<ReservePictureDO>();
                string carType = string.Empty;
                if (!string.IsNullOrEmpty(reserve.Vehicle))
                {
                    carType += "|" + reserve.Vehicle;
                }

                if (!string.IsNullOrEmpty(reserve.PaiLiang))
                {
                    carType += "|" + reserve.PaiLiang;
                }

                if (!string.IsNullOrEmpty(reserve.Nian))
                {
                    carType += "|" + reserve.Nian;
                }

                if (!string.IsNullOrEmpty(reserve.SalesName))
                {
                    carType += "|" + reserve.SalesName;
                }

                if (!string.IsNullOrEmpty(carType))
                {
                    carType = carType.Substring(1);
                }
                else
                {
                    carType = "无";
                }

                ReserveDetailForWebVo result = new ReserveDetailForWebVo()
                {
                    ReserveId = reserve.Id,
                    UserId = reserve.UserId,
                    UserName = reserve.UserName,
                    UserTel = reserve.UserTel,
                    ReserveType = reserve.ServiceType,
                    ReserveTypeName = reserveTypeEnum.FirstOrDefault(x => x.ServiceType.Equals(reserve.ServiceType))
                        ?.DisplayName,
                    ReserveTime = reserve.ReserveTime.ToString("yyyy-MM-dd HH:mm"),
                    Technician = reserve.TechName,
                    TechId = reserve.TechId,
                    Vehicle = new ReserveVehicleVo()
                    {
                        CarId = reserve.CarId,
                        CarPlate = reserve.CarNo,
                        BrandUrl = string.IsNullOrEmpty(reserve.Brand)
                            ? ""
                            : _configuration["QiNiuImageDomain"] + ImageHelper.GetLogoUrlByName(reserve.Brand),
                        CarType = carType,
                        Brand = reserve.Brand,
                        Vehicle = reserve.Vehicle,
                        VehicleId = reserve.VehicleId,
                        PaiLiang = reserve.PaiLiang,
                        Nian = reserve.Nian,
                        SalesName = reserve.SalesName,
                        Tid = reserve.Tid
                    },
                    Projects = orderProduct.Select(x => new ReserveProject
                    {
                        Pid = x.ProductId,
                        DisplayName = x.ProductName,
                        Count = x.TotalNumber,
                        TotalPrice = x.TotalAmount
                    }).ToList(),
                    Remark = reserve.Remark,
                    ImageList = reserveImage.Select(x => GetImageUrl(x.Url)).ToList(),
                    HistoryProcessList = reserveTrackTask.Result ?? new List<ReserveTrackVo>(),
                    ReserveChannel = reserve.Channel,
                    ChannelDisplay = GetReserveChannel(reserve.Channel),
                    CreatedTime = reserve.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    HistoryReceive = historyReceive?.Select(x => new Core.Model.Reserve.HistoryReceiveVo
                    {
                        RecId = x.RecId,
                        ReceiveTime = x.ShowArrivalTime,
                        ServiceType = x.ServiceType,
                        CarPlate = x.CarNo,
                        ReceiveBy = x.TechName
                    })?.ToList() ?? new List<Core.Model.Reserve.HistoryReceiveVo>()
                };
                if (reserve.Status == 1 &&
                    DateTime.Now.AddMinutes(-reserveDelayMinute).CompareTo(reserve.ReserveTime) <= 0)
                {
                    result.Operation.Add("CanCancel");
                    result.Operation.Add("CanModify");
                }

                return result;
            }

            return null;
        }

        /// <summary>
        /// 预约时间看板
        /// </summary>
        /// <returns></returns>
        public async Task<List<ReserveDateVo>> GetReserveDate(ReserveDateRequest request)
        {
            DateTime now = DateTime.Now;
            DateTime todayDateTime = new DateTime(now.Year, now.Month, now.Day);
            var defaultConfigTask =
                _shopReserveTimeConfigRepository.GetDefaultReserveTimeConfigByShopId(request.ShopId);
            var definedConfigTask = _shopReserveTimeConfigRepository.GetDefinedReserveTimeConfigByShopId(request.ShopId,
                now.ToString("yyyy-MM-dd"), now.AddDays(10).ToString("yyyy-MM-dd"));
            var reserveListTask = _shopReserveRepository.GetReserveListWithCondition(new ReceiveListWithCondition()
            {
                ShopId = request.ShopId,
                StarTime = todayDateTime,
                EndTime = todayDateTime.AddDays(10),
                HideCanceled = true
            });
            await Task.WhenAll(defaultConfigTask, definedConfigTask, reserveListTask);
            var defaultConfig = defaultConfigTask.Result ?? new List<ShopReserveTimeConfigDO>();
            var definedConfig = definedConfigTask.Result ?? new List<ShopReserveTimeConfigDO>();
            var reserveList = reserveListTask.Result ?? new List<ShopReserveDO>();
            List<ReserveDateVo> result = new List<ReserveDateVo>();
            for (int i = 0; i < 10; i++)
            {
                List<ReserveTimeRecord> records = new List<ReserveTimeRecord>();
                var date = now.AddDays(i);
                var dayOfWeek = GetDayOfWeekInt(date);
                var dayOfWeekDisplay = GetDayOfWeek(date);
                if (date.ToString("yyyyMMdd").Equals(now.ToString("yyyyMMdd")))
                {
                    dayOfWeekDisplay = "今天";
                }

                if (date.ToString("yyyyMMdd").Equals(now.AddDays(1).ToString("yyyyMMdd")))
                {
                    dayOfWeekDisplay = "明天";
                }

                for (int j = 6; j < 23; j++)
                {
                    DateTime reserveTime = new DateTime(date.Year, date.Month, date.Day, j, 0, 0);
                    string hour = j < 10 ? $"0{j}:00" : $"{j}:00";
                    ReserveTimeRecord timeRecord = new ReserveTimeRecord()
                    {
                        DatePart = hour,
                        DateTimePart = reserveTime.ToString("yyyy-MM-dd HH:mm"),
                        ReservedNum = reserveList.Where(_ => _.ReserveTime == reserveTime).ToList().Count
                    };

                    var definedItem = definedConfig.FirstOrDefault(_ =>
                        _.YearDay == date.ToString("yyyy-MM-dd") && _.StartTime == hour);
                    if (definedItem == null)
                    {
                        definedItem = defaultConfig.FirstOrDefault(_ => _.WeekDay == dayOfWeek && _.StartTime == hour);
                    }

                    if (definedItem != null)
                    {
                        if (definedItem.ReserveCount > 0)
                        {
                            timeRecord.EnableNum = definedItem.ReserveCount;
                        }
                    }

                    if (now > reserveTime)
                    {
                        timeRecord.Enable = false; //逾期
                        timeRecord.Overdue = true;
                    }
                    else
                    {
                        if (timeRecord.EnableNum != null && timeRecord.EnableNum > 0)
                        {
                            if (timeRecord.EnableNum > timeRecord.ReservedNum)
                            {
                                timeRecord.Enable = true;
                            }
                            else
                            {
                                timeRecord.Enable = false; //已约满
                            }
                        }
                        else
                        {
                            timeRecord.Enable = false; //繁忙
                            timeRecord.Remark = "繁忙";
                        }
                    }

                    records.Add(timeRecord);
                }

                ReserveDateVo reserveDate = new ReserveDateVo()
                {
                    Date = date.ToString("MM/dd"),
                    Week = dayOfWeekDisplay,
                    Items = records,
                    FullReserve = records.All(x => !x.Enable)
                };

                result.Add(reserveDate);
            }

            return result;
        }

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopReserveDTO> GetShopReserveDO(ReserveDetailRequest request)
        {
            var reserveDo = await _shopReserveRepository.GetAsync(request.ReserveId);
            return _mapper.Map<ShopReserveDTO>(reserveDo);
        }

        #endregion

        #region 预约处理记录

        /// <summary>
        /// 获取预约处理记录
        /// </summary>
        /// <param name="reserveId"></param>
        /// <returns></returns>
        public async Task<List<ReserveTrackVo>> GetReserveTrackLog(long reserveId)
        {
            List<ReserveTrackVo> result = new List<ReserveTrackVo>();
            var reserveLog = await _reserveTrackRepository.GetReserveTrackList(reserveId);
            if (reserveLog != null && reserveLog.Any())
            {
                var mainLog = reserveLog.GroupBy(_ => _.LogId).ToDictionary(o => o.Key, o => o.Select(t => t).ToList());
                foreach (var itemLog in mainLog.OrderByDescending(_ => _.Key).ToList())
                {
                    var defaultItem = itemLog.Value.FirstOrDefault();
                    if (defaultItem != null)
                    {
                        ReserveTrackVo trackLog = new ReserveTrackVo()
                        {
                            OptTitle = defaultItem.Content,
                            OptTime = defaultItem.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")
                        };
                        foreach (var detailItem in itemLog.Value.Where(x => !string.IsNullOrEmpty(x.FieldName)))
                        {
                            if (defaultItem.OptType.Equals(ReserveOptEnum.Modify.ToString()))
                            {
                                trackLog.Detail.Add(
                                    $"{detailItem.FieldName}：{detailItem.BeforeValue} 修改为 {detailItem.AfterValue}");
                            }
                            else
                            {
                                trackLog.Detail.Add($"{detailItem.FieldName}：{detailItem.AfterValue}");
                            }
                        }

                        result.Add(trackLog);
                    }
                }
            }

            return result;
        }

        public async Task<bool> AddReserveTrackLog(long reserveId, string submitBy, ReserveOptEnum operationType,
            List<ReserveTrackDetail> detail)
        {
            DateTime now = DateTime.Now;
            string content = string.Empty;
            switch (operationType)
            {
                case ReserveOptEnum.Create:
                    content = $"{submitBy}创建了预约记录";
                    break;
                case ReserveOptEnum.Modify:
                    content = $"{submitBy}修改了预约记录";
                    break;
                case ReserveOptEnum.Cancel:
                    content = $"{submitBy}取消了预约记录";
                    break;
            }

            ReserveTrackDO reserveTrack = new ReserveTrackDO()
            {
                ReserveId = reserveId,
                OptType = operationType.ToString(),
                Content = content,
                CreateTime = now,
                CreateBy = submitBy
            };
            var logId = await _reserveTrackRepository.InsertAsync(reserveTrack);
            if (logId > 0)
            {
                List<ReserveTrackDetailDO> trackDetail = detail.Select(_ => new ReserveTrackDetailDO
                {
                    LogId = logId,
                    Field = _.Field,
                    FieldName = _.FieldName,
                    BeforeValue = _.BeforeValue,
                    AfterValue = _.AfterValue,
                    CreatedBy = submitBy,
                    CreatedTime = now
                }).ToList();
                if (trackDetail.Any())
                {
                    await _reserveTrackDetailRepository.InsertBatchAsync(trackDetail);
                }
            }

            return true;
        }

        #endregion


        #region  内部调用

        /// <summary>
        /// 校验一人一次是否存在预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> CheckTheDayReserveWithUserCarId(CheckReserveTimeRequest request)
        {
            DateTime dt = DateTime.Now;
            //判断一人一车是否已有预约
            var reserveInfo = await _shopReserveRepository.GetTheDayReserveeInfo(request.UserId, request.ReserveTime.DayOfYear - dt.DayOfYear, request.CarId, request.ShopId);
            if (reserveInfo != null)
            {
                return new ApiResult<bool>
                {
                    Data = true,
                    Code = ResultCode.Success,
                    Message = CommonConstant.ReserveExist
                };
            }
            else
            {
                return new ApiResult<bool>
                {
                    Data = false,
                    Code = ResultCode.Success,
                    Message = CommonConstant.NoReserveInfo
                };
            }
        }
        #endregion

        #region 私有

        /// <summary>
        /// 新增预约
        /// </summary>
        /// <param name="shopReserveDO"></param>
        /// <returns></returns>
        private async Task<int> AddReserve(ShopReserveDO shopReserveDO)
        {
            return await _shopReserveRepository.InsertAsync(shopReserveDO);
        }

        #endregion
    }
}
