using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Clients.Order;
using Ae.ShopOrder.Service.Client.Clients.OrderForC;
using Ae.ShopOrder.Service.Client.Clients.Stock;
using Ae.ShopOrder.Service.Client.Clients.WMS;
using Ae.ShopOrder.Service.Client.Model.WMS;
using Ae.ShopOrder.Service.Client.Request.Order;
using Ae.ShopOrder.Service.Client.Request.WMS;
using Ae.ShopOrder.Service.Common.Constant;
using Ae.ShopOrder.Service.Common.Extension;
using Ae.ShopOrder.Service.Core.Enums;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.OrderForC;
using Ae.ShopOrder.Service.Core.Request.Sign;
using Ae.ShopOrder.Service.Core.Response.Sign;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;

namespace Ae.ShopOrder.Service.Imp.Services
{
    /// <summary>
    /// 签收服务
    /// </summary>
    public class SignService : ISignService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<SignService> _logger;
        private readonly IWMSClient _WMSClient;
        private readonly ISignRepository _signRepository;
        private readonly IOrderClient _orderClient;
        private readonly IOrderCommandForCClient _orderCommandForCClient;
        private readonly IOrderQueryService _orderQueryService;
        private readonly IShopStockClient _shopStockClient;
        private readonly IOrderRepository _orderRepository;
        private readonly IDelegateUserOrderRepository _delegateUserOrderRepository;


