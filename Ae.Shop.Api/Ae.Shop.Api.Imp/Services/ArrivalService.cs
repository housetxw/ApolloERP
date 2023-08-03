using AutoMapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Common.BrandLogoHelper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Arrival;
using Ae.Shop.Api.Core.Request.Arrival;
using Ae.Shop.Api.Core.Response.Arrival;
using Ae.Shop.Api.Dal.Model.Arrival;
using Ae.Shop.Api.Dal.Repositorys.Arrival;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Ae.Shop.Api.Common.Constant;
using Ae.Shop.Api.Common.Extension;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;

namespace Ae.Shop.Api.Imp.Services
{
    public class ArrivalService : IArrivalService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<ArrivalService> _logger;
        private readonly IArrivalRepository _arrivalRepo;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IIdentityService identityService;
        private readonly IShopReceiveCheckRepository _shopReceiveCheckRepository;
        private readonly IShopArrivalVideoRepository _shopArrivalVideoRepository;
        private readonly IShopArrivalCarReportRepository _shopArrivalCarReportRepository;

        public ArrivalService(ApolloErpLogger<ArrivalService> logger,
            IArrivalRepository arrivalRepository, IMapper mapper,
            IConfiguration configuration, IIdentityService identityService,
            IShopReceiveCheckRepository shopReceiveCheckRepository,
            IShopArrivalVideoRepository shopArrivalVideoRepository,
            IShopArrivalCarReportRepository shopArrivalCarReportRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _arrivalRepo = arrivalRepository;
            _mapper = mapper;
            this.identityService = identityService;
            _shopReceiveCheckRepository = shopReceiveCheckRepository;
            _shopArrivalVideoRepository = shopArrivalVideoRepository;
            _shopArrivalCarReportRepository = shopArrivalCarReportRepository;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 到店记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetArrivalListResponse>> GetArrivalList(GetArrivalListRequest request)
        {
            var organizationId = identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            if (shopId <= 0)
            {
                throw new CustomException("登录信息异常");
            }
            request.ShopId = shopId.ToString();
            var dalResponse = await _arrivalRepo.GetArrivalList(request);

            var ids = dalResponse?.Items?.Select(_ => _.Id)?.ToList();
            if (ids != null && ids.Any())
            {
               var shopReceiveChecks=await _shopReceiveCheckRepository.GetShopReceiveCheckInfo(ids);

                dalResponse?.Items?.ToList()?.ForEach(item =>
                {
                    item.CheckId = shopReceiveChecks?.Where(_ => _.ReceiveId == item.Id)?.Select(_ => _.Id)?.FirstOrDefault() ?? 0;
                });
            }
           

            return Result.Success(dalResponse.Items, dalResponse.TotalItems);
        }

        /// <summary>
        /// 到店记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetArrivalDetailResponse>> GetArrivalDetail(GetArrivalDetailRequest request)
        {
            var shopArrival = await _arrivalRepo.GetShopArrival(request.ArrivalId);

            var shopArrivalOrders = await _arrivalRepo.GetShopArrivalOrders(new List<long>() { request.ArrivalId });

            var arrivalVideos = await _shopArrivalVideoRepository.GetShopArrivalVideos(request.ArrivalId);//施工视频

            var carReport = await _shopArrivalCarReportRepository.GetCarReportByRecId(request.ArrivalId);//车辆诊断报告

            var projectItems = new List<ProjectItemVo>();

            shopArrivalOrders?.ForEach(_ =>
            {
                var price = _.Num * _.Price;
                projectItems.Add(new ProjectItemVo()
                {
                    OrderId = _.OrderNo,
                    Name = _.ProductName,
                    Num = _.Num,
                    Price = $"￥{price.ToString("F2")}",
                });
            });

            if (shopArrival == null)
            {
                return new ApiResult<GetArrivalDetailResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = "无此到店记录，请核对后重试！"
                };
            }

            var leaveShopReasonInfo = string.Empty;
            var shopArrivalCancelDo = await _arrivalRepo.GetAsync<ShopArrivalCancelDO>(request.ArrivalId);
            leaveShopReasonInfo = shopArrivalCancelDo != null ? $"{shopArrivalCancelDo.ReasonType}-{shopArrivalCancelDo.Remark}" : null;
            return new ApiResult<GetArrivalDetailResponse>()
            {
                Code = ResultCode.Success,
                Data = new GetArrivalDetailResponse()
                {
                    ArrivalId = request.ArrivalId.ToString(),
                    UserId = shopArrival?.UserId,
                    UserName = shopArrival?.UserName,
                    Telephone = shopArrival?.UserTel,
                    ShowArrivalDate = shopArrival?.ArrivalTime != null
                        ? shopArrival?.ArrivalTime.ToString("yyyy-MM-dd")
                        : "",
                    ShowArrivalStatus = shopArrival?.Status != null
                        ? ((ArrivalRecordStatusEnum) shopArrival?.Status).ToString()
                        : "",
                    PickOne = shopArrival?.TechName,
                    Remark = shopArrival?.Remark,
                    CarInfo = new CarVo()
                    {
                        CarId = shopArrival?.CarId,
                        CarLogo =
                            $"{_configuration["QiNiuImageDomain"]}{ImageHelper.GetLogoUrlByName(shopArrival?.Brand)}",
                        CarNo = shopArrival?.CarNo,
                        Brand = shopArrival?.Brand,
                        Vehicle = shopArrival?.Vehicle
                    },
                    ProjectItems = projectItems,
                    LeaveShopReasonInfo = leaveShopReasonInfo,
                    CarReportUrl = carReport?.CarReportUrl ?? string.Empty,
                    ArrivalVideos = arrivalVideos.Select(_ => new ArrivalVideo
                    {
                        Id = _.Id,
                        Name = _.VideoName,
                        Url = _.VideoPath
                    }).ToList()
                }
            };

        }

