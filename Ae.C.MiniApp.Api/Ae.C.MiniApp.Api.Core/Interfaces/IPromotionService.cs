using Ae.C.MiniApp.Api.Core.Model.Promotion;
using Ae.C.MiniApp.Api.Core.Request.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IPromotionService
    {
        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PromotionDetailVo> GetPromotionDetail(PromotionDetailRequest request);
    }
}