        public SignService(IWMSClient WMSClient, IMapper mapper, ApolloErpLogger<SignService> logger,
            ISignRepository signRepository, IOrderClient orderClient, IOrderCommandForCClient orderCommandForCClient, IOrderQueryService orderQueryService, IShopStockClient shopStockClient, IOrderRepository orderRepository, IDelegateUserOrderRepository delegateUserOrderRepository)
        {
            _WMSClient = WMSClient;
            _mapper = mapper;
            _logger = logger;
            _signRepository = signRepository;
            _orderClient = orderClient;
            _orderCommandForCClient = orderCommandForCClient;
            _orderQueryService = orderQueryService;
            _shopStockClient = shopStockClient;
            _orderRepository = orderRepository;
            _delegateUserOrderRepository = delegateUserOrderRepository;

        }
        /// <summary>
        /// 验证待签收的订单/包裹号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<ValidateWaitingSignOrPackageResponse>> GetValidateWaitingSign(ValidateWaitingSignOrderOrPackageRequest request)
        {
            // var haveSignPackage = await _signRepository.GetHaveSignPackageRelationNo(request.Content, request.ShopId);

            //有签收记录去验证是否已经被签收过
            //if (haveSignPackage != null && haveSignPackage.Any())
            //{
            //    var signStatus = haveSignPackage?.FirstOrDefault()?.SignStatus;
            //    //如果订单号里面完全被签收，直接提示
            //    if (signStatus == SignRecordStatusEnum.HaveSign.ToInt())
            //    {
            //        return new ApiResult<ValidateWaitingSignOrPackageResponse>()
            //        {
            //            Code = ResultCode.SUCCESS_EXIST,
            //            Message = $"{request.Content.Trim()}{CommonConstant.PackOrOrderHaveSign}"
            //        };
            //    }
            //    else
            //    {
            //        var relationNo = haveSignPackage?.FirstOrDefault()?.RelationNo;
            //        //如果录入的是包裹号，需要去验证此包裹号已经被重复签收
            //        if (relationNo?.Trim() != request.Content.Trim())
            //        {
            //            bool? checkIsExist = haveSignPackage.Exists(_ => _.PackageNo.Trim() == request.Content.Trim());

            //            if (checkIsExist ?? false)
            //            {
            //                return new ApiResult<ValidateWaitingSignOrPackageResponse>()
            //                {
            //                    Code = ResultCode.SUCCESS_EXIST,
            //                    Message = $"{request.Content.Trim()}{CommonConstant.PackOrOrderHaveSign}"
            //                };
            //            }

            //        }
            //    }
            //}

            var transferTypeName = string.Empty;
            var transferTypeValue = string.Empty;
            if (request.Content.IndexOf("PHD") != -1)
            {
                transferTypeName = WmsStockOutTypeEnum.Distribute.GetDescription();
                transferTypeValue = WmsStockOutTypeEnum.Distribute.ToString();
            }
            else if (request.Content.IndexOf("RGC") != -1)
            {
                transferTypeName = WmsStockOutTypeEnum.Order.GetDescription();
                transferTypeValue = WmsStockOutTypeEnum.Order.ToString();
            }

            //  var content = Regex.Replace(request.Content.Trim(), @"\D", "");
            // long.TryParse(content, out long transferId);
            var getWareHouseTransferClientRequest = new GetWareHouseTransferClientRequest
            {
                TargetWarehouse = request.ShopId,
                TransferType = transferTypeName,
                TransferId = request.Content.Trim()
            };
            var getWarehouseTransferAllTaskResponse = await _WMSClient.GetWarehouseTransferAllTask(getWareHouseTransferClientRequest);
            if (getWarehouseTransferAllTaskResponse.Code == ResultCode.Success &&
                getWarehouseTransferAllTaskResponse.Data != null)
            {
                if (getWarehouseTransferAllTaskResponse.Data.TargetWarehouse != request.ShopId)
                {
                    return new ApiResult<ValidateWaitingSignOrPackageResponse>()
                    {
                        Code = ResultCode.Failed,
                        Message = $"{request.Content.Trim()}{CommonConstant.IsNotShop}"
                    };
                }
                var deliveryCodes = getWarehouseTransferAllTaskResponse.Data?.Packages?.ToList();
                var waitingSignPackage = new List<WaitingSignPackageVO>();
                if (deliveryCodes != null && deliveryCodes.Any())
                {
                    var validateWaitingSignOrPackage = new ValidateWaitingSignOrPackageResponse
                    {
                        SignCode = request.Content.Trim(),
                        SignType = transferTypeValue
                    };
                    deliveryCodes.ForEach(_ =>
                    {
                        var tupleRes = GetPackageStatus(_.Status);
                        waitingSignPackage.Add(new WaitingSignPackageVO()
                        {
                            PackageNo = _.DeliveryCode,
                            Status = tupleRes.Item1,
                            Code = tupleRes.Item2
                        });
                    });
                    validateWaitingSignOrPackage.WaitingPackage = waitingSignPackage;
                    return new ApiResult<ValidateWaitingSignOrPackageResponse>()
                    {
                        Code = ResultCode.Success,
                        Data = validateWaitingSignOrPackage
                    };
                }
            }
            else
            {
                var getTransferPackageTaskResponse = await _WMSClient.GetTransferPackageTask(new GetWareHouseTransferPackageClientRequest
                {
                    DeliveryCode = request.Content.Trim()
                });

                if (getTransferPackageTaskResponse.Code != ResultCode.Success)
                    return new ApiResult<ValidateWaitingSignOrPackageResponse>()
                    {
                        Code = ResultCode.SUCCESS_NOT_EXIST,
                        Message = $"{request.Content.Trim()}{CommonConstant.ValidatePackageIsNotExist}"
                    };
                var transferId = getTransferPackageTaskResponse?.Data?.FirstOrDefault()?.TransferId;
                var transferType = getTransferPackageTaskResponse?.Data?.FirstOrDefault()?.TransferType;
                var packageStatus = getTransferPackageTaskResponse?.Data?.FirstOrDefault()?.Status;

                var typeValue = string.Empty;
                if (transferType == WmsStockOutTypeEnum.Distribute.GetDescription())
                {
                    typeValue = WmsStockOutTypeEnum.Distribute.ToString();
                }
                else if (transferType == WmsStockOutTypeEnum.Order.GetDescription())
                {
                    typeValue = WmsStockOutTypeEnum.Order.ToString();
                }

                //为了验证物流单号是否属于此门店
                var getWarehouseTransferAllTaskAllResponse = await _WMSClient.GetWarehouseTransferAllTask(new GetWareHouseTransferClientRequest
                {
                    TargetWarehouse = request.ShopId,
                    TransferType = transferType,
                    TransferId = transferId
                });
                if (getWarehouseTransferAllTaskAllResponse.Code == ResultCode.Success &&
                    getWarehouseTransferAllTaskAllResponse.Data != null)
                {
                    if (getWarehouseTransferAllTaskAllResponse.Data.TargetWarehouse != request.ShopId)
                    {
                        return new ApiResult<ValidateWaitingSignOrPackageResponse>()
                        {
                            Code = ResultCode.Failed,
                            Message = $"{request.Content.Trim()}{CommonConstant.IsNotShop}"
                        };
                    }
                }
                var deliveryCode = getTransferPackageTaskResponse?.Data?.FirstOrDefault();
                if (!string.IsNullOrEmpty(deliveryCode?.DeliveryCode))
                {
                    var tupleRes = GetPackageStatus(packageStatus);
                    return new ApiResult<ValidateWaitingSignOrPackageResponse>()
                    {
                        Code = ResultCode.Success,
                        Data = new ValidateWaitingSignOrPackageResponse
                        {
                            SignCode = request.Content.Trim(),
                            SignType = SignTypeEnum.Package.ToString(),
                            WaitingPackage = new List<WaitingSignPackageVO>()
                            {
                                new WaitingSignPackageVO() {PackageNo = deliveryCode.DeliveryCode,Status = tupleRes.Item1,Code = tupleRes.Item2}
                            },
                            Type = typeValue
                        }
                    };
                }
            }
            return new ApiResult<ValidateWaitingSignOrPackageResponse>()
            {
                Code = ResultCode.SUCCESS_NOT_EXIST,
                Message = $"{request.Content.Trim()}{CommonConstant.ValidatePackageIsNotExist}"
            };
        }

