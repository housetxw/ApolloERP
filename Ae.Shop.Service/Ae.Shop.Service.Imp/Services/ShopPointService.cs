using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using Ae.Shop.Service.Dal.Repositorys.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Imp.Services
{
    public class ShopPointService : IShopPointService
    {
        private readonly IShopPointRepository _shopPointRepository;
        private readonly IShopPointDetailRepository _shopPointDetailRepository;

        public ShopPointService(IShopPointRepository shopPointRepository, IShopPointDetailRepository shopPointDetailRepository)
        {
            _shopPointRepository = shopPointRepository;
            _shopPointDetailRepository = shopPointDetailRepository;
        }

        /// <summary>
        /// 获取门店积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopPointVo> GetShopPoint(ShopPointRequest request)
        {
            ShopPointVo result = new ShopPointVo();
            var pointTask = _shopPointRepository.GetShopPointByShopId(request.ShopId, false);
            var pointDetailTask = _shopPointDetailRepository.GetShopPointDetailByShopId(request.ShopId);
            await Task.WhenAll(pointTask, pointDetailTask);
            var point = pointTask.Result;
            var pointDetail = pointDetailTask.Result ?? new List<ShopPointDetailDo>();
            if (point != null)
            {
                result.CurrentPoint = point.CurrentPoint;
            }
            result.PointDetail = pointDetail.Select(_ => new ShopPointDetailVo
            {
                Description = _.Description,
                CreatedDate = _.CreateTime.ToString("yyyy-MM-dd"),
                PointValueDisplay = _.PointValue > 0 ? $"+{_.PointValue}" : _.PointValue.ToString()
            }).ToList();

            return result;
        }

        /// <summary>
        /// 操作门店积分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddShopPointRecord(AddShopPointRecordRequest request)
        {
            var point = await _shopPointRepository.GetShopPointByShopId(request.ShopId, false);
            if (request.PointValue < 0)
            {
                if (point == null)
                {
                    throw new CustomException("当前账户积分不够");
                }

                if (point.CurrentPoint + request.PointValue < 0)
                {
                    throw new CustomException("当前账户积分不够");
                }
            }

            ShopPointDo shopPointDo = new ShopPointDo()
            {
                ShopId = request.ShopId,
                CurrentPoint = request.PointValue
            };
            if (point == null)
            {
                shopPointDo.CreateBy = request.SubmitBy;
                shopPointDo.CreateTime = DateTime.Now;
                shopPointDo.UpdateBy = "";
                shopPointDo.UpdateTime = new DateTime(1900, 1, 1);
            }
            else
            {
                shopPointDo.Id = point.Id;
                shopPointDo.UpdateBy = request.SubmitBy;
                shopPointDo.UpdateTime = DateTime.Now;
            }

            await _shopPointRepository.OperatePointPoint(shopPointDo);

            await _shopPointDetailRepository.InsertAsync(new ShopPointDetailDo()
            {
                ShopId = request.ShopId,
                OperationType = request.OperateType.ToString(),
                Description = request.OperateTypeDescription,
                PointValue = request.PointValue,
                Remark = request.Remark,
                CreateBy = request.SubmitBy,
                CreateTime = DateTime.Now,
                UpdateBy = "",
                UpdateTime = new DateTime(1900, 1, 1)
            });

            return true;
        }
    }
}