        public ArrivalTrendStatisticsResDTO GetArrivalTrendStatistics(ArrivalTrendStatisticsReqDTO req)
        {
            var res = new ArrivalTrendStatisticsResDTO();

            var (all, completed, leave, uncompleted) = GetArrivalTrendStatisticsAsync(req);

            var allDic = all.ToDictionary(d => d.ArrivalTime, d => d.Amount);
            var completedDic = completed.ToDictionary(d => d.ArrivalTime, d => d.Amount);
            var leaveDic = leave.ToDictionary(d => d.ArrivalTime, d => d.Amount);
            var uncompletedDic = uncompleted.ToDictionary(d => d.ArrivalTime, d => d.Amount);

            var dtList = req.EndTime.GetStartTimeOfDay().GetSpecificDaysDateTimeList(req.EndTime.Subtract(req.StartTime).Days);
            var tableList = new List<ArrivalTrendTableStatisticsEntityResDTO>();
            dtList.ForEach(d =>
            {
                var allVal = allDic.ContainsKey(d) ? allDic[d] : default;
                var completeVal = completedDic.ContainsKey(d) ? completedDic[d] : default;
                var leaveVal = leaveDic.ContainsKey(d) ? leaveDic[d] : default;
                var uncompletedVal = uncompletedDic.ContainsKey(d) ? uncompletedDic[d] : default;

                //Chart Data
                res.ChartStatistics.Dates.Add(d.ToString(CommonConstant.FmtDTToYearMonthDayLowerWithPeriod));
                res.ChartStatistics.Amount.Add(allVal);
                res.ChartStatistics.Completed.Add(completeVal);
                res.ChartStatistics.Leave.Add(leaveVal);
                res.ChartStatistics.UnCompleted.Add(uncompletedVal);

                //Table Data
                tableList.Add(new ArrivalTrendTableStatisticsEntityResDTO
                {
                    ArrivalTime = d,
                    Amount = allVal,
                    Completed = completeVal,
                    Leave = leaveVal,
                    UnCompleted = uncompletedVal
                });
            });

            //generated the format for the front end
            res.ChartStatistics.Dates.Reverse();
            res.ChartStatistics.Amount.Reverse();
            res.ChartStatistics.Completed.Reverse();
            res.ChartStatistics.Leave.Reverse();
            res.ChartStatistics.UnCompleted.Reverse();

            res.TableStatistics.Items.AddRange(tableList.OrderBy(o => o.ArrivalTime));

            return res;
        }

        // ---------------------------------- 私有方法 --------------------------------------
        private (List<ArrivalTrendChartEntityDTO> all, List<ArrivalTrendChartEntityDTO> completed, List<ArrivalTrendChartEntityDTO> leave, List<ArrivalTrendChartEntityDTO> uncompleted) GetArrivalTrendStatisticsAsync(ArrivalTrendStatisticsReqDTO req)
        {
            var reqDo = _mapper.Map<ArrivalTrendStatisticsReqDO>(req);
            reqDo.StartTime = reqDo.StartTime.GetStartTimeOfDay();
            reqDo.EndTime = reqDo.EndTime.GetEndTimeOfDay();

            var allTask = GetAllStatusArrivalTrendStatistics(JsonConvert.DeserializeObject<ArrivalTrendStatisticsReqDO>(JsonConvert.SerializeObject(reqDo)));
            var completedTask = GetCompletedArrivalTrendStatistics(JsonConvert.DeserializeObject<ArrivalTrendStatisticsReqDO>(JsonConvert.SerializeObject(reqDo)));
            var leaveTask = GetLeaveArrivalTrendStatistics(JsonConvert.DeserializeObject<ArrivalTrendStatisticsReqDO>(JsonConvert.SerializeObject(reqDo)));
            var uncompletedTask = GetUnCompletedArrivalTrendStatistics(JsonConvert.DeserializeObject<ArrivalTrendStatisticsReqDO>(JsonConvert.SerializeObject(reqDo)));

            Task.WaitAll(allTask, completedTask, leaveTask, uncompletedTask);

            return (allTask.Result, completedTask.Result, leaveTask.Result, uncompletedTask.Result);
        }

        private async Task<List<ArrivalTrendChartEntityDTO>> GetAllStatusArrivalTrendStatistics(ArrivalTrendStatisticsReqDO req)
            => await _arrivalRepo.GetArrivalTrendStatisticsByStatus(req);

        private async Task<List<ArrivalTrendChartEntityDTO>> GetCompletedArrivalTrendStatistics(ArrivalTrendStatisticsReqDO req)
        {
            req.Status.Add(Convert.ToInt32(ArrivalRecordStatusEnum.Finish));
            return await _arrivalRepo.GetArrivalTrendStatisticsByStatus(req);
        }

        private async Task<List<ArrivalTrendChartEntityDTO>> GetLeaveArrivalTrendStatistics(ArrivalTrendStatisticsReqDO req)
        {
            req.Status.Add(Convert.ToInt32(ArrivalRecordStatusEnum.Leave));
            return await _arrivalRepo.GetArrivalTrendStatisticsByStatus(req);
        }

        private async Task<List<ArrivalTrendChartEntityDTO>> GetUnCompletedArrivalTrendStatistics(ArrivalTrendStatisticsReqDO req)
        {
            req.Status.Add(Convert.ToInt32(ArrivalRecordStatusEnum.Waiting));
            req.Status.Add(Convert.ToInt32(ArrivalRecordStatusEnum.Dispatching));
            return await _arrivalRepo.GetArrivalTrendStatisticsByStatus(req);
        }

    }
}