        public Tuple<string, int> GetPackageStatus(string status)
        {

            var packageStatus = string.Empty;
            int code = 0;
            if (status == PackageStatusEnum.已签收.ToString())
            {
                packageStatus = PackageStatusEnum.已签收.ToString();
                code = 1;
            }
            else if (status == PackageStatusEnum.已清点.ToString())
            {
                packageStatus = $"{PackageStatusEnum.已签收.ToString()}/{PackageStatusEnum.已清点.ToString()}";
                code = 2;
            }
            else if (status == PackageStatusEnum.已发出.ToString())
            {
                code = 0;
            }
            return new Tuple<string, int>(packageStatus, code);
        }

        #region 备份代码
        /// <summary>
        /// 验证待签收的订单/包裹号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //public async Task<ApiResult<ValidateWaitingSignOrPackageResponse>> GetValidateWaitingSign(ValidateWaitingSignOrderOrPackageRequest request)
        //{
        //    var haveSignPackage = await _signRepository.GetHaveSignPackageRelationNo(request.Content, request.ShopId);

        //    //有签收记录去验证是否已经被签收过
        //    if (haveSignPackage != null && haveSignPackage.Any())
        //    {
        //        var signStatus = haveSignPackage?.FirstOrDefault()?.SignStatus;
        //        //如果订单号里面完全被签收，直接提示
        //        if (signStatus == SignRecordStatusEnum.HaveSign.ToInt())
        //        {
        //            return new ApiResult<ValidateWaitingSignOrPackageResponse>()
        //            {
        //                Code = ResultCode.SUCCESS_EXIST,
        //                Message = $"{request.Content.Trim()}{CommonConstant.PackOrOrderHaveSign}"
        //            };
        //        }
        //        else
        //        {
        //            var relationNo = haveSignPackage?.FirstOrDefault()?.RelationNo;
        //            //如果录入的是包裹号，需要去验证此包裹号已经被重复签收
        //            if (relationNo?.Trim() != request.Content.Trim())
        //            {
        //                bool? checkIsExist = haveSignPackage.Exists(_ => _.PackageNo.Trim() == request.Content.Trim());

        //                if (checkIsExist ?? false)
        //                {
        //                    return new ApiResult<ValidateWaitingSignOrPackageResponse>()
        //                    {
        //                        Code = ResultCode.SUCCESS_EXIST,
        //                        Message = $"{request.Content.Trim()}{CommonConstant.PackOrOrderHaveSign}"
        //                    };
        //                }

        //            }
        //        }
        //    }

        //    //  var content = Regex.Replace(request.Content.Trim(), @"\D", "");
        //    // long.TryParse(content, out long transferId);
        //    var getWareHouseTransferClientRequest = new GetWareHouseTransferClientRequest
        //    {
        //        TargetWarehouse = request.ShopId,
        //        TransferType = WmsStockOutTypeEnum.Order.GetDescription(),
        //        TransferId = request.Content.Trim()
        //    };
        //    var getWarehouseTransferAllTaskResponse = await _WMSClient.GetWarehouseTransferAllTask(getWareHouseTransferClientRequest);
        //    if (getWarehouseTransferAllTaskResponse.Code == ResultCode.Success &&
        //        getWarehouseTransferAllTaskResponse.Data != null)
        //    {
        //        if (getWarehouseTransferAllTaskResponse.Data.TargetWarehouse != request.ShopId)
        //        {
        //            return new ApiResult<ValidateWaitingSignOrPackageResponse>()
        //            {
        //                Code = ResultCode.Failed,
        //                Message = $"{request.Content.Trim()}{CommonConstant.IsNotShop}"
        //            };
        //        }
        //        var deliveryCodes = getWarehouseTransferAllTaskResponse.Data?.Packages?.Select(x => x.DeliveryCode).ToList();
        //        var waitingSignPackage = new List<WaitingSignPackageVO>();
        //        if (deliveryCodes != null && deliveryCodes.Any())
        //        {
        //            var validateWaitingSignOrPackage = new ValidateWaitingSignOrPackageResponse
        //            {
        //                SignCode = request.Content.Trim(),
        //                SignType = SignTypeEnum.Order.ToString()
        //            };
        //            deliveryCodes.ForEach(_ =>
        //            {
        //                if (!haveSignPackage?.Exists(item => item.PackageNo == _) ?? false)
        //                {
        //                    waitingSignPackage.Add(new WaitingSignPackageVO()
        //                    {
        //                        PackageNo = _
        //                    });
        //                }
        //            });
        //            validateWaitingSignOrPackage.WaitingPackage = waitingSignPackage;
        //            return new ApiResult<ValidateWaitingSignOrPackageResponse>()
        //            {
        //                Code = ResultCode.Success,
        //                Data = validateWaitingSignOrPackage
        //            };
        //        }
        //    }
        //    else
        //    {
        //        var getTransferPackageTaskResponse = await _WMSClient.GetTransferPackageTask(new GetWareHouseTransferPackageClientRequest
        //        {
        //            DeliveryCode = request.Content.Trim()
        //        });

        //        if (getTransferPackageTaskResponse.Code != ResultCode.Success)
        //            return new ApiResult<ValidateWaitingSignOrPackageResponse>()
        //            {
        //                Code = ResultCode.SUCCESS_NOT_EXIST,
        //                Message = $"{request.Content.Trim()}{CommonConstant.ValidatePackageIsNotExist}"
        //            };
        //        var transferId = getTransferPackageTaskResponse?.Data?.FirstOrDefault()?.TransferId;
        //        //为了验证物流单号是否属于此门店
        //        var getWarehouseTransferAllTaskAllResponse = await _WMSClient.GetWarehouseTransferAllTask(new GetWareHouseTransferClientRequest
        //        {
        //            TargetWarehouse = request.ShopId,
        //            TransferType = WmsStockOutTypeEnum.Order.GetDescription(),
        //            TransferId = transferId
        //        });
        //        if (getWarehouseTransferAllTaskAllResponse.Code == ResultCode.Success &&
        //            getWarehouseTransferAllTaskAllResponse.Data != null)
        //        {
        //            if (getWarehouseTransferAllTaskAllResponse.Data.TargetWarehouse != request.ShopId)
        //            {
        //                return new ApiResult<ValidateWaitingSignOrPackageResponse>()
        //                {
        //                    Code = ResultCode.Failed,
        //                    Message = $"{request.Content.Trim()}{CommonConstant.IsNotShop}"
        //                };
        //            }
        //        }
        //        var deliveryCode = getTransferPackageTaskResponse?.Data?.FirstOrDefault();
        //        if (!string.IsNullOrEmpty(deliveryCode?.DeliveryCode))
        //        {
        //            return new ApiResult<ValidateWaitingSignOrPackageResponse>()
        //            {
        //                Code = ResultCode.Success,
        //                Data = new ValidateWaitingSignOrPackageResponse
        //                {
        //                    SignCode = request.Content.Trim(),
        //                    SignType = SignTypeEnum.Package.ToString(),
        //                    WaitingPackage = new List<WaitingSignPackageVO>()
        //                    {
        //                        new WaitingSignPackageVO() {PackageNo = deliveryCode.DeliveryCode}
        //                    }
        //                }
        //            };
        //        }

        //    }
        //    return new ApiResult<ValidateWaitingSignOrPackageResponse>()
        //    {
        //        Code = ResultCode.SUCCESS_NOT_EXIST,
        //        Message = $"{request.Content.Trim()}{CommonConstant.ValidatePackageIsNotExist}"
        //    };
        //}

        #endregion


        /// <summary>
        /// 今日签收包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TodaySignPackageApiPagedResult<GetTodaySignPackageResponse>> GetTodaySignPackage(GetTodaySignPackageRequest request)
        {
            var getSignDetailList = await _signRepository.GetSignDetailList(request.ShopId, request.PageSize, request.PageIndex);

            if (getSignDetailList?.Items != null && getSignDetailList.Items.Any())
            {
                var pageData = _mapper.Map<TodaySignPackageApiPagedResult<GetTodaySignPackageResponse>>(getSignDetailList);
                pageData.TitleNum = pageData.TotalItems;
                return pageData;
            }

            return null;

        }
        /// <summary>
        ///  今日收货进度
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<GetTodayReceiveResponse> GetTodayReceiveTask(TodayReceiveRequest request)
        {
            GetTodayReceiveResponse receiveResponse = new GetTodayReceiveResponse()
            {
                Items = new List<GetTodayReceiveVO>()
            };

            var geTodayReceiveDosList = await _signRepository.GetTodayReceiveOrder(request.ShopId);

            var getBaseInfoForBusinessStatusResponse = await _orderClient.GetOrderBaseInfoForBusinessStatus(new GetOrderBaseInfoForBusinessStatusRequest()
            {
                OrderBusinessStatus = OrderBusinessStatusEnum.WaitingSign,
                ShopId = request.ShopId
            });

            if (geTodayReceiveDosList != null && geTodayReceiveDosList.Any())
            {

                var getTodayReceiveVos = _mapper.Map<List<GetTodayReceiveVO>>(geTodayReceiveDosList);

                receiveResponse.Items = getTodayReceiveVos;

                receiveResponse.TitleNum = receiveResponse?.Items?.Count ?? 0;
            }

            if (getBaseInfoForBusinessStatusResponse.Code == ResultCode.Success)
            {
                var orderNos = getBaseInfoForBusinessStatusResponse?.Data?.Items?.Select(x => x.OrderNo)?.ToList();

                var packageInfo = await _orderQueryService.GetBatchWarehouseTransferPackages(new GetBatchWarehouseTransferPackagesRequest()
                {
                    OrderIds = orderNos,
                    TransferType = TransferTypeEnum.Order.GetDescription()
                });
                orderNos?.ForEach(x =>
                {
                    var isExistSign = receiveResponse?.Items.Where(item => item.OrderNo == x)?.Any() ?? false;
                    if (!isExistSign)
                    {
                        receiveResponse.Items.Add(new GetTodayReceiveVO()
                        {
                            OrderNo = x,
                            DeliveryInfo = string.Empty,
                            HaveSignNum = 0,
                            SignStatus = string.Empty,
                            SignUser = string.Empty,
                            SumNum = packageInfo?.Data?.Where(item => item.TransferId == x)?.Count() ?? 0,
                            SignTime = new DateTime(1900, 1, 1)
                        });
                    }
                });
            }

            receiveResponse.TitleNum = receiveResponse?.Items?.Count() ?? 0;
            return receiveResponse;
        }

        /// <summary>
        /// 签收包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SignResponse>> UserSign(SignRequest request)
        {
            var getHaveSignRecord = await _signRepository.GetHaveSignDetail(request.Content, request.ShopId);

            if (getHaveSignRecord != null && getHaveSignRecord.Any())
            {
                var codeMessage = new StringBuilder();
                getHaveSignRecord?.Select(x => x.PackageNo)?.ToList()?.ForEach(x =>
                {
                    codeMessage.AppendLine($"{x} {CommonConstant.PackageHaveSign}");
                });

                return new ApiResult<SignResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = codeMessage.ToString()
                };
            }
            else
            {

                var getWareHouseTransferPackageClientRequest = new GetWareHouseTransferPackageClientRequest
                {
                    DeliveryCode = request.Content?.FirstOrDefault()
                };
                var getTransferPackageTaskResponse = await _WMSClient.GetTransferPackageTask(getWareHouseTransferPackageClientRequest);

                if (getTransferPackageTaskResponse.Code == ResultCode.Success)
                {
                    if (getTransferPackageTaskResponse.Data == null || !getTransferPackageTaskResponse.Data.Any())
                    {
                        return new ApiResult<SignResponse>()
                        {
                            Code = ResultCode.Failed,
                            Message = $"{request.Content?.FirstOrDefault()} {CommonConstant.PackageNotExist}"
                        };
                    }

                    var transferId = getTransferPackageTaskResponse?.Data?.FirstOrDefault()?.TransferId;
                    var transferTypeName = string.Empty;
                    var transferTypeValue = string.Empty;
                    if (transferId.IndexOf("PHD") != -1)
                    {
                        transferTypeName = WmsStockOutTypeEnum.Distribute.GetDescription();
                        transferTypeValue = WmsStockOutTypeEnum.Distribute.ToString();
                    }
                    else if (transferId.IndexOf("RGC") != -1)
                    {
                        transferTypeName = WmsStockOutTypeEnum.Order.GetDescription();
                        transferTypeValue = WmsStockOutTypeEnum.Order.ToString();
                    }

                    var getWareHouseTransferClientRequest = new GetWareHouseTransferClientRequest
                    {
                        TargetWarehouse = request.ShopId,
                        TransferType = transferTypeName,
                        TransferId = transferId
                    };
                    var getWarehouseTransferAllTaskResponse = await _WMSClient.GetWarehouseTransferAllTask(getWareHouseTransferClientRequest);
                    if (getWarehouseTransferAllTaskResponse.Code == ResultCode.Success &&
                        getWarehouseTransferAllTaskResponse.Data != null)
                    {
                        var notExistPackage = new List<string>();

                        request.Content?.ForEach(x =>
                        {
                            var packageNoObj = getWarehouseTransferAllTaskResponse?.Data?.Packages
                                ?.Where(_ => _.DeliveryCode.Trim() == x.Trim())?.ToList();
                            if (packageNoObj == null || !packageNoObj.Any())
                            {
                                notExistPackage.Add(x);
                            }
                        });

                        if (notExistPackage.Any())
                        {
                            var codeMessage = new StringBuilder();
                            notExistPackage?.ForEach(x =>
                            {
                                codeMessage.AppendLine($"{x} 包裹号不存在;");
                            });
                            return new ApiResult<SignResponse>()
                            {
                                Code = ResultCode.Failed,
                                Message = codeMessage.ToString()
                            };
                        }

                        if (request.ShopId != getWarehouseTransferAllTaskResponse.Data.TargetWarehouse)
                        {
                            return new ApiResult<SignResponse>()
                            {
                                Code = ResultCode.Failed,
                                Message = CommonConstant.PackageIsNotShop
                            };
                        }

                        //订单
                        if (transferId.IndexOf("RGC") != -1)
                        {
                            var orderBaseInfo = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
                            {
                                ShopId = request.ShopId,
                                OrderNos = new List<string>() { transferId }
                            });
                            if (orderBaseInfo.Code == ResultCode.Success && orderBaseInfo.Data != null && orderBaseInfo.Data.Any())
                            {
                                var sumNum = getWarehouseTransferAllTaskResponse?.Data?.Packages?.Count ?? 0;
                                var haveSignNum = request.Content?.Count ?? 0;
                                var packageNos =
                                    getWarehouseTransferAllTaskResponse?.Data?.Packages?.Select(x => x.DeliveryCode) ?? new List<string>();
                                var signDO = new SignDO()
                                {
                                    RelationNo = orderBaseInfo.Data?.FirstOrDefault()?.OrderNo,
                                    SourceType = SignSourceTypeEnum.Order.ToSbyte(),
                                    Num = getWarehouseTransferAllTaskResponse?.Data?.Packages?.Count ?? 0,
                                    HaveSignNum = 0,
                                    SignStatus = (sumNum == haveSignNum)
                                        ? SignRecordStatusEnum.HaveSign.ToSbyte()
                                        : SignRecordStatusEnum.PartSign.ToSbyte(),
                                    ShopId = request.ShopId,
                                    PackageNos = String.Join("、", packageNos),
                                    Remark = string.Empty,
                                    CreateBy = request.SignUser,
                                    CreateTime = DateTime.Now,
                                    UpdateBy = request.SignUser,
                                    UpdateTime = DateTime.Now
                                };

                                var signDetails = new List<SignDetailDO>();

                                getWarehouseTransferAllTaskResponse?.Data?.Packages?.ForEach(item =>
                                {
                                    if (request.Content.Contains(item.DeliveryCode))
                                    {
                                        signDetails.Add(new SignDetailDO()
                                        {
                                            RelationNo = orderBaseInfo.Data?.FirstOrDefault()?.OrderNo,
                                            PackageNo = item.DeliveryCode,
                                            SignUser = request.SignUser,
                                            SignTime = DateTime.Now,
                                            ShopId = request.ShopId,
                                            Remark = string.Empty,
                                            CreateBy = request.SignUser,
                                            CreateTime = DateTime.Now,
                                            UpdateBy = request.SignUser,
                                            UpdateTime = DateTime.Now
                                        });
                                    }

                                });

                                var userSignRecord = await _signRepository.SaveSign(signDO, signDetails);

                                if (userSignRecord > 0)
                                {
                                    Task.Run(() =>
                                      {
                                          AfterSignComplete(getWarehouseTransferAllTaskResponse.Data, request.Content,
                                              request.ShopId, request.SignUser);
                                      });

                                    return new ApiResult<SignResponse>()
                                    {
                                        Code = ResultCode.Success,
                                        Message = CommonConstant.SignSuccess,
                                        Data = null
                                    };

                                }
                                else
                                {
                                    var codeMessage = new StringBuilder();
                                    request.Content?.ForEach(x =>
                                    {
                                        codeMessage.AppendLine($"{x} {CommonConstant.SignFailure};");
                                    });
                                    return new ApiResult<SignResponse>()
                                    {
                                        Code = ResultCode.Failed,
                                        Message = codeMessage.ToString(),
                                        Data = null
                                    };
                                }
                            }
                            else
                            {
                                return new ApiResult<SignResponse>()
                                {
                                    Code = ResultCode.SYSTEM_ERROR,
                                    Message = CommonConstant.OrderNotExist
                                };
                            }
                        }
                        //铺货
                        else if (transferId.IndexOf("PHD") != -1)
                        {
                            var sumNum = getWarehouseTransferAllTaskResponse?.Data?.Packages?.Count ?? 0;
                            var haveSignNum = request.Content?.Count ?? 0;
                            var packageNos =
                                getWarehouseTransferAllTaskResponse?.Data?.Packages?.Select(x => x.DeliveryCode) ?? new List<string>();
                            var signDO = new SignDO()
                            {
                                RelationNo = transferId,
                                SourceType = SignSourceTypeEnum.Distribute.ToSbyte(),
                                Num = getWarehouseTransferAllTaskResponse?.Data?.Packages?.Count ?? 0,
                                HaveSignNum = 0,
                                SignStatus = (sumNum == haveSignNum)
                                    ? SignRecordStatusEnum.HaveSign.ToSbyte()
                                    : SignRecordStatusEnum.PartSign.ToSbyte(),
                                ShopId = request.ShopId,
                                PackageNos = String.Join("、", packageNos),
                                Remark = string.Empty,
                                CreateBy = request.SignUser,
                                CreateTime = DateTime.Now,
                                UpdateBy = request.SignUser,
                                UpdateTime = DateTime.Now
                            };

                            var signDetails = new List<SignDetailDO>();

                            getWarehouseTransferAllTaskResponse?.Data?.Packages?.ForEach(item =>
                            {
                                if (request.Content.Contains(item.DeliveryCode))
                                {
                                    signDetails.Add(new SignDetailDO()
                                    {
                                        RelationNo = transferId,
                                        PackageNo = item.DeliveryCode,
                                        SignUser = request.SignUser,
                                        SignTime = DateTime.Now,
                                        ShopId = request.ShopId,
                                        Remark = string.Empty,
                                        CreateBy = request.SignUser,
                                        CreateTime = DateTime.Now,
                                        UpdateBy = request.SignUser,
                                        UpdateTime = DateTime.Now
                                    });
                                }

                            });

                            var userSignRecord = await _signRepository.SaveSign(signDO, signDetails);

                            if (userSignRecord > 0)
                            {
                                Task.Run(() =>
                                {
                                    AfterSignComplete(getWarehouseTransferAllTaskResponse.Data, request.Content,
                                        request.ShopId, request.SignUser);
                                });

                                return new ApiResult<SignResponse>()
                                {
                                    Code = ResultCode.Success,
                                    Message = CommonConstant.SignSuccess,
                                    Data = null
                                };

                            }
                            else
                            {
                                var codeMessage = new StringBuilder();
                                request.Content?.ForEach(x =>
                                {
                                    codeMessage.AppendLine($"{x} {CommonConstant.SignFailure};");
                                });
                                return new ApiResult<SignResponse>()
                                {
                                    Code = ResultCode.Failed,
                                    Message = codeMessage.ToString(),
                                    Data = null
                                };
                            }
                        }
                    }
                    else
                    {
                        return new ApiResult<SignResponse>()
                        {
                            Code = ResultCode.SYSTEM_ERROR,
                            Message = CommonConstant.ValidatePackageIsNotExist
                        };
                    }
                }

                return new ApiResult<SignResponse>()
                {
                    Code = ResultCode.SYSTEM_ERROR,
                    Message = CommonConstant.ErrorOccured
                };

            }

        }

        /// <summary>
        /// 签收之后的回写动作
        /// </summary>
        /// <param name="transferClientDto"></param>
        /// <param name="packageNos"></param>
        /// <param name="shopId"></param>
        /// <param name="signUser"></param>
        /// <returns></returns>
        private async Task AfterSignComplete(GetWareHouseTransferClientDTO transferClientDto, List<string> packageNos,
            int shopId, string signUser)
        {
            ApiResult UpdateWarehouseTransferPackageStatusResponse;
            try
            {
                var getSignDetail = await _signRepository.GetHaveSignDetail(new List<string>(), shopId, transferClientDto?.TransferId);
                var sumSignNum = getSignDetail?.Count ?? 0;

                var packageSumNumber = transferClientDto?.Packages.Count ?? 0;
                if (packageSumNumber == sumSignNum)
                {
                    var signRecordUpdate = await _signRepository.UpdateSignStatus(transferClientDto?.TransferId, signUser, SignRecordStatusEnum.HaveSign.ToInt(), packageNos.Count);

                    if (signRecordUpdate <= 0)
                    {
                        _logger.Error($"_signRepository UpdateSignStatus {transferClientDto?.TransferId} {UpdateOrderStatusTypeEnum.SignStatus} 更新失败");
                    }

                    if (transferClientDto?.TransferId.IndexOf("RGC") != -1)
                    {
                        var updateOrderStatusResponse = await _orderCommandForCClient.UpdateOrderStatus(new UpdateOrderStatusRequest()
                        {
                            OrderNos = new List<string>() { transferClientDto?.TransferId },
                            UpdateBy = signUser,
                            UpdateStatusType = UpdateOrderStatusTypeEnum.SignStatus
                        });
                        if (updateOrderStatusResponse.Code != ResultCode.Success)
                        {
                            _logger.Error($"_orderCommandForCClient UpdateOrderStatus {transferClientDto?.TransferId } {UpdateOrderStatusTypeEnum.SignStatus}  更新失败 {updateOrderStatusResponse.Message}");
                        }
                    }

                    //WMS签收主表
                    var updateWarehouseTransferSignUpResponse = await _WMSClient.UpdateWarehouseTransferSignUp(new UpdateWarehouseTransferSignUpClientRequest()
                    {
                        TransferId = transferClientDto?.TransferId,
                        TransferType = transferClientDto.TransferType,
                        Status = "已送达",
                        UpdateBy = signUser
                    });
                    if (updateWarehouseTransferSignUpResponse == null || updateWarehouseTransferSignUpResponse.Code != ResultCode.Success)
                    {
                        _logger.Error($"_WMSClient UpdateWarehouseTransferSignUp {transferClientDto?.TransferId} {UpdateOrderStatusTypeEnum.SignStatus} 更新失败 {updateWarehouseTransferSignUpResponse?.Message}");
                    }

                    //WMS入库主表
                    var transferProductList = new List<TransferProduct>();
                    transferClientDto?.Products?.ForEach(x =>
                        {
                            transferProductList.Add(new TransferProduct()
                            {
                                TransferProductId = x.Id,
                                ReceiveNum = x.Num
                            });
                        });
                    //签收时  订单回写出库任务的收货数量
                    if (transferClientDto.TransferType == WmsStockOutTypeEnum.Order.ToString())
                    {
                        var updateWarehouseTransferProductReceiveNumResponse = await _WMSClient.UpdateWarehouseTransferProductReceiveNum(
                               new UpdateWarehouseTransferProductReceiveNumClientRequest()
                               {
                                   TransferId = transferClientDto.TransferId,
                                   TransferType = transferClientDto.TransferType,
                                   UpdateBy = signUser,
                                   TransferList = transferProductList
                               });

                        if (updateWarehouseTransferProductReceiveNumResponse == null || updateWarehouseTransferProductReceiveNumResponse.Code != ResultCode.Success)
                        {
                            _logger.Error($"_WMSClient updateWarehouseTransferProductReceiveNumResponse {transferClientDto?.TransferId} {UpdateOrderStatusTypeEnum.SignStatus} 更新失败 {updateWarehouseTransferProductReceiveNumResponse?.Message}");
                        }
                    }
                }
                else
                {
                    await _signRepository.UpdateSignStatus(transferClientDto?.TransferId, signUser, SignRecordStatusEnum.PartSign.ToInt(), packageNos.Count);
                }

                //WMS签收包裹明细
                UpdateWarehouseTransferPackageStatusResponse = await _WMSClient.UpdateWarehouseTransferPackageStatus(new UpdateWarehouseTransferPackageStatusClientRequest()
                {
                    TransferId = transferClientDto?.TransferId,
                    TransferType = transferClientDto?.TransferType,
                    DeliveryCodes = packageNos,
                    UpdateBy = signUser
                });


                if (UpdateWarehouseTransferPackageStatusResponse == null || UpdateWarehouseTransferPackageStatusResponse.Code != ResultCode.Success)
                {
                    _logger.Error($"_WMSClient UpdateWarehouseTransferSignUp {transferClientDto?.TransferId} {UpdateOrderStatusTypeEnum.SignStatus} 更新失败 {UpdateWarehouseTransferPackageStatusResponse?.Message}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"AfterSignComplete  {transferClientDto?.TransferId} {UpdateOrderStatusTypeEnum.SignStatus} 更新失败 {JsonConvert.SerializeObject(ex)}");
            }
        }

        public async Task<ApiResult<SignResponse>> ShopToSamllwarehouseOrderUserSign(SignRequest request)
        {
            var orderBaseInfo = await _orderClient.GetOrderBaseInfo(new GetOrderBaseInfoRequest()
            {
                OrderNos = request.Content
            });
            if (orderBaseInfo?.Data?.Count() > 0)
            {
                var order = orderBaseInfo?.Data?.FirstOrDefault();
                var signDO = new SignDO()
                {
                    RelationNo = order?.OrderNo,
                    SourceType = SignSourceTypeEnum.Order.ToSbyte(),
                    Num = order.TotalProductNum,
                    HaveSignNum = order.TotalProductNum,
                    SignStatus = SignRecordStatusEnum.HaveSign.ToSbyte(),
                    ShopId = request.ShopId,
                    PackageNos = string.Empty,
                    Remark = string.Empty,
                    CreateBy = request.SignUser,
                    CreateTime = DateTime.Now,
                    UpdateBy = request.SignUser,
                    UpdateTime = DateTime.Now
                };

                var signDetails = new List<SignDetailDO>();

                signDetails.Add(new SignDetailDO()
                {
                    RelationNo = order?.OrderNo,
                    PackageNo = string.Empty,
                    SignUser = request.SignUser,
                    SignTime = DateTime.Now,
                    ShopId = request.ShopId,
                    Remark = string.Empty,
                    CreateBy = request.SignUser,
                    CreateTime = DateTime.Now,
                    UpdateBy = request.SignUser,
                    UpdateTime = DateTime.Now
                });

                await _signRepository.SaveSign(signDO, signDetails);

                if (order.OrderNo.StartsWith("RGB"))
                {
                    var delegateOrder = await _delegateUserOrderRepository.GetListAsync(" where is_deleted=0 and  order_no=@OrderNo", new { OrderNo = order.OrderNo });
                    _shopStockClient.OrderInstallReduceStock(new Core.Request.Stock.OrderInstallReduceStockRequest
                    {
                        QueueNo = order.OrderNo,
                        TargetShopId = delegateOrder?.FirstOrDefault()?.ShopId??0,
                        CreateBy = request.SignUser,
                        QueueStatus = string.Empty,
                        SourceType = string.Empty
                    });
                }

                await _orderRepository.UpdateOrderSignStatus(order.OrderNo, request.SignUser);

                await _orderClient.UpdateOrderStatus(new UpdateOrderStatusRequest()
                {
                    OrderNos = new List<string>() { order.OrderNo },
                    UpdateBy = request.SignUser,
                    UpdateStatusType = UpdateOrderStatusTypeEnum.InstallStatus
                });

                await _orderRepository.UpdateOrderCompleteStatus(order.OrderNo, order.UpdateBy);
            }

            //_fmsClient.CalculationReconciliationFee(new CalculationReconciliationFeeRequest()
            //{
            //    OrderNo = request.OrderNo,
            //    CreateBy = request.UpdateBy
            //});

            return new ApiResult<SignResponse>()
            {
                Code = ResultCode.Success,
                Message = CommonConstant.SignSuccess,
                Data = null
            };
        }
    }
}
